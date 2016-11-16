using EDA_TSP_Multiobjective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ReadingData
            int cities;
            cities = Convert.ToInt32(Console.ReadLine());
            int arcs = cities * cities - cities;
            Queue<Tuple<int, int, int>> inputData = new Queue<Tuple<int, int, int>>();
            for (int i = 0; i < arcs; i++)
            {
                //Very bad practice. Bad Fernando, bad.
                inputData.Enqueue(Tuple.Create(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine())));
            }
            #endregion
            //Creating Graph
            EvalFunctions nue = new EvalFunctions(200, 200, 200);
            Graph chart = new Graph(inputData, cities);
            Random rd = new Random();
            ParetoFront frontier = new ParetoFront();
            int eda_proportion = (int)(0.6 * cities * 4);
            int elitism_proportion = (int)(0.1 * cities * 4);
            int random_proportion = elitism_proportion;
            int sa_proportion = (int)(0.2 * cities * 4);
            Population current_population, next_population;
            current_population = new Population(cities * 4);
            current_population.CreatePopulation(cities);

            for (int generations = 0; generations < 100; generations++)
            {
                next_population = new Population(cities * 4);
                EDA new_distribution;
                SimulatedAnnealing new_process;

                //Estimating new distribution;
                new_distribution = new EDA(current_population.GetSample(cities));
                //Individuals created with EDA
                for (int i = 0; i < eda_proportion; i++)
                    next_population.Inhabitants[i] = new_distribution.CreateIndividual();
                //Individuals created with Simulated Annealing.
                for (int i = 0; i < sa_proportion; i++)
                {
                    new_process = new SimulatedAnnealing(
                        current_population.Inhabitants[rd.Next() % current_population.Size]
                        );
                    new_process.Cooling(2000, 3);
                    next_population.Inhabitants[i + eda_proportion] = new_process.historical_best_solution;
                }
                //Individuals created with Knuth Algorithm.
                for (int i = 0; i < random_proportion; i++)
                {
                    int[] new_path;
                    new_path = current_population.Inhabitants[current_population.Size - i - 1].Path.GetRandomPermutation();
                    next_population.Inhabitants[i + eda_proportion + sa_proportion] = new Individual(new_path);

                    var fitness = EvalFunctions.GetFitness(next_population.Inhabitants[i + eda_proportion + sa_proportion]);
                    next_population.Inhabitants[i + eda_proportion + sa_proportion].FitnessD = fitness.Item1;
                    next_population.Inhabitants[i + eda_proportion + sa_proportion].FitnessT = fitness.Item2;
                    next_population.Inhabitants[i + eda_proportion + sa_proportion].FitnessC = fitness.Item3;
                    next_population.Inhabitants[i + eda_proportion + sa_proportion].Fitness = fitness.Item4;
                }
                //Individuals selected with elitism
                for (int i = 1; i <= elitism_proportion; i++)
                    next_population.Inhabitants[next_population.Size - i] = current_population.Inhabitants[current_population.Size - i];
                //Making next population the current population.
                current_population = next_population;
                current_population.Sort();

                //Updating Pareto's front.
                frontier.UpdateFront(current_population.Inhabitants.OrderBy(i => i.FitnessC).Last());
                frontier.UpdateFront(current_population.Inhabitants.OrderBy(i => i.FitnessD).Last());
                frontier.UpdateFront(current_population.Inhabitants.OrderBy(i => i.FitnessT).Last());
                   
            }
            foreach(Individual i in frontier.front)
            {
                Console.WriteLine("{0}\n\n", i.ToString());
            }
            Console.WriteLine();
        }
    }
}
