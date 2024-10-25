using AutoMapper;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, JwtToken>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;
    private readonly KeyClient _keyClient;
    public LoginCommandHandler(SocialPlatformDbContext context, IMapper mapper,KeyClient keyClient)
    {
        _keyClient = keyClient;
        _context = context;
        _mapper = mapper;
    }

    public async Task<JwtToken> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.UserLoginDto);
        var query = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x =>
            x.Email == user.Email);
        var password_check = BCrypt.Net.BCrypt.Verify(request.UserLoginDto.Password, query.Password);
        if (password_check)
            return query.CreateToken(query.Username, query.Email, query.Id, query.Role.Name,_keyClient.GetKey("jwtkey").Value.ToString());
        return null;
    }
}