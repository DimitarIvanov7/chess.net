namespace WebApplication3.Domain.Features.Messages.Dtos
{
    public class PostMessageDto
    {

        public Guid ReceiverId { get; set; }

        public string Message { get; set; }
    }
}
