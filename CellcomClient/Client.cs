using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellcomClient
{
    public class Client
    {
        private SerialPort serialPort;// Serial port object for communication
        private string portName;//Port name

        // Constructor for initializing the Client class
        public Client(string portName)
        {
            this.portName = portName;
            this.serialPort = new SerialPort(portName, 9600); // Initialize serial port with specified name and baud rate
            this.serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); // Attach event handler for data received
            this.serialPort.Open();  // Open the serial port for communication
        }

        // Method for sending requests asynchronously
        public async Task sendReq()
        {
            Console.WriteLine("Enter a commad:");
            Console.WriteLine("Enter 'JOIN' for joining cellcom services");
            Console.WriteLine("Enter 'NEW' for opening call to the server");
            Console.WriteLine("Enter 'STOP' for closing call");
            Console.WriteLine("Enter 'end' for client");
            Console.WriteLine();
            string input = Console.ReadLine();
            while (!input.Equals("end"))
            {
                await SendCommand(input); // Send the user command
                await Task.Delay(1000); // Delay for 1 second
                input = Console.ReadLine();
            }

            Console.ReadLine(); // השארת התוכנית פעילה להמתנה לתשובות
        }

        // Event handler for when data is received from the server serial port
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();
            Console.WriteLine($"Received: {inData}");
        }

        // Method for sending a command to the server serial port
        private async Task SendCommand(string command)
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Write(command);
                Console.WriteLine($"{this.portName} Sent: {command}");
            }
        }
    }
}
