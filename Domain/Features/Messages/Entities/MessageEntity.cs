

using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Features.Messages.Entities
{
    public class MessageEntity
    {

        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public Guid ThreadId { get; set; }  
        public string Data { get; set; } 

        public PlayerEntity Sender { get; set; }
        public PlayerEntity Receiver { get; set; }

        public Thread Thread { get; set; }


    }
}
