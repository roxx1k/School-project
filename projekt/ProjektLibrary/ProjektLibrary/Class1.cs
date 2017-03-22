using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace ProjektLibrary
{
    public delegate void WritingFinishedEventHandler();
    

    public class Class1
    {
        public event WritingFinishedEventHandler WritingFinished;
        XmlDocument doc;
        XmlNode root;
        Random r;
        string path,copyPath;
        public List<int> numbers;

        public Class1()
        {
            doc = new XmlDocument();
            root = doc.CreateNode(XmlNodeType.Element,"numbers",null);
            path = @"C:\test\numbers.xml";
            copyPath = @"C:\test\numbersCopy.xml";
            r = new Random();
            numbers = new List<int>();
        }

        public void GenerateRandomNumbers(int count, int maxValue)
        {
            root = doc.CreateNode(XmlNodeType.Element, "databaze", null);
            
            for (int i = 0; i < count; i++)
            {
                XmlNode cislo = doc.CreateNode(XmlNodeType.Element, "cislo", null);               
                cislo.InnerText = r.Next(maxValue).ToString();

                root.AppendChild(cislo);
                doc.AppendChild(root);
                doc.Save(path);                
            }
            this.WritingFinished += CreateCopy;
            WritingFinished.Invoke();

        }
        public void CreateCopy()
        {
            Console.WriteLine("Creating of copy...");
            File.Copy(path,copyPath);
            Console.WriteLine("Copy created");
        }
        public void GetRandomNumbers() 
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(this.path);
            XmlNode root = xml.SelectSingleNode("//databaze");

            foreach (XmlNode node in root.ChildNodes)
            {
                numbers.Add(Int32.Parse(node.InnerText));
            }
        }
        public void PrintNumbers()
        {
            foreach ( int num in numbers)
            {
                Console.WriteLine(num);
            }
        }
        public void LoadXml()
        {            
            XmlDocument xml = new XmlDocument();
            xml.Load(this.path);
            xml.SelectSingleNode("databaze");
            XmlNodeList author = xml.GetElementsByTagName("cislo");
        }
        public List<int> ReturnList()
        {
            return this.numbers;
        }
        public void ParseXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNodeList cislo = xml.GetElementsByTagName("cislo");
            List<int> cisla = new List<int>();

            foreach (XmlElement cis in cislo)
            {
                cisla.Add(Int32.Parse(cis.InnerText));
                Console.WriteLine("Cislo: {0}",  cis.InnerText);
                //Console.WriteLine();
            }
            cisla.Sort();
            foreach (int s in cisla)
            {
                Console.WriteLine("Sorted: {0}",s);
            }


        }
    }
}
