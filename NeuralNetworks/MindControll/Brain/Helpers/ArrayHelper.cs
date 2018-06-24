using System.Linq;

namespace Brain.Helpers
{
    public class ArrayHelper
    {
        public static float[] GetRow(float[,] array, int row)
        {
            float[] result = new float[array.GetLength(1)];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = array[row, i];
            }

            return result;
        }

        public static float[,] GetArray(int x, int y, string input)
        {
            var result = new float[x, y];
            float[] inputs = input
                .Split(new char[] { '\n', ' ' }, System.StringSplitOptions.RemoveEmptyEntries)
                .Select(float.Parse)
                .ToArray();
            int index = 0;

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = inputs[index];
                    index++;
                }
            }

            return result;
        }
    }
}
