using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgoritm
{
    class DNA
    {
        private int length;
        private float fitness = 0;
        private char[] genes;

        public DNA(int length)
        {
            this.Length = length;
            this.Genes = new char[length];

            for (int i = 0; i < length; i++)
            {
                Genes[i] = RandChar();
            }

            //Genes = "To be$ia0jndoei987".ToCharArray();
        }

        private char RandChar()
        {
            var c = StaticRandom.GetNext(63, 122);
            if (c == 63)
            {
                c = 32;
            }
            if (c == 64)
            {
                c = 46;
            }

            return (char)c;
        }

        internal void CalculateFitness(char[] target)
        {
            float score = 0.0f;
            for (int i = 0; i < this.Genes.Length; i++)
            {
                if (Genes[i] == target[i])
                {
                    score++;
                }
            }

            this.Fitness = score / target.Length;
        }

        internal void Mutate(float mutationRate)
        {
            for (int i = 0; i < this.genes.Length; i++)
            {
                var chance = StaticRandom.GetNext(0, 100)/100.0;
                if (chance < mutationRate)
                {
                    this.genes[i] = RandChar();
                }
            }
        }

        internal DNA Crossover(DNA partnerB)
        {
            var child = new DNA(this.Genes.Length);
            var midpoint = StaticRandom.GetNext(0, this.Genes.Length);

            for (int i = 0; i < midpoint; i++)
            {
                child.Genes[i] = this.Genes[i];
            }
            for (int i = midpoint; i < this.Genes.Length; i++)
            {
                child.Genes[i] = partnerB.Genes[i];
            }

            return child;
        }

        public int Length { get => length; set => length = value; }
        public float Fitness { get => fitness; set => fitness = value; }
        public char[] Genes { get => genes; set => genes = value; }
    }
}
