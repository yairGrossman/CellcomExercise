using System;
using System.Threading.Tasks;
using System.IO.Ports;
using CellcomServer;

public class Program
{
    // Array of COM port names to iterate over
    private static readonly string[] portNames = { "COM1", "COM3", "COM5", "COM7", "COM9" };
    public static async Task Main()
    {
        // Array to hold tasks for each serial port
        Task[] tasks = new Task[portNames.Length];
        int i = 0;
        for (i = 0; i < portNames.Length; i++)
        {
            int index = i;
            // Create a new ServerPort instance for each COM port
            tasks[i] = Task.Run(() => new ServerPort(portNames[index]));
        }
        // Wait for all tasks to complete
        await Task.WhenAll(tasks);
    }
}
