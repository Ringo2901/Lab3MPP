class Program
{
    static void Main(string[] args)
    {
        IntPtr someHandle = new IntPtr(123);
        using (PoseHandle poseHandle = new PoseHandle(someHandle))
        {
            Console.WriteLine("Handle value: " + poseHandle.Handle);
        } 

        Console.WriteLine("Resources released.");
    }
}