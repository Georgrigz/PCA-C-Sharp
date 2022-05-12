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

            
            double[][] marks = new double[][]
                {
                   new double[]{ 2.5, 0.5, 2.2, 1.9, 3.1, 2.3, 2.0, 1.0, 1.5, 1.1},
                   new double[]{ 2.4, 0.7, 2.9, 2.2, 3.0, 2.7, 1.6, 1.1, 1.6, 0.9 },
                    new double[]{ 2.5, 0.5, 2.2, 1.9, 3.1,2.7, 1.6, 1.1, 1.6, 0.9 }
                  
                };
            double[] Means = new double[marks.Length];
            
            for (int i = 0; i < marks.Length; i++)
            {
                Means[i] = Vector.Mean(marks[i]);
            }
            
            

            Vector.Print(Means);
            Console.WriteLine(  );
            Console.WriteLine("Начальная матрица");
            Matrix.Print(marks);

            Console.WriteLine();
            Console.WriteLine("Коварационная матрица");
            var cov = Matrix.MatrixCovariance(marks);
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

            var MeanDATA = Matrix.Meaning(marks);
            Console.WriteLine("Средние значения");
            Matrix.Print(MeanDATA);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(MeanDATA.Length+"  "+MeanDATA[0].Length);
            
            Console.WriteLine();

            Console.ReadKey();
            MeanDATA = Matrix.MatrixTranspose(MeanDATA);
            //double[]NewData = Matrix.MatrixCreate(1, MeanDATA[0].Length);
            double []NewData = Matrix.Dot(MeanDATA,  eigenvectors[1]);
            IList<double[]> eigv = Matrix.DecomposeMatrixToColumnVectors(eigenvectors);
            Vector.Print(NewData);
            Console.WriteLine();
            Console.WriteLine(NewData[8]);
            double[] Xrestored = Vector.ScalarToVectorProduct(NewData[9], eigenvectors[1]);

            for (int i = 0; i < Xrestored.Length; i++)
            {
                Xrestored[i] += Means[i];
            }

            Vector.Print(Xrestored);
            //double a = NewData[8];
            //double[][] XRestored;
            //XRestored = Matrix.Dot(NewData, eigv[1]);
            //Console.WriteLine();
            //Matrix.Print(XRestored);   
                
            

            Console.ReadKey();

        }

    }

} 

