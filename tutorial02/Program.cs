namespace tutorial02;

class Program
{
    static void Main(string[] args)
    {
        // string name = "Jakub";
        // string city = "Warsaw";
        // Console.WriteLine("Hello, " + name + " from " + city);

        int[] array = [2, 2, 2, 3, 1, 1];
        Console.WriteLine(CountAverage(array));
        Console.WriteLine(MaxValue(array));
    }

    private static double CountAverage(int[] array)
    {
        double sum = 0;
        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }
        return sum / array.Length;
    }

    private static int MaxValue(int[] array)
    {
        int max = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (max < array[i])
            {
                max = array[i];
            }
        }
        return max;
    }
    
}