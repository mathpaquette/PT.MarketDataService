using System;
using Moq;
using NUnit.Framework;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Extensions;
using PT.MarketDataService.Core.Models;
using PT.MarketDataService.Core.Providers;

namespace PT.MarketDataService.Tests.Models
{
    public class ScannerRequestTests
    {
        [TestCase("2018-04-02 9:30:00", Description = "business day and in range")]
        [TestCase("2018-04-01 9:30:00", Description = "!business day and in range")]
        [TestCase("2018-04-02 7:00:00", Description = "busines day and time < start time")]
        [TestCase("2018-04-01 7:00:00", Description = "!busines day and time < start time")]
        [TestCase("2018-04-06 17:00:00", Description = "busines day and time > end time")]
        [TestCase("2018-04-07 17:00:00", Description = "!busines day and time > end time")]
        public void Whenever_Time_UntilExpiration_Should_Return_BusinessDay_Between_Start_And_End_Time(DateTime now)
        {
            // Arrange
            var startTime = new TimeSpan(9, 30, 0);
            var endTime = new TimeSpan(16, 0, 0);
            var frequency = 60;
            var scannerParameter = new ScannerParameter();
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.SetupGet(x => x.Now).Returns(now);

            // Act
            var scannerRequest = new ScannerRequest(startTime, endTime, frequency, scannerParameter, timeProvider.Object);
            var next = now + scannerRequest.UntilExpiration;

            // Assert
            Assert.True(next.IsBusinessDay());
            Assert.True(next.TimeOfDay >= startTime);
            Assert.True(next.TimeOfDay <= endTime);
        }

        [TestCase("2018-04-02 9:30:00")]
        [TestCase("2018-04-02 12:00:00")]
        [TestCase("2018-04-02 16:00:00")]
        public void When_Time_Is_In_Range_And_Business_Day_TrySetOffline_Should_Return_False(DateTime now)
        {
            // Arrange
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.SetupGet(x => x.Now).Returns(now);

            // Act
            var scannerRequest = new ScannerRequest(new TimeSpan(9, 30, 0), new TimeSpan(16, 0, 0), 60, new ScannerParameter(), timeProvider.Object);

            // Assert
            Assert.False(scannerRequest.TrySetOffline());
        }

        [TestCase("2018-04-02 9:29:59")]
        [TestCase("2018-04-02 16:00:01")]
        public void When_Time_Is_Out_Of_Range_And_Business_Day_TrySetOffline_Should_Return_True(DateTime now)
        {
            // Arrange
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.SetupGet(x => x.Now).Returns(now);

            // Act
            var scannerRequest = new ScannerRequest(new TimeSpan(9, 30, 0), new TimeSpan(16, 0, 0), 60, new ScannerParameter(), timeProvider.Object);

            // Assert
            Assert.True(scannerRequest.TrySetOffline());
        }

        [TestCase("2018-04-01 9:30:00")]
        [TestCase("2018-04-01 12:00:00")]
        [TestCase("2018-04-01 16:00:00")]
        public void When_Time_Is_In_Range_And_Weekend_Day_TrySetOfflineShould_Return_False(DateTime now)
        {
            // Arrange
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.SetupGet(x => x.Now).Returns(now);

            // Act
            var scannerRequest = new ScannerRequest(new TimeSpan(9, 30, 0), new TimeSpan(16, 0, 0), 60, new ScannerParameter(), timeProvider.Object);

            // Assert
            Assert.True(scannerRequest.TrySetOffline());
        }
    }
}