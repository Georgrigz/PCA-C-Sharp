
using System;


namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] marks = new double[][] 
                {
                    new []{ 1.0        ,  2.0        ,  3.0        ,  4.0       ,  5.0        ,
         6.0        ,  7.0        ,  8.0        ,  9.0        , 10.0         },
                    new []{ 2.88728642,  2.49420833,  2.20845789,  6.43993946, 10.78518991,
        10.87374318, 14.33891091, 16.79766407, 16.97181491, 20.85161094},
                    new []{11.01792737, 12.38619263, 11.48804931, 24.10099224, 30.21117481,
        32.83617975, 42.4237342 , 51.14366228, 47.1998583 , 60.19540684},
                    new []{98.47615116,  20.46282367,  67.17093415,  77.66304739,
        142.25100662, 127.60618803, 199.82062948, 110.97707692,
        218.21847896, 269.12150528}

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
            
            Console.WriteLine("__________________________________________________");

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
            
            Console.WriteLine("");
            Console.WriteLine("__________________________________________________");
            Matrix.Print(restoredData);
            //double[] Xrestored = Vector.ScalarToVectorProduct(NewData[9], eigenvectors[1]);
            marks = Matrix.MatrixTranspose((marks));
            Console.WriteLine("__________________________________________________");
            //Matrix.Print(restoredData);
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

