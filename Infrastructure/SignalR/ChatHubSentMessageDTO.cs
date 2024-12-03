using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SignalR
{
    public class ChatHubSentMessageDTO
    {
        public string RecipientId { get; set; }
        public string Message { get; set; }
    }
}
