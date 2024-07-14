namespace WebApplication3.Model.Domain { 

    public class Game
    {

        public Guid Id { get; set; }
        public Guid whitePlayerId { get; set; }

        public Guid blackPlayerId { get; set; }

        public Guid winnerId { get; set; }

        public Guid gameTypeId { get; set; }
        public DateTime? gameTime { get; set; }

        public Player whitePlayer { get; set;  }

        public Player blackPlayer { get; set; }

        public Player winner { get; set; }

    public GameType gameType { get; set; }






    }

}

