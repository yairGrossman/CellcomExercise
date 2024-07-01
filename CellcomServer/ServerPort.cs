using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellcomServer
{
    public class ServerPort
    {
        private SerialPort serialPort;// Serial port object for communication
        private string portName;       // Name of the serial port
        private bool sendCellcom = false; // Flag to control the message processing loop

        public ServerPort(string portName)
        {
            
            this.portName = portName;
            this.serialPort = new SerialPort(portName, 9600);// Initialize serial port with specified name and baud rate
            this.serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);// Attach event handler for data received
            this.serialPort.Open(); // Open the serial port for communication
            Console.ReadLine();// Wait for user input to keep the program running
        }

        // Event handler for when data is received from the serial port
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();// Read incoming data from client serial port
            Task.Run(() => ProcessMessage(inData));// Process the received message asynchronously
        }

        // Method for processing incoming messages
        private async Task ProcessMessage(string message)
        {
            if (message.Contains("JOIN"))
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.Write($"{i}");// Print numbers 1 to 10
                }
                Console.WriteLine();
                this.serialPort.Write($"{this.portName} DONE");// Send response indicating completion
            }
            else if (message.Contains("NEW"))
            {
                sendCellcom = true;// Set flag to true to start sending messages
                while (sendCellcom)
                {
                    this.serialPort.Write($"{this.portName} CELLCOM");
                    await Task.Delay(1000);
                }
            }
            else if (message.Contains("STOP"))
            {
                sendCellcom = false; // Set flag to false to stop sending messages
                this.serialPort.Write($"{this.portName} BYE"); // Send response
            }
        }
    }
}
