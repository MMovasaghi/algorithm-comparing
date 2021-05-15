using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace fftPolynomialMulti
{
    public class Multiplication
    {
        //Sequential FFT
        public static Complex[] SequentialFFT(double[] array)
        {
            int n = array.Count();

            // if input contains just one element
            if (n == 1)
                return new Complex[]{ new Complex(array[0], 0) };

            double fi = (2 * Math.PI) / n;
            var wn = new Complex(Math.Cos(fi), Math.Sin(fi));
            var w = new Complex(1, 0);

            double[] A0 = new double[n / 2];
            double[] A1 = new double[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                // even indexed coefficients
                A0[i] = array[i * 2];
                // odd indexed coefficients
                A1[i] = array[i * 2 + 1];
            }

            // Recursive call for even indexed coefficients
            Complex[] y0 = SequentialFFT(A0);

            // Recursive call for odd indexed coefficients
            Complex[] y1 = SequentialFFT(A1);

            // for storing values of y0, y1, y2, ..., yn-1.
            Complex[] y = new Complex[n];

            for (int k = 0; k < n / 2; k++)
            {
                y[k] = y0[k] + (w * y1[k]);
                y[k + (n / 2)] = y0[k] - (w * y1[k]);
                w = Complex.Multiply(w, wn);
            }
            return y;
        }

        //Sequential IFFT
        public static Complex[] SequentialIFFT(Complex[] array)
        {
            int n = array.Count();

            // if input contains just one element
            if (n == 1)
                return array;

            double fi = (-1 * (2 * Math.PI)) / n;
            var wn = new Complex(Math.Cos(fi), Math.Sin(fi));
            var w = new Complex(1, 0);

            Complex[] A0 = new Complex[n / 2];
            Complex[] A1 = new Complex[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                // even indexed coefficients
                A0[i] = array[i * 2];
                // odd indexed coefficients
                A1[i] = array[i * 2 + 1];
            }

            // Recursive call for even indexed coefficients
            Complex[] y0 = SequentialIFFT(A0);

            // Recursive call for odd indexed coefficients
            Complex[] y1 = SequentialIFFT(A1);

            // for storing values of y0, y1, y2, ..., yn-1.
            Complex[] y = new Complex[n];

            for (int k = 0; k < n / 2; k++)
            {
                y[k] = ((y0[k] + (w * y1[k])) / n);
                y[k + (n / 2)] = ((y0[k] - (w * y1[k])) / n);
                w = Complex.Multiply(w, wn);
            }
            return y;
        }
        //Parallel FFT
        public static void ParallelFFT(double[] array, int depthRemaining, Complex[] yn)
        {
            int n = array.Count();

            // if input contains just one element
            if (n == 1)
            {
                yn = new Complex[] { new Complex(array[0], 0) };
                return;
            }
                

            double fi = (2 * Math.PI) / n;
            var wn = new Complex(Math.Cos(fi), Math.Sin(fi));
            var w = new Complex(1, 0);

            double[] A0 = new double[n / 2];
            double[] A1 = new double[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                // even indexed coefficients
                A0[i] = array[i * 2];
                // odd indexed coefficients
                A1[i] = array[i * 2 + 1];
            }
            Complex[] y0 = new Complex[n / 2];
            Complex[] y1 = new Complex[n / 2];
            if (depthRemaining > 0)
            {
                Parallel.Invoke(
                    () => ParallelFFT(A0, depthRemaining - 1, y0),
                    () => ParallelFFT(A0, depthRemaining - 1, y1));
            }
            else
            {
                // Recursive call for even indexed coefficients
                ParallelFFT(A0, 0, y0);

                // Recursive call for odd indexed coefficients
                ParallelFFT(A1, 0, y1);
            }

            

            // for storing values of y0, y1, y2, ..., yn-1.
            Complex[] y = new Complex[n];

            for (int k = 0; k < n / 2; k++)
            {
                y[k] = y0[k] + (w * y1[k]);
                y[k + (n / 2)] = y0[k] - (w * y1[k]);
                w = Complex.Multiply(w, wn);
            }
            return;
        }

        //Parallel IFFT
        public static void ParallelIFFT(Complex[] array, int depthRemaining, Complex[] yn)
        {
            int n = array.Count();

            // if input contains just one element
            if (n == 1)
            {
                yn = array;
                return;
            }

            double fi = (-1 * (2 * Math.PI)) / n;
            var wn = new Complex(Math.Cos(fi), Math.Sin(fi));
            var w = new Complex(1, 0);

            Complex[] A0 = new Complex[n / 2];
            Complex[] A1 = new Complex[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                // even indexed coefficients
                A0[i] = array[i * 2];
                // odd indexed coefficients
                A1[i] = array[i * 2 + 1];
            }
            Complex[] y0 = new Complex[n / 2];
            Complex[] y1 = new Complex[n / 2];
            if (depthRemaining > 0)
            {
                Parallel.Invoke(
                    () => ParallelIFFT(A0, depthRemaining - 1, y0),
                    () => ParallelIFFT(A0, depthRemaining - 1, y1));
            }
            else
            {
                // Recursive call for even indexed coefficients
                ParallelIFFT(A0, 0, y0);

                // Recursive call for odd indexed coefficients
                ParallelIFFT(A1, 0, y1);
            }
            

            // for storing values of y0, y1, y2, ..., yn-1.
            Complex[] y = new Complex[n];

            for (int k = 0; k < n / 2; k++)
            {
                y[k] = ((y0[k] + (w * y1[k])) / n);
                y[k + (n / 2)] = ((y0[k] - (w * y1[k])) / n);
                w = Complex.Multiply(w, wn);
            }
            return;
        }
    }
}
