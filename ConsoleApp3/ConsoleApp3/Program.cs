
using System;
using System.Runtime.InteropServices;


namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*double[][] marks = new double[4][] 
                {
                    new []{ 1.0        ,  2.0        ,  3.0        ,  4.0       ,  5.0        ,
         6.0        ,  7.0        ,  8.0        ,  9.0        , 10.0         },
                    new []{ 2.73446908  , 4.35122722 ,  7.21132988 , 11.24872601 ,  9.58103444,
  12.09865079 , 13.78706794 , 13.85301221  ,15.29003911  ,18.0998018},
                    new []{11.01792737, 12.38619263, 11.48804931, 24.10099224, 30.21117481,
        32.83617975, 42.4237342 , 51.14366228, 47.1998583 , 60.19540684},
                    new []{98.47615116,  20.46282367,  67.17093415,  77.66304739,
        142.25100662, 127.60618803, 199.82062948, 110.97707692,
        218.21847896, 269.12150528}

                };*/

            Random rnd = new Random();

            double[][] marks = Matrix.MatrixCreate(4, 100);
            for (int i = 0; i < marks.Length; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    marks[i][j] = (i + 1)* 1.0 * rnd.Next(0, 50);
                }
            }

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
            var cov = Matrix.MatrixCovariance(meanData);
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
            //Vector.Print(eigenvectors[]);
            Console.WriteLine();
            
            
            //
            
            eigenvectors = Matrix.SortEigenVectors(eigenvalues, eigenvectors);
            eigenvectors = Matrix.MatrixTranspose(eigenvectors);
            Console.WriteLine("BSM");
            Matrix.BSM(eigenvalues);

            Matrix.Print(eigenvectors);

            Console.WriteLine();

            double[][] Ev2 = new double[2][];
            Ev2[0] = eigenvectors[2];
            Ev2[1] = eigenvectors[3];

            double[][] Ev3 = new double[3][];
            Ev3[0] = eigenvectors[1];
            Ev3[1] = eigenvectors[2];
            Ev3[2] = eigenvectors[3];

            Ev2 = Matrix.MatrixTranspose(Ev2);
            Ev3 = Matrix.MatrixTranspose(Ev3);
            //Console.ReadKey();
            meanData = Matrix.MatrixTranspose(meanData);
            //double[]NewData = Matrix.MatrixCreate(1, MeanDATA[0].Length);
            double []newData = Matrix.Dot(meanData,  eigenvectors[3]);
            
            //IList<double[]> eigv = Matrix.DecomposeMatrixToColumnVectors(eigenvectors);
            
            Console.WriteLine("Один вектор");
            Vector.Print(newData);
            //eigenvectors = Matrix.MatrixTranspose(eigenvectors);
            Console.WriteLine();
            //Console.WriteLine(NewData[8]);
            
            //Console.WriteLine("__________________________________________________");

           // double[][] duoEigenVectors = Matrix.MatrixDuplicate(eigenvectors);
            //duoEigenVectors.
           
            double[][] restoredData = Matrix.MatrixCreate(10, 4);
            //Matrix.Print(restoredData);
            for (int i = 0; i < restoredData.Length; i++)
            {
                restoredData[i] = Vector.ScalarToVectorProduct(newData[i], eigenvectors[3]);
                for (int j = 0; j < means.Length; j++)
                {
                    restoredData[i][j] += means[j];
                }

            }
            
            Console.WriteLine("");
            Console.WriteLine("Один вектор");
            
            Matrix.Print(restoredData);
            
            double[][] newData2 = Matrix.MatrixProduct(meanData, Ev2);
            Console.WriteLine("Два вектора");
            Matrix.Print(newData2);
            
            
            double[][] restoredData2 = Matrix.MatrixCreate(10, 4);

            Ev2 = Matrix.MatrixTranspose(Ev2);
            Matrix.Print(Ev2);

            restoredData2 = Matrix.MatrixProduct(newData2, Ev2);
            for (int i = 0; i < restoredData2.Length; i++)
            {
                for (int j = 0; j < means.Length; j++)
                {
                    restoredData2[i][j] += means[j];
                }
            }
            

            Console.WriteLine("Два вектора");
            Matrix.Print(restoredData2);
            //double[] Xrestored = Vector.ScalarToVectorProduct(NewData[9], eigenvectors[1]);
            marks = Matrix.MatrixTranspose((marks));

            Console.WriteLine("Три вектора");

            double[][] newData3 = Matrix.MatrixProduct(meanData, Ev3);
            Matrix.Print(newData3);

            Ev3 = Matrix.MatrixTranspose(Ev3);
            double[][] restoredData3 = Matrix.MatrixCreate(10, 4);
            restoredData3 = Matrix.MatrixProduct(newData3, Ev3);
            for (int i = 0; i < restoredData3.Length; i++)
            {
                for (int j = 0; j < means.Length; j++)
                {
                    restoredData3[i][j] += means[j];
                }
            }
            
            Console.WriteLine("Три вектора");
            Matrix.Print(restoredData3);
            restoredData = Matrix.MatrixTranspose(restoredData);



            //for (int i = 0; i < marks.Length; i++)
            //{
            //    means[i] = Vector.Mean(marks[i]);
            //}
            
            Console.WriteLine("####################3");
            Matrix.Print(marks);
            k_Means.kMeans(marks, 4, 30);
            k_Means.kMeans(restoredData, 4, 30);
            k_Means.kMeans(restoredData2, 4, 30);
            k_Means.kMeans(restoredData3, 4,30);
            Console.ReadKey();

        }

    }

} 

