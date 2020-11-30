using System.Collections.Generic;

namespace LocationsApi.Domain.Dto
{
    public class Rootobject
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
        public string requestId { get; set; }
    }

    public class Response
    {
        public Venue[] venues { get; set; }
        public bool confident { get; set; }
        public Geocode geocode { get; set; }
    }

    public class Geocode
    {
        public string what { get; set; }
        public string where { get; set; }
        public Feature feature { get; set; }
        public object[] parents { get; set; }
    }

    public class Feature
    {
        public string cc { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string matchedName { get; set; }
        public string highlightedName { get; set; }
        public int woeType { get; set; }
        public string slug { get; set; }
        public string id { get; set; }
        public string longId { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public Center center { get; set; }
        public Bounds bounds { get; set; }
    }

    public class Center
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Bounds
    {
        public Ne ne { get; set; }
        public Sw sw { get; set; }
    }

    public class Ne
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Sw
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public Category[] categories { get; set; }
        public string referralId { get; set; }
        public bool hasPerk { get; set; }
        public Venuepage venuePage { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string crossStreet { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public Labeledlatlng[] labeledLatLngs { get; set; }
        public string postalCode { get; set; }
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string[] formattedAddress { get; set; }
    }

    public class Labeledlatlng
    {
        public string label { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Venuepage
    {
        public string id { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public Icon icon { get; set; }
        public bool primary { get; set; }
    }

    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

}

