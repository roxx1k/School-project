using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Calculator
{
    public delegate int MyDelegate(int param);
    public class Calculator
    {
        List<int> numbers;
        
        public Calculator(List<int> list)
        {
            numbers = list;
            
        }

        public decimal Sum()
        {
            decimal sum = numbers.Sum();
            return sum;
        }

        public int Median()
        {
            List<int> tmpNums = numbers;
            int count = tmpNums.Count;
            tmpNums.Sort();            
            int med;

            //podle velikosti pole vybereme ktera cast se provede
            if (count % 2 == 0)
            {
                int prvek1 = tmpNums[(count / 2) - 1];
                int prvek2 = tmpNums[(count / 2)];
                med = (prvek1 + prvek2) / 2;
            }
            else
            {
                med = tmpNums[(count / 2)];
            }            
            return med;
        }
        public double Average()
        {
            double avg = numbers.Average();            
            return avg;
        }
        public List<int> Mocniny(List<int> nums)
        {
            List<int> tmp = new List<int>();
            int i =0;
            foreach (int num in nums)
            {
                MyDelegate myDel = x => x * x; 
                tmp[i] = myDel(num);
                i++;
            }
            return tmp;
        }
        public List<int> VynasobeniCislem(List<int> nums,int cislo)
        {
            List<int> tmp = new List<int>();
            int i = 0;
            foreach (int num in nums)
            {
                MyDelegate myDel = x => cislo * x;
                tmp[i] = myDel(num);
                i++;
            }
            return tmp;
        }
    
/*
        public double Result { get; set; }

        private List<double> results;
        //private List<int> numbers;
        private int threadsCount = 1;

        public enum Operation
        {
            Minimum,
            Maximum,
            Average
        }

        private void Min(object threadsStart)
        {
            int start = (int)threadsStart;
            int end = numbers.Count;
            int min = int.MaxValue;

            for (int i = start; i < end; i += threadsCount)
            {
                if (numbers[i] < min)
                {
                    min = numbers[i];
                }
            }

            //save min to results if no threads uses it
            lock (results)
            {
                results.Add(min);
            }
        }

        private void Max(object threadsStart)
        {
            int start = (int)threadsStart;
            int end = numbers.Count;
            int max = int.MinValue;

            for (int i = start; i < end; i += threadsCount)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            //save max to results if no threads uses it
            lock (results)
            {
                results.Add(max);
            }
        }

        private void Average(object threadsStart)
        {
            int start = (int)threadsStart;
            int end = numbers.Count;
            double average = 0;
            List<double> averageList = new List<double>();

            for (int i = start; i < end; i += threadsCount)
            {
                averageList.Add(numbers[i]);
                //Thread.Sleep(1);                //wait for demonstrate power of the threads
            }

            average = averageList.Sum() / (double)numbers.Count;

            //save avg to results if no threads uses it
            lock (results)
            {
                results.Add(average);
            }
        }

        public double calculateOperation(string operation)
        {
            List<Thread> threads = new List<Thread>();
            Thread t;
            int min = int.MaxValue;
            int max = int.MinValue;

            if (operation == "Minimum")
            {
                for (int i = 0; i < threadsCount; i++)
                {
                    t = new Thread(Min);
                    t.Priority = ThreadPriority.Highest;
                    threads.Add(t);
                    t.Start(i);                    
                }

                //wait for threads are completed
                foreach (Thread th in threads)
                {
                    th.Join();
                }

                //find result from results
                foreach (int i in results)
                {
                    if (i < min)
                    {
                        min = i;
                    }
                }
                Result = min;
            }
            else if (operation == "Maximum")
            {
                for (int i = 0; i < threadsCount; i++)
                {
                    t = new Thread(Max);
                    t.Priority = ThreadPriority.Highest;
                    threads.Add(t);
                    t.Start(i);

                }

                //wait for threads are completed
                foreach (Thread th in threads)
                {
                    th.Join();
                }

                //find result from results
                foreach (int i in results)
                {
                    if (i > max)
                    {
                        max = i;
                    }
                }

                Result = max;
            }
            else if (operation == "Average")
            {
                for (int i = 0; i < threadsCount; i++)
                {
                    t = new Thread(Average);
                    t.Priority = ThreadPriority.Highest;
                    threads.Add(t);
                    t.Start(i);
                    
                }

                //wait for threads are completed
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                //find result from results
                Result = results.Sum();
            }

            return Result;
        }

        public void calculateOperation(Operation operation)
        {
            List<Thread> threads = new List<Thread>();
            Thread t;
            int min = int.MaxValue;
            int max = int.MinValue;

            if (operation == Operation.Minimum)
            {
                for (int i = 0; i < threadsCount; i++)
                {
                    t = new Thread(Min);
                    t.Priority = ThreadPriority.Highest;
                    threads.Add(t);
                    t.Start(i);
                }

                //wait for threads are completed
                foreach (Thread th in threads)
                {
                    th.Join();
                }

                //find result from results
                foreach (int i in results)
                {
                    if (i < min)
                    {
                        min = i;
                    }
                }

                Result = min;
            }
            else if (operation == Operation.Maximum)
            {
                for (int i = 0; i < threadsCount; i++)
                {
                    t = new Thread(Max);
                    t.Priority = ThreadPriority.Highest;
                    threads.Add(t);
                    t.Start(i);

                }

                //wait for threads are completed
                foreach (Thread th in threads)
                {
                    th.Join();
                }

                //find result from results
                foreach (int i in results)
                {
                    if (i > max)
                    {
                        max = i;
                    }
                }

                Result = max;
            }
            else if (operation == Operation.Average)
            {
                for (int i = 0; i < threadsCount; i++)
                {
                    t = new Thread(Average);
                    t.Priority = ThreadPriority.Highest;
                    threads.Add(t);
                    t.Start(i);

                }

                //wait for threads are completed
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                //find result from results
                Result = results.Sum();
            }
        }
 */ 
    }
}