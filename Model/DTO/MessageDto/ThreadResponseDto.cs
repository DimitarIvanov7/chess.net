namespace WebApplication3.Model.DTO.MessageDto
{
    public class ThreadResponseDto
    {
        public Guid Id { get; set; }

        public List<MessageResponseDto> Messages;
    }
}
