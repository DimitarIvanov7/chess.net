public class MessageGroupedBySenderDto
{
    public Guid SenderId { get; set; }
    public List<MessageResponseDto> Messages { get; set; }
}