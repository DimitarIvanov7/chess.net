namespace WebApplication3.Domain.Features.Messages.Dtos
{
    public class ThreadResponseDto
    {
        public Guid Id { get; set; }

        public List<MessageResponseDto> Messages;
    }
}
