﻿namespace Infrastructure.SignalR;
public class ChatHubMessageDTO
{
    public string ChatId { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public string UserId { get; set; }
}
