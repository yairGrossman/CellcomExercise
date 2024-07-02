using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace CellcomClient
{
    public class Program
    {
        private static SerialPort serialPort;

        private const string portName = "COM2";
        private const int clientNum = 3;
        public static async Task Main()
        {
            serialPort = new SerialPort(portName, 9600); // Initialize serial port with specified name and baud rate
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); // Attach event handler for data received
            serialPort.Open();  // Open the serial port for communication

            Client[] clients = new Client[clientNum];
            Task[] tasks = new Task[clientNum];
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i] = new Client(serialPort, "Client " + i);
                int index = i;
                tasks[i] = Task.Run(async () => await clients[index].SendCommands());
            }
            await Task.WhenAll(tasks);
            Console.ReadLine();
        }

        // Event handler for when data is received from the server serial port
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();
            Console.WriteLine($"Received: {inData}");
        }

        
    }
}
