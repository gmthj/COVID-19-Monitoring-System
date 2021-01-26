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


    }
}
