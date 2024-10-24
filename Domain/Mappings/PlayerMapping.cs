using AutoMapper;
using WebApplication3.Domain.Features.Games.Dtos;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Messages.Entities;
using WebApplication3.Domain.Features.Players.Dtos;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Model.DTO.PlayerDto;


namespace WebApplication3.Domain.Mappings
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

            CreateMap<AddGameDto, GameEntity>()
                .ForMember(dest => dest.WhitePlayerId, opt => opt.MapFrom(src => src.WhitePlayerId));

            CreateMap<UpdateGameDto, GameEntity>()
              .ReverseMap();

            CreateMap<GameEntity, GameResponseDto>()
                .ReverseMap();

            CreateMap<CreateMessageDto, MessageEntity>()
               .ReverseMap();
            
            CreateMap<MessageEntity, MessageResponseDto>()
               .ReverseMap();
        }
    }
}
