using Autofac;
using Boxed.Mapping;
using Realeyes.Application.DTOs;
using Realeyes.Application.Mappers;
using Realeyes.Domain.Models;

namespace Realeyes.Infrastructure.IOCs
{
    class ContainerModule: Module
    {
        protected override  void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SessionDTOMapper>()
             .As<IMapper<Session, SessionDTO>>()
             .InstancePerLifetimeScope();


            builder.RegisterType<SurveyDTOMapper>()
             .As<IMapper<Survey, SurveyDTO>>()
             .InstancePerLifetimeScope();
        }
       
    }
}
