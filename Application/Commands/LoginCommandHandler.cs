using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, JwtToken>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;

    public LoginCommandHandler(SocialPlatformDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<JwtToken> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.UserLoginDto);
        var query = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x =>
            x.Email == user.Email);
        var password_check = BCrypt.Net.BCrypt.Verify(request.UserLoginDto.Password, query.Password);

        if (!user.IsActivated)
            return null;

        if (password_check)
            return query.CreateToken(query.Username, query.Email, query.Id, query.Role.Name);

        return null;
    }
}