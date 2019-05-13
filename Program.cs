using System;
using System.Collections.Generic;
using System.IO;

namespace FileDataInputOutput
{
    class FileData
    {
        private readonly string LOC = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FileD.dat";

        private readonly DataManager _dataManager;
        /*
         * initialization
         */
        public FileData()
        {
            if(!File.Exists(LOC)) File.Create(LOC).Close();
            _dataManager = new DataManager(LOC);
        }

        public DataManager GetManager()
        {
            return this._dataManager;
        }
        public void Write(string name, string subName, string[] data)
        {
            _dataManager.ReadAllAddress();
            _dataManager.ReadAllSubs();
            if (!IsIn(name, _dataManager.ListOfDatas))
                _dataManager.NewAddress(name, subName, data);
            else _dataManager.Append(name, subName, data);
        }

        private bool IsIn(string name, IReadOnlyCollection<string> Data)
        {
            if (Data == null) return false;
            foreach (var s in Data)
            {
                if (s.Equals(name)) return true;
            }

            return false;
        }
        public string[] ReadLine(string name, string SubAddress)
        {
            return _dataManager.ReadLine(name, SubAddress);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var data = new FileData();
            data.Write("Hello", "Bye", new string[3]{"Test", "World", "Hello"});

            data.Write("Hell", "Bye", new string[3] { "Bye", "World", "Hello" });

            data.Write("Hell", "asg", new string[3] { "BA", "World", "Hello" });
            data.Write("Hell", "asg", new string[3] {"BA", "SA", "AD"});

            data.Write("Hello", "Bye", new string[4] {"Test", "Read", "fin", "Cute"});

            data.Write("Hello", "Bye2", new string[3] { "Test", "World", "Hello" });
            data.Write("Hello", "Bye2", new string[0]);

        }
    }
}
