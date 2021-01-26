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
    class TraceTogetherToken
    {
        private string serialNo;

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        private string collectionLocation;

        public string CollectionLocation
        {
            get { return collectionLocation; }
            set { collectionLocation = value; }
        }

        private DateTime expiryDate;

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public TraceTogetherToken() { }

        public TraceTogetherToken(string serialNumber, string collectionLocation, DateTime expiryDate)
        {
            SerialNo = serialNumber;
            CollectionLocation = collectionLocation;
            ExpiryDate = expiryDate;
        }

        public bool IsEligibleForReplacement()
        {
            bool eligibility = true;
            DateTime today = DateTime.Today; // DateTime.Today is used instead of DateTime. Now as the precising of exact time inst needed as we are only comparing months/days
            int dateDifferenceTimeSpan = ((today.Year - ExpiryDate.Year) * 12) + ((today.Month - ExpiryDate.Month) *30) + today.Day - ExpiryDate.Day;
            if (dateDifferenceTimeSpan < -30 || dateDifferenceTimeSpan > 0) //assuming there are 30 days in a month
            {
                eligibility = false;
            }
            return eligibility;
        }

        public void ReplaceToken(string newSerialNumber, string newCollectionLocation)
        {
            SerialNo = newSerialNumber;
            CollectionLocation = newCollectionLocation;
            DateTime newExpiryDate = DateTime.Today.AddMonths(6); //Trace Together Token expiries 6 months after issuing
            ExpiryDate = newExpiryDate;
        }

        public override string ToString()
        {
            return "SerialNo: " + SerialNo + "/tCollection Location:" + CollectionLocation + "/tExpiry Date: " + ExpiryDate;
        }
    }
}
