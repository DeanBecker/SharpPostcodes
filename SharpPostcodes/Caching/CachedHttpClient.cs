using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;

namespace SharpPostcodes.Caching
{
    public class CachedHttpClient : HttpClient
    {
        private IRequestCache _cache;

        public CachedHttpClient(IRequestCache cache)
        {
            _cache = cache;
        }

        public Task<T> GetAsync<T>(string uri)
        {
            T cachedResponse;
            if (_cache.TryGetResponse(uri, out cachedResponse))
            {
                return Task.FromResult(cachedResponse);
            }

            return Task.Run(async () =>
            {
                var httpResult = await this.GetAsync(uri);

                if (!httpResult.IsSuccessStatusCode)
                    throw new HttpRequestException($"Http Failure: Status {httpResult.StatusCode}");

                var response = JsonConvert.DeserializeObject<T>(await httpResult.Content.ReadAsStringAsync());
                _cache.CacheResponse(uri, response);

                return response;
            });
        }
    }
}
