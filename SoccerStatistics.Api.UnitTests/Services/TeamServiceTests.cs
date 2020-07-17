﻿using AutoMapper;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class TeamServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITeamRepository> _repositoryMock;
        private readonly ITeamService _service;

        public TeamServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperTeamProfile>());

            _mapper = new Mapper(configuration);
            _repositoryMock = new Mock<ITeamRepository>();
            _service = new TeamService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async void ReturnAllTeamsFromDb()
        {
            IEnumerable<Team> teams = new List<Team>
            {
                new Team()
                {
                    Id = 1,
                    FullName = "Manchester United Football Club",
                    ShortName = "Manchester United",
                    City = "Stretford",
                    CreatedAt = 1878,
                    Coach = "Ole Gunnar Solskjær"
                },
                new Team()
                {
                    Id = 2,
                    FullName = "Real Madrid Club de Futbol",
                    ShortName = "Real Madrid",
                    City = "Madrid",
                    CreatedAt = 1902,
                    Coach = "Zinedine Zidane"
                },
                new Team()
                {
                    Id = 3,
                    FullName = "Futbol Club Barcelona",
                    ShortName = "FC Barcelona",
                    City = "Barcelona",
                    CreatedAt = 1899,
                    Coach = "Quique Setien"
                }
            };

            IEnumerable<TeamBasicDTO> expectedTeams = new List<TeamBasicDTO>()
            {
                new TeamBasicDTO()
                {
                    Id = 1,
                    FullName = "Manchester United Football Club",
                    ShortName = "Manchester United",
                    City = "Stretford",
                    Coach = "Ole Gunnar Solskjær"

                },
                new TeamBasicDTO()
                {
                    Id = 2,
                    FullName = "Real Madrid Club de Futbol",
                    ShortName = "Real Madrid",
                    City = "Madrid",
                    Coach = "Zinedine Zidane"
                },
                new TeamBasicDTO()
                {
                    Id = 3,
                    FullName = "Futbol Club Barcelona",
                    ShortName = "FC Barcelona",
                    City = "Barcelona",
                    Coach = "Quique Setien"
                }
            };

            IEnumerable<TeamBasicDTO> testTeams = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(teams);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeams = await _service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testTeams);
            Assert.Equal(expectedTeams.Count(), teams.Count());

            testTeams.ShouldCompare(expectedTeams);
        }

        [Fact]
        public async void ReturnNullCollectionWhenDbDoesNotContainsAnyTeam()
        {
            IEnumerable<TeamBasicDTO> testTeams = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<Team>)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeams = await _service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.Equal(Enumerable.Empty<TeamBasicDTO>(), testTeams);
        }

        [Fact]
        public async void ReturnTeamWhichExistsInDbByGivenId()
        {
            // Assert
            var team = new Team()
            {
                Id = 1,
                FullName = "Manchester United Football Club",
                ShortName = "Manchester United",
                City = "Stretford",
                CreatedAt = 1878,
                Coach = "Ole Gunnar Solskjær"
            };

            TeamDTO testTeam = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(team);

            var expectedTeam = _mapper.Map<TeamDTO>(team);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await _service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testTeam);

            testTeam.ShouldCompare(expectedTeam);
        }

        [Fact]
        public async void ReturnNullWhenTeamDoNotExistsInDbByGivenId()
        {
            // Assert
            TeamDTO testTeam = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Team)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await _service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testTeam);
        }
    }
}
