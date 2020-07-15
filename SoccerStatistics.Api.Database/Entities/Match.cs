﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Match
    {
        public uint Id { get; set; }
        [Required]
        public virtual Stadium Stadium { get; set; }
        public uint AmountOfFans { get; set; }
        [Required]
        public virtual Round Round { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public virtual IEnumerable<Activity> Activities { get; set; }
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionBetweenPlayers { get; set; }
        [Required]
        [ForeignKey("MatchTeam1")]
        public virtual TeamInMatchStats Team1 { get; set; }
        [Required]
        [ForeignKey("MatchTeam2")]
        public virtual TeamInMatchStats Team2 { get; set; }
    }
}
