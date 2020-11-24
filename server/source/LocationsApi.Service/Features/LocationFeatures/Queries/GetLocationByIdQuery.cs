using MediatR;
using LocationsApi.Domain.Entities;
using LocationsApi.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocationsApi.Service.Features.LocationFeatures.Queries
{
    public class GetLocationByIdQuery : IRequest<Location>
    {
        public int LocationId { get; set; }
        public class GetLocationsByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, Location>
        {
            private readonly IApplicationDbContext _context;
            public GetLocationsByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Location> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
            {
                var location = _context.Locations.Where(a => a.LocationId == request.LocationId).FirstOrDefault();
                if (location == null) return null;
                return location;
            }
        }
    }
}
