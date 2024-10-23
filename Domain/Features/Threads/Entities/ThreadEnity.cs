
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Features.Messages.Entities;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Features.Threads.Entities
{
    public class ThreadEntity : IEntity
    {

        public Guid Id { get; set; }

        public PlayerEntity PlayerOne { get; set; }
        
        public PlayerEntity PlayerTwo { get; set; }


        public ICollection<MessageEntity> Messages { get; set; }




    }
}
