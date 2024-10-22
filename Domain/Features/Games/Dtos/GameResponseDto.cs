using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Features.Players.Dtos;

namespace WebApplication3.Domain.Features.Games.Dtos
{
    public class GameResponseDto
    {

        public Guid Id { get; set; }

        public PlayerResponseDto WhitePlayer { get; set; }
        public PlayerResponseDto BlackPlayer { get; set; }

        public Guid WinnerId { get; set; }

        public PlayStateSubTypes GameType { get; set; }
        public DateTime DateTime { get; set; }




    }
}
