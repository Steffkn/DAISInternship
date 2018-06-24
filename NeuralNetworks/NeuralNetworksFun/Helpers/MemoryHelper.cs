namespace Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class MemoryHelper
    {
        public static void WriteToFile(string fileName, float[,] array, FileMode mode)
        {
            using (FileStream stream = new FileStream(fileName, mode))
            {
                using (StreamWriter writetext = new StreamWriter(stream))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}x{1}\n", array.GetLength(0), array.GetLength(1));

                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        for (int y = 0; y < array.GetLength(1) - 1; y++)
                        {
                            sb.Append($"{array[i, y]} ");
                        }

                        sb.Append($"{array[i, array.GetLength(1) - 1]}");
                        sb.AppendLine();
                    }

                    writetext.Write(sb.ToString());
                }
            }
        }

        public static float[][,] ReadFromFile(string fileName)
        {
            var result = new List<float[,]>();

            string[] fileInput = File.ReadAllLines(fileName);

            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < fileInput.Length; i++)
            {
                if (fileInput[i].Contains("x"))
                {
                    string[] sizes = fileInput[i].Split(new char[] { 'x' }, System.StringSplitOptions.RemoveEmptyEntries);
                    int x = int.Parse(sizes[0]);
                    int y = int.Parse(sizes[1]);

                    for (int j = 0; j < x; j++)
                    {
                        i++;
                        sb.AppendLine(fileInput[i]);
                    }

                    result.Add(ArrayHelper.GetArray(x, y, sb.ToString()));
                    sb.Clear();
                }
            }

            return result.ToArray();

        }
    }
}
