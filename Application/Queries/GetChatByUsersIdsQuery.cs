using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetChatByUsersIdsQuery : IRequest<Chat>
    {
        public GetChatByUsersIdsQuery(Guid[] guids)
        {
            userIds = guids;
        }

        public Guid[] userIds;
    }
}
