using MediatR;
using LocationsApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LocationsApi.Service.Contract;

namespace LocationsApi.Service.Features.LocationFeatures.Queries
{
    public class GetAllLocationsQuery : IRequest<IEnumerable<Location>>
    {
        public string Query { get; set; }
        public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<Location>>
        {           
            private readonly IFoursquareService _fourSqureService;            
            public GetAllLocationsQueryHandler(IFoursquareService foursquareService)
            {
                _fourSqureService = foursquareService;
            }

            public async Task<IEnumerable<Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
            {
                PopulateDbWithLocationsFromFourSqaure(request.Query);
                var locations = await _fourSqureService.GetLocations();              
                if (locations == null)
                {
                    return null;
                }
                return locations.AsReadOnly();
            }

            private void PopulateDbWithLocationsFromFourSqaure(string query)
            {
                //call fourSqure  service to populate the db
                _fourSqureService.StoreLocationsAsync(query);
            }
        }
    }
}
