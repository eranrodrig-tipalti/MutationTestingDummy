using Xunit;
using MutationTestIng;
using System;
using FluentAssertions;
using System.Collections.Generic;

namespace MutationTesting.Tests
{
    public class MutationTestingTestSuite
    {
        private CheckInService _checkInService;
        private readonly Traveler[] _defaultTravelers = new Traveler[] { new Traveler(1, "Master Splinter"), new Traveler(2, "April O'Neil") };

        public MutationTestingTestSuite()
        {
        }

        private void InitCheckInService(ICollection<Traveler> travelers)
        {
            if (travelers == null)
            {
                Console.WriteLine("bla");
                travelers = _defaultTravelers;

            }
            _checkInService = new CheckInService(travelers);
        }

        [Fact]
        public void RegisterTraveler_Throws_Exception_For_Null_Traveler()
        {
            InitCheckInService(null);
            Assert.Throws<ArgumentException>(() => _checkInService.CheckInTraveler(null));
        }

        [Fact]
        public void CheckInTraveler_ChecksIn_Valid_Traveler_Successfully()
        {
            InitCheckInService(null);
            _checkInService.CheckInTraveler(_defaultTravelers[0]);
            _checkInService.GetNumberOfCheckedInTravelers().Should().Be(1);
        }

        [Fact]
        public void CheckInTraveler_Which_Has_Been_Registered_throws_exception()
        {
            InitCheckInService(null);
            _checkInService.CheckInTraveler(_defaultTravelers[0]);

            Assert.Throws<ArgumentException>(() => _checkInService.CheckInTraveler(_defaultTravelers[0]));
            _checkInService.GetNumberOfCheckedInTravelers().Should().Be(1);
        }

        [Fact]
        public void InitCheckInService_with_null_traveler_throws_exception()
        {
            Traveler[] travelers = new Traveler[] { new Traveler(1, "Master Splinter"), null };
            Assert.Throws<ArgumentException>(() => InitCheckInService(travelers));
        }

        [Fact]
        public void CheckInTraveler_Checks_In_All_Valid_Travelers_Successfully()
        {
            InitCheckInService(null);
            foreach (Traveler t in _defaultTravelers)
            {
                _checkInService.CheckInTraveler(t);
            }
            _checkInService.GetNumberOfCheckedInTravelers().Should().Be(_defaultTravelers.Length);
            _checkInService.AllTravelersCheckedIn().Should().BeTrue();
        }

        [Fact]
        public void AllTravelersCheckedIn_returns_false_if_not_all_travelers_checked_in()
        {
            InitCheckInService(null);
            _checkInService.AllTravelersCheckedIn().Should().BeFalse();

            _checkInService.CheckInTraveler(_defaultTravelers[0]);
            _checkInService.AllTravelersCheckedIn().Should().BeFalse();
        }

    }
}
