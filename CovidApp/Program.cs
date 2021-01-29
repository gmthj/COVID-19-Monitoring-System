//============================================================
// Student Number : S10203190, S10203166
// Student Name : Tan Hiang Joon Gabriel, Marc Lim Liang Kiat
// Module Group : T07
//============================================================

using System;
using System.Collections.Generic;
using System.IO;

namespace CovidApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialisation
            List<string[]> personDataList = new List<string[]>();
            List<Person> personList = new List<Person>();
            List<BusinessLocation> businessList = new List<BusinessLocation>();
            while (true)
            {
                DisplayMenu();
            }

            //BASIC FEATURES

            //1) Load Person and Business Location Data
            LoadPersonData(personDataList);



            //random testing
            //foreach (string[] arr in personDataList)
            //{
            //    foreach (string data in arr)
            //    {
            //        Console.Write(data + "\t");
            //    }
            //    Console.WriteLine("");
            //}

            //CalculateSHNCharges() testing;
            //DateTime time = new DateTime(2021, 1, 29, 20, 40, 30);
            //DateTime EntryDate = new DateTime(2021, 2, 1, 8, 40, 30);
            //Person r1 = new Resident("Marc", "760 Waterfront Waves", time);
            //TravelEntry te = new TravelEntry("Macao SAR", "Air", EntryDate);
            //r1.AddTravelEntry(te);
            //r1.CalculateSHNCharges();
        }

        //1) Load Person Data
        static void LoadPersonData(List<string[]> personDataList)
        {
            using (StreamReader sr = new StreamReader("csv files/Person.csv"))
            {
                string line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(",");
                    personDataList.Add(splitLine);
                }
            }
        }

        static void LoadVisitor(List<string[]> personDataList, List<Person> personList) //wip
        {
            foreach (string[] personArray in personDataList)
            {
                if (personArray[0] == "visitor")
                {
                    personList.Add(new Visitor(personArray[1], personArray[4], personArray[5]));
                }
            }
        }


        // Student Number : S10203166
        // Student Name : Marc Lim Liang Kiat
        static void DisplayMenu()
        {
            Console.WriteLine("******************Menu******************");
            Console.WriteLine("[1]  List All Visitors");
            Console.WriteLine("[2]  List Person Details");
            Console.WriteLine("[3]  Assign/Replace TraceTogether Token");
            Console.WriteLine("[4]  List All Business Locations");
            Console.WriteLine("[5]  Edit Business Location Capacity");
            Console.WriteLine("[6]  SafeEntry Check-in");
            Console.WriteLine("[7]  SafeEntry Check-out");
            Console.WriteLine("[8]  List All SHN Facilities");
            Console.WriteLine("[9]  Create Visitor");
            Console.WriteLine("[10] Create TravelEntry Record");
            Console.WriteLine("[11] Calculate SHN Charges");
            Console.WriteLine("[12] Contact Tracing Reporting");
            Console.WriteLine("[13] SHN Status Reporting");
        }
    }
}
