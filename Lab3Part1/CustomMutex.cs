public class CustomMutex
{
    private int locked = 0; 

    public void Lock()
    {
        while (true)
        {
            if (Interlocked.CompareExchange(ref locked, 1, 0) == 0)
            {
                return;
            }
            Thread.Sleep(10);
        }
    }

    public void Unlock()
    {
        Interlocked.Exchange(ref locked, 0);
    }
}