﻿using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly SoccerStatisticsDbContext _dbContext;

        public MatchRepository(SoccerStatisticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Match> GetByIdAsync(uint id)
            => await _dbContext.Matches.Include(m => m.Round)
                                       .Include(m => m.Stadium)
                                       .Include(m => m.Activities)
                                       .Include(m => m.InteractionsBetweenPlayers)
                                       .Include(m => m.TeamOneStats)
                                           .ThenInclude(t => t.Team)
                                               .ThenInclude(t => t.Players)
                                       .Include(m => m.TeamTwoStats)
                                           .ThenInclude(t => t.Team)
                                               .ThenInclude(t => t.Players)
                                       .SingleOrDefaultAsync(match => match.Id == id);

        public async Task<IEnumerable<Match>> GetHistoryOfMatchesByLeagueId(uint leagueId)
             => await _dbContext.Matches.Include(m => m.Round)
                                        .Include(m => m.Stadium)
                                        .Include(m => m.Activities)
                                        .Include(m => m.InteractionsBetweenPlayers)
                                        .Include(m => m.TeamOneStats)
                                            .ThenInclude(t => t.Team)
                                                .ThenInclude(t => t.Players)
                                        .Include(m => m.TeamTwoStats)
                                            .ThenInclude(t => t.Team)
                                                .ThenInclude(t => t.Players)
                                        .Where(x => x.Round.League.Id == leagueId)
                                        .OrderByDescending(o => o.Date)
                                        .Take(5)
                                        .ToListAsync();
    }
}
