using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjektLibrary;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using Calculator;
using System.Reflection;

namespace Plugin
{
    class Program
    {
        public static decimal sum=0;
        public static int median=0;
        public static double avg = 0;
        public static List<int> numbers = new List<int>();

        static void Main(string[] args)
        {
            
            /*FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"C:\test\backup\";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.xml";

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Starting watching folder on path {0}\npress the \'q\' for exit",watcher.Path);
            while (Console.Read() != 'q') ;
            */
            Class1 c = new Class1();
            
            //c.GenerateRandomNumbers(100, 10000);
            c.GetRandomNumbers();
            numbers = c.ReturnList();
            //c.PrintNumbers();
            //c.LoadXml();
            //c.ParseXmlDocument();
            //c.CreateCopy();
            ReflexCalculator();
            ParallelCalculator pcal = new ParallelCalculator(numbers);
            //pcal.SetThreadCount(7);
            //pcal.ChooseOperation("mocniny");
            //pcal.VypisMocnin();
            //pcal.WaitForFinishedThreads();
            //SendMail("abc@vsb.cz", "honza.roxx1k@gmail.com", "statistiky xmlka");
            Console.WriteLine("Konec programu.");
            Console.ReadKey();
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
        public static void GetStatistikyForMail()
        {
            Calculator.Calculator calc = new Calculator.Calculator(numbers);
            sum = calc.Sum();
            median = calc.Median();
            avg = calc.Average();
        }
        public static void SendMail(string from,string to,string subject)
        {
            //GetStatistikyForMail();
            ReflexCalculator();
            string msg = "Results:\nsummary: "+ sum.ToString()+"\nmedian: "+median.ToString()+"\naverage: "+avg.ToString();
            MailMessage message = new MailMessage(from, to, subject, msg);
                        
            //Send the message.
            SmtpClient client = new SmtpClient("Smtp.vsb.cz");

            try
            {
                client.Send(message);                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                      ex.ToString());
            }
        }
        public static void ReflexCalculator()
        {
            
            Assembly asm = Assembly.LoadFile(@"C:\_skola\extract\AT.NET\projekt\ProjektLibrary\Calculator\bin\Debug\Calculator.dll");
            
            Type calc = asm.GetType("Calculator.Calculator");

            Object calculator = Activator.CreateInstance(calc, new object[] {numbers });
            MethodInfo mi1 = calc.GetMethod("Sum");
            MethodInfo mi2 = calc.GetMethod("Median");
            MethodInfo mi3 = calc.GetMethod("Average");

            mi1.Invoke(calculator, null);
            mi2.Invoke(calculator, null);
            mi3.Invoke(calculator, null);
            Console.WriteLine(mi1.Invoke(calculator,new object[]{}));
            Console.WriteLine(mi2.Invoke(calculator, new object[] {  }));
            Console.WriteLine(mi3.Invoke(calculator, new object[] {  }));
            
        }
    }
}
