using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA_TSP_Multiobjective
{
    class SimulatedAnnealing
    {
        Graph myChart;
        Random rd;
        private Individual candidate;
        private Individual current_best_solution;
        public Individual historical_best_solution { get; private set; }

        public SimulatedAnnealing(Individual solution)
        {
            myChart = new Graph();
            current_best_solution = solution;
            historical_best_solution = solution;
        }

        private void swap(ref int a, ref int b)
        {
            int temp1 = a;
            int temp2 = b;
            a = temp2;
            b = temp1;
        }

        /// <summary>
        /// Creates a new solution in the neighborhood of the given solution.
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        private Individual newSolution(Individual solution)
        {
            rd = new Random();
            int[] new_path = solution.Path;
            //Creating new path in the neighborhood.
            for (int i = 0; i < new_path.Length / 10; i++)
            {
                swap(ref new_path[rd.Next() % new_path.Length], ref new_path[rd.Next() % new_path.Length]);
            }
            Individual new_solution = new Individual(new_path);
            
            //Setting fitness for new solution.
            var fitness = EvalFunctions.GetFitness(new_solution);
            new_solution.FitnessD = fitness.Item1;
            new_solution.FitnessT = fitness.Item2;
            new_solution.FitnessC = fitness.Item3;
            new_solution.Fitness = fitness.Item4;

            return new_solution;
        }

        private bool metropolisStep(double fx, double fx1, double T)
        {
            //This evaluation function was changed to use fitness as parameter 
            //instead the objective function.
            return Math.Exp((fx1 - fx) / T) > rd.NextDouble();
        }

        /// <summary>
        /// Cooling the given temperature while calculates new solutins.
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="cooling_schedule">Iterations per temperature change.</param>
        public void Cooling(double temperature, int cooling_schedule)
        {
            do
            {
                for(int i = 0; i < cooling_schedule; i++)
                {
                    //Getting a new solutin in the neighborhood of our current best solution.
                    candidate = newSolution(current_best_solution);
                    //if the new solution is better than the current_best_solution
                    //the new solution will become the current_best_solution.
                    //Otherwise, the new solution will be evaluated with the Metropolis Step.
                    //Wheter metroopolis step is true, then the new solutin will become the current best solution.
                    if (current_best_solution.Fitness <= candidate.Fitness)
                        current_best_solution = candidate;
                    else if (metropolisStep(current_best_solution.Fitness, candidate.Fitness, temperature))
                        current_best_solution = candidate;
                    //Checking if the new solution is better than the historical best solution.
                    if (current_best_solution.Fitness > historical_best_solution.Fitness)
                        historical_best_solution = current_best_solution;
                }
                //Cooling temperature
                temperature *= 0.9;
            } while (temperature > 0.1);
        }
    }
}
