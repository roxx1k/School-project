using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Calculator
{
    public class ParallelCalculator
    {
        int pocetVlaken = 1;
        decimal sum = 0;
        List<int> numbers;
        List<Thread> listOfThreads;
        List<int> listMocnin, stopList, startList;
        List<decimal> listNasobku;
        public enum Operace { Suma,Mocniny,NasobeniCislem}

        public ParallelCalculator(List<int> list)
        {
            this.numbers = list;            
            this.listMocnin = new List<int>();
            this.stopList = new List<int>();
            this.startList = new List<int>();            
        }
        public void SetThreadCount(object pocet)
        {
            this.pocetVlaken = (int)pocet;            
            
            /*
            #region 
            int zbytek = numbers.Count % listOfThreads.Count;
            int segment = numbers.Count / listOfThreads.Count;

            for (int i = 0; i < listOfThreads.Count; i++)
            {
                //kazdemu vlaknu urcim odkud kam bude zpracovavat
                if (i == 0) { startList.Add(0);}
                else 
                {                    
                    startList.Add(stopList[i-1] + 1);
                    Console.WriteLine("{0}", stopList[i-1]);
                }
                stopList.Add(startList[i] + (segment -1));
                

                //podminky pro spravne rozdeleni pole na zhruba stejne velke casti
                if (i < zbytek)
                {
                    if (i == 0)
                    {
                        stopList[i] += 1;
                    }
                    else
                    {
                        stopList[i] += 1 + i;
                        startList[i] += i;
                    }
                }
                else
                {
                    stopList[i] += zbytek;
                    startList[i] += zbytek;
                }
                //Console.WriteLine("start {0}, stop {1}", startList[i], stopList[i]);
            }
            #endregion
            */            
        }
        public void ChooseOperation(string operace)
        {
            this.listOfThreads = new List<Thread>();

            //osetreni spatneho poctu vlaken ifem
            if (pocetVlaken <= 0) pocetVlaken = 1;

            for (int i = 0; i < pocetVlaken; i++)
            {
                if (operace == "mocniny") 
                {
                    Thread t = new Thread(Mocniny);
                    Console.WriteLine("{0} - thread number {1}", i, t.ManagedThreadId);
                    listOfThreads.Add(t);
                }
                else if (operace == "nasobeni") 
                {
                    Thread t = new Thread(NasobeniCislem);
                    Console.WriteLine("{0} - thread number {1}", i, t.ManagedThreadId);
                    listOfThreads.Add(t);
                }
                else 
                {
                    Console.WriteLine("spatna operace");
                }
            }
            for (int i = 0; i < listOfThreads.Count; i++)
            {
                listOfThreads[i].Start(i);

            }
        }
        public void WaitForFinishedThreads()
        {
            foreach (Thread t in listOfThreads)
            {
                //Console.WriteLine("join thread {0}", t.ManagedThreadId);
                t.Join();
            }
        }
        public decimal Sum(List<int> nums,object startingPos)
        {
            int start = (int)startingPos;
            int stop = numbers.Count;
            
            for (int i = start; i < stop; i += pocetVlaken)
            {
                sum += numbers[i];
            }
            
            return sum;
        }
        public void NasobeniCislem(object startingPos)
        {
            int nasobek = 15;
            this.listNasobku = new List<decimal>();
            int start = (int)startingPos;
            int stop = numbers.Count;                        
            
            for (int i = start; i < stop; i += pocetVlaken)
            {
                MyDelegate myDel = x => x * (int)nasobek;
                int vysledek = myDel(numbers[i]);
                lock (listNasobku) listNasobku.Add(vysledek);
                Console.WriteLine("cislo: {0} \tvynasobeno: {1}", numbers[i], vysledek);
                Thread.Sleep(1);
            }
        }
        public void Mocniny(object startingPos)
        {
            int start = (int)startingPos;
            int stop = numbers.Count;

            ////////////////
            for (int i = start; i < stop; i += pocetVlaken )
            {
                MyDelegate myDel = x => x * x;
                int mocnina = myDel(numbers[i]);
                //int mocnina = (numbers[i]*numbers[i]);                
                lock (listMocnin) listMocnin.Add(mocnina);
                Console.WriteLine("cislo {0} \tmocnina {1}", numbers[i], mocnina);
                Thread.Sleep(1);
            }
            
            //////////////
            //for (int i = 0; i < startList.Count ; i++)
            //{                  
            //    for (int j = startList[i]; j <= stopList[i]; j++)
            //    {
                    
            //        MyDelegate myDel = x => x * x;
            //        int mocnina = myDel(numbers[j]);
            //        lock (listMocnin) listMocnin.Add(mocnina);    
            //        //lock(tmpMocniny)tmpMocniny.Add(myDel(numbers[j]));
            //        //lock (listMocnin) listMocnin.Add(tmpMocniny[j]);
            //        /*foreach (Thread t in listOfThreads)
            //        {
            //            Console.WriteLine("vlakno: {0} \t cislo {1}", t.ManagedThreadId, numbers[j] * numbers[j]);
            //        }*/
            //        Console.WriteLine("cislo {0}, mocnina {1}", numbers[j], listMocnin[j]);
            //        Thread.Sleep(1);                        
            //    }
            //}
            
            
        }
        public void VypisMocnin()
        {
            WaitForFinishedThreads();
            foreach (int mocnina in listMocnin)
            {
                //vypis z listu mocnin
                Console.WriteLine("mocnina {0}", mocnina);
            }

        }
    }
}
