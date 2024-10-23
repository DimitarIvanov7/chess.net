

using WebApplication3.Domain.Database;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Features.Messages.Entities
{
    public class MessageEntity: IEntity
    {

        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public string Data { get; set; } 

        public PlayerEntity Sender { get; set; }
        public PlayerEntity Receiver { get; set; }



    }
}
