using System;
using NUnit.Framework;
using PT.MarketDataService.Core.Extensions;

namespace PT.MarketDataService.Tests.Extensions
{
    public class DateExtensionsTests
    {
        [TestCase("00:02:59.001", "00:03:00.000")]
        [TestCase("00:03:00.000", "00:03:00.000")]
        [TestCase("00:03:00.001", "00:03:01.000")]
        public void TimeSpan_Should_Be_RoundUp_To_Nearest_Second(string ts, string res)
        {
            // Arrange
            var timeSpan = TimeSpan.Parse(ts);
            var result = TimeSpan.Parse(res);

            // Act
            var rounded = timeSpan.RoundUp(TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(rounded, result);
        }
    }
}