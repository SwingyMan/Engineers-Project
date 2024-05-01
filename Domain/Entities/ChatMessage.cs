using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int MessageId { get; set; }

        public GroupChat GroupChat { get; set; }
        public Message Message { get; set; }
    }
}
