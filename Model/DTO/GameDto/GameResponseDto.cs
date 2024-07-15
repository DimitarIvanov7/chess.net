using WebApplication3.Model.DTO.PlayerDto;
using WebApplication3.ChessLogic;

namespace WebApplication3.Model.GameDto
{
    public class GameResponseDto
    {

        public Guid Id { get; set; }

        public PlayerResponseDto WhitePlayer { get; set; }
        public PlayerResponseDto BlackPlayer { get; set; }

        public Guid WinnerId { get; set; }

        public PlayStateSubTypes GameType {  get; set; }    
        public DateTime DateTime { get; set; }




    }
}
