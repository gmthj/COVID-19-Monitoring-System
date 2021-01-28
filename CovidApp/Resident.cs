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
    class Resident : Person
    {
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private DateTime lastLeftCountry;

        public DateTime LastLeftCountry
        {
            get { return lastLeftCountry; }
            set { lastLeftCountry = value; }
        }

        private TraceTogetherToken token;

        public TraceTogetherToken Token
        {
            get { return token; }
            set { token = value; }
        }

        public Resident(string name, string address, DateTime lastLeftCountry): base(name)
        {
            Address = address;
            LastLeftCountry = lastLeftCountry;
        }

        public override double CalculateSHNCharges()
        {
            //string lastCountryVisited = this.TravelEntryList[TravelEntryList.Count-1].LastCountryOfEmbarkation;
            double subTotalCost = 200;
            TravelEntry latestTravelEntry = this.TravelEntryList[TravelEntryList.Count - 1];
            int duration = (latestTravelEntry.ShnEndDate - latestTravelEntry.EntryDate).Days;
            if (duration == 7)
            {
                subTotalCost += 20;
            }
            else if (duration == 14)
            {
                subTotalCost += 20 + 1000;
            }
            double totalCost = subTotalCost * 1.07;
            return totalCost;
        }

        public override string ToString()
        {
            return base.ToString() + "\tAddress: " + Address + "\tLast Left Country: " + LastLeftCountry;
        }

    }
}
