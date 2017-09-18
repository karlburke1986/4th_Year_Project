using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.IO.Ports;
using System.IO;

namespace DeveiceToCloudMessages
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUrl = "ITTWeather.azure-devices.net";
        static string deviceKey = "eAYnRjy4Q7SJaLWEFKj/gCUB91Jrcguw7O6YGF8X2Yk=";

        private static void SendDeviceMessages(string cPort)
        {
            SerialPort port = new SerialPort();
            port.BaudRate = 9600;
            port.PortName = cPort;
            port.Open();            

            Console.WriteLine("To end press ESC");

            List<Weather> wList = new List<Weather>();


            do
            {
                while (!Console.KeyAvailable)
                {
                    int count = 0;

                    System.Threading.Thread.Sleep(1000);

                    while (count < 5)
                    {
                        string data1 = port.ReadLine();
                        string data2 = port.ReadLine();
                        Console.WriteLine(data1);
                        Console.WriteLine(data2);

                        Weather w = new Weather();

                        if (data2.Contains("Humidity"))
                        {
                            data2 = data2.Substring(11);

                            w.humidity = Double.Parse(data2);
                        }

                        if (data1.Contains("Tempature"))
                        {
                            data1 = data1.Substring(12);

                            w.temp = Double.Parse(data1);
                        }

                        w.dateTime = DateTime.Now;

                        wList.Add(w);

                        System.Threading.Thread.Sleep(60000);

                        count++;
                    }

                    double temp = 0.0;
                    double humidity = 0.0;

                    foreach (var item in wList)
                    {
                        temp += item.temp;
                        humidity += item.humidity;
                    }

                    Weather tempWeather = new Weather();

                    tempWeather.temp = temp / wList.Count;
                    tempWeather.humidity = humidity / wList.Count;
                    tempWeather.dateTime = DateTime.Now;
                    string temperatureWarning = "";
                    if (tempWeather.temp <= 0)
                    {
                        temperatureWarning = "true";
                    }
                    else
                    {
                        temperatureWarning = "false";
                    }

                    var WeatherData = new
                    {
                        deviceId = "myFirstDevice",
                        temperature = tempWeather.temp,
                        humidity = tempWeather.humidity,
                        timing = tempWeather.dateTime,
                        temperatureAlert = temperatureWarning
                    };

                    var messageString = JsonConvert.SerializeObject(WeatherData);
                    var message = new Message(Encoding.ASCII.GetBytes(messageString));

                    deviceClient.SendEventAsync(message);
                                                            
                    wList.Clear();                    

                    Console.WriteLine(messageString);                   

                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            port.Close();
        }


        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please Enter the com port you will be using");
                string input = "COM" + Console.ReadLine();

                try
                {
                    deviceClient = DeviceClient.Create(iotHubUrl, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey), TransportType.Mqtt);
                    SendDeviceMessages(input);
                }
                catch(ArgumentException)
                {
                    Console.WriteLine("There seems to be a problem connecting your device to the IOT hub");
                    Console.ReadLine();
                }
                                
            }
            catch(IOException)
            {
                Console.WriteLine("The port entered is invalid or busy");
                Console.ReadLine();
            }  
        }
    }
}
