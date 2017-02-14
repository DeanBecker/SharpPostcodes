using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpPostcodes.Caching;

namespace SharpPostcodes.Test.Caching
{
    [TestClass]
    public class RequestCachingTests
    {
        [TestMethod]
        public void SimpleRequestCacheInstanceInit()
        {
            var cache = SimpleRequestCache.GetInstance();

            Assert.IsInstanceOfType(cache, typeof(SimpleRequestCache));
        }

        [TestMethod]
        public void SimpleRequestCacheInstanceIsSingleton()
        {
            var cache_1 = SimpleRequestCache.GetInstance();
            var cache_2 = SimpleRequestCache.GetInstance();

            Assert.AreSame(cache_1, cache_2);
        }

        [TestMethod]
        public void SimpleRequestCacheRetrievesCachedResponse()
        {
            var request = @"http://my-test-request/";
            var response = @"My Test Response";

            var cache = SimpleRequestCache.GetInstance();
            cache.CacheResponse(request, response);

            string cachedResponse;
            var cachedResponseExists = cache.TryGetResponse<string>(request, out cachedResponse);

            Assert.IsTrue(cachedResponseExists);
        }

        [TestMethod]
        public void SimpleRequestCacheMissesUncachedResponse()
        {
            var request = @"THIS DOES NOT EXIST";

            var cache = SimpleRequestCache.GetInstance();
            string cacheMiss;
            var cacheExists = cache.TryGetResponse<string>(request, out cacheMiss);

            Assert.IsFalse(cacheExists);
        }
    }
}
