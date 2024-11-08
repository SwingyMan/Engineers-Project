using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, bool>
{
    private readonly SocialPlatformDbContext _context;

    public ActivateAccountCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.ActivationToken == request.ActivationToken,
            cancellationToken);

        if (user == null || user.IsActivated)
        {
            return false;
        }

        user.IsActivated = true;
        //Usuniecie uzytego tokena
        user.ActivationToken = null;

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

