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
            List<string> serialNoList = new List<string>();
            List<Resident> residentList = new List<Resident>();
            List<BusinessLocation> businessList = new List<BusinessLocation>();

            while (true)
            {
                DisplayMenu();
                int selectedOption = ObtainMenuInput();
                if (selectedOption == 1) //Load Person and Business Location Data
                {
                    ObtainResidentsData(serialNoList, residentList);
                    //LoadVisitor()
                    ObtainBusinessesData(businessList);
                    InitializePersonList(personList, residentList, null);
                    Console.WriteLine("Data has been loaded.");
                    Console.WriteLine();
                }
                else if (selectedOption == 2) //Load SHN Facility Data
                {

                }
                else if (selectedOption == 3) //List All Visitors
                {

                }
                else if (selectedOption == 4) //List Person Details
                {

                }

                else if (selectedOption == 5) //Assign/Replace TraceTogether Token
                {
                    UpdateToken(residentList, serialNoList);
                    Console.WriteLine();
                    continue;
                }

                else if (selectedOption == 6) //List All Business Locations
                {
                    DisplayBusinessList(businessList);
                    continue;
                }

                else if (selectedOption == 7) //Edit Business Location Capacity
                {
                    EditLocationCapacity(businessList);
                    continue;
                }

                else if (selectedOption == 8) //SafeEntry Check-in
                {
                    SafeEntryCheckIn(personList, businessList);
                    continue;
                }

                else if (selectedOption == 9) //SafeEntry Check-out
                {
                    SafeEntryCheckOut(personList);
                    continue;
                }

                else if (selectedOption == 10) //List All SHN Facilities
                {

                }

                else if (selectedOption == 11) //Create Visitor
                {

                }

                else if (selectedOption == 12) //Create TravelEntry Record
                {

                }

                else if (selectedOption == 13) //Calculate SHN Charges
                {

                }

                else if (selectedOption == 14) //Contact Tracing Reporting
                {

                }

                else if (selectedOption == 15) //SHN Status Reporting
                {

                }

                else if (selectedOption == 16) //Exit
                {
                    break;
                }
            }





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

            //CalculateSHNCharges() testing;
            //DateTime time = new DateTime(2021, 1, 29, 20, 40, 30);
            //DateTime EntryDate = new DateTime(2021, 2, 1, 8, 40, 30);
            //Person r1 = new Visitor("john", "wefwe", "vewdvw");
            //TravelEntry te = new TravelEntry("Macao kb kSAR", "Air", EntryDate);
            //r1.AddTravelEntry(te);
            //Console.WriteLine(r1.CalculateSHNCharges());

            //ObtainResidentsData() testing
            //foreach (Resident r in residentList)
            //{
            //    Console.WriteLine(r);
            //}

            //UpdateToken() testing
            //Resident testResident = new Resident("Marc", "123 East Road", new DateTime(2020, 12, 20));
            //TraceTogetherToken testToken = new TraceTogetherToken("T23451", "test", new DateTime(2021, 2, 20));
            //testResident.Token = testToken;
            //residentList.Add(testResident);
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

        // Methods below were coded by:
        // Student Number : S10203166
        // Student Name : Marc Lim Liang Kiat
        static void DisplayMenu()
        {
            Console.WriteLine("******************Menu******************");
            Console.WriteLine("[1]  Load Person and Business Location Data");
            Console.WriteLine("[2]  Load SHN Facility Data");
            Console.WriteLine("[3]  List All Visitors");
            Console.WriteLine("[4]  List Person Details");
            Console.WriteLine("[5]  Assign/Replace TraceTogether Token");
            Console.WriteLine("[6]  List All Business Locations");
            Console.WriteLine("[7]  Edit Business Location Capacity");
            Console.WriteLine("[8]  SafeEntry Check-in");
            Console.WriteLine("[9]  SafeEntry Check-out");
            Console.WriteLine("[10] List All SHN Facilities");
            Console.WriteLine("[11] Create Visitor");
            Console.WriteLine("[12] Create TravelEntry Record");
            Console.WriteLine("[13] Calculate SHN Charges");
            Console.WriteLine("[14] Contact Tracing Reporting");
            Console.WriteLine("[15] SHN Status Reporting");
            Console.WriteLine("[16] Exit");
        }

        static int ObtainMenuInput()
        {
            Console.Write("Please enter your option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }
        static List<Resident> ObtainResidentsData(List<string> serialNoList, List<Resident> residentList)
        {
            using (StreamReader sr = new StreamReader("csv files/Person.csv"))
            {
                string line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] formattedLine = line.Split(",");
                    if (formattedLine[0] == "resident")
                    {
                        if (formattedLine[6]!="")
                        {
                            Resident newResident = new Resident(formattedLine[1], formattedLine[2], Convert.ToDateTime(formattedLine[3]));
                            TraceTogetherToken newToken = new TraceTogetherToken(formattedLine[6], formattedLine[7], Convert.ToDateTime(formattedLine[8]));
                            newResident.Token = newToken;
                            serialNoList.Add(newToken.SerialNo);
                            residentList.Add(newResident);
                        }
                        else
                        {
                            Resident newResident = new Resident(formattedLine[1], formattedLine[2], Convert.ToDateTime(formattedLine[3]));
                            residentList.Add(newResident);
                        }
                    }
                }
            }
            return residentList;
        }
        static Resident SearchResidentByName(List<Resident> residentList, string name)
        {
            foreach(Resident r in residentList)
            {
                if (name == r.Name)
                {
                    return r;
                }
            }
            return null;
        }

        static string GenerateNewSerialNo(List<string> serialNoList)
        {
            string serialNo;
            while (true)
            {
                Random rnd = new Random();
                int min = 0;
                int max = 99999;
                int randomNumber = rnd.Next(min,max);
                serialNo = "T" + randomNumber.ToString("D5");
                if (serialNoList.Contains(serialNo))
                {
                    Console.WriteLine("Working");
                    continue;
                }
                else
                {
                    serialNoList.Add(serialNo);
                    break;
                }
            }
            //foreach(string s in serialNoList)
            //{
            //    Console.WriteLine(s);
            //}
            //Console.WriteLine(serialNo);
            return serialNo;
        }
        static string ObtainCollectionLocation(Resident r)
        {
            string address = r.Address;
            string[] formattedAddress = address.Split(" ");
            string collectionLocation = formattedAddress[1] + " " + formattedAddress[2] + " CC";
            return collectionLocation;
        }

        static void UpdateToken(List<Resident> residentList, List<string> serialNoList)
        {
            Console.Write("Please enter name of resident: ");
            string residentName = Console.ReadLine();
            Resident searchedResident = SearchResidentByName(residentList, residentName);
            if (searchedResident != null)
            {
                if (searchedResident.Token == null)
                {
                    string collectionLocation = ObtainCollectionLocation(searchedResident);
                    string serialNo = GenerateNewSerialNo(serialNoList);
                    DateTime expiryDate = DateTime.Today.AddMonths(6);
                    TraceTogetherToken newToken = new TraceTogetherToken(serialNo, collectionLocation, expiryDate);
                    searchedResident.Token = newToken;
                    Console.WriteLine();
                    Console.WriteLine("New TraceTogether Token has been assigned to resident with name {0}.", residentName);
                    Console.WriteLine();
                    Console.WriteLine("Assigned TraceTogether Token has the following details.");
                    Console.WriteLine("Serial No: {0}", newToken.SerialNo);
                    Console.WriteLine("Collection Location: {0}", newToken.CollectionLocation);
                    Console.WriteLine("Expiry Date: {0}", newToken.ExpiryDate);
                }
                else
                {
                    bool IsEligible = searchedResident.Token.IsEligibleForReplacement();
                    if (IsEligible)
                    {
                        string serialNo = GenerateNewSerialNo(serialNoList);
                        string collectionLocation = ObtainCollectionLocation(searchedResident);
                        searchedResident.Token.ReplaceToken(serialNo, collectionLocation);
                        Console.WriteLine();
                        Console.WriteLine("TraceTogether Token of resident with name {0} has been replaced.", residentName);
                        Console.WriteLine();
                        Console.WriteLine("Replacement TraceTogether Token has the following details.");
                        Console.WriteLine("Serial No: {0}", searchedResident.Token.SerialNo);
                        Console.WriteLine("Collection Location: {0}", searchedResident.Token.CollectionLocation);
                        Console.WriteLine("Expiry Date: {0}", searchedResident.Token.ExpiryDate);

                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("TraceTogether Token of resident with name {0} is not eligible for replacement.", residentName);
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Resident with name {0} does not exist.", residentName);
            }
        }
        static List<BusinessLocation> ObtainBusinessesData(List<BusinessLocation> businessList)
        {
            using (StreamReader sr = new StreamReader("csv files/BusinessLocation.csv"))
            {
                string line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] formattedLine = line.Split(",");
                    BusinessLocation newBusiness = new BusinessLocation(formattedLine[0], formattedLine[1], Convert.ToInt32(formattedLine[2]));
                    businessList.Add(newBusiness);
                }
            }
            return businessList;
        }
        static void DisplayBusinessList(List<BusinessLocation> businessList)
        {
            int businessCount = 1;
            Console.WriteLine();
            foreach(BusinessLocation b in businessList)
            {
                Console.WriteLine("Business Location Number [{0}]", Convert.ToString(businessCount));
                Console.WriteLine("Business Name: {0}",b.BusinessName);
                Console.WriteLine("Branch Code: {0}",b.BranchCode);
                Console.WriteLine("Maximum Capacity: {0}",b.MaximumCapacity);
                Console.WriteLine("Visitors Now: {0}",b.VisitorsNow);
                Console.WriteLine();
                businessCount += 1;
            }
        }
        static BusinessLocation SearchBusinessLocation(List<BusinessLocation> businessList)
        {
            Console.Write("Pleae enter the name of the business: ");
            string businessName = Console.ReadLine();
            Console.Write("Please enter the branch code of business location: ");
            string branchCode = Console.ReadLine();
            foreach(BusinessLocation b in businessList)
            {
                if (b.BusinessName == businessName && b.BranchCode == branchCode)
                {
                    return b;
                }
            }
            return null;
        }
        static void EditLocationCapacity(List<BusinessLocation> businessList)
        {
            BusinessLocation searchedLocation = SearchBusinessLocation(businessList);
            Console.Write("Please enter new maximum capacity of business location: ");
            int newMaxCapacity = Convert.ToInt32(Console.ReadLine());
            searchedLocation.MaximumCapacity = newMaxCapacity;
            Console.WriteLine();
        }

        static void InitializePersonList(List<Person> personList, List<Resident> residentList, List<Visitor> visitorList)
        {
            foreach (Resident r in residentList)
            {
                personList.Add(r);
            }
        }

        static Person SearchPersonByName(List<Person> personList, string name)
        {
            foreach(Person p in personList)
            {
                if (p.Name == name)
                {
                    return p;
                }
            }
            return null;
        }
        static void SafeEntryCheckIn(List<Person> personList, List<BusinessLocation> businessList)
        {
            Console.Write("Please enter the name of the person that is checking in: ");
            string personName = Console.ReadLine();
            Person searchedPerson = SearchPersonByName(personList, personName);
            DisplayBusinessList(businessList);
            Console.Write("Please enter the Business Location Number (1, 2, 3, etc.) of the business location that is being checked in to: ");
            int locationNumber = Convert.ToInt32(Console.ReadLine());
            BusinessLocation safeEntryLocation = businessList[locationNumber-1];
            if (safeEntryLocation.IsFull())
            {
                Console.WriteLine("Unable to SafeEntry Check In as business location is at maximum capacity.");
                Console.WriteLine();
            }
            else
            {
                SafeEntry newSafeEntry = new SafeEntry(DateTime.Now, safeEntryLocation);
                safeEntryLocation.VisitorsNow += 1;
                searchedPerson.AddSafeEntry(newSafeEntry);
                Console.WriteLine("SafeEntry Entry Check In Successfull.");
                Console.WriteLine();
            }

        }
        static void SafeEntryCheckOut(List<Person> personList)
        {
            int count = 1;
            Console.Write("Please enter the name of the person that is checking out: ");
            string personName = Console.ReadLine();
            Person searchedPerson = SearchPersonByName(personList, personName);
            foreach (SafeEntry se in searchedPerson.SafeEntryList)
            {
                Console.WriteLine("Safe Entry Record Number [{0}]",count);
                Console.WriteLine("Check In Date And Time: {0}", se.CheckIn);
                Console.WriteLine("Business Location: {0}", se.Location);
                Console.WriteLine();
                count += 1;
            }
            Console.Write("Please enter the SafeEntry Record Number (1, 2, 3, etc.) of the SafeEntry Record to check out of: ");
            int chosenRecord = Convert.ToInt32(Console.ReadLine());
            SafeEntry chosenSafeEntry = searchedPerson.SafeEntryList[chosenRecord-1];
            chosenSafeEntry.PerformCheckOut();
            chosenSafeEntry.Location.VisitorsNow -= 1;
            Console.WriteLine("Person with name {0} has been checked out of {1}.", personName, chosenSafeEntry.Location.BusinessName);
            Console.WriteLine();

        }

        
    }
}
