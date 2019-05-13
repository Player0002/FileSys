using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace FileDataInputOutput
{
    class FileData
    {
        private readonly string LOC = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FileD.dat";

        private DataManager dataManager;
        /*
         * initialization
         */
        public FileData()
        {
            if(!File.Exists(LOC)) File.Create(LOC).Close();
            dataManager = new DataManager(LOC);
        }

        public DataManager getManager()
        {
            return this.dataManager;
        }
        public void Write(String name, String subName, String[] data)
        {
            dataManager.readAllAddress();
            dataManager.readAllSubs();
            if (!isIn(name, dataManager.listOfDatas))
                dataManager.NewAddress(name, subName, data);
            else dataManager.Append(name, subName, data);
        }

        private bool isIn(string name, List<String> datas)
        {
            foreach (String s in datas)
            {
                if (s.Equals(name)) return true;
            }

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FileData data = new FileData();
            data.Write("Hello", "Bye", new string[3]{"Test", "World", "Hello"});

            data.Write("Hell", "Bye", new string[3] { "Bye", "World", "Hello" });

            data.Write("Hell", "asg", new string[3] { "BA", "World", "Hello" });
            data.Write("Hell", "asg", new string[3] {"BA", "SA", "AD"});
            //Console.WriteLine("List of ArrayList");
            foreach (String s in data.getManager().listOfDatas)
            {
                //Console.WriteLine(s);
            }
            //Console.WriteLine("SUBS");
            foreach (KeyValuePair<string, List<String>> s in data.getManager().listOfSubData)
            {
                Console.WriteLine(s.Key);
                foreach (string str in s.Value)
                {
                    Console.WriteLine("\t" + str);
                }
            }
        }
    }
}
