﻿using AutoMapper;
using WebApplication3.Model.DTO;


namespace WebApplication3.Mappings
{
    public class AutomapperProfiles: Profile
    {

        public AutomapperProfiles()
        {

            CreateMap<Player, PlayerDto>()
                .ReverseMap();

            CreateMap<AddPlayerDto, Player>()
              .ReverseMap();

            CreateMap<UpdatePlayerDto, Player>()
              .ReverseMap();
        }
    }
}