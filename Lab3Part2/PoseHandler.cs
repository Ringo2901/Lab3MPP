using System;
using System.Runtime.InteropServices;

public class PoseHandle : IDisposable
{
    private IntPtr handle = IntPtr.Zero;
    private bool disposed = false;

    public PoseHandle(IntPtr initialHandle)
    {
        handle = initialHandle;
    }

    public IntPtr Handle
    {
        get { return handle; }
        set { handle = value; }
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (handle != IntPtr.Zero)
            {
                CloseHandle(handle);
                handle = IntPtr.Zero;
            }

            disposed = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~PoseHandle()
    {
        Dispose(false);
    }
    
    public void ReleaseHandle()
    {
        Dispose();
    }
    
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);
}