
using System;


namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] marks = new double[][] 
                {
                    new []{  1.0,           2,           3,          4,           5,          
                       6,           7,           8,           9,          10       },
                    new []{ 2.73446908 ,  4.35122722  , 7.21132988 , 11.24872601 ,  9.58103444   ,
                       12.09865079 , 13.78706794 , 13.85301221,  15.29003911,  18.0998018},
                    new []{0.477041630296522, 3.74049076332734, 0.236134687082905, 7.83950773432828, 10.87222775485, 17.7559589612093, 
                        14.5838508105762, 
                        5.43233595668913, 18.0826615579811, 3.58967947940793 }
                  
                };
            double[] means = new double[marks.Length];
            
            for (int i = 0; i < marks.Length; i++)
            {
                means[i] = Vector.Mean(marks[i]);
            }
            Random rnd = new Random();
            double[] RandomValuesCreation = new double[10];
            
            for (int i = 0; i < RandomValuesCreation.Length; i++)
            {
                RandomValuesCreation[i] = (i+1) * 3 *rnd.NextDouble( );
            }
            Console.WriteLine("Рандомная генерация");
            Vector.Print(RandomValuesCreation);
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
            
            //Console.ForegroundColor = ConsoleColor.DarkBlue;
            //Console.WriteLine(meanData.Length+"  "+meanData[0].Length);
            
            Console.WriteLine();

            //Console.ReadKey();
            meanData = Matrix.MatrixTranspose(meanData);
            //double[]NewData = Matrix.MatrixCreate(1, MeanDATA[0].Length);
            double []newData = Matrix.Dot(meanData,  eigenvectors[2]);
            //IList<double[]> eigv = Matrix.DecomposeMatrixToColumnVectors(eigenvectors);
            Vector.Print(newData);
            //eigenvectors = Matrix.MatrixTranspose(eigenvectors);
            Console.WriteLine();
            //Console.WriteLine(NewData[8]);
            
            Console.WriteLine("__________________________________________________");

            double[][] duoEigenVectors = Matrix.MatrixDuplicate(eigenvectors);
            duoEigenVectors.
           
            double[][] restoredData = Matrix.MatrixCreate(10, 3);
            //Matrix.Print(restoredData);

            double[][] Rd2 = Matrix.MatrixCreate(10, 3);
            
            Rd2 = Matrix.MatrixProduct()

            for (int i = 0; i < restoredData.Length; i++)
            {
                restoredData[i] = Vector.ScalarToVectorProduct(newData[i], eigenvectors[2]);
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

