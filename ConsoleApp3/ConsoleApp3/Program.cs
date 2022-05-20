
using System;


namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] marks = new double[][] 
                {
                    new []{1.0        ,   2.0        ,   3.0        ,   4.0        ,
          5.0        ,   6.0        ,   7.0        ,   8.0        ,
          9.0      ,  10.0              },
                    new []{ -1.03251297,   3.64527379,   7.61009982,   9.10397504,
         10.51532508,   7.9597771 ,  11.2355247 ,  17.594655  ,
         18.20544082,  17.34616815},
                    new []{-5.60401268,  10.06795406,  21.41277131,  27.59801065,
         34.92297529,  22.34955146,  34.75353587,  50.96007769,
         54.74286992,  50.66176108},
                    new []{-27.05688438,  40.49541   ,  80.12802275, 112.96285875,
        145.25985257,  85.40388321, 138.22364201, 198.6238365 ,
        216.7731859 , 201.77255413}

                };
            double[] means = new double[marks.Length];
            
            for (int i = 0; i < marks.Length; i++)
            {
                means[i] = Vector.Mean(marks[i]);
            }
            Console.WriteLine("Начальная матрица");
            Matrix.Print(marks);
            
            Console.WriteLine("Средние значения");
            Vector.Print(means);
            Console.WriteLine();
            
            var meanData = Matrix.Meaning(marks);
            Console.WriteLine("Усредненные значения");
            Matrix.Print(meanData);
            
            Console.WriteLine("Коварационная матрица");
            var cov = Matrix.MatrixCovariance(marks);
            Matrix.Print(cov);
            Console.WriteLine();

            double[] eigenvalues = new double[cov.Length];
            //Console.WriteLine(eigenvalues[1]);
            double[][] eigenvectors = Matrix.MatrixCreate(cov.Length, cov.Length);
            Matrix.Jacobi(out eigenvalues, out eigenvectors, cov);

            Console.WriteLine("Собсвтвенные значения");
            Vector.Print(eigenvalues);
            Console.WriteLine();
            
            Console.WriteLine("Собственные векторы");
            Matrix.Print(eigenvectors);
            Console.WriteLine();
            
            eigenvectors = Matrix.MatrixTranspose(eigenvectors);

            
            Console.WriteLine();

            //Console.ReadKey();
            meanData = Matrix.MatrixTranspose(meanData);
            //double[]NewData = Matrix.MatrixCreate(1, MeanDATA[0].Length);
            double []newData = Matrix.Dot(meanData,  eigenvectors[3]);
            //IList<double[]> eigv = Matrix.DecomposeMatrixToColumnVectors(eigenvectors);
            Vector.Print(newData);
            //eigenvectors = Matrix.MatrixTranspose(eigenvectors);
            Console.WriteLine();
            //Console.WriteLine(NewData[8]);
            
            //Console.WriteLine("__________________________________________________");

            double[][] duoEigenVectors = Matrix.MatrixDuplicate(eigenvectors);
            //duoEigenVectors.
           
            double[][] restoredData = Matrix.MatrixCreate(10, 3);
            //Matrix.Print(restoredData);

            double[][] Rd2 = Matrix.MatrixCreate(10, 3);
            
            //Rd2 = Matrix.MatrixProduct()

            for (int i = 0; i < restoredData.Length; i++)
            {
                restoredData[i] = Vector.ScalarToVectorProduct(newData[i], eigenvectors[3]);
                //Vector.Print(restoredData[i]);
                for (int j = 0; j < means.Length; j++)
                {
                    restoredData[i][j] += means[j];
                }
                //Vector.Print(restoredData[i]);
                //Console.WriteLine();
            }
            Console.WriteLine("Востановленные данные");
            Console.WriteLine("");
            Matrix.Print(restoredData);
          
            marks = Matrix.MatrixTranspose((marks));

            //Matrix.Print(restoredData);
            Matrix.Print(marks);
   
            Console.ReadKey();

        }

    }

} 

