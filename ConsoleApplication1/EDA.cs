using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA_TSP_Multiobjective
{
    class EDA
    {
        private double[,] distribution;
        private int path_size;

        public EDA(Individual[] sample)
        {
            path_size = sample.Length;
            distribution = new double[path_size, path_size];
            for(int i = 0; i < path_size; i++)
                for(int j = 0; j < path_size; j++)
                    distribution[i, sample[j].Path[i]] += 1.0 / path_size;
        }
        

        private void validateAndCorrectPath(ref int[] path)
        {
            List<Pair<int, List<int>>> appearance;
            appearance = new List<Pair<int, List<int>>>(path_size);
            //initializing list of pairs.
            for (int i = 0; i < path_size; i++)
                appearance[i] = new Pair<int, List<int>>(i, new List<int>());
            //Searching duplicated and lost nodes.
            for(int i = 0; i < path_size; i++)
                appearance[(int)path[i]].Second.Add(i);
            //Removing correct values.
            appearance.RemoveAll(p => p.Second.Count == 1);

        }

        
        public Individual CreateIndividual()
        {
            HashSet<int> inPath;
            inPath = new HashSet<int>();
            Random rd = new Random();
            int[] new_path = Enumerable.Range(0, path_size).ToArray();
            double threshold, cumulative_probability;
            
            //Creating path.
            //i -> x; j -> y
            for (int i = 0; i < path_size - 1; i++)
            {
                threshold = rd.NextDouble();
                cumulative_probability = 0;
                for (int j = 0; j < path_size; j++)
                {
                    cumulative_probability += distribution[i, j];
                    
                    if (cumulative_probability < threshold)
                        continue;
                    //If a node is already in the set, start the search for another node.
                    if (inPath.Contains(j))
                    {
                        break;
                    }
                    else
                    {
                        //If the node isn't in the set, add it.
                        //place node in the right position.
                        inPath.Add(j);
                        int position = new_path.FindValue(j);
                        int temp;
                        temp = new_path[i];
                        new_path[i] = new_path[position];
                        new_path[position] = temp;
                        break;
                    }
                }
            }

            Individual new_individual = new Individual(new_path);
            var fitness = EvalFunctions.GetFitness(new_individual);
            new_individual.FitnessD = fitness.Item1;
            new_individual.FitnessT = fitness.Item2;
            new_individual.FitnessC = fitness.Item3;
            new_individual.Fitness = fitness.Item4;

            return new_individual;
        }

        private class Pair<T, V>
        {
            public T First { get; set; }
            public V Second { get; set; }

            public Pair()
            {

            }

            public Pair(T first, V second)
            {
                First = first;
                Second = second;
            }
        };
    }
}
