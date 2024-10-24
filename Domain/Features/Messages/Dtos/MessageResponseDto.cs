using WebApplication3.Domain.Features.Players.Dtos;

public class MessageResponseDto
{
    public Guid Id { get; set; }
    public PlayerResponseDto Sender { get; set; }
    public PlayerResponseDto Receiver { get; set; }
    public string Message { get; set; }
    public DateTime SentDate { get; set; }


}