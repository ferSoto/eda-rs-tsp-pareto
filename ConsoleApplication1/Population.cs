using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA_TSP_Multiobjective
{
    class Population
    {
        public Individual[] Inhabitants { get; set; }
        public int Size { get; private set; }

        public Population() { }

        public Population(int p_size)
        {
            Inhabitants = new Individual[p_size];
            Size = p_size;
        }

        public Population(Individual[] inhabitants)
        {
            Inhabitants = inhabitants;
        }

        public void CreatePopulation(int i_size)
        {
            int[] permutation = new int[i_size];
            for (int i = 0; i < i_size; i++)
                permutation[i] = i;
            Inhabitants[0] = new Individual(permutation);
            var temp = EvalFunctions.GetFitness(Inhabitants[0]);
            Inhabitants[0].FitnessD = temp.Item1;
            Inhabitants[0].FitnessT = temp.Item2;
            Inhabitants[0].FitnessC = temp.Item3;
            Inhabitants[0].Fitness = temp.Item4;
            //Creating random permutations using Knuth algorithm.
            for(int i = 1; i < Size; i++)
            {
                permutation = ExtensionMethods.GetRandomPermutation(permutation);
                Inhabitants[i] = new Individual(permutation);
                var fitness = EvalFunctions.GetFitness(Inhabitants[i]);
                Inhabitants[i].FitnessD = fitness.Item1;
                Inhabitants[i].FitnessT = fitness.Item2;
                Inhabitants[i].FitnessC = fitness.Item3;
                Inhabitants[i].Fitness = fitness.Item4;
            }
            Sort();
        }

        /// <summary>
        /// Sort population by Fitness. Ascendent order.
        /// </summary>
        public void Sort()
        {
            Inhabitants = Inhabitants.OrderBy(i => i.Fitness).ToArray();
        }

        /// <summary>
        /// Returns a sample f the population. Sample should be smaller than population size.
        /// The sample is created with the best n Individuals.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Individual[] GetSample(int n)
        {
            Individual[] sample = new Individual[n];
            for (int i = 0; i < n; i++)
                sample[i] = Inhabitants[Size - i - 1];
            return sample;
        }
    }
}
