using WebApplication3.Domain.Constants;

namespace WebApplication3.Domain.Features.ChessLogic
{
    public class BoardSquare
    {
        public Colors Color { get; set; }
        public Piece? Piece { get; set; }
    }
}
