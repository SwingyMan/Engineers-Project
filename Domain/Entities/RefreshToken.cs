namespace Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiryDate { get; set; }

    public RefreshToken(Guid id, string token, Guid userId, DateTime expiryDate)
    {
        Id = id;
        Token = token;
        UserId = userId;
        ExpiryDate = expiryDate;
    }
    //map
    public User User { get; set; }
}