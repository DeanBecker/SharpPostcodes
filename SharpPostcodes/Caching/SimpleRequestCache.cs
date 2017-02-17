using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpPostcodes.Caching
{
    public class SimpleRequestCache : IRequestCache
    {
        private Dictionary<string, object> _cachedRequests;

        #region Singleton
        private static SimpleRequestCache _singleton;

        public static SimpleRequestCache GetInstance()
        {
            // The cache being system-wide makes sense
            if (_singleton == null)
                _singleton = new SimpleRequestCache();

            return _singleton;
        }
        #endregion

        private SimpleRequestCache()
        {
            _cachedRequests = new Dictionary<string, object>();
        }

        public bool TryGetResponse<T>(string forRequest, out T cachedResponse)
        {
            cachedResponse = default(T);
            object o;
            var responseExists = _cachedRequests.TryGetValue(forRequest, out o);

            if (responseExists)
            {
                cachedResponse = (T)o;
                return true;
            }
            return false;
        }

        public void CacheResponse(string forRequest, object response)
        {
            _cachedRequests[forRequest] = response;
        }
    }
}
