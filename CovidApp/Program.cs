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
            
            ObtainSHNFacilityData(facilityList);
            ObtainResidentsData(serialNoList, residentList, facilityList);
            ObtainVisitorData(visitorList, facilityList);
            ObtainBusinessesData(businessList);
            InitializePersonList(personList, residentList, visitorList);

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
                    facilityList.Clear();
                    ObtainSHNFacilityData(facilityList); //this is here because to intialise personList, the TravelEntry requires there needs to be SHNFacilities to assign the person to as indicated in the person.csv file
                    ObtainResidentsData(serialNoList, residentList, facilityList);
                    ObtainVisitorData(visitorList, facilityList);
                    ObtainBusinessesData(businessList);
                    InitializePersonList(personList, residentList, visitorList);
                    Console.WriteLine("Data has been loaded.");
                }
                else if (selectedOption == 2) //Load SHN Facility Data
                {
                    facilityList.Clear();
                    ObtainSHNFacilityData(facilityList);
                    Console.WriteLine("Data has been loaded.");
                }
                else if (selectedOption == 3) //List All Visitors
                {
                    DisplayVisitorList(personList);
                }
                else if (selectedOption == 4) //List Person Details
                {
                    DisplayPersonDetails(personList);
                }

                else if (selectedOption == 5) //Assign/Replace TraceTogether Token
                {
                    UpdateToken(personList, serialNoList);
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
                    DisplaySHNFacilitiesDetails(facilityList);
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
                    Console.WriteLine("Exit.");
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
            if (facilityName != "" && facilityName != null)
            {
                te.AssignSHNFacility(SearchSHNFacility(facilityName, facilityList));
                te.ShnStay.FacilityVacancy--;
            }
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
            Console.WriteLine("==\nERROR: facility name not found in SHN facility list\n=="); //error message for testing.
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
                        facilityList.Add(new SHNFacility(sf.FacilityName, sf.FacilityCapacity, sf.DistFromAirCheckpoint, sf.DistFromSeaCheckpoint, sf.DistFromLandCheckpoint));
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

        static void DisplayPersonDetails(List<Person> personList)
        {
            Console.Write("Enter the Name of the Person: ");
            string name = Console.ReadLine();
            Person p = SearchPersonByName(personList, name);
            if (p != null)
            {
                Console.WriteLine("\nPerson Name: {0}", p.Name);
                if (p is Visitor)
                {
                    Visitor v = (Visitor)p;
                    Console.WriteLine("Person Passport Number: {0}", v.PassportNo);
                    Console.WriteLine("Person Nationality: {0}", v.Nationality);
                }
                else if (p is Resident)
                {
                    Resident r = (Resident)p;
                    Console.WriteLine("Person Address: {0}", r.Address);
                    Console.WriteLine("Person Last Left Country: {0}", r.LastLeftCountry);
                    if (r.Token != null)
                    {
                        Console.WriteLine("Person TraceTogether Token Serial Number: {0}", r.Token.SerialNo);
                        Console.WriteLine("Person TraceTogether Token Collection Location: {0}", r.Token.CollectionLocation);
                        Console.WriteLine("Person TraceTogether Token Expiry Date: {0}", r.Token.ExpiryDate);
                    }
                    else
                    {
                        Console.WriteLine("Person TraceTogether Token: {0}", "None");
                    }
                }
                int safeEntryCount = 1;
                foreach (SafeEntry se in p.SafeEntryList)
                {
                    // Student Number : S10203166
                    // Student Name : Marc Lim Liang Kiat
                    Console.WriteLine("\nSafe Entry Record Number [{0}]", safeEntryCount);
                    Console.WriteLine("Check In Date And Time: {0}", se.CheckIn);
                    Console.WriteLine("Check In Date And Time: {0}", se.CheckOut);
                    Console.WriteLine("Business Location: {0}", se.Location);
                    //end marc
                }
                
                int travelEntryCount = 1;
                foreach (TravelEntry te in p.TravelEntryList)
                {
                    Console.WriteLine("\nTravel Entry Record Number [" + travelEntryCount + "]");
                    Console.WriteLine("Last Country Of Embarkation: " + te.LastCountryOfEmbarkation);
                    Console.WriteLine("Entry Mode: " + te.EntryMode);
                    Console.WriteLine("Entry Date: " + te.EntryDate);
                    Console.WriteLine("Shn End Date: " + te.ShnEndDate);
                    if (te.ShnStay == null)
                    {
                        Console.WriteLine("SHN Facility Name: None");
                    }
                    else
                    {
                        Console.WriteLine("SHN Facility Name: " + te.ShnStay.FacilityName);
                    }
                    Console.WriteLine("Is Paid: " + te.IsPaid);
                }
            }
            else
            {
                Console.WriteLine("Could not find a person with that name. Please try again.");
            }
        }

        static void DisplaySHNFacilitiesDetails(List<SHNFacility> facilityList)
        {
            int shnFacilityCount = 1;
            foreach (SHNFacility sf in facilityList)
            {
                Console.WriteLine("\nSHN Facility Number [{0}]", shnFacilityCount);
                Console.WriteLine("Facility Name: " + sf.FacilityName);
                Console.WriteLine("Facility Capacity: " + sf.FacilityCapacity);
                Console.WriteLine("Facility Vacancy: " + sf.FacilityVacancy);
                Console.WriteLine("Distance From Air Checkpoint: " + sf.DistFromAirCheckpoint);
                Console.WriteLine("Distance From Sea Checkpoint: " + sf.DistFromSeaCheckpoint);
                Console.WriteLine("Distance From Land Checkpoint: " + sf.DistFromLandCheckpoint);
                shnFacilityCount++;
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
            while (true)
            {
                try
                {
                    Console.Write("Please enter your option: ");
                    int option = Convert.ToInt32(Console.ReadLine());
                    if (option > 16 || option < 1)
                    {
                        Console.WriteLine("Error, invalid option. Option must be integer between 1 and 16");
                        continue;
                    }
                    else
                    {
                        return option;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input string is not in the correct format, input string should be an integer. Please try again.");
                }
            }

        }
        static void ObtainResidentsData(List<string> serialNoList, List<Resident> residentList, List<SHNFacility> facilityList)
        {
            try
            {
                using (StreamReader sr = new StreamReader("csv files/Person.csv"))
                {
                    string line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] formattedLine = line.Split(",");
                        if (formattedLine[0] == "resident")
                        {
                            Resident newResident = new Resident(formattedLine[1], formattedLine[2], Convert.ToDateTime(formattedLine[3]));
                            if (formattedLine[6] != "")
                            {
                                TraceTogetherToken newToken = new TraceTogetherToken(formattedLine[6], formattedLine[7], Convert.ToDateTime(formattedLine[8]));
                                newResident.Token = newToken;
                                serialNoList.Add(newToken.SerialNo);
                            }
                            if (formattedLine[9] != "")
                            {
                                newResident.AddTravelEntry(NewTravelEntry(formattedLine[9], formattedLine[10], Convert.ToDateTime(formattedLine[11]), Convert.ToDateTime(formattedLine[12]), Convert.ToBoolean(formattedLine[13]), formattedLine[14], facilityList));
                            }
                            residentList.Add(newResident);
                        }
                    }
                }
           
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable to find File \"csv files/Person.csv\".");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error, data in csv file is not in the correct format.");
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

        static Resident SearchResidentByName(List<Person> personList, string name)
        {
            foreach(Person p in personList)
            {
                if(p is Resident)
                {
                    if (name == p.Name)
                    {
                        return (Resident)p;
                    }
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

        static void UpdateToken(List<Person> personList, List<string> serialNoList)
        {
            Console.Write("Please enter name of resident: ");
            string residentName = Console.ReadLine();
            Resident searchedResident = SearchResidentByName(personList, residentName);
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
                    Console.WriteLine("Success, new TraceTogether Token has been assigned to resident with name {0}.", residentName);
                    Console.WriteLine();
                    Console.WriteLine("Newly Assigned TraceTogether Token has the following details.");
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
                        Console.WriteLine("Success, TraceTogether Token of resident with name {0} has been replaced.", residentName);
                        Console.WriteLine();
                        Console.WriteLine("Replacement TraceTogether Token has the following details.");
                        Console.WriteLine("Serial No: {0}", searchedResident.Token.SerialNo);
                        Console.WriteLine("Collection Location: {0}", searchedResident.Token.CollectionLocation);
                        Console.WriteLine("Expiry Date: {0}", searchedResident.Token.ExpiryDate);

                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error, TraceTogether Token of resident with name {0} is not eligible for replacement.", residentName);
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Error, resident with name {0} does not exist, please try again.", residentName);
            }
        }
        static void ObtainBusinessesData(List<BusinessLocation> businessList)
        {
            try
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
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable to find File \"csv files/BusinessLocation.csv\".");
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
            try
            {
                DisplayBusinessList(businessList);
                BusinessLocation searchedLocation = SearchBusinessLocation(businessList);
                if (searchedLocation != null)
                {
                    Console.Write("Please enter new maximum capacity of business location: ");
                    int newMaxCapacity = Convert.ToInt32(Console.ReadLine());
                    searchedLocation.MaximumCapacity = newMaxCapacity;
                }
                else
                {
                    Console.WriteLine("Business Location not found");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not in the correct format, input string should be an integer. Please try again.");
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
            try
            {
                Console.Write("Please enter the name of the person that is checking in: ");
                string personName = Console.ReadLine();
                Person searchedPerson = SearchPersonByName(personList, personName);
                if (searchedPerson != null)
                {
                    DisplayBusinessList(businessList);
                    Console.Write("Please enter the Business Location Number (1, 2, 3, etc.) of the business location that is being checked in to: ");
                    int locationNumber = Convert.ToInt32(Console.ReadLine());
                    if (locationNumber > 0 && locationNumber <= businessList.Count)
                    {
                        BusinessLocation safeEntryLocation = businessList[locationNumber - 1];
                        if (safeEntryLocation.IsFull())
                        {
                            Console.WriteLine("Unable to SafeEntry Check In as business location is at maximum capacity.");
                        }
                        else
                        {
                            SafeEntry newSafeEntry = new SafeEntry(DateTime.Now, safeEntryLocation);
                            safeEntryLocation.VisitorsNow += 1;
                            searchedPerson.AddSafeEntry(newSafeEntry);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error, invalid option. Option must be integer between 1 and {0}, please try again.", businessList.Count);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Error, person with name {0} does not exist, please try again.", personName);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not in the correct format, input string should be an integer. Please try again.");
            }
        }
        static void SafeEntryCheckOut(List<Person> personList)
        {
            try
            {
                int count = 1;
                Console.Write("Please enter the name of the person that is checking out: ");
                string personName = Console.ReadLine();
                Console.WriteLine();
                Person searchedPerson = SearchPersonByName(personList, personName);
                if (searchedPerson != null)
                {
                    foreach (SafeEntry se in searchedPerson.SafeEntryList)
                    {
                        if (se.CheckOut == DateTime.MinValue)
                        {
                            Console.WriteLine("Safe Entry Record Number [{0}]", count);
                            Console.WriteLine("Check In Date And Time: {0}", se.CheckIn);
                            Console.WriteLine("Business Location: {0}", se.Location);
                            Console.WriteLine();
                            count += 1;
                        }
                    }
                    Console.Write("Please enter the SafeEntry Record Number (1, 2, 3, etc.) of the SafeEntry Record to check out of: ");
                    int chosenRecord = Convert.ToInt32(Console.ReadLine());
                    if (chosenRecord >0 && chosenRecord <= searchedPerson.SafeEntryList.Count)
                    {
                        SafeEntry chosenSafeEntry = searchedPerson.SafeEntryList[chosenRecord - 1];
                        chosenSafeEntry.PerformCheckOut();
                        chosenSafeEntry.Location.VisitorsNow -= 1;
                        Console.WriteLine("Person with name {0} has been checked out of {1}.", personName, chosenSafeEntry.Location.BusinessName);
                    }
                    else
                    {
                        Console.WriteLine("Error, invalid option. Option must be integer between 1 and {0}, please try again.", searchedPerson.SafeEntryList.Count);
                    }
                }
                else
                {
                    Console.WriteLine("Error, person with name {0} does not exist, please try again.", personName);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not in the correct format, input string should be an integer. Please try again.");
            }
        }
        static void ContactTracingReport(List<Person> personList)
        {
            try
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
                    bool exist = false;
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
                                exist = true;
                            }
                        }
                    }
                    if (exist == false)
                    {
                        Console.WriteLine("Business Location with name {0} not found", businessName);
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not in the correct date format, input string should be in format of (DD/MM/YYYY hh:mm:ss). Please try again.");
            }
        }
        // End of methods coded by:
        // Student Number : S10203166
        // Student Name : Marc Lim Liang Kiat

    }
}
