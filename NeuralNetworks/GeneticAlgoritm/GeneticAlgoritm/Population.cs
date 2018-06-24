using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgoritm
{
    class Population
    {
        private char[] target;
        private float mutationRate;
        private int popMax;
        private int generations;
        private List<DNA> currentPopulation = new List<DNA>();
        private List<DNA> matingPool = new List<DNA>();

        public Population(char[] target, float mutationRate, int popMax)
        {
            this.Target = target;
            this.MutationRate = mutationRate;
            this.PopMax = popMax;

            for (int i = 0; i < popMax; i++)
            {
                CurrentPopulation.Add(new DNA(target.Length));
            }
        }

        internal void CalculateFitness()
        {
            foreach (var item in currentPopulation)
            {
                item.CalculateFitness(target);
            }
        }

        internal void Generate()
        {

            for (int i = 0; i < this.currentPopulation.Count; i++)
            {
                var a = StaticRandom.GetNext(0, this.matingPool.Count);
                var b = StaticRandom.GetNext(0, this.matingPool.Count);
                var partnerA = this.matingPool[a];
                var partnerB = this.matingPool[b];

                DNA child = partnerA.Crossover(partnerB);

                child.Mutate(this.mutationRate);

                this.currentPopulation[i] = child;
            }

            this.generations++;
        }

        internal void Evaluate()
        {
            float maxFitness = this.currentPopulation[0].Fitness;
            int index = 0;
            for (int i = 0; i < this.currentPopulation.Count; i++)
            {
                if (this.currentPopulation[i].Fitness > maxFitness)
                {
                    maxFitness = this.currentPopulation[i].Fitness;
                    index = i;
                }
            }

            Console.WriteLine(String.Join("", this.currentPopulation[index].Genes));
        }

        internal void NaturalSelection()
        {
            matingPool.Clear();

            float maxFitness = this.currentPopulation[0].Fitness;
            float minFitness = this.currentPopulation[0].Fitness;

            for (int i = 0; i < this.currentPopulation.Count; i++)
            {
                if (this.currentPopulation[i].Fitness > maxFitness)
                {
                    maxFitness = this.currentPopulation[i].Fitness;
                }
                if (this.currentPopulation[i].Fitness < minFitness)
                {
                    minFitness = this.currentPopulation[i].Fitness;
                }
            }

            for (int i = 0; i < this.currentPopulation.Count; i++)
            {
                float fitness = 0;
                if (minFitness == maxFitness)
                {
                    fitness = 1.0f / this.currentPopulation[i].Genes.Length;
                }
                else
                {
                    fitness = (this.currentPopulation[i].Fitness - minFitness) / (maxFitness - minFitness);
                }

                int n = (int)Math.Ceiling(fitness * 100);

                for (int j = 0; j < n; j++)
                {
                    this.matingPool.Add(this.currentPopulation[i]);
                }
            }
        }

        public char[] Target { get => target; set => target = value; }
        public float MutationRate { get => mutationRate; set => mutationRate = value; }
        public int PopMax { get => popMax; set => popMax = value; }
        internal List<DNA> CurrentPopulation { get => currentPopulation; set => currentPopulation = value; }
    }
}
