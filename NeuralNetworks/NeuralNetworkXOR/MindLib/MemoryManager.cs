using System;
using System.IO;
using System.Text;

namespace MindLib
{
    public class MemoryManager
    {
        public static void LoadWeights(string fileName, double[][] weights)
        {
            string[] fileInput = File.ReadAllLines(fileName);

            int line = Mind.Inputs.GetLength(0) + Mind.Outputs.GetLength(0) +1;

            for (int x = 0; x < weights.GetLength(0); x++)
            {
                string[] lineText = fileInput[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                weights[x] = new double[lineText.Length];

                for (int y = 0; y < weights[x].Length; y++)
                {
                    weights[x][y] = double.Parse(lineText[y]);
                }

                line++;
            }

            Console.WriteLine("Weights loaded from file {0}", fileName);
        }

        public static void WriteToFile(string fileName, double[][] weights)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter writetext = new StreamWriter(stream))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}x{1}x{2}x{3}\n", Mind.Inputs.GetLength(0), Mind.Inputs.GetLength(1), Mind.Outputs.GetLength(0), Mind.Outputs.GetLength(1));

                    for (int i = 0; i < Mind.Inputs.GetLength(0); i++)
                    {
                        for (int y = 0; y < Mind.Inputs.GetLength(1) - 1; y++)
                        {
                            sb.Append($"{Mind.Inputs[i, y]} ");
                        }

                        sb.Append($"{Mind.Inputs[i, Mind.Inputs.GetLength(1) - 1]}");
                        sb.AppendLine();
                    }

                    for (int i = 0; i < Mind.Outputs.GetLength(0); i++)
                    {
                        for (int y = 0; y < Mind.Outputs.GetLength(1) - 1; y++)
                        {
                            sb.Append($"{Mind.Outputs[i, y]} ");
                        }

                        sb.Append($"{Mind.Outputs[i, Mind.Outputs.GetLength(1) - 1]}");
                        sb.AppendLine();
                    }

                    //for (int i = 0; i < Mind.Outputs.Length - 1; i++)
                    //{
                    //    sb.Append($"{Mind.Outputs[i]} ");
                    //}

                    //sb.Append($"{Mind.Outputs[Mind.Outputs.Length - 1]}");
                    //sb.AppendLine();


                    for (int x = 0; x < weights.GetLength(0); x++)
                    {
                        for (int y = 0; y < weights[x].GetLength(0) - 1; y++)
                        {
                            sb.Append($"{weights[x][y]} ");
                        }

                        sb.Append($"{weights[x][weights[x].Length - 1]}");
                        sb.AppendLine();
                    }

                    writetext.Write(sb.ToString());
                }
            }
        }

        public static int[,] LoadInput(string[] fileInput)
        {
            int line = 0;
            string[] lineText = fileInput[line].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
            int inputLenghtX = int.Parse(lineText[0]);
            int inputLenghtY = int.Parse(lineText[1]);

            int[,] result = new int[inputLenghtX, inputLenghtY];

            for (int i = 0; i < inputLenghtX; i++)
            {
                line++;
                lineText = fileInput[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int y = 0; y < inputLenghtY; y++)
                {
                    result[i, y] = int.Parse(lineText[y]);
                }
            }

            return result;
        }

        public static int[,] LoadOutput(string[] fileInput)
        {
            string[] lineText = fileInput[0].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);

            int inputLenghtX = int.Parse(lineText[0]);
            int outputLenghtX = int.Parse(lineText[2]);
            int outputLenghtY = int.Parse(lineText[3]);
            int line = inputLenghtX;

            int[,] result = new int[outputLenghtX, outputLenghtY];

            lineText = fileInput[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < outputLenghtX; i++)
            {
                line++;
                lineText = fileInput[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int y = 0; y < outputLenghtY; y++)
                {
                    result[i, y] = int.Parse(lineText[y]);
                }
            }

            return result;
        }
    }
}
