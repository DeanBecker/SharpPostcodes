using SharpPostcodes.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpPostcodes.Test.Mocks
{
    public class MockRequestCache : IRequestCache
    {
        public void CacheResponse(string forRequest, object response) { }

        public bool TryGetResponse<T>(string forRequest, out T cachedResponse)
        {
            cachedResponse = default(T);
            return false;
        }
    }
}
