using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Premi invio per connetterti");
            Console.ReadLine();
            var endpoint = new IPEndPoint(IPAddress.Loopback, 8888);
            Socket socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(endpoint);
            NetworkStream networkStream = new NetworkStream(socket);
            Console.WriteLine("Inserisci il messaggio:");
            string dataToServer = Console.ReadLine();
            byte[] bytesToServer = System.Text.Encoding.ASCII.GetBytes(dataToServer);
            networkStream.Write(bytesToServer, 0, bytesToServer.Length);
            byte[] bytesFromServer = new byte[10000];
            networkStream.Read(bytesFromServer, 0, bytesFromServer.Length);
            string dataFromServer = System.Text.Encoding.ASCII.GetString(bytesFromServer);
            Console.WriteLine(dataFromServer);
            //Invio dati verso il client
            networkStream.Close();
            socket.Close();
            socket.Dispose();
        }
    }
}
