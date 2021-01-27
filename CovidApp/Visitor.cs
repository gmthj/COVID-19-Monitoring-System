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
    class Visitor : Person
    {
        private string passportNo;

        public string PassportNo
        {
            get { return passportNo; }
            set { passportNo = value; }
        }

        private string nationality;

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        public Visitor(string name, string passportNo, string nationality) : base(name)
        {
            PassportNo = passportNo;
            Nationality = nationality;
        }

        public override double CalculateSHNCharges()
        {
            double charges = 200;
            if (base.TravelEntryList[-1].ShnStay == null)
            {
                charges += 80;
            }
            else
            {
                charges += 2000 + base.TravelEntryList[-1].ShnStay.CalculateTravelCost(base.TravelEntryList[-1].EntryMode, base.TravelEntryList[-1].EntryDate);
            }

            return charges * 1.07;
        }

        public override string ToString()
        {
            return "Passport No.: " + PassportNo + "\tNationality: " + Nationality;
        }
    }
}
