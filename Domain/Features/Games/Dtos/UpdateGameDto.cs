using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Features.Players.Dtos;

namespace WebApplication3.Domain.Features.Games.Dtos
{
    public class UpdateGameDto
    {

        public Guid? WhitePlayer { get; set; }

        public Guid? BlackPlayer { get; set; }

        public Guid? WinnerId { get; set; }

    }
}
