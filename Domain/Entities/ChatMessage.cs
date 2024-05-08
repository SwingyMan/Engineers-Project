using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }

        public GroupChat GroupChat { get; set; }
        public Message Message { get; set; }
    }
}
