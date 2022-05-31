using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Vector
    {
        public static double Mean(double[] X) => X.Sum() / X.Length;

        public static double Spread(double[] X, double mean)
        {
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += Math.Pow(X[i] - mean, 2);
            }
            return Math.Sqrt(sum / (X.Length - 1));
        }

        public static double Variance(double[] X)
        {
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += Math.Pow(X[i], 2);
            }
            return sum / (X.Length - 1);
        }

        public static double Covariance(double[] X, double[] Y)
        {
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {              
                sum += (X[i]) * (Y[i]);
            }
            return sum / (X.Length - 1);
        }

        public static double[] VectorProjection(double[] a, double[] b) // Проекция вектора на вектор
        {
            double k = ScalarVectorProduct(a, b) / ScalarVectorProduct(b, b);
            return ScalarToVectorProduct(k, b);
        }

        public static double ScalarVectorProduct(double[] a, double[] b)// Скалярное произведение векторов
        {
            double result = 0;
            for (int i = 0; i < a.Length; ++i)
                result += a[i] * b[i];
            return result;
        }

        public static double VectorProduct(double[] a, double[] b)// Скалярное произведение векторов
        {
            double result = 0;
            for (int i = 0; i < a.Length; ++i)
                result = a[i] * b[i];
            return result;
        }

        public static double[] Dot(double[] a, double[] b)// Скалярное произведение векторов
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; ++i)
                for (int j = 0; j < b.Length; j++)
                {
                    result[i] += a[i] * b[j];
                }
            return result;
        }

        public static double[] ScalarToVectorProduct(double k, double[] a)// Произведение числа на вектор
        {
            double[] res = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                res[i] = a[i] * k;
            }
            return res;
        }

        public static double NormOfVector(double[] vector)
        {
            double sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i] * vector[i];
            }
            return Math.Sqrt(sum);
        }
        public static void Print(double[] Vector)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < Vector.Length; i++)
            {
                Console.Write($"{Vector[i],5} "); //ВЫВОД матрицы  
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("_________________________");
        }
        
        public static void Print(int[] Vector)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < Vector.Length; i++)
            {
                Console.Write($"{Vector[i],5} "); //ВЫВОД матрицы  
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("_________________________");
        }

        public static double[] Reverse(double[] a)
        {
            for (var i = 0; i < a.Length / 2; i++)
            {
                (a[i], a[a.Length - (i + 1)]) = (a[a.Length - (i + 1)], a[i]); 
            }

            return a;
        }
    }
}
