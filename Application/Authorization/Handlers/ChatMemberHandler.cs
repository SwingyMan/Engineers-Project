using Application.Authorization.Requirements;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Handlers
{
    public class ChatMemberHandler : AuthorizationHandler<ChatMemberRequirement>
    {
        //private readonly IChatUserService _chatUserService;
        private SocialPlatformDbContext _dbContext;

        public ChatMemberHandler(SocialPlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ChatMemberRequirement requirement)
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

                //if (routeData.Values.TryGetValue("id", out var chatIdValue) && Guid.TryParse(chatIdValue?.ToString(), out var chatId))
                //{
                //    bool isMember = await _dbContext.ChatUsers.AnyAsync(cu => cu.UserId == userId && cu.ChatId == chatId);

                //    if (isMember)
                //    {
                //        context.Succeed(requirement);
                //        return;
                //    }
                //}
            }

            context.Fail();
        }
    }
}