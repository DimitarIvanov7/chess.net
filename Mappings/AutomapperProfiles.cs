using AutoMapper;
using WebApplication3.Model.Domain;
using WebApplication3.Model.DTO.Player;


namespace WebApplication3.Mappings
{
    public class AutomapperProfiles: Profile
    {

        public AutomapperProfiles()
        {

            CreateMap<Player, PlayerResponseDto>()
                .ReverseMap();

            CreateMap<AddPlayerDto, Player>()
              .ReverseMap();

            CreateMap<UpdatePlayerDto, Player>()
              .ReverseMap();
        }
    }
}
