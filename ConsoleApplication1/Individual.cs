using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA_TSP_Multiobjective
{
    class Individual
    {
        private Graph myChart;
        public int[] Path { get; private set; }
        public int Distance { get; private set; }
        public int Time { get; private set; }
        public int Cost { get; private set; }
        public double FitnessD { get; set; }
        public double FitnessT { get; set; }
        public double FitnessC { get; set; }
        public double Fitness { get; set; }     //Additive function.

        public Individual(int[] path)
        {
            myChart = new Graph();
            //Setting path.
            Path = path;
            //Getting Cost.
            var pathCost = myChart.GetPathCost(Path);
            Distance = pathCost.Item1;
            Time = pathCost.Item2;
            Cost = pathCost.Item3;
        }

        public Individual(Individual individual)
        {
            Path = individual.Path;
            Distance = individual.Distance;
            Time = individual.Time;
            Cost = individual.Cost;
            Fitness = individual.Fitness;
            FitnessC = individual.FitnessC;
            FitnessD = individual.FitnessD;
            FitnessT = individual.FitnessT;
        }

        public override string ToString()
        {
            string toPrint = "|";
            foreach (int node in Path)
                toPrint += (node.ToString() + "|");
            toPrint += Environment.NewLine;
            toPrint += "Distance (Km): " + Distance.ToString() + Environment.NewLine;
            toPrint += "Time (Minutes): " + Time.ToString() + Environment.NewLine;
            toPrint += "Cost (Gas): " + Cost.ToString() + Environment.NewLine + Environment.NewLine;
            return toPrint;
        }
    }
}
