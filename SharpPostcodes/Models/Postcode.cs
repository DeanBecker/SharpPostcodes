using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpPostcodes.Models
{
    public class Postcode
    {
        public double distance { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string postcode { get; set; }
        public string uri { get; set; }
    }
}
