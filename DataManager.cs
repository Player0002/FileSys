using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileDataInputOutput
{
    class DataManager
    {
        private String Location;

        public List<string> listOfDatas = new List<string>();
        public Dictionary<string, List<string>> listOfSubData = new Dictionary<string, List<string>>();
        public DataManager(String Location)
        {
            this.Location = Location;
        }

        public void readAllSubs()
        {
            listOfSubData.Clear();
            foreach (string name in listOfDatas)
            {
                //Console.WriteLine(name);
                using (StreamReader reader = new StreamReader(Location))
                {
                    string current;

                    while ((current = reader.ReadLine()) != null)
                    {
                        List<string> imsi = new List<string>();
                        if (current.Equals(name + ":"))
                        {
                            while ((current = reader.ReadLine()) != null)
                            {
                                if (current.StartsWith("") && current.EndsWith(":")) break;
                                if (current.StartsWith("\t-"))
                                {
                                    imsi.Add(current.Replace("\t-", ""));
                                }
                            }

                            listOfSubData.Add(name, imsi);
                            break;
                        }

                    }

                }
            }
        }
        public void readAllAddress()
        {
            listOfDatas.Clear();
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

        public void MainAppend(String name, String subAddress, String[] data)
        {
            //Console.WriteLine("MainAppend");
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
                        {
                            if (current.StartsWith("-") || current.StartsWith(""))
                                break;
                        }
                    }

                    datas.Add(current);
                }
            }
            using (StreamWriter writer = new StreamWriter(Location))
            {
                foreach (String s in datas)
                {
                    writer.WriteLine(s);
                }
            }
        }

        public void SubAppend(string name, string subAddress, string[] data)
        {
            Console.WriteLine("Sub Append");
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
                        while ((current = reader.ReadLine()) != null) {
                            if (current.Equals("\t-" + subAddress))
                            {
                                while((current = reader.ReadLine()) != null)
                                    if (current.StartsWith("\t-") || (current.StartsWith("") && current.EndsWith(":")))
                                        break;
                                break;
                            }
                            datas.Add(current);
                            if (current.StartsWith("") && current.EndsWith(":"))
                                break;
                        }
                    }
                    datas.Add(current);
                }
            }
            using (StreamWriter writer = new StreamWriter(Location))
            {
                foreach (String s in datas)
                {
                    writer.WriteLine(s);
                }
            }
        }
        public void Append(String name, String subAddress, String[] data)
        {
            //Console.WriteLine("appends / isIn? : " + isIn(name, listOfSubData[name]));
            if(!isIn(subAddress, listOfSubData[name])) MainAppend(name, subAddress, data);
            else SubAppend(name, subAddress, data);
        }

        private bool isIn(String name, List<String> data)
        {
            Console.WriteLine("Name : " + name);
            foreach (KeyValuePair<string, List<String>> s in listOfSubData)
            {
                Console.WriteLine("Key : " + s.Key);
                foreach(String str in s.Value) Console.WriteLine("\t" + str);
            }
            foreach (string s in data)
            {
                if (s == name) return true;
            }

            return false;
        }
        //Is Not Contains
        public void NewAddress(String name, String subAddress, String[] data)
        {
            //Console.WriteLine("New Address");
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
