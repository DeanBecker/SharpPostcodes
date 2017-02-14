using Newtonsoft.Json;
using SharpPostcodes.Models;
using SharpPostcodes.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace SharpPostcodes
{
    public partial class Postcodes
    {
        private static string PostcodeEndpoint = string.Format($"{Config.Host}postcode/");
        private static string NearestPostcodeEndpoint = string.Format($"{Config.Host}latlng/");
        private static string AreaPostcodesEndpoint = string.Format($"{Config.Host}postcode/nearest?");

        public static async Task<PostcodeDetail> GetPostcodeData(string postcode)
        {
            var sanitisedPostcode = postcode.Replace(" ", string.Empty);
            var requestUrl = string.Format($"{PostcodeEndpoint}{sanitisedPostcode}.json");

            using (var client = new HttpClient())
            {
                var httpResult = await client.GetAsync(requestUrl);

                if (!httpResult.IsSuccessStatusCode)
                    throw new HttpRequestException($"Http Failure: Status {httpResult.StatusCode}");

                return JsonConvert.DeserializeObject<PostcodeDetail>(await httpResult.Content.ReadAsStringAsync());
            }
        }

        public static async Task<PostcodeDetail> GetNearestPostcode(LatLong coords)
        {
            var requestUrl = string.Format($"{NearestPostcodeEndpoint}{coords.Lat},{coords.Long}.json");

            using (var client = new HttpClient())
            {
                var httpResult = await client.GetAsync(requestUrl);

                if (!httpResult.IsSuccessStatusCode)
                    throw new HttpRequestException($"Http Failure: Status {httpResult.StatusCode}");

                return JsonConvert.DeserializeObject<PostcodeDetail>(await httpResult.Content.ReadAsStringAsync());
            }
        }

        public static async Task<IList<Postcode>> GetNearbyPlaces(string postcode, uint radius)
        {
            if (radius > 5)
                throw new ArgumentException("The maximum radius is 5.");

            var sanitisedPostcode = postcode.Replace(" ", string.Empty);
            var requestUrl = string.Format($"{AreaPostcodesEndpoint}postcode={sanitisedPostcode}&miles={radius}&format=json");

            using (var client = new HttpClient())
            {
                var httpResult = await client.GetAsync(requestUrl);

                if (!httpResult.IsSuccessStatusCode)
                    throw new HttpRequestException($"Http Failure: Status {httpResult.StatusCode}");

                return JsonConvert.DeserializeObject<List<Postcode>>(await httpResult.Content.ReadAsStringAsync());
            }
        }

        public static async Task<IList<Postcode>> GetNearbyPlaces(LatLong coords, uint radius)
        {
            if (radius > 5)
                throw new ArgumentException("The maximum radius is 5.");

            var requestUrl = string.Format($"{AreaPostcodesEndpoint}lat={coords.Lat}&lng={coords.Long}&miles={radius}&format=json");

            using (var client = new HttpClient())
            {
                var httpResult = await client.GetAsync(requestUrl);

                if (!httpResult.IsSuccessStatusCode)
                    throw new HttpRequestException($"Http Failure: Status {httpResult.StatusCode}");

                return JsonConvert.DeserializeObject<List<Postcode>>(await httpResult.Content.ReadAsStringAsync());
            }
        }
    }
}
