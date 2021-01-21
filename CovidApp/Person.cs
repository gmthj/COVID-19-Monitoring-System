//============================================================
// Student Number : S10203190
// Student Name : Tan Hiang Joon Gabriel
// Module Group : T07
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace CovidApp
{
    abstract class Person
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<SafeEntry> safeEntryList;

        public List<SafeEntry> SafeEntryList
        {
            get { return safeEntryList; }
            set { safeEntryList = value; }
        }

        private List<TravelEntry> travelEntryList;

        public List<TravelEntry> TravelEntryList
        {
            get { return travelEntryList; }
            set { travelEntryList = value; }
        }

        public Person()
        {
            SafeEntryList = new List<SafeEntry>();
            TravelEntryList = new List<TravelEntry>();
        }

        public Person(string n)
        {
            Name = n;
            SafeEntryList = new List<SafeEntry>();
            TravelEntryList = new List<TravelEntry>();
        }

        public void AddTravelEntry(TravelEntry travelEntry)
        {
            TravelEntryList.Add(travelEntry);
        }

        public void AddSafeEntry(SafeEntry safeEntry)
        {
            SafeEntryList.Add(safeEntry);
        }

        public abstract double CalculateSHNCharges();

        public override string ToString()
        {
            return "Name: " + Name;
        }
    }
}
