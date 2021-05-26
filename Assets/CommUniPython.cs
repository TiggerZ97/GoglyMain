using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System;

public class CommUniPython : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 receivedPos = Vector3.zero;
    public double[] data_ESP= new double[3];
    public double[] data_Wii= new double[5];


    bool running;


    private void Start()
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
    }

    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
        listener.Stop();
    }

    void SendAndReceiveData()
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string

        if (dataReceived != null)
        {
            //---Using received data--
            //Debug.Log(dataReceived.GetType().ToString());
            double[] data_temp = StringToDouble(dataReceived);
            if(data_temp.Length==3)
            {
                Debug.Log("Recieved bpm, spo2 and state data succesfully!");
                //Debug.Log(data_temp);
                data_ESP= data_temp;
            }
            else if(data_temp.Length==5)
            {
                Debug.Log("Recieved weights");
                //Debug.Log(data_temp);
                data_Wii = data_temp;
            }
            else
            {
                Debug.Log("Invalid array");
            }
            

            //---Sending Data to Host----
            //byte[] myWriteBuffer = Encoding.ASCII.GetBytes("R"); //Converting string to byte data
            //nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
        }
    }

    double[] StringToDouble(string data)
    {
        double[] value = Array.ConvertAll(data.Split(','), s => double.Parse(s));
        return value;
    }
    /*
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    */
}