//============================================================
// Student Number : S10203166
// Student Name : Marc Lim Liang Kiat
// Module Group : T07
//============================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidApp
{
    class SafeEntry
    {
        private DateTime checkIn;

        public DateTime CheckIn
        {
            get { return checkIn; }
            set { checkIn = value; }
        }

        private DateTime checkOut;

        public DateTime CheckOut
        {
            get { return checkOut; }
            set { checkOut = value; }
        }

        private BusinessLocation location;

        public BusinessLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        public SafeEntry() { }

        public SafeEntry(DateTime checkIn, BusinessLocation location)
        {
            CheckIn = checkIn;
            Location = location;
            Location.VisitorsNow += 1;
        }

        public void PerformCheckOut()
        {
            CheckOut = DateTime.Now;
            Location.VisitorsNow -= 1;
        }

        public override string ToString()
        {
            return "Check In: " + CheckIn + "/tCheck Out: " + CheckOut + "/tLocation: " + Location;
        }
    }
}
