using Autofac;
using Boxed.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Realeyes.Application.DTOs;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Application.Mappers;
using Realeyes.Domain.Models;
using Realeyes.Infrastructure.Data;
using Realeyes.Infrastructure.Repositories;

namespace Realeyes.Infrastructure.IOCs
{
    class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SessionDTOMapper>()
             .As<IMapper<Session, SessionDTO>>()
             .InstancePerLifetimeScope();


            builder.RegisterType<SurveyDTOMapper>()
             .As<IMapper<Survey, SurveyDTO>>()
             .InstancePerLifetimeScope();

            builder.Register<RealeyeDbContext>((cc) =>
            {
                var config = cc.Resolve<IConfiguration>();
                string databaseName = config["Database:Name"];
                var options = new DbContextOptionsBuilder<RealeyeDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
                return new RealeyeDbContext(options);


            }).SingleInstance();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }

    }
}
