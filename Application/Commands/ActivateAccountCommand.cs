
using MediatR;

namespace Application.Commands;

public record ActivateAccountCommand(Guid ActivationToken) : IRequest<bool>;
