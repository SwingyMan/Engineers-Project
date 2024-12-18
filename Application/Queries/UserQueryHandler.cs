using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class UserQueryHandler : IRequestHandler<UserQuery,List<User>>
{
    private readonly SocialPlatformDbContext _context;

    public UserQueryHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<List<User>> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        return _context.Users.ToList().Where(
            x => x.Username.Contains(request.Username, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}