using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiffusionEqnCrankNicolson
{
    class Program
    {
        public static void Main(string[]args)
        {
            double dt = 0.01;
            double dx = 0.01;
            double alpha = 1;
            int N = (int)(1.0 / dx);
            int M = (int)(1.0 / dt);
            double[] D = new double[N];
            D[0] = 1;
            //Console.WriteLine("{0}{1}", N, M);

            for(int i=0;i<M;i++)
            {
                double[] U = CrankNicolson(alpha, dt, dx, D);

                string[] array = U.Select(ele => ele.ToString()).ToArray();
                string line = string.Join(",", array);
                using (StreamWriter writer = new StreamWriter("data.csv", append: true))
                {
                    writer.WriteLine(line);
                }

                // for (int j = 0; j < N; j++)
                // {
                //     // Console.Write("{0},", 100 * U[j]);
                //     //Console.WriteLine(";\t");
                // }

                for (int k = 1; k < N; k++)
                {
                    D[k] = U[k];
                }
                // Console.WriteLine(";\t");
            }
            //Console.WriteLine(";\t");
            Console.ReadKey();
        }
        public static double[] CrankNicolson(double alpha, double dt, double dx, double[] D)
        {
            
            double r = alpha * dt / (2.0 * dx * dx);
            int N = (int)(1.0 / dx);
            double[] A = new double[N], B = new double[N], C = new double[N], U = new double[N];

            B[0] = B[N - 1] = 1.0 + 2.0 * r;

            for (int i = 1; i < N - 1; i++)
            {
                A[i] = -r;
                B[i] = 1.0 + 2.0 * r;
                C[i] = -r;
            }

            for (int k = 1; k < N; k++)
            {
                if (B[k - 1] == 0)
                    return null;

                double m = A[k] / B[k - 1];

                B[k] -= m * C[k - 1];
                D[k] -= m * D[k - 1];
            }

            if (B[N - 1] == 0)
                return null;

            U[N - 1] = D[N - 1] / B[N - 1];

            for (int k = N - 2; k >= 0; k--)
                U[k] = (D[k] - C[k] * U[k + 1]) / B[k];

            return U;
        }
    }
}
