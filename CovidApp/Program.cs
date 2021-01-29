//============================================================
// Student Number : S10203190, S10203166
// Student Name : Tan Hiang Joon Gabriel, Marc Lim Liang Kiat
// Module Group : T07
//============================================================

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialisation
            List<Person> personList = new List<Person>();
            List<string> serialNoList = new List<string>();
            List<Resident> residentList = new List<Resident>();
            List<Visitor> visitorList = new List<Visitor>();
            List<BusinessLocation> businessList = new List<BusinessLocation>();
            List<SHNFacility> facilityList = new List<SHNFacility>();

            ObtainResidentsData(serialNoList, residentList);
            ObtainVisitorData(visitorList, facilityList);
            ObtainBusinessesData(businessList);
            InitializePersonList(personList, residentList, visitorList);
            ObtainSHNFacilityData(facilityList);

            while (true)
            {
                DisplayMenu();
                int selectedOption = ObtainMenuInput();
                if (selectedOption == 1) //Load Person and Business Location Data
                {
                    residentList.Clear();
                    serialNoList.Clear();
                    visitorList.Clear();
                    personList.Clear();
                    businessList.Clear();
                    ObtainResidentsData(serialNoList, residentList);
                    ObtainVisitorData(visitorList, facilityList);
                    ObtainBusinessesData(businessList);
                    InitializePersonList(personList, residentList, visitorList);
                    Console.WriteLine("Data has been loaded.");
                }
                else if (selectedOption == 2) //Load SHN Facility Data
                {
                    facilityList.Clear();
                    ObtainSHNFacilityData(facilityList);
                }
                else if (selectedOption == 3) //List All Visitors
                {
                    DisplayVisitorList(personList);
                }
                else if (selectedOption == 4) //List Person Details
                {

                }

                else if (selectedOption == 5) //Assign/Replace TraceTogether Token
                {
                    UpdateToken(residentList, serialNoList);
                }

                else if (selectedOption == 6) //List All Business Locations
                {
                    DisplayBusinessList(businessList);
                }

                else if (selectedOption == 7) //Edit Business Location Capacity
                {
                    EditLocationCapacity(businessList);
                }

                else if (selectedOption == 8) //SafeEntry Check-in
                {
                    SafeEntryCheckIn(personList, businessList);
                }

                else if (selectedOption == 9) //SafeEntry Check-out
                {
                    SafeEntryCheckOut(personList);
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
                    ContactTracingReport(personList);
                }

                else if (selectedOption == 15) //SHN Status Reporting
                {

                }

                else if (selectedOption == 16) //Exit
                {
                    break;
                }
                Console.WriteLine();
            }


            //TESTING
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

            //Resident testResident = new Resident("Marc", "123 East Road", new DateTime(2020, 12, 20));
            //TraceTogetherToken testToken = new TraceTogetherToken("T23451", "test", new DateTime(2021, 2, 20));
            //testResident.Token = testToken;
            //residentList.Add(testResident);
        }


        // Methods below were coded by:
        // Student Number : S10203190
        // Student Name : Tan Hiang Joon Gabriel
        static void ObtainVisitorData(List<Visitor> visitorList, List<SHNFacility> facilityList)
        {
            using (StreamReader sr = new StreamReader("csv files/Person.csv"))
            {
                string line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(",");
                    if (splitLine[0] == "visitor")
                    {
                        Visitor newVisitor = new Visitor(splitLine[1], splitLine[4], splitLine[5]);

                        if (splitLine[9] != "")
                        {
                            newVisitor.AddTravelEntry(NewTravelEntry(splitLine[9], splitLine[10], Convert.ToDateTime(splitLine[11]), Convert.ToDateTime(splitLine[12]), Convert.ToBoolean(splitLine[13]), splitLine[14], facilityList));
                        }
                        
                        visitorList.Add(newVisitor);
                    }
                }
            }
        }

        static TravelEntry NewTravelEntry(string travelEntryLastCountry, string travelEntryMode, DateTime travelEntryDate, DateTime travelShnEndDate, bool travelIsPaid, string facilityName, List<SHNFacility> facilityList)
        {
            TravelEntry te = new TravelEntry(travelEntryLastCountry, travelEntryMode, travelEntryDate);
            te.IsPaid = travelIsPaid;
            //te.AssignSHNFacility(SearchSHNFacility(facilityName, facilityList)); loading of shn facilities to be completed first
            if (travelShnEndDate == null)
            {
                te.CalculateSHNDuration();
            }

            return te;
        }

        static SHNFacility SearchSHNFacility(string facilityName, List<SHNFacility> facilityList)
        {
            foreach (SHNFacility sf in facilityList)
            {
                if (sf.FacilityName == facilityName)
                {
                    return sf;
                }
            }
            Console.WriteLine("\nERROR: facility name not found in SHN facility list\n"); //error message for testing.
            return null;
        }

        static void ObtainSHNFacilityData(List<SHNFacility> facilityList)
        {
            List<SHNFacility> tempFacilityList = new List<SHNFacility>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://covidmonitoringapiprg2.azurewebsites.net");
                Task<HttpResponseMessage> responseTask = client.GetAsync("/facility");
                responseTask.Wait();
                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Task<string> readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string data = readTask.Result;
                    tempFacilityList = JsonConvert.DeserializeObject<List<SHNFacility>>(data);
                    foreach (SHNFacility sf in tempFacilityList) //bootleg solution but hey it works:)
                    {
                        facilityList.Add(sf);
                    }
                }
            }
        }

        static void DisplayVisitorList(List<Person> personList)
        {
            int visitorCount = 1;
            foreach (Person p in personList)
            {
                if (p is Visitor)
                {
                    Visitor v = (Visitor)p;
                    Console.WriteLine("\nVisitor Number [" + visitorCount + "]");
                    Console.WriteLine("Visitor Name: {0}", v.Name);
                    Console.WriteLine("Visitor Passport Number: {0}", v.PassportNo);
                    Console.WriteLine("Visitor Nationality: {0}", v.Nationality);
                    visitorCount++;
                }
            }
        }

        // End of methods coded by:
        // Student Number : S10203190
        // Student Name : Tan Hiang Joon Gabriel


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
        static void ObtainResidentsData(List<string> serialNoList, List<Resident> residentList)
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
        }

        static void InitializePersonList(List<Person> personList, List<Resident> residentList, List<Visitor> visitorList)
        {
            foreach (Resident r in residentList)
            {
                personList.Add(r);
            }
            // Student Number : S10203190
            // Student Name : Tan Hiang Joon Gabirel
            foreach (Visitor v in visitorList)
            {
                personList.Add(v);
            }
            //end gabriel
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
        static void ObtainBusinessesData(List<BusinessLocation> businessList)
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
            }
            else
            {
                SafeEntry newSafeEntry = new SafeEntry(DateTime.Now, safeEntryLocation);
                safeEntryLocation.VisitorsNow += 1;
                searchedPerson.AddSafeEntry(newSafeEntry);
                Console.WriteLine("SafeEntry Entry Check In Successfull.");
            }

        }
        static void SafeEntryCheckOut(List<Person> personList)
        {
            int count = 1;
            Console.Write("Please enter the name of the person that is checking out: ");
            string personName = Console.ReadLine();
            Console.WriteLine();
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

        }
        static void ContactTracingReport(List<Person> personList)
        {
            Console.Write("Please enter a Starting Date/Time (DD/MM/YYYY hh:mm:ss): ");
            DateTime startingCheckTime = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Please enter a Ending Date/Time (DD/MM/YYYY hh:mm:ss): ");
            DateTime endingCheckTime = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Please enter a Business Name: ");
            string businessName = Console.ReadLine();
            List<Person> CheckedInList = new List<Person>();
            //foreach(Person p in personList)
            //{
            //    foreach(SafeEntry se in p.SafeEntryList)
            //    {
            //        if (se.Location.BusinessName == businessName && se.CheckIn > startingCheckTime && se.CheckOut<endingCheckTime)
            //        {
            //            CheckedInList.Add(p);
            //        }
            //    }
            //}
            using (StreamWriter sw = new StreamWriter("Contact_Tracing_Report.csv", false))
            {
                sw.WriteLine("Check In Date/Time, Check Out Date/Time, Location");
                foreach (Person p in personList)
                {
                    foreach (SafeEntry se in p.SafeEntryList)
                    {
                        if (se.Location.BusinessName == businessName && se.CheckIn >= startingCheckTime && se.CheckOut <= endingCheckTime)
                        {
                            CheckedInList.Add(p);
                            string data = Convert.ToString(se.CheckIn) + "," + Convert.ToString(se.CheckOut) + "," + se.Location.BusinessName;
                            sw.WriteLine(data);
                        }
                    }
                }
            }
        }
        // End of methods coded by:
        // Student Number : S10203166
        // Student Name : Marc Lim Liang Kiat

    }
}
