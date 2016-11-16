using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA_TSP_Multiobjective
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Knuth algorithm to get a random permutation.
        /// </summary>
        /// <param name="permutation"></param>
        /// <returns></returns>
        public static int[] GetRandomPermutation(this int[] permutation)
        {
            Random rd = new Random();
            int new_position;
            int temp;
            int[] new_permutation = new int[permutation.Length];
            new_permutation = (from int i in permutation select i).ToArray();
            for (int i = 0; i < permutation.Length; i++)
            {
                new_position = i + rd.Next() % (permutation.Length - i);
                temp = new_permutation[i];
                new_permutation[i] = new_permutation[new_position];
                new_permutation[new_position] = temp;
            }
            return new_permutation;
        }

        public static int FindValue(this int[] path, int value)
        {
            for (int i = 0; i < path.Length; i++)
                if (path[i] == value)
                    return i;
            return -1;
        }

    }
}
