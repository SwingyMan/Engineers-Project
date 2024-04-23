using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int MessageId { get; set; }

        [ForeignKey("ChatId")]
        public GroupChat GroupChat { get; set; }
        [ForeignKey("MessageId")]
        public Message Message { get; set; }
    }
}
