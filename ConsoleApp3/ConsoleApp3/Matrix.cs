﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Matrix
    {
        public static double[] DiagVector(double[][] a)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = a[i][i];
            }
            return result;
        }

        public static double[][] Meaning(double[][] matrix)
        {
            double[][] res = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; i++)
            {
                double mean = Vector.Mean(matrix[i]);
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    res[i][j] = matrix[i][j] - mean;
                }
            }
            return res;
        }

        public static void Jacobi(out double[] d, out double[][] v, double[][] matrix)
        {
            int nRows = matrix.Length;
            d = new double[nRows];
            v = MatrixCreate(nRows, nRows);
            double[] b = new double[nRows];
            double[] z = new double[nRows];
            for (int i = 0; i < nRows; ++i)
            {
                z[i] = 0.00;
                b[i] = d[i] = matrix[i][i];
                for (int j = 0; j < nRows; ++j)
                {
                    v[i][ j] = i == j ? 1.0 : 0.0;
                }
            }
            for (int i = 0; i < 50; ++i)
            {
                double sm = 0.0;
                for (int p = 0; p < nRows - 1; ++p)
                {
                    for (int q = p + 1; q < nRows; ++q)
                    {
                        sm += Math.Abs(matrix[p][q]);
                    }
                }
                if (sm == 0) break;
                double tresh = i < 3 ? 0.2 * sm / (nRows * nRows) : 0.00;
                for (int p = 0; p < nRows - 1; ++p)
                {
                    for (int q = p + 1; q < nRows; ++q)
                    {
                        double g = 1e12 * Math.Abs(matrix[p][q]);
                        if (i >= 3 && Math.Abs(d[p]) > g && Math.Abs(d[q]) > g) matrix[p][ q] = 0.0;
                        else
                        if (Math.Abs(matrix[p][q]) > tresh)
                        {
                            double theta = 0.5 * (d[q] - d[p]) / matrix[p][q];
                            double t = 1.0 / (Math.Abs(theta) + Math.Sqrt(1.0 + theta * theta));
                            if (theta < 0) t = -t;
                            double c = 1.0 / Math.Sqrt(1.0 + t * t);
                            double s = t * c;
                            double tau = s / (1.0 + c);
                            double h = t * matrix[p] [q];
                            z[p] -= h;
                            z[q] += h;
                            d[p] -= h;
                            d[q] += h;
                            matrix[p][q] = 0.0;
                            for (int j = 0; j < p; ++j)
                            {
                                g = matrix[j][p];
                                h = matrix[j][q];
                                matrix[j][p] = g - s * (h + g * tau);
                                matrix[j][q] = h + s * (g - h * tau);
                            }
                            for (int j = p + 1; j < q; ++j)
                            {
                                g = matrix[p][j];
                                h = matrix[j][q];
                                matrix[p][j] = g - s * (h + g * tau);
                                matrix[j][q] = h + s * (g - h * tau);
                            }
                            for (int j = q + 1; j < nRows; ++j)
                            {
                                g = matrix[p][j];
                                h = matrix[q][j];
                                matrix[p][j] = g - s * (h + g * tau);
                                matrix[q][j] = h + s * (g - h * tau);
                            }
                            for (int j = 0; j < nRows; ++j)
                            {
                                g = v[j][p];
                                h = v[j][ q];
                                v[j][ p] = g - s * (h + g * tau);
                                v[j][ q] = h + s * (g - h * tau);
                            }
                        }
                    }
                }
                for (int p = 0; p < nRows; ++p)
                {
                    d[p] = b[p] += z[p];
                    z[p] = 0.0;
                }
            }
        }
        
        public static double[][] ConvertMatrToArrArr(double[,] matr)
        {
            double[][] result = MatrixCreate(matr.GetLength(0), matr.GetLength(1));
            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = matr[i, j];
                }
            return result;
        }

        public static double[,] ConvertArrArrToMatr(double[][] matr)
        {
            double[,] result = new double[matr.Length, matr[0].Length];
            for (int i = 0; i < result.GetLength(0); i++)
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = matr[i][j];
                }
            return result;
        }

        public static int BSM(double[] eigenvalues)
        {
            int res=0;
            int n = eigenvalues.Length;
            double[] sl = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = n; j > i+1; j--)
                {
                    sl[i] = 1.0/j;
                }
                sl[i] = sl[i]/n;
            }
            sl = Vector.Reverse(sl);
            Vector.Print(sl);
            for (int i = 0; i < n; i++)
            {
                if (sl[i] < eigenvalues[i])
                    res++;
            }
            return res;
        }

        static double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
        {
            // Разложение LUP Дулитла. Предполагается,
            // что матрица квадратная.
            int n = matrix.Length; // для удобства
            double[][] result = MatrixDuplicate(matrix);
            perm = new int[n];
            for (int i = 0; i < n; ++i) { perm[i] = i; }
            toggle = 1;
            for (int j = 0; j < n - 1; ++j) // каждый столбец
            {
                double colMax = Math.Abs(result[j][j]); // Наибольшее значение в столбце j
                int pRow = j;
                for (int i = j + 1; i < n; ++i)
                {
                    if (result[i][j] > colMax)
                    {
                        colMax = result[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) // перестановка строк
                {
                    (result[pRow], result[j]) = (result[j], result[pRow]);
                    int tmp = perm[pRow]; // Меняем информацию о перестановке
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle; // переключатель перестановки строк
                }
                if (Math.Abs(result[j][j]) < 1.0E-20)
                    return null;
                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                        result[i][k] -= result[i][j] * result[j][k];
                }
            } // основной цикл по столбцу j
            return result;
        }
        public static double[] Dot(double[][] a, double[] b)
        {
            double[] dot = new double[a.Length];
            for (int i = 0; i < dot.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    dot[i] += a[i][j] * b[j];
                }
            }
            return dot;

        }

        public static double[][] MatrixTranspose(double[][] matrix)
        {
            int w = matrix.Length;
            int h = matrix[0].Length;

            double[][] result = MatrixCreate(h, w);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j][ i] = matrix[i][j];
                }
            }

            return result;
        }

        public static double[][] MatrixDuplicate(double[][] matrix)
        {
            // Предполагается, что матрица не нулевая
            double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i) // Коxxпирование значений
                for (int j = 0; j < matrix[i].Length; ++j)
                    result[i][j] = matrix[i][j];
            return result;
        }
        
        

        public static double[][] MatrixCreate(int rows, int cols)
        {
            // Создаем матрицу, полностью инициализированную
            // значениями 0.0. Проверка входных параметров опущена.
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols]; // автоинициализация в 0.0
            return result;
        }

        public static double[][] MatrixProduct(double[][] matrixA, double[][] matrixB)
        {
            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
            int bRows = matrixB.Length; int bCols = matrixB[0].Length;
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices in MatrixProduct");
            // Проверка ошибок, вычисление aRows, aCols, bCols
            double[][] result = MatrixCreate(aRows, bCols);
            Parallel.For(0, aRows, i =>
            {
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
            }
            );
            return result;
        }

        public static double[][] MatrixProduct(double[][] matrixA, double a)
        {
            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
           
            // Проверка ошибок, вычисление aRows, aCols, bCols
            double[][] result = MatrixCreate(aRows, aCols);
                for (int i = 0; i < aRows; ++i)
                    for (int j = 0; j < aCols; ++j)
                        result[i][j] = matrixA[i][j] * a ;

            return result;
        }

        public static List<double[]> DecomposeMatrixToColumnVectors(double[][] a)//Разбиение матрицы на столбцы
        {
            List<double[]> result = new List<double[]>();

            for (int i = 0; i < a.Length; i++)
            {
                result.Add(a[i]);
            }
            return result;
        }

        public static double[][] MatrixCovariance(double[][] Matr)
        {
            double[][] matrix = new double[Matr.Length][];
            for (int i = 0; i < Matr.Length; i++)
            {
                matrix[i] = new double[Matr.Length];
                for (int j = 0; j < Matr.Length; j++)
                {
                    matrix[i][j] = Vector.Covariance(Matr[i], Matr[j]);
                }
            }
            return matrix;
        }

        public static double[][] SortEigenVectors(double[] eigenvalues, double[][] eigenvectors)
        {
            double[][] res = new double[eigenvectors.Length][]; 
            double[] copyvalues = new double[eigenvalues.Length];
            for (int i = 0; i < eigenvalues.Length; i++)
            {
                copyvalues[i] = eigenvalues[i];
            }

            int[] index = new int[eigenvalues.Length];
            for (int i = 0; i < index.Length; i++)
            {
                index[i] = i;
            }

            Array.Sort(eigenvalues);
            for (int i = 0; i < eigenvalues.Length ;i++)
            {
                index[i] = Array.IndexOf(eigenvalues, copyvalues[i]);
            }

            for (int i = 0; i < eigenvectors.Length; i++)
            {
                res[i] = eigenvectors[index[i]];
            }
            Vector.Print(eigenvalues);
            Vector.Print(index);
            return res;
        }
        
        public static void Print(double[][] Matr)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < Matr.Length; i++)
            {
                for (int j = 0; j < Matr[i].Length; j++)
                {
                    Console.Write($"{Matr[i][j],5} "); //ВЫВОД матрицы  
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("__________________________");
        }
    }
}
