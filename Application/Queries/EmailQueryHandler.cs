using Infrastructure.IRepositories;
using MediatR;

namespace Application.Queries;

public class EmailQueryHandler : IRequestHandler<EmailQuery, bool>
{
    private readonly IUserRepository _userRepository;

    public EmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(EmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.CheckEmail(request.Email);
    }
}