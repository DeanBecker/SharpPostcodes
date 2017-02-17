using System;

namespace SharpPostcodes.Caching
{
    public interface IRequestCache
    {
        void CacheResponse(string forRequest, object response);
        bool TryGetResponse<T>(string forRequest, out T cachedResponse);
    }
}