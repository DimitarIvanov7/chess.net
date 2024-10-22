using WebApplication3.Model.Domain;
using WebApplication3.Domain.Features.Players.Dtos;
using WebApplication3.Domain.Constants;

namespace WebApplication3.Domain.Features.Friends.Dtos
{
    public class FriendResponseDto
    {

        public PlayerResponseDto Player { get; set; }

        public FriendStates State { get; set; }


    }
}
