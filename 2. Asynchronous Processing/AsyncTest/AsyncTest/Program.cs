namespace AsyncDemo
{
    public class Program
    {
        public static void Main()
        {
            PrintNumbersInRange(0, 100);

            Task task = Task.Run(() => PrintNumbersInRange(100, 200));
            Task newTask = Task.Run(() => PrintNumbersInRange(500, 600));

            Console.WriteLine("Done.");

            task.Wait();
            newTask.Wait();
        }

        static void PrintNumbersInRange(int a, int b)
        {
            for (int i = a; i <= b; i++)
            {
                Console.Write($"{i},");
            }
        }

    }
}