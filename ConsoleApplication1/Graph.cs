using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA_TSP_Multiobjective
{
    class Graph
    {
        //All matrix are (y, x) oriented.
        private static int[,] distanceKm;   //Distance in Kms.
        private static int[,] timeMin;      //Time in minutes.
        private static int[,] costGas;      //Costin gas lts.

        public Graph()
        {

        }

        public Graph(Queue<Tuple<int, int, int>> inputData, int dimension)
        {
            distanceKm  = new int[dimension, dimension];
            timeMin     = new int[dimension, dimension];
            costGas     = new int[dimension, dimension];
            for(int i = 0; i < dimension; i++)
                for(int j = 0; j < dimension && inputData.Count > 0; j++)
                {
                    if (i == j)
                        continue;
                    var data = inputData.Dequeue();
                    distanceKm[i, j] = data.Item1;
                    timeMin[i, j]    = data.Item2;
                    costGas[i, j]    = data.Item3;
                }
        }

        /// <summary>
        /// Returns the 3 cost of the given path.
        /// </summary>
        /// <param name="path">Hamiltonian cycle.</param>
        /// <returns></returns>
        public Tuple<int, int, int> GetPathCost(int[] path)
        {
            int distance = 0,
                      time = 0,
                      cost = 0;
            for(int i = 0; i < path.Length; i++)
            {
                //(y, x)
                int y = (i + 1) % path.Length;
                distance += distanceKm[path[y], path[i]];
                time += timeMin[path[y], path[i]];
                cost += costGas[path[y], path[i]];
            }
            return Tuple.Create(distance, time, cost);
        }

    }
}
