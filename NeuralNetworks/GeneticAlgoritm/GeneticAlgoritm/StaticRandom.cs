using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgoritm
{
    public static class StaticRandom
    {
        static Random rand = new Random();

        public static int GetNext(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
