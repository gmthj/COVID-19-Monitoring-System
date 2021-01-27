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



            //BASIC FEATURES

            //1) Load Person and Business Location Data
            LoadPersonData(personDataList);
            


            //random testing
            foreach (string[] arr in personDataList)
            {
                foreach (string data in arr)
                {
                    Console.Write(data + "\t");
                }
                Console.WriteLine("");
            }
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

        //1) Load Business Location Data

    }
}
