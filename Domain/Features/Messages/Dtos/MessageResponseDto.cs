using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Features.Messages.Dtos
{
    public class MessageResponseDto
    {

        public PlayerEntity Sender { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }



    }
}
