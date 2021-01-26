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
    class SHNFacility
    {
        private string facilityName;

        public string FacilityName
        {
            get { return facilityName; }
            set { facilityName = value; }
        }

        private int facilityCapacity;

        public int FacilityCapacity
        {
            get { return facilityCapacity; }
            set { facilityCapacity = value; }
        }

        private int facilityVacancy;

        public int FacilityVacancy
        {
            get { return facilityVacancy; }
            set { facilityVacancy = value; }
        }

        private double distFromAirCheckpoint;

        public double DistFromAirCheckpoint
        {
            get { return distFromAirCheckpoint; }
            set { distFromAirCheckpoint = value; }
        }

        private double distFromSeaCheckpoint;

        public double DistFromSeaCheckpoint
        {
            get { return distFromSeaCheckpoint; }
            set { distFromSeaCheckpoint = value; }
        }

        private double distFromLandCheckpoint;

        public double DistFromLandCheckpoint
        {
            get { return distFromLandCheckpoint; }
            set { distFromLandCheckpoint = value; }
        }

        public SHNFacility() { }

        public SHNFacility(string facilityName, int facilityCapacity, double distFromAirCheckpoint, double distFromSeaCheckpoint, double distFromLandCheckpoint)
        {
            FacilityName = facilityName;
            FacilityCapacity = facilityCapacity;
            FacilityVacancy = facilityCapacity; //*
            DistFromAirCheckpoint = distFromAirCheckpoint;
            DistFromLandCheckpoint = distFromLandCheckpoint;
            DistFromSeaCheckpoint = distFromSeaCheckpoint;
        }

        public double CalculateTravelCost(string entryMode, DateTime entryDate)
        {
            double travelCost = 50;
            if (entryMode == "Air")
            {
                travelCost += 0.22 * DistFromAirCheckpoint;
            }
            if (entryMode == "Sea")
            {
                travelCost += 0.22 * DistFromSeaCheckpoint;
            }
            if (entryMode == "Lead")
            {
                travelCost += 0.22 * DistFromLandCheckpoint;
            }

            TimeSpan am0600 = new TimeSpan(6, 0, 0);
            TimeSpan am0859 = new TimeSpan(8, 59, 0);

            TimeSpan pm1800 = new TimeSpan(18, 0, 0);
            TimeSpan pm2359 = new TimeSpan(23, 59, 0);

            TimeSpan am0000 = new TimeSpan(0, 0, 0);
            TimeSpan am0559 = new TimeSpan(5, 59, 0);
            TimeSpan time = entryDate.TimeOfDay;

            if ((am0600 <= time) && (time <= am0859) || (pm1800 <= time) && (time <= pm2359))
            {
                travelCost *= 1.25;
            }
            if ((am0000 <= time) && (time <= am0559))
            {
                travelCost *= 1.5;
            }

            return travelCost;
        }

        public bool IsAvailable()
        {
            if (FacilityVacancy > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Facility Name: " + FacilityName + "/tFacility Capacity: " + FacilityCapacity + "/tFacility Vacancy: " + FacilityVacancy + "/tDistance From Air Checkpoint: " + DistFromAirCheckpoint + "/tDistance From Sea Checkpoint: " + DistFromSeaCheckpoint + "/tDistance From Land Checkpoint: " + DistFromLandCheckpoint;
        }
    }
}
