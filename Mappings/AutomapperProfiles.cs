using AutoMapper;
using WebApplication3.Model.DTO;
using WebApplication3.Model.Domain;


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
