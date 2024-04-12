namespace Lab3Tests;

[TestFixture]
public class CustomMutexTests
{
    [Test]
    public void Mutex_LockAndUnlock_Success()
    {
        CustomMutex mutex = new CustomMutex();

        mutex.Lock();
        Assert.IsTrue(IsMutexLocked(mutex));

        mutex.Unlock();
        Assert.IsFalse(IsMutexLocked(mutex));
    }
    

    [Test]
    public void TestUnlockWithoutLock()
    {
        CustomMutex mutex = new CustomMutex();
        
        mutex.Unlock();
    }

    [Test]
    public void TestMultipleThreads()
    {
        CustomMutex mutex = new CustomMutex();
        int counter = 0;

        Thread thread1 = new Thread(() =>
        {
            mutex.Lock();
            Thread.Sleep(100);
            counter++;
            mutex.Unlock();
        });

        Thread thread2 = new Thread(() =>
        {
            mutex.Lock();
            Thread.Sleep(100);
            counter++;
            mutex.Unlock();
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
        
        Assert.AreEqual(2, counter);
    }
    private bool IsMutexLocked(CustomMutex mutex)
    {
        var lockedField = typeof(CustomMutex).GetField("locked", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (int)lockedField.GetValue(mutex) == 1;
    }
}

[TestFixture]
public class PoseHandleTests
{
    [Test]
    public void AccessDisposedHandleThrowsException()
    {
        var handle = new PoseHandle(IntPtr.Zero);
        
        handle.Dispose();
        
        TestDelegate testDelegate = () => { var test = handle.Handle; };
        Assert.Throws<ObjectDisposedException>(testDelegate);
    }
}
