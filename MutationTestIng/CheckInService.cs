using MutationTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MutationTestIng
{
    public class CheckInService
    {
        private readonly IDictionary<int, Traveler> _registeredTravelers;
        public CheckInService(ICollection<Traveler> registeredTravelers)
        {
            if (registeredTravelers == null || registeredTravelers.Any(x => x == null))
            {
                throw new ArgumentException(Constants.NoNullTravelers);
            }
            _registeredTravelers = registeredTravelers
                .ToDictionary( x => x.PassportId, x => x);
        }

        public void CheckInTraveler(Traveler traveler)
        {
            if (traveler == null)
            {
                throw new ArgumentException(Constants.NoNullTravelers);
            }
            if (!IsRegisteredTraveler(traveler))
            {
                throw new ArgumentException(Constants.NotInRegisteredTravelers);
            }
            if (IsCheckedIn(traveler))
            {   
                throw new ArgumentException(Constants.AlreadyCheckedIn);
            }

            _registeredTravelers[traveler.PassportId].IsCheckedIn = true;
        }

        private bool IsRegisteredTraveler(Traveler traveler)
        {
            return _registeredTravelers.ContainsKey(traveler.PassportId);
        }

        private bool IsCheckedIn(Traveler traveler)
        {
            return _registeredTravelers[traveler.PassportId].IsCheckedIn;
        }

        public int GetNumberOfCheckedInTravelers()
        {
            return _registeredTravelers
                .Values
                .Where(x => x.IsCheckedIn == true)
                .Count();
        }

        public bool AllTravelersCheckedIn()
        {
            return _registeredTravelers
                .All(x => x.Value.IsCheckedIn);
        }
    }
}
