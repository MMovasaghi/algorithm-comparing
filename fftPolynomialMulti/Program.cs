using System;
using System.Collections.Generic;
using System.Numerics;

namespace fftPolynomialMulti
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            List<Complex> a1 = new List<Complex>();
            List<Complex> a2 = new List<Complex>();
            int tmp;
            Console.Write("A1 : ");
            for (int i = 1; i <= 4; i++)
            {
                tmp = rand.Next(1, 10);
                Console.Write(tmp + " ");
                a1.Add(new Complex(tmp, 0));
            }
            Console.Write("\nA2 : ");
            for (int i = 1; i <= 4; i++)
            {
                tmp = rand.Next(1, 10);
                Console.Write(tmp + " ");
                a2.Add(new Complex(tmp, 0));
            }
            List<Complex> b = Multiplication.FFT(a1);
            List<Complex> c = Multiplication.FFT(a2);
            List<Complex> result = new List<Complex>();
            for (int i = 0; i < c.Count; i++)
            {
                result.Add(Complex.Multiply(b[i], c[i]));
            }
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
                Console.WriteLine(result[i]);
        }
    }
}
