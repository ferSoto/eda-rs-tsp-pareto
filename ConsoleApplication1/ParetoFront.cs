using EDA_TSP_Multiobjective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ParetoFront
    {
        public List<Individual> front { get; private set; }
        
        public ParetoFront()
        {
            front = new List<Individual>();
        }

        /// <summary>
        /// Returns if a solutions is dominated by the solutions of the pareto's front.
        /// </summary>
        /// <param name="inList"></param>
        /// <param name="candidate"></param>
        /// <returns></returns>
        private bool isDominated(Individual candidate)
        {
            
            foreach(Individual i in front)
            {
                if (i.FitnessC > candidate.FitnessC && i.FitnessD > candidate.FitnessD && i.FitnessT > candidate.FitnessT)
                    return true;
            }
            return false;
        }

        public void UpdateFront(Individual candidate)
        {
            if(front.Count == 0)
            {
                front.Add(candidate);
            }
            else
            {
                bool putInside = true;
                if(!isDominated(candidate))
                {
                    front.Add(candidate);
                    List<Individual> toRemove = new List<Individual>();
                    foreach(Individual i in front)
                    {
                        if(i.FitnessC < candidate.FitnessC && i.FitnessD < candidate.FitnessD && i.FitnessT < candidate.FitnessT)
                        {
                            toRemove.Add(new Individual(i));
                        }
                    }
                    foreach(Individual i in toRemove)
                    {
                        front.Remove(i);
                    }
                    front = front.Distinct().ToList();
                }
            }
        }
    }
}
