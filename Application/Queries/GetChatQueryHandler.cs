using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetChatQueryHandler : IRequestHandler<GetChatQuery, Chat>
    {
        private readonly IChatRepository _chatRepository;

        public GetChatQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Chat> Handle(GetChatQuery request, CancellationToken cancellationToken)
        {
            return await _chatRepository.GetChatByUserIds(request.userIds);
        }
    }
}
