

using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Model.Domain.Games.Entities;

namespace WebApplication3.Domain.Features.Games.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public Guid WhitePlayerId { get; set; }

        public Guid BlackPlayerId { get; set; }

        public Guid WinnerId { get; set; }

        public Guid GameTypeId { get; set; }
        public DateTime? GameTime { get; set; }

        public PlayerEntity WhitePlayer { get; set;  }

        public PlayerEntity BlackPlayer { get; set; }

        public PlayerEntity Winner { get; set; }

        public GameTypeEntity GameType { get; set; }


    }

}

