using AutoMapper;
using LocationsApi.Domain.Entities;
using LocationsApi.Infrastructure.ViewModel;

namespace LocationsApi.Infrastructure.Mapping
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationModel, Location>()           
                .ReverseMap();
        }
    }
}
