using LocationsApi.Domain.Dto;
using LocationsApi.Persistence;
using LocationsApi.Service.Contract;
using LocationsApi.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationsApi.Service.Services
{
    public class FoursquareService : IFoursquareService
    {
        private readonly IApplicationDbContext _context;

        private readonly ApiConfiguration _apiConfiguration;

        public FoursquareService(IApplicationDbContext context, ApiConfiguration apiConfiguration)
        {
            _context = context;
            _apiConfiguration = apiConfiguration;
        }

        public async Task<List<Domain.Entities.Location>> GetLocations()
        {
            return await _context.Locations.Include(c => c.Categories).ToListAsync() ?? new List<Domain.Entities.Location>();
        }

        public void StoreLocationsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            var url = $"https://api.foursquare.com/v2/venues/search?client_id={_apiConfiguration.ClientId}&client_secret={_apiConfiguration.ClientSecret}&near={query},&v={DateTime.Now.Ticks}";
            using var response = ApiHelper.ApiClient.GetAsync(url);
            if (response.Result.IsSuccessStatusCode)
            {
                var results = response.Result?.Content?.ReadAsStringAsync().Result;
                if (results.Length > 0)
                {
                    var venues = JsonConvert.DeserializeObject<Rootobject>(results);
                    var categories = new List<Domain.Entities.Category>();

                    if (venues != null && venues.response.venues.Length > 0)
                    {
                        var locations = MapLocationWith(venues.response);

                        if (locations != null && locations.Count > 0)
                        {
                            foreach (var location in locations)
                            {
                                var existingLocation = GetLocations().Result
                                    .Any(x => x.LocationId == location.LocationId
                                    || x.Name.ToLowerInvariant() == location.Name.ToLowerInvariant());
                                if (!existingLocation)
                                {
                                    if (location?.Categories.Count > 0)
                                    {
                                        foreach (var category in location.Categories)

                                            if (_context.Categories.Any(c => c.DataId != category.DataId))
                                                _context.Categories.Add(category);
                                    }
                                    _context.Locations.Add(location);
                                }
                            }
                        }
                        _context.SaveChangesAsync();
                    }
                }
            }
        }
        private List<Domain.Entities.Location> MapLocationWith(Response response)
        {
            var venues = response?.venues;
            var locations = new List<Domain.Entities.Location>();
            foreach (var venue in venues)
            {
                var displayName = response?.geocode.feature.displayName;
                var country = displayName.Substring(displayName.LastIndexOf(",") + 1).Trim();
                var location = SetLocationValuesOn(venue, country);

                if (venue.categories.Length > 0)
                {
                    foreach (var category in venue.categories)
                        location.Categories.Add(MapValuesFor(category));
                }
                locations.Add(location);
            }
            return locations;
        }

        private static Domain.Entities.Category MapValuesFor(Category category)
        {
            return new Domain.Entities.Category
            {
                DataId = category.id,
                IconPrefix = category.icon.prefix,
                IconSuffic = category.icon.suffix,
                Name = category.name,
                PluralName = category.pluralName,
                Primary = category.primary,
                ShortName = category.shortName
            };
        }

        private static Domain.Entities.Location SetLocationValuesOn(Venue venue, string country)
        {
            return new Domain.Entities.Location()
            {
                Latitude = venue.location.lat,
                Longitude = venue.location.lng,
                Name = venue.name,
                City = venue.location.city,
                Country = country,
                State = venue.location.state,
                ReferralId = venue.referralId,
                Address = venue.location.address,
                CountryCode = venue.location.cc,
                CrossStreet = venue.location.crossStreet,
                HasPerk = venue.hasPerk,
                VenueId = venue.id,
                PostalCode = venue.location.postalCode,
                Categories = new List<Domain.Entities.Category>()
            };
        }
    }
}
