public class CustomMutex
{
    private Thread thread;
    public void Lock()
    {
        Thread t = Thread.CurrentThread;
        while (Interlocked.CompareExchange(ref thread, t, null) != null)
            Thread.Yield();
        Thread.MemoryBarrier();
    }
    public void Unlock()
    {
        Thread t = Thread.CurrentThread;
        if (Interlocked.CompareExchange(ref thread, null, t) != t)
            throw new SynchronizationLockException();
        Thread.MemoryBarrier();
    }
}