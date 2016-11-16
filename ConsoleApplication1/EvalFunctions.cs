using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA_TSP_Multiobjective
{
    class EvalFunctions
    {
        private static int minDistance;
        private static int minTime;
        private static int minCost;

        public EvalFunctions()
        {

        }

        public EvalFunctions(int distance, int time, int cost)
        {
            minDistance = 200;
            minTime = 200;
            minCost = 200;
        }

        private static double GetDistanceFitness(int distance)
        {
            return minDistance * 10000.0 / distance;
        }

        private static double GetTimeFitness(int time)
        {
            return minTime * 10000.0 / time;
        }

        private static double GetCostFitness(int cost)
        {
            return minCost * 10000.0 / cost;
        }

        public static Tuple<double, double, double,double> GetFitness(Individual individual)
        {
            return Tuple.Create(
                GetDistanceFitness(individual.Distance),
                GetTimeFitness(individual.Time),
                GetCostFitness(individual.Cost),
                GetDistanceFitness(individual.Distance) + GetTimeFitness(individual.Time) + GetCostFitness(individual.Cost)
                );
        }
    }
}
