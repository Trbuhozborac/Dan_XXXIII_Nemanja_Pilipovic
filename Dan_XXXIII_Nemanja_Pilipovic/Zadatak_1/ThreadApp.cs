using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Zadatak_1
{
    class ThreadApp
    {
        #region Functions

        /// <summary>
        /// Get the names for the threads and call method for creating threads
        /// </summary>
        public static void Start()
        {
            List<string> threads = CreateThreadNames();
            CreateThreads(threads);
        }

        /// <summary>
        /// Gets the names for the threads
        /// </summary>
        /// <returns>List of names for threads</returns>
        private static List<string> CreateThreadNames()
        {
            List<string> names = new List<string>();
            string thread = "";
            for (int i = 1; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    thread = "THREAD_" + i + i;
                }
                else
                {
                    thread = "THREAD_" + i;
                }
                names.Add(thread);
            }
            return names;
        }

        /// <summary>
        /// Create threads and call methods for each of the threads
        /// </summary>
        /// <param name="threads">List of names for threads</param>
        private static void CreateThreads(List<string> threads)
        {
            Stopwatch s = new Stopwatch();

            Thread t1 = new Thread(() => FirstThread());
            t1.Name = threads[0];
            Console.WriteLine("{0} created", t1.Name);
            s.Start();
            Thread t2 = new Thread(() => SecondThread());
            t2.Name = threads[1];
            Console.WriteLine("{0} created", t2.Name);

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            s.Stop();
            TimeSpan time = s.Elapsed;
            Console.WriteLine("Time for complete first two threads: {0} ms", time.Milliseconds);


            Thread t3 = new Thread(() => ThirdThread());
            t3.Name = threads[2];
            Console.WriteLine("{0} created", t3.Name);

            Thread t4 = new Thread(() => FourthThread());
            t4.Name = threads[3];
            Console.WriteLine("{0} created", t4.Name);

            t3.Start();
            t4.Start();

        }

        /// <summary>
        /// Method that writes matrix  to the txt file
        /// </summary>
        private static void FirstThread()
        {
            string location = @"~/../../../FileByThread_1.txt";
            if (File.Exists(location))
            {

                StreamWriter sw = new StreamWriter(location);
                int[,] matrix = new int[100, 100];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    matrix[i, i] = 1;
                }
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    sw.Write("\n");
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        sw.Write(Convert.ToString(matrix[i, j]));
                    }
                }
                sw.Close();
            }
            else
            {
                Console.WriteLine("Sorry we cant find the file");
            }
        }

        /// <summary>
        /// Method that writes just odd numbers to txt file
        /// </summary>
        private static void SecondThread()
        {
            string location = @"~/../../../FileByThread_22.txt";
            StreamWriter sw = new StreamWriter(location);
            int[] numbers = GetNumbers();
            foreach (int number in numbers)
            {
                if (File.Exists(location))
                {
                    sw.WriteLine(number.ToString());
                }
                else
                {
                    Console.WriteLine("Sorry we could not find your file...");
                }
            }
            sw.Close();
        }

        /// <summary>
        /// Reads the matrix from txt file and writes it to the console
        /// </summary>
        private static void ThirdThread()
        {
            string location = @"~/../../../FileByThread_1.txt";
            if (File.Exists(location))
            {
                StreamReader sr = new StreamReader(location);
                string all = sr.ReadToEnd();
                Console.WriteLine(all);
                sr.Close();
            }
            else
            {
                Console.WriteLine("Sorry we could not find the file");
            }
        }

        /// <summary>
        /// Get the sum of all numbers from txt file
        /// </summary>
        private static void FourthThread()
        {
            string location = @"~/../../../FileByThread_22.txt";
            int sum = 0;
            if (File.Exists(location))
            {
                string[] allLines = File.ReadAllLines(location);
                foreach (string number in allLines)
                {
                    int oneNumber = Convert.ToInt32(number);
                    sum += oneNumber;
                }
                Console.WriteLine("Sum of all numbers: {0}", sum);
            }
            else
            {
                Console.WriteLine("Sorry we could not find the file");
            }
        }

        /// <summary>
        /// Gets the 1000 random odd numbers from 1 to 10000
        /// </summary>
        /// <returns></returns>
        private static int[] GetNumbers()
        {
            int[] numbers = new int[1000];
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int random = r.Next(0, 10001);
                if (random % 2 == 0)
                {
                    i--;
                    continue;
                }
                else
                {
                    numbers[i] = random;
                }
            }
            return numbers;
        }

        #endregion
    }
}
