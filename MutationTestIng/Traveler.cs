using System;
using System.Collections.Generic;
using System.Text;

namespace MutationTestIng
{
    public class Traveler
    {
        public int PassportId { get; private set; }
        public string Name { get; private set; }
        public bool IsCheckedIn { get; set; }
        public Traveler(int passportId, string name)
        {
            PassportId = passportId;
            Name = name;
            IsCheckedIn = false;
        }
    }
}
