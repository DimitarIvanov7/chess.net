

using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Features.Games.Entities
{
    public class GameEntity:  IEntity
    {
        public Guid Id { get; set; }
        public Guid WhitePlayerId { get; set; }

        public Guid? BlackPlayerId { get; set; }

        public Guid? WinnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public PlayStateTypes GameStateType { get; set; }

        public PlayStateSubTypes? GameStateSubType { get; set; }

        public virtual PlayerEntity WhitePlayer { get; set;  }

        public virtual PlayerEntity? BlackPlayer { get; set; }

        public virtual PlayerEntity? Winner { get; set; }

        


        public GameEntity()
        {
            CreatedDate = DateTime.UtcNow;
            GameStateType = PlayStateTypes.Pause;
        }


    }

}

