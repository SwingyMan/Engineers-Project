
using Application.Authorization.Requirements;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Authorization.Handlers;

public class ChatMessageMemberHandler : AuthorizationHandler<ChatMessageMemberRequirement>
{

    private SocialPlatformDbContext _dbContext;

    public ChatMessageMemberHandler(SocialPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ChatMessageMemberRequirement requirement)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var userIdClaim = context.User.FindFirst("id")?.Value;
        var roleClaim = context.User.FindFirst(ClaimTypes.Role)?.Value;

        if (userIdClaim == null)
        {
            context.Fail();
            return;
        }

        var userId = Guid.Parse(userIdClaim);

        if (roleClaim == "ADMIN")
        {
            context.Succeed(requirement);
            return;
        }

        if (context.Resource is Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var routeData = httpContext.GetRouteData();

            if (routeData.Values.TryGetValue("id", out var chatMessageIdValue) && Guid.TryParse(chatMessageIdValue?.ToString(), out var chatMessageId))
            {
                var chatMessage = await _dbContext.ChatMessages.FirstOrDefaultAsync(cm => cm.Id == chatMessageId);

                //if (chatMessage.ChatId != Guid.Empty)
                //{
                //    bool isMember = await _dbContext.ChatUsers.AnyAsync(cu => cu.UserId == userId && cu.ChatId == chatMessage.ChatId);

                //    if (isMember)
                //    {
                //        context.Succeed(requirement);
                //        return;
                //    }
                //}
            }
        }

        context.Fail();
    }
}