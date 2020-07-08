using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

//Listens on UDP-Port to catch messages send to this PC.

public class UDPServer : MonoBehaviour
{
    public int serverPort = 23222;
    public string lastReceived = "";
    public string lastSender = "";

    void Start()
    {
        lastReceived = "";
        lastSender = "";
        Debug.Log($"Starting receiver...");
        Init();
    }
    public void Init()
    {
        new Thread(delegate () { Receiver(serverPort, this); }).Start();
    }
    public void ReceivedMessageParse(string message, string ip)
    {
        lastReceived = message;
        lastSender = ip;
    }
    public static void Receiver(int port, UDPServer server)
    {
        
        UdpClient UdpClient = new UdpClient(port);
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        Debug.Log("Receiver started successfully");
        Debug.Log($"Listening on port {port}");
        try
        {
            byte[] receiveBytes = UdpClient.Receive(ref RemoteIpEndPoint);
            string receivedMessage = Encoding.ASCII.GetString(receiveBytes);
            server.ReceivedMessageParse(receivedMessage, RemoteIpEndPoint.ToString());
            UdpClient.Close();
            server.Init();
        }
        catch (Exception e)
        {
            Debug.Log($"ERROR ON INCOMING STRING: {e.ToString()}");
            UdpClient.Close();
            server.Init();
        }
    }
}
