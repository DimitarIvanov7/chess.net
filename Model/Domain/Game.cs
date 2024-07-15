using WebApplication3.ChessLogic;

namespace WebApplication3.Model.Domain { 

    public class Game
    {

        public Guid Id { get; set; }
        public Guid WhitePlayerId { get; set; }

        public Guid BlackPlayerId { get; set; }

        public Guid WinnerId { get; set; }

        public Guid GameTypeId { get; set; }
        public DateTime? GameTime { get; set; }

        public Player WhitePlayer { get; set;  }

        public Player BlackPlayer { get; set; }

        public Player Winner { get; set; }

        public PlayStateSubTypes GameType { get; set; }


    }

}

