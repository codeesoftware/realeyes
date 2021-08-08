using System.Threading;
using System.Threading.Tasks;
using Boxed.Mapping;
using MediatR;
using Realeyes.Application.DTOs;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Sessions.Commands.Handlers
{
    class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDTO>
    {
        private readonly IMapper<Session, SessionDTO> mapper;

        public CreateSessionCommandHandler(IMapper<Session, SessionDTO> mapper)
        {
            this.mapper = mapper;
        }
        public async Task<SessionDTO> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var s = new Session();
            var result = this.mapper.Map<Session, SessionDTO>(s);
            return await Task.FromResult(result);
        }
    }
}
