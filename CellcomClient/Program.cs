using CellcomClient;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

public class Program
{
    private static SerialPort _serialPort;

    public static async Task Main()
    {
        _serialPort = new SerialPort("COM2", 9600);
        _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        _serialPort.Open();

        // דוגמאות לשליחת הודעות
        await SendCommand("JOIN");
        //await Task.Delay(2000); // המתנה של שנייה
        //await SendCommand("NEW");
        //await Task.Delay(5000); // המתנה של שנייה
        //await SendCommand("<ID>STOP");

        Console.ReadLine(); // השארת התוכנית פעילה להמתנה לתשובות
    }

    private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        string inData = sp.ReadExisting();
        Console.WriteLine($"Received: {inData}");
    }

    private static async Task SendCommand(string command)
    {
        if (_serialPort.IsOpen)
        {
            _serialPort.Write(command);
            Console.WriteLine($"Sent: {command}");
        }
    }
}
