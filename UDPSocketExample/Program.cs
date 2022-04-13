
using System.Net;
using System.Net.Sockets;
using System.Text;

SideCli();
//SideSvr();

static void SideCli()
{
    Socket socketCli = new Socket(SocketType.Dgram, ProtocolType.Udp);

    IPAddress iPAddressCli = IPAddress.Parse("127.0.0.1");
    int portCli = 60000;
    IPEndPoint iPEndPointCli = new IPEndPoint(iPAddressCli, portCli);

    socketCli.Bind(iPEndPointCli);


    IPAddress iPAddressSvr = IPAddress.Parse("127.0.0.1");
    int portSvr = 50000;
    IPEndPoint IPEndPointSvr = new IPEndPoint(iPAddressSvr, portSvr);

    string data = "Hello from client";

    byte[] bufferToSend = Encoding.ASCII.GetBytes(data);
    socketCli.SendTo(bufferToSend, IPEndPointSvr);

    byte[] receivedBuffer = new byte[1024];

    try
    {
        socketCli.Receive(receivedBuffer);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Port number: {portSvr} is not been listening now \nEx Mesagge: {ex.Message}");
    }


    string receivedData = Encoding.ASCII.GetString(receivedBuffer);
    Console.WriteLine(receivedData);
}

static void SideSvr()
{
    Socket socketSvr = new Socket(SocketType.Dgram, ProtocolType.Udp);

    IPAddress iPAddressSvr = IPAddress.Parse("127.0.0.1");
    int portSvr = 50000;
    IPEndPoint iPEndPointSvr = new IPEndPoint(iPAddressSvr, portSvr);

    socketSvr.Bind(iPEndPointSvr);


    byte[] buffer = new byte[1024];
    EndPoint endPointCli = new IPEndPoint(IPAddress.Any, 0);
    socketSvr.ReceiveFrom(buffer, ref endPointCli);

    string receivedData = Encoding.ASCII.GetString(buffer);
    Console.WriteLine($"Data: {receivedData}");

    socketSvr.SendTo(buffer, endPointCli);

    socketSvr.Close();
}

