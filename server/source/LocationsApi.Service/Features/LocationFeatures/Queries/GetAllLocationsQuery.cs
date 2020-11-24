using MediatR;
using Microsoft.EntityFrameworkCore;
using LocationsApi.Domain.Entities;
using LocationsApi.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LocationsApi.Service.Features.LocationFeatures.Queries
{
    public class GetAllLocationsQuery : IRequest<IEnumerable<Location>>
    {

        public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<Location>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllLocationsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
            {
                var locations = await _context.Locations.ToListAsync();
                if (locations == null)
                {
                    return null;
                }
                return locations.AsReadOnly();
            }
        }
    }
}
