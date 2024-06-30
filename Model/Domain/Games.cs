
    public class Games
    {

        public Guid Id { get; set; }
        public Guid whitePlayerId { get; set; }

        public Guid blackPlayerId { get; set; }

        public Guid winnerId { get; set; }

        public Guid gameTypeId { get; set; }
        public DateTime? gameTime { get; set; }

        public Players whitePlayer { get; set;  }

        public Players blackPlayer { get; set; }

        public Players winner { get; set; }

    public GameType gameType { get; set; }






    }

