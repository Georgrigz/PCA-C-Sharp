
using System;


namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] marks = new double[][]
                {
                   new []{ 2.5, 0.5, 2.2, 1.9, 3.1, 2.3, 2.0, 1.0, 1.5, 1.1},
                   new []{ 2.4, 0.7, 2.9, 2.2, 3.0, 2.7, 1.6, 1.1, 1.6, 0.9 }
                //new double[]{ 2.5, 0.5, 2.2, 1.9, 3.1,2.7, 1.6, 1.1, 1.6, 0.9 }
                  
                };
            double[] means = new double[marks.Length];
            
            for (int i = 0; i < marks.Length; i++)
            {
                means[i] = Vector.Mean(marks[i]);
            }
            

            Console.WriteLine(  );
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
            
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(meanData.Length+"  "+meanData[0].Length);
            
            Console.WriteLine();

            Console.ReadKey();
            meanData = Matrix.MatrixTranspose(meanData);
            //double[]NewData = Matrix.MatrixCreate(1, MeanDATA[0].Length);
            double []newData = Matrix.Dot(meanData,  eigenvectors[1]);
            //IList<double[]> eigv = Matrix.DecomposeMatrixToColumnVectors(eigenvectors);
            Vector.Print(newData);
            Console.WriteLine();
            //Console.WriteLine(NewData[8]);
            
            Console.WriteLine("__________________________________________________");
            
            double[] Xrestored = Vector.ScalarToVectorProduct(newData[2], eigenvectors[1]);

            for (int i = 0; i < Xrestored.Length; i++)
            {
                Xrestored[i] += means[i];
            }

            Vector.Print(Xrestored);
            Console.WriteLine();

            double[][] restoredData = Matrix.MatrixCreate(10, 2);
            Matrix.Print(restoredData);
            
            for (int i = 0; i < restoredData.Length; i++)
            {
                restoredData[i] = Vector.ScalarToVectorProduct(newData[i], eigenvectors[1]);
                //Vector.Print(restoredData[i]);
                for (int j = 0; j < means.Length; j++)
                {
                    restoredData[i][j] += means[j];
                }
                //Vector.Print(restoredData[i]);
                //Console.WriteLine();
            }
            
            Console.WriteLine("");
            Console.WriteLine("__________________________________________________");
            Matrix.Print(restoredData);
            //double[] Xrestored = Vector.ScalarToVectorProduct(NewData[9], eigenvectors[1]);
            restoredData = Matrix.MatrixTranspose((restoredData));
            Console.WriteLine("__________________________________________________");
            Matrix.Print(restoredData);
            Matrix.Print(marks);
  

            //Vector.Print(Xrestored);
            //double a = NewData[8];
            //double[][] XRestored;
            //XRestored = Matrix.Dot(NewData, eigv[1]);
            //Console.WriteLine();
            //Matrix.Print(XRestored);   
                
            

            Console.ReadKey();

        }

    }

} 

