using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.Moq;
using Boxed.Mapping;
using Moq;
using Realeyes.Application.DTOs;
using Realeyes.Application.Exceptions;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Application.Mappers;
using Realeyes.Application.Sessions.Commands;
using Realeyes.Application.Sessions.Commands.Handlers;
using Realeyes.Domain.Enums;
using Realeyes.Domain.Models;
using Xunit;

namespace Realeyes.Application.Test
{
    public class SessionTest
    {

        [Fact]
        public async void CreateSessionCommand_CreateSessionWithoutSurveyId()
        {
            var sessions = new List<Session>();
            using (var mock = AutoMock.GetLoose(Register))
            {
                // Arrange
                mock.Mock<IRepository<Session>>()
                    .Setup(x => x.InsertAsync(It.IsNotNull<Session>())).
                    Callback<Session>(s =>
                    {
                        s.Id = Guid.NewGuid();
                        s.SurveyId = Guid.NewGuid();
                        sessions.Add(s);
                    });

                var command = new CreateSessionCommand(null);
                var handler = mock.Create<CreateSessionCommandHandler>();

                // Act
                SessionDTO x = await handler.Handle(command, new System.Threading.CancellationToken());

                // Assert - assert on the mock
                Assert.NotNull(x);
                Assert.Equal(States.New, x.States);
                Assert.NotEqual(Guid.Empty, x.SurveyId);
            }
        }

        [Fact]
        public async void CreateSessionCommand_CreateSessionWithSurveyId()
        {
            var surveys = new List<Survey>();
            Guid surveyId = Guid.NewGuid();
            var survey = new Survey { Id = surveyId, CreatedOn = DateTime.Now };
            surveys.Add(survey);

            var sessions = new List<Session>();

            using (var mock = AutoMock.GetLoose(Register))
            {
                // Arrange
                mock.Mock<IRepository<Session>>()
                    .Setup(x => x.InsertAsync(It.IsNotNull<Session>())).
                    Callback<Session>(s =>
                    {
                        s.Id = Guid.NewGuid();
                        s.SurveyId = s.Survey.Id;
                        sessions.Add(s);
                    });
                mock.Mock<IRepository<Survey>>()
                 .Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).
                 Returns<Guid>(async g => await Task.FromResult(surveys.First(s => s.Id == g)));

                var command = new CreateSessionCommand(surveyId);
                var handler = mock.Create<CreateSessionCommandHandler>();

                // Act
                SessionDTO x = await handler.Handle(command, new System.Threading.CancellationToken());

                // Assert - assert on the mock
                Assert.NotNull(x);
                Assert.Equal(States.New, x.States);
                Assert.Equal(surveyId, x.SurveyId);
            }
        }

        [Fact]
        public async void UpdateSessionCommand_UpdateSessionToInProgress()
        {
            var sessions = new List<Session>();
            Guid sessionId = Guid.NewGuid();
            var session = new Session { Id = sessionId, States = States.New };
            sessions.Add(session);
            using (var mock = AutoMock.GetLoose(Register))
            {
                // Arrange
                mock.Mock<IRepository<Session>>()
               .Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).
               Returns<Guid>(async g => await Task.FromResult(sessions.First(s => s.Id == g)));

                var command = new StartSessionCommand(sessionId);
                var handler = mock.Create<StartSessionCommandHandler>();

                // Act
                SessionDTO x = await handler.Handle(command, new System.Threading.CancellationToken());

                // Assert - assert on the mock
                Assert.NotNull(x);
                Assert.Equal(States.InProgress, x.States);
            }
        }

        [Fact]
        public async void UpdateSessionCommand_UpdateSessionToCompleted()
        {
            var sessions = new List<Session>();
            Guid sessionId = Guid.NewGuid();
            var session = new Session { Id = sessionId, States = States.InProgress };
            sessions.Add(session);
            using (var mock = AutoMock.GetLoose(Register))
            {
                // Arrange
                mock.Mock<IRepository<Session>>()
               .Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).
               Returns<Guid>(async g => await Task.FromResult(sessions.First(s => s.Id == g)));

                var command = new CompleteSessionCommand(sessionId);
                var handler = mock.Create<CompleteSessionCommandHandler>();

                // Act
                SessionDTO x = await handler.Handle(command, new System.Threading.CancellationToken());

                // Assert - assert on the mock
                Assert.NotNull(x);
                Assert.Equal(States.Completed, x.States);
            }
        }
        [Fact]
        public async void UpdateSessionCommand_UpdateSessionToCompleted_ThrowsSessionException()
        {
            var sessions = new List<Session>();
            Guid sessionId = Guid.NewGuid();
            var session = new Session { Id = sessionId, States = States.Completed };
            sessions.Add(session);
            using (var mock = AutoMock.GetLoose(Register))
            {
                // Arrange
                mock.Mock<IRepository<Session>>()
               .Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).
               Returns<Guid>(async g => await Task.FromResult(sessions.First(s => s.Id == g)));

                var command = new CompleteSessionCommand(sessionId);
                var handler = mock.Create<CompleteSessionCommandHandler>();

                // Act
                Exception exception = await Record.ExceptionAsync(() => handler.Handle(command, new System.Threading.CancellationToken()));
                // Assert - assert on the mock
                Assert.IsType<SessionException>(exception);
                Assert.Equal("Session state is not valid!", exception.Message);
            }
        }
        private static void Register(ContainerBuilder builder)
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
