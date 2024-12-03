using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetChatQuery : IRequest<Chat>
    {
        public GetChatQuery(Guid[] guids)
        {
            userIds = guids;
        }

        public Guid[] userIds;
    }
}
