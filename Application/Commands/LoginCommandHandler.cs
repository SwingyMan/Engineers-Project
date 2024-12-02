using System.Security.Cryptography;
using Application.DTOs;
using AutoMapper;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, UserReturnDTO>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;
    public LoginCommandHandler(SocialPlatformDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserReturnDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.UserLoginDto);
        var query = await _context.Users.Include(x => x.Role).Include(x=>x.RefreshToken).FirstOrDefaultAsync(x =>
            x.Email == user.Email);
        if (query is null)
            return null;
        var password_check = BCrypt.Net.BCrypt.Verify(request.UserLoginDto.Password, query.Password);

        if (!query.IsActivated)
            return null;

        if (password_check)
        {
            var token = query.CreateToken(query.Username, query.Email, query.Id, query.Role.Name);
            var userReturn = _mapper.Map<UserReturnDTO>(query);
            userReturn.Token = token.Token;
            var refreshToken = new RefreshToken(Guid.NewGuid(), Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), userReturn.Id, DateTime.Now.AddHours(8));
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            userReturn.RefreshToken = refreshToken.Token;
            return userReturn;
        }

        return null;
    }
}