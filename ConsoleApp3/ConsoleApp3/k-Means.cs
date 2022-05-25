using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class k_Means
    {
        public static void kMeans(double[][] data, int numClusters, int maxCount)
        {
            try
            {
                int numAttributes = data[0].Length;
                Console.WriteLine("\nk = " + numClusters + " and maxCount = " + maxCount);
                int[] clustering = Cluster(data, numClusters, numAttributes, maxCount);
                
                Vector.Print(clustering);
                Console.WriteLine("Clustered data:");
                ShowClustering(data, numClusters, clustering);               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ShowClustering(double[][] rawData, int numClusters, int[] clustering)
        {
            for (int k = 0; k < numClusters; ++k) // Каждый кластер
            {
                for (int i = 0; i < rawData.Length; ++i) // Каждая последовательность
                    if (clustering[i] == k)
                    {
                        for (int j = 0; j < rawData[i].Length; ++j)
                            Console.Write(rawData[i][j].ToString("F1") + " ");
                        Console.WriteLine("");
                    }
                Console.WriteLine("");
            }
        }

        static void UpdateMeans(double[][] rawData, int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < rawData.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
                for (int j = 0; j < rawData[i].Length; ++j)
                    means[cluster][j] += rawData[i][j];
            }
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k]; // опасность
            return;
        }

        static double[][] Allocate(int numClusters, int numAttributes)
        {
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numAttributes];
            return result;
        }

        static double[] ComputeCentroid(double[][] rawData, int[] clustering, int cluster, double[][] means)
        {
            int numAttributes = means[0].Length;
            double[] centroid = new double[numAttributes];
            double minDist = double.MaxValue;
            for (int i = 0; i < rawData.Length; ++i) // Перебираем каждую последовательность данных
            {
                int c = clustering[i];
                if (c != cluster) continue;
                double currDist = Distance(rawData[i], means[cluster]);
                if (currDist < minDist)
                {
                    minDist = currDist;
                    for (int j = 0; j < centroid.Length; ++j)
                        centroid[j] = rawData[i][j];
                }
            }
            return centroid;
        }

        static void UpdateCentroids(double[][] rawData, int[] clustering, double[][] means, double[][] centroids)
        {
            for (int k = 0; k < centroids.Length; ++k)
            {
                double[] centroid = ComputeCentroid(rawData, clustering, k, means);
                centroids[k] = centroid;
            }
        }

        static double Distance(double[] tuple, double[] vector)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - vector[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        static bool Assign(double[][] rawData, int[] clustering, double[][] centroids)
        {
            int numClusters = centroids.Length;
            bool changed = false;
            double[] distances = new double[numClusters];
            for (int i = 0; i < rawData.Length; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(rawData[i], centroids[k]);
                int newCluster = MinIndex(distances);
                if (newCluster != clustering[i])
                {
                    changed = true;
                    clustering[i] = newCluster;
                }
            }
            return changed;
        }

        static int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k]; indexOfMin = k;
                }
            }
            return indexOfMin;
        }

        static int[] Cluster(double[][] rawData, int numClusters, int numAttributes, int maxCount)
        {
            bool changed = true;
            int ct = 0;
            int numTuples = rawData.Length;
            int[] clustering = InitClustering(numTuples, numClusters, 0);
            double[][] means = Allocate(numClusters, numAttributes);
            double[][] centroids = Allocate(numClusters, numAttributes);
            UpdateMeans(rawData, clustering, means);
            UpdateCentroids(rawData, clustering, means, centroids);
            while (changed == true && ct < maxCount)
            {
                ++ct;
                changed = Assign(rawData, clustering, centroids);
                UpdateMeans(rawData, clustering, means);
                UpdateCentroids(rawData, clustering, means, centroids);
            }
            return clustering;
        }

        static int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i)
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters);
            return clustering;
        }
    }
}
