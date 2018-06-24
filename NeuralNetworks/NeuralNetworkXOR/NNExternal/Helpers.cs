namespace NNExternal
{
    public class Helpers
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
    }
}
