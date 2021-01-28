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
    class BusinessLocation
    {
        private string businessName;

        public string BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }

        private string branchCode;

        public string BranchCode
        {
            get { return branchCode; }
            set { branchCode = value; }
        }

        private int maximumCapacity;

        public int MaximumCapacity
        {
            get { return maximumCapacity; }
            set { maximumCapacity = value; }
        }

        private int visitorsNow;

        public int VisitorsNow
        {
            get { return visitorsNow; }
            set { visitorsNow = value; }
        }

        public BusinessLocation() { }

        public BusinessLocation(string name, string branchCode, int maximumCapacity)
        {
            BusinessName = name;
            BranchCode = branchCode;
            MaximumCapacity = maximumCapacity;
            VisitorsNow = 0;
        }

        public bool IsFull()
        {
            if (VisitorsNow == MaximumCapacity) // "==" is used instead of ">=" as there should not be a case where VisitorsNow > MaximumCapacity if checks are done correctly in Program.cs
            {
                return true;
            }
            return false;

        }

        public override string ToString()
        {
            return "Business Name: " + BusinessName + "\tBranch Code: " + BranchCode + "\tMaximum Capacity: " + MaximumCapacity + "\tVisitors Now: " + VisitorsNow;
        }
    }
}
