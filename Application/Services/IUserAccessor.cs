using System.Security.Claims;

namespace Application.Services;

public interface IUserAccessor { ClaimsPrincipal User { get; } }
