using AutoMapper;
using WebApplication3.Domain.Features.Players.Dtos;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Model.Domain;
using WebApplication3.Model.DTO.PlayerDto;


namespace WebApplication3.WebApi.Mappings
{
    public class AutomapperProfiles : Profile
    {

        public AutomapperProfiles()
        {

            CreateMap<PlayerEntity, PlayerResponseDto>()
                .ReverseMap();

            CreateMap<AddPlayerDto, PlayerEntity>()
              .ReverseMap();

            CreateMap<UpdatePlayerDto, PlayerEntity>()
              .ReverseMap();
        }
    }
}
