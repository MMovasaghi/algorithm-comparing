using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace fftPolynomialMulti
{
    class Program
    {
        static void Main(string[] args)
        {
            const int LENGTH = 999000;
            Random rand = new Random();
            double[] a1 = new double[LENGTH];
            double[] a2 = new double[LENGTH];
            for (int i = 0; i < LENGTH; i++)
            {
                a1[i] = rand.Next(1, 100);
                a2[i] = rand.Next(1, 100);
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //FFT -------------------------------------------------

            Complex[] r1 = Multiplication.SequentialFFT(a1);
            Complex[] r2 = Multiplication.SequentialFFT(a2);
            Complex[] r3 = new Complex[LENGTH];
            for (int i = 0; i < r2.Length; i++)
            {
                r3[i] = Complex.Multiply(r1[i], r2[i]);
            }
            Complex[] R = Multiplication.SequentialIFFT(r3);

            //------------------------------------------------------

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}", ts.Milliseconds / 10);
            Console.WriteLine("RunTime Of Sequential DFT (ms): " + elapsedTime);


            stopWatch = new Stopwatch();
            stopWatch.Start();

            //FFT -------------------------------------------------

            Complex[] r11 = new Complex[LENGTH]; 
            Multiplication.ParallelFFT(a1, (int)Math.Log(Environment.ProcessorCount, 2) + 4, r11);
            Complex[] r22 = new Complex[LENGTH]; 
            Multiplication.ParallelFFT(a2, (int)Math.Log(Environment.ProcessorCount, 2) + 4, r22);
            Complex[] r33 = new Complex[LENGTH];
            for (int i = 0; i < r2.Length; i++)
            {
                r33[i] = Complex.Multiply(r11[i], r22[i]);
            }
            Complex[] RR = new Complex[LENGTH]; 
            Multiplication.ParallelIFFT(r33, (int)Math.Log(Environment.ProcessorCount, 2) + 4, RR);

            //------------------------------------------------------

            stopWatch.Stop();
            ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            elapsedTime = String.Format("{0:00}", ts.Milliseconds / 10);
            Console.WriteLine("RunTime Of Parallel DFT (ms): " + elapsedTime);
        }
    }
}
