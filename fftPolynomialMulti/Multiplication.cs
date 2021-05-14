﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace fftPolynomialMulti
{
    public class Multiplication
    {
        List<Complex> cd;
        public Multiplication()
        {
            cd = new List<Complex>();
        }
        // Utility function for reversing the bits
        // of given index x
        static int bitReverse(int x, int log2n)
        {
            int n = 0;
            for (int i = 0; i < log2n; i++)
            {
                n <<= 1;
                n |= (x & 1);
                x >>= 1;
            }
            return n;
        }
        // Iterative FFT function to compute the DFT
        // of given coefficient vector
        static void fft(Complex[] a, Complex[] A, int log2n)
        {
            int n = 4;

            // bit reversal of the given array
            for (int i = 0; i < n; ++i)
            {
                int rev = bitReverse(i, log2n);
                A[i] = a[rev];
            }

            // j is iota
            Complex J = new Complex(0, 1);
            for (int s = 1; s <= log2n; ++s)
            {
                int m = 1 << s; // 2 power s
                int m2 = m >> 1; // m2 = m/2 -1
                Complex w = new Complex(1, 0);

                // principle root of nth complex 
                // root of unity.
                Complex wm = new Complex(Math.Cos(Math.PI / m2), Math.Sin(Math.PI / m2));
                for (int j = 0; j < m2; ++j)
                {
                    for (int k = j; k < n; k += m)
                    {

                        // t = twiddle factor
                        Complex t = w * A[k + m2];
                        Complex u = A[k];

                        // similar calculating y[k]
                        A[k] = u + t;

                        // similar calculating y[k+n/2]
                        A[k + m2] = u - t;
                    }
                    w *= wm;
                }
            }
        }
        public static List<Complex> FFT(List<Complex> array)
        {
            int n = array.Count();

            // if input contains just one element
            if (n == 1)
                return new List<Complex> { new Complex(1, 0) };

            // For storing n complex nth roots of unity
            List<Complex> w = new List<Complex>();
            for (int i = 0; i < n; i++)
            {
                double alpha = -2 * Math.PI * i / n;
                w.Add(new Complex(Math.Cos(alpha), Math.Sin(alpha)));
            }

            List<Complex> A0 = new List<Complex>(n / 2);
            List<Complex> A1 = new List<Complex>(n / 2);
            for (int i = 0; i < n / 2; i++)
            {

                // even indexed coefficients
                A0.Add(array[i * 2]);

                // odd indexed coefficients
                A1.Add(array[i * 2 + 1]);
            }

            // Recursive call for even indexed coefficients
            List<Complex> y0 = FFT(A0);

            // Recursive call for odd indexed coefficients
            List<Complex> y1 = FFT(A1);

            // for storing values of y0, y1, y2, ..., yn-1.
            Complex[] y = new Complex[n];

            for (int k = 0; k < n / 2; k++)
            {
                y[k] = y0[k] + w[k] * y1[k];
                y[k + n / 2] = y0[k] - w[k] * y1[k];
            }
            return y.ToList();
        }
    }
}