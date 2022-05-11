using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        //public IList<double[]> _eigenVectors = null;



        static void Main(string[] args)
        {

            double[][] Marks = new double[][]
                {
                   new double[]{ 1.0,           2.0,           3.0,           4.0,           5.0, 6.0,           7.0,           8.0,           9.0, 10},
                   new double[]{ 2.73446908 ,  4.35122722 ,  7.21132988,  11.24872601,   9.58103444 ,12.09865079 , 13.78706794 , 13.85301221 , 15.29003911 , 18.0998018 }
                  
                };

            double Xmean = Vector.Mean(Marks[0]);
            double Ymean = Vector.Mean(Marks[1]);

            Console.WriteLine(Xmean+"   "+Ymean);
            Console.WriteLine(  );
            Console.WriteLine("Начальная матрица");
            Matrix.Print(Marks);

            Console.WriteLine();
            Console.WriteLine("Коварационная матрица");
            var cov = Matrix.MatrixCovariance(Marks);
            Matrix.Print(cov);

            Console.WriteLine();
            //Console.WriteLine("Собственные значения");

            double[] eigenvalues = new double[cov.Length];
            Console.WriteLine(eigenvalues[1]);
            double[][] eigenvectors = Matrix.MatrixCreate(cov.Length, cov.Length);
            Matrix.Jacobi(out eigenvalues, out eigenvectors, cov);

            Console.WriteLine("Собсвтвенные значения");
                Vector.Print(eigenvalues);
            Console.WriteLine();
            Console.WriteLine("Собственные векторы");
            //eigenvectors = Matrix.MatrixProduct(eigenvectors, -1);
                Matrix.Print(eigenvectors);
            eigenvectors = Matrix.MatrixTranspose(eigenvectors);
            Console.WriteLine();

            var MeanDATA = Matrix.Meaning(Marks);
            Console.WriteLine("Средние значения");
            Matrix.Print(MeanDATA);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(MeanDATA.Length+"  "+MeanDATA[0].Length);
            
            Console.WriteLine();

            Console.ReadKey();
            MeanDATA = Matrix.MatrixTranspose(MeanDATA);
            double[][] NewData = Matrix.MatrixCreate(1, MeanDATA[0].Length);
            NewData = Matrix.Dot(MeanDATA,  eigenvectors[1]);
            IList<double[]> eigv = Matrix.DecomposeMatrixToColumnVectors(eigenvectors);
            Matrix.Print(NewData);
            //double a = NewData[8];
            //double[][] XRestored;
            //XRestored = Matrix.Dot(NewData, eigv[1]);
            //Console.WriteLine();
            //Matrix.Print(XRestored);   
                
            

            Console.ReadKey();

        }

    }

} 

