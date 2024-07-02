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
        private SerialPort _serialPort;
        private string _clientId;

        public Client(string portName, string clientId)
        {
            _serialPort = new SerialPort(portName, 9600);
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            _clientId = clientId;
        }

        public void Start()
        {
            _serialPort.Open();
            Task.Run(() => SendCommands());
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();
            Console.WriteLine($"{_clientId} Received: {inData}");
        }

        private async Task SendCommands()
        {
            await SendCommand($"{_clientId}JOIN");
            await Task.Delay(1000); // המתנה של שנייה
            await SendCommand($"{_clientId}NEW");
            //await Task.Delay(1000); // המתנה של שנייה
            //await SendCommand($"{_clientId}STOP");
        }

        private async Task SendCommand(string command)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Write(command);
                Console.WriteLine($"{_clientId} Sent: {command}");
            }
        }
    }
}
