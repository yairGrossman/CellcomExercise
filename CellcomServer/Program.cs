using System;
using System.Threading.Tasks;
using System.IO.Ports;

namespace CellcomServer
{
    public class Program
    {
        private static SerialPort serialPort;
        private static bool sendCellcom = false; // Flag to control the message processing loop
        private const string portName = "COM1";
        public static async Task Main()
        {
            serialPort = new SerialPort(portName, 9600);// Initialize serial port with specified name and baud rate
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);// Attach event handler for data received
            serialPort.Open(); // Open the serial port for communication
            Console.ReadLine();// Wait for user input to keep the program running
        }

        // Event handler for when data is received from the serial port
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();// Read incoming data from client serial port
            Task.Run(() => ProcessMessage(inData));// Process the received message asynchronously
        }

        // Method for processing incoming messages
        private static async Task ProcessMessage(string message)
        {
            string[] messages = message.Split(',');
            string msg = messages[0];
            string clientID= messages[1];
            if (msg.Contains("JOIN"))
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.Write($"{i}");// Print numbers 1 to 10
                }
                Console.WriteLine();
                serialPort.Write($"{clientID} {portName} DONE");// Send response indicating completion
            }
            else if (msg.Contains("NEW"))
            {
                sendCellcom = true;// Set flag to true to start sending messages
                while (sendCellcom)
                {
                    serialPort.Write($"{clientID} {portName} CELLCOM");
                    await Task.Delay(1000);
                }
            }
            else if (msg.Contains("STOP"))
            {
                sendCellcom = false; // Set flag to false to stop sending messages
                serialPort.Write($"{clientID} {portName} BYE"); // Send response
            }
        }
    }
}

