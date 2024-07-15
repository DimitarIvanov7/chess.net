using WebApplication3.Model.DTO.PlayerDto;
using WebApplication3.Model.Domain;

namespace WebApplication3.Model.DTO.FriendDto
{
    public class FriendResponseDto
    {

        public PlayerResponseDto Player { get; set; }

        public DomainEnums.FriendStates State { get; set; } 


    }
}
