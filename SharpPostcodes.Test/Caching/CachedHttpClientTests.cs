using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpPostcodes.Caching;
using System.Threading.Tasks;
using SharpPostcodes.Models;

namespace SharpPostcodes.Test.Caching
{
    [TestClass]
    public class CachedHttpClientTests
    {
        [TestMethod]
        public async Task CachedHttpClientAttemptsCacheHit()
        {
            var mockedCache = new Mock<IRequestCache>();
            PostcodeDetail mockOutput;
            mockedCache.Setup(c => c.TryGetResponse(It.IsAny<string>(), out mockOutput))
                       .Returns(false);

            var client = new CachedHttpClient(mockedCache.Object);
            await client.GetAsync<PostcodeDetail>("http://uk-postcodes.com/postcode/SW1A0AA.json");

            mockedCache.Verify(c => c.TryGetResponse(It.IsAny<string>(), out mockOutput), Times.AtLeastOnce);
        }
    }
}
