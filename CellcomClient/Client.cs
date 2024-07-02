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
        private SerialPort serialPort;
        private string clientID;
        private static readonly object taskLock = new object();

        public Client(SerialPort serialPort, string clientID) 
        { 
            this.serialPort = serialPort;
            this.clientID = clientID;
        }

        public async Task SendCommands()
        {
           await SendCommand("JOIN"); // Send the user command
           await Task.Delay(1000); // Delay for 1 second
           await SendCommand("NEW"); // Send the user command
           await Task.Delay(3000); // Delay for 1 second
           await SendCommand("STOP"); // Send the user command
         
            
        }

        // Method for sending a command to the server serial port
        private async Task SendCommand(string command)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(command + "," + clientID);
                Console.WriteLine($"{clientID} Sent: {command}");
            }
        }
    }
}
