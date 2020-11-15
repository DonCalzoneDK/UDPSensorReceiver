using System;
using System.ComponentModel.Design.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace UDPSensorReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            double co = 0;
            double nox = 0;
            string level = " ";
            int number = 0;
            double sumCO = 0, sumNOx = 0;

            //UdpClient
            //When defining receiver you have to mention port number here
            //you have to define the port number so it knows where to read from
            UdpClient udpBroadcastReceiver = new UdpClient(1111);

            //Any indicates that the server must listen for client activity on 
            //all network interfaces
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            
            Console.WriteLine("Receive is block until received message from Host device");


            try
            {
                while (true)
                {
                    Byte[] receiveBytes = udpBroadcastReceiver.Receive(ref iPEndPoint);
                    //convert bytes into a message (string)
                    string receiveData = Encoding.ASCII.GetString(receiveBytes);
                    
                    if(receiveData.Equals("stop".ToLower()))
                        throw new Exception("Receiver Stopped!");

                    Console.WriteLine("Sender: " + receiveData.ToString());


                    string[] textLines = receiveData.Split("\n");
                    //Console.WriteLine(number);
                    for (int index = 0; index < textLines.Length; index++ )
                        Console.WriteLine(textLines[index]);
                    Console.WriteLine();


                    string[] CoText = textLines[3].Split(':');
                    string CoValue = CoText[1];
                    string[] NoText = textLines[4].Split(':');
                    string NoValue = NoText[1];
                    string[] list3 = textLines[5].Split(':');
                    string text3 = list3[1];
                    string[] num = textLines[7].Split(':');
                    //string number = num[1];



                    co = Double.Parse(CoValue.Trim());
                    nox = Int32.Parse(NoValue.Trim());
                    level = text3;
                    sumCO = sumCO + co;
                    sumNOx = sumNOx + nox;

                    number++;
                    Thread.Sleep(2000);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine("Average CO: " + sumCO / number);
                Console.WriteLine("Average NOx: " + sumNOx / number);
                Console.ReadKey();
                
            }

            
        }
    }
}

