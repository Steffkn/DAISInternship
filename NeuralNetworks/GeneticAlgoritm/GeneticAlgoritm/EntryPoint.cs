namespace EntryPoint
{
    using GeneticAlgoritm;
    using System;

    public class EntryPoint
    {
        static void Main()
        {
            string phrase = "fuck you little brat";

            char[] target = phrase.ToCharArray();
            float mutationRate = 0.01f;
            int popMax = 200;

            var population = new Population(target, mutationRate, popMax);

            for (int i = 0; i < 100000; i++)
            {
                Console.ReadKey();
                population.CalculateFitness();
                population.NaturalSelection();
                population.Generate();
                population.Evaluate();
            }
        }
    }
}
