﻿using Autofac;
using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Linq;
using System.Reflection;

namespace SoccerStatistics.Api.Application.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            var assembly = typeof(LeagueRepository)
                 .GetTypeInfo()
                 .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IRepository>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<FakeData>()
                   .As<IFakeData>()
                   .SingleInstance();

            builder.RegisterType<DataInitializer>()
                   .As<IDataInitializer>()
                   .SingleInstance();            
        }
    } 
}
