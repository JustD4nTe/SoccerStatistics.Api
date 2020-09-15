﻿using SoccerStatistics.Api.Database.Entities;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface IMatchRepository : IRepository
    {
        Task<Match> GetByIdAsync(uint id);
    }
}