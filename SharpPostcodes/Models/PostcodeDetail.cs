using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpPostcodes.Models
{
    public class GeoData
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public double easting { get; set; }
        public double northing { get; set; }
        public string geohash { get; set; }
    }

    public class AdministrativeData
    {
        public string title { get; set; }
        public string uri { get; set; }
        public string code { get; set; }
    }

    public class Council : AdministrativeData { }
    public class Ward : AdministrativeData { }
    public class Constituency : AdministrativeData { }
    public class Parish : AdministrativeData { }

    public class Administrative
    {
        public Council council { get; set; }
        public Ward ward { get; set; }
        public Constituency constituency { get; set; }
        public Parish parish { get; set; }
    }

    public class PostcodeDetail
    {
        public string postcode { get; set; }
        public GeoData geo { get; set; }
        public Administrative administrative { get; set; }
    }
}
