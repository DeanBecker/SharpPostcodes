using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpPostcodes;
using System.Threading.Tasks;
using SharpPostcodes.Models;

namespace SharpPostcodes.Test
{
    [TestClass]
    public class PostcodesTests
    {
        private static string _testPostCode = "SW1A 0AA";
        private static LatLong _testLatLong
            = new LatLong() { Lat = 51.499840469853467, Long = -0.12464832839964303 };

        [TestMethod]
        public async Task GetPostcodeDataParsesCorrectly()
        {
            var data = await Postcodes.GetPostcodeData(_testPostCode);

            Assert.AreEqual(data.postcode, _testPostCode);
        }

        [TestMethod]
        public async Task GetNearestPostcodeFromCoordsReturnsExpected()
        {
            var data = await Postcodes.GetNearestPostcode(_testLatLong);

            Assert.AreEqual(data.postcode, _testPostCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetNearbyPlacesFromPostCodeValidatesRadius()
        {
            var greaterThanFive = 10u;
            var data = await Postcodes.GetNearbyPlaces(_testPostCode, greaterThanFive);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetNearbyPlacesFromLatLongValidatesRadius()
        {
            var greaterThanFive = 10u;
            var data = await Postcodes.GetNearbyPlaces(_testLatLong, greaterThanFive);
        }

        [TestMethod]
        public async Task GetNearbyPlacesFromPostcodeReturnsExpectedCount()
        {
            var radius = 0u;
            var data = await Postcodes.GetNearbyPlaces(_testPostCode, radius);

            Assert.IsTrue(data.Count == 6);
        }

        [TestMethod]
        public async Task GetNearbyPlacesFromLatLongReturnsExpectedCount()
        {
            var radius = 0u;
            var data = await Postcodes.GetNearbyPlaces(_testLatLong, radius);

            Assert.IsTrue(data.Count == 6);
        }
    }
}
