using MediatR;

namespace Application.Queries;

public class EmailQuery : IRequest<bool>
{
    public EmailQuery(string email)
    {
        Email = email;
    }
    public string Email { get; set; }
}
