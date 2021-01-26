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
    class TravelEntry
    {
        private string lastCountryOfEmbarkation;

        public string LastCountryOfEmbarkation
        {
            get { return lastCountryOfEmbarkation; }
            set { lastCountryOfEmbarkation = value; }
        }

        private string entryMode;

        public string EntryMode
        {
            get { return entryMode; }
            set { entryMode = value; }
        }

        private DateTime entryDate;

        public DateTime EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }

        private DateTime shnEndDate;

        public DateTime ShnEndDate
        {
            get { return shnEndDate; }
            set { shnEndDate = value; }
        }

        private SHNFacility shnStay;

        public SHNFacility ShnStay
        {
            get { return shnStay; }
            set { shnStay = value; }
        }

        private bool isPaid;

        public bool IsPaid
        {
            get { return isPaid; }
            set { isPaid = value; }
        }

        public TravelEntry() { }

        public TravelEntry(string lastCountryOfEmbarkation, string entryMode, DateTime entryDate)
        {
            LastCountryOfEmbarkation = lastCountryOfEmbarkation;
            EntryMode = entryMode;
            EntryDate = entryDate;
            IsPaid = false;
        }

        public void AssignSHNFacility(SHNFacility shnStay)
        {
            ShnStay = shnStay;
        }

        public void CalculateSHNDuration()
        {
            if (LastCountryOfEmbarkation == "New Zealand" || LastCountryOfEmbarkation == "Vietnam")
            {
                ShnEndDate = EntryDate;
            }
            else if (LastCountryOfEmbarkation == "Macao SAR")
            {
                ShnEndDate = EntryDate.AddDays(7);
            }
            else
            {
                ShnEndDate = EntryDate.AddDays(14);
            }
        }

        public override string ToString()
        {
            return "Last Country Of Embarkation: " + LastCountryOfEmbarkation + "/tEntry Mode: " + EntryMode + "/tEntry Date: " + EntryDate + "/tShn End Date: " + ShnEndDate + "/tShn Stay: " + ShnStay + "/tIs Paid: " + IsPaid;
        }
    }
}
