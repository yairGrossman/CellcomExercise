using System;
using System.Threading.Tasks;
using System.IO.Ports;
using CellcomServer;

public class Program
{
    private static SerialPort _serialPort;
    private static bool sendCellcom = false; // Flag to control the message processing loop

    public static async Task Main()
    {
        _serialPort = new SerialPort("COM1", 9600);
         _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        _serialPort.Open();

        Console.ReadLine();
    }

    private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        string inData = sp.ReadExisting();
        Task.Run(() => ProcessMessage(inData));
    }

    private static async Task ProcessMessage(string message)
    {
        if (message.Equals("JOIN"))
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"{i}");
            }
            _serialPort.Write("DONE");
        }
        else if (message.Equals("NEW"))
        {
            sendCellcom = true;
            while (sendCellcom)
            {
                _serialPort.Write("CELLCOM");
                await Task.Delay(1000);
            }
        }
        else if (message.Equals("STOP"))
        {
            sendCellcom = false;
            _serialPort.Write("BYE");
        }
    }
}
