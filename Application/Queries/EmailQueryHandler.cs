

using MediatR;

namespace Application.Queries;

public class EmailQueryHandler : IRequestHandler<EmailQuery, bool>
{
    private readonly Context _context;

    public EmailQueryHandler(Context context)
    {
        _context = context;
    }
    public async Task<bool> Handle(EmailQuery request, CancellationToken cancellationToken)
    {
        var query = await _context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (query)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
