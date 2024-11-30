using MediatR;

namespace Application.Commands;

public class RefreshTokenCommand : IRequest<string>
{
    public string RefreshToken { get; set; }

    public RefreshTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}