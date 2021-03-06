﻿using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperTeamProfile : Profile
    {
        public AutoMapperTeamProfile()
        {
            CreateMap<Team, TeamDTO>();
            CreateMap<Team, TeamBasicDTO>();
        }
    }
}
