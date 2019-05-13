using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDataInputOutput
{
    class DataManager
    {
        private String Location;

        public static List<string> listOfDatas = new List<string>();
        public DataManager(String Location)
        {
            this.Location = Location;
            listOfDatas.Clear();
        }

        public void readAllAddress()
        {
            using (StreamReader reader = new StreamReader(Location))
            {
                string current;
                while ((current = reader.ReadLine()) != null)
                {
                    if (current.StartsWith("") && current.EndsWith(":"))
                    {
                        listOfDatas.Add(current.Substring(0, current.Length -1));
                    }
                }
            }
        }

        public void Append(String name, String subAddress, String[] data)
        {
            Console.WriteLine("appends");
            List<String> datas = new List<string>();
            using (StreamReader reader = new StreamReader(Location))
            {
                String current;
                while ((current = reader.ReadLine()) != null)
                {
                    if (current.Equals(name + ":"))
                    {
                        datas.Add(name + ":");
                        datas.Add("\t-" + subAddress);
                        foreach (string s in data)
                            datas.Add("\t\t-" + s);
                        while ((current = reader.ReadLine()) != null)
                            if (current.StartsWith("-"))
                                break;
                    }

                    datas.Add(current);
                }
            }
            using(StreamWriter writer = new StreamWriter(Location)) { 
                foreach (String s in datas)
                {
                    writer.WriteLine(s);
                }
            }
        }
        //Is Not Contains
        public void NewAddress(String name, String subAddress, String[] data)
        {
            Console.WriteLine("New Address");
            using (StreamWriter writer = new StreamWriter(Location, true))
            {
                writer.WriteLine(name + ":");
                writer.WriteLine("\t-" + subAddress);
                foreach (string s in data)
                {
                    writer.WriteLine("\t\t-" + s);
                }
            }
        }
    }
}
