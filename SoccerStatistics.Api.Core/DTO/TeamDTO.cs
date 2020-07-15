﻿using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public uint CreatedAt { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
        public virtual IEnumerable<PlayerDTO> Players { get; set; }
        public  uint StadiumId { get; set; }
        public  uint LeagueId { get; set; }

    }
}
