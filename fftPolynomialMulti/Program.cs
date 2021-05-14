using System;
using System.Collections.Generic;
using System.Numerics;

namespace fftPolynomialMulti
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Complex> a = new List<Complex>();
            for (int i = 0; i < 4; i++)
            {
                a.Add(new Complex(i+1, 0));
            }
            List<Complex> b = Multiplication.FFT(a);
            for (int i = 0; i < 4; i++)
                Console.WriteLine(b[i]);
        }
    }
}
