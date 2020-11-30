using LocationsApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationsApi.Service.Contract
{
    public interface IFoursquareService
    {
        void StoreLocationsAsync(string query);
        Task<List<Location>> GetLocations();
    }
}
