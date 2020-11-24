using MediatR;
using LocationsApi.Domain.Entities;
using LocationsApi.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace LocationsApi.Service.Features.LocationFeatures.Commands
{
    public class CreateLocationCommand : IRequest<int>
    {
        //location properties goes here
        public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateLocationCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
            {
                var location = new Location();
              
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                return location.LocationId;
            }
        }
    }
}
