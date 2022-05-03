using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class DimensionalityReductionPCA
    {
        private IList<double[]> _eigenVectors = null;
        internal DimensionalityReductionPCA(double[][] dataSet, double accuracyQR, int maxIterationQR, int componentsNumber)
        {

            double[][] cov = Matrix.MatrixCovariance(dataSet);

            //<double[][]> eigen = Matrix.QRIteationBasic(dataSet, maxIterationQR);
            IList<double[]> eigenVectors =Matrix.DecomposeMatrixToColumnVectors(eigen[0]);

            if (componentsNumber > eigenVectors.Count)
            {
                throw new ArgumentException("componentsNumber > eigenVectors.Count");
            }

            _eigenVectors = new List<double[]>();
            for (int i = 0; i < componentsNumber; i++)
            {
                _eigenVectors.Add(eigenVectors[i]);
            }

        }

        public double[] Transform(double[] dataItem)
        {
            if (_eigenVectors[0].Length != dataItem.Length)
            {
                throw new ArgumentException("_eigenVectors[0].Length != dataItem.Length");
            }
            double[] res = new double[_eigenVectors.Count];
            for (int i = 0; i < _eigenVectors.Count; i++)
            {
                res[i] = 0;
                for (int j = 0; j < dataItem.Length; j++)
                {
                    res[i] += _eigenVectors[i][j] * dataItem[j];
                }
            }
            return res;
        }

        public double[] Reconstruct(double[] transformedDataItem)
        {
            if (_eigenVectors.Count != transformedDataItem.Length)
            {
                throw new ArgumentException("_eigenVectors.Count != transformedDataItem.Length");
            }
            double[] res = new double[_eigenVectors[0].Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = 0;
                for (int j = 0; j < _eigenVectors.Count; j++)
                {
                    res[i] += _eigenVectors[j][i] * transformedDataItem[j];
                }
            }
            return res;
        }
    }
}
