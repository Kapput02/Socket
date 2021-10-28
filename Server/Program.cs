using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, 8888);
            Socket serverSocket = new Socket(endpoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            //Binding
            serverSocket.Bind(endpoint);
            //Mi metto in ascolto
            serverSocket.Listen(128);
            //Accetto una connessione
            while (true)
            {


                Socket clientSocket = serverSocket.Accept();
                //Stream di comunicazione
                NetworkStream networkStream = new NetworkStream(clientSocket);
                if (networkStream.CanRead)
                {
                    while (true)
                    {
                        try
                        {
                            //Ricevo i dati inviati al client
                            byte[] bytesFormClient = new byte[10000];
                            networkStream.Read(bytesFormClient, 0, bytesFormClient.Length);
                            string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFormClient);

                            //Invio dati verso il client
                            string dataToClient = "Echo di: " + dataFromClient;
                            byte[] bytesToClient = System.Text.Encoding.ASCII.GetBytes(dataToClient);
                            networkStream.Write(bytesToClient, 0, bytesToClient.Length);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            break;
                        }
                    }
                }
            }
        }
    }
}
