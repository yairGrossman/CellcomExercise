using CellcomClient;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        //if no COM port argument is provided
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a COM port as a command-line argument.");
            return;
        }
         
        string portName = args[0];// Get the COM port name from command-line arguments
        Client client = new Client(portName);// Create a new Client object with the specified COM port
        await client.sendReq();// Call the asynchronous method to start sending requests
    }
}
