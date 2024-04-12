
class Program
{
    static CustomMutex mutex = new CustomMutex();
    static int sharedResource = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Enter the number of threads:");
        int numThreads = int.Parse(Console.ReadLine());

        // Запускаем задачи в пуле потоков
        for (int i = 0; i < numThreads; i++)
        {
            ThreadPool.QueueUserWorkItem(IncrementSharedResource);
        }

        // Ждем завершения всех задач
        Thread.Sleep(1000);

        // Выводим значение sharedResource
        Console.WriteLine("Final value of sharedResource: " + sharedResource);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void IncrementSharedResource(object state)
    {
        mutex.Lock();
        
        sharedResource++;
        
        mutex.Unlock();
    }
}