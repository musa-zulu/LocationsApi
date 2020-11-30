using System.Collections.Generic;

namespace LocationsApi.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CrossStreet { get; set; }
        public string FormattedAddress { get; set; }
        public string State { get; set; }
        public string ReferralId { get; set; }
        public bool HasPerk { get; set; }
        public string VenueId { get; set; }

        public List<Category> Categories { get; set; }        
    }
}
