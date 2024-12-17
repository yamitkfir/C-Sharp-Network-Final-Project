using System;
using System.Windows;
//using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace ServerProject
{ 
    public class Connections
    {
        private static String hosting = "127.0.0.1";
        //private static String hosting = "172.19.131.132";
        private static String DEFAULT_LOC = "C:\\yk2020\\";
        private static Socket server;
        private static Socket client;
        private static int PORT = 10001;
        public static int num_of_connections = 0;
        private static Sqling sqling = new Sqling();

        public static void StartServer()
        {
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry(hosting); // can also apply an actual IP
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);
            
            server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // A Socket must be associated with an endpoint using the Bind method  
            server.Bind(localEndPoint);
            // how many requests a Socket can listen before it gives Server busy response.  
            server.Listen(10);
            Console.WriteLine("SERVER STARTED");
            client = server.Accept();
            num_of_connections++;
            Console.WriteLine("SERVER CONNECTED");
        }

        public static void newStartServer()
        {
            PORT = PORT + 1;

            client.Send(Encoding.ASCII.GetBytes("bye" + " <EOF>"));
            Thread.Sleep(1000);

            //server.Disconnect(true); // closes the connection. the socket can be reused afterwards
            client.Disconnect(true);
            //client.Shutdown(SocketShutdown.Both);
            Thread.Sleep(2000);

            IPHostEntry host = Dns.GetHostEntry(hosting); // can also apply an actual IP
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);
            
            server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(localEndPoint);
            server.Listen(10);
            Console.WriteLine("SERVER STARTED");
            client = server.Accept();
            num_of_connections++;
            Console.WriteLine("SERVER CONNECTED");
        }

        public static string RecieveRequest()
        {
            string send = "";
            string data = null;
            byte[] bytes = null;
            bytes = new byte[1024];
            try
            {
                while (true)
                {
                    int bytesRec = client.Receive(bytes); // now we recive info as ByteSecs instead of full strings
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                } //recieving info in bytes til the end of msg
            }
            catch (System.ObjectDisposedException e)
            {
                return "esc "+ num_of_connections.ToString();
            }
            if (data.Contains("SEND_IMG_TO_CLIENT"))
            {
                send = newSendRequestedImage(data); // the image will be sent FROM client there. it will return status of sending. //NEW
                return "";
            }
            else if (data.Contains("RECIEVE_IMG_FROM_CLIENT"))
            {
                send = UploadToServer(data); // the image will be sent TO client there. it will return status of sending.
                return "got img";
            }
            else if (data.Contains("esc2"))
            {
                send = "bye";
            }
            else // its a normal message
            {
                send = ConnectionsTat.CreateMessage(data);
            }
            if (!send.Contains("NULL"))
                try
                {
                    client.Send(Encoding.ASCII.GetBytes(send + " <EOF>"));
                }
                catch(Exception e)
                {
                    return "esc " + e.ToString();
                }
            if (send.Equals(""))
            {
                //newStartServer();
            }
            return "GOT: " + data + "SENT: " + send;
        }
        
        public static string newSendRequestedImage(string data)
        {
            string imgLoc = DEFAULT_LOC;
            byte[] bytes = new byte[1024];
            String returni = "";

            string[] splitted = data.Split(' ');
            string name;
            int family_id;

            if (data.Contains("ANIMAL"))
            {
                // ex: SEND_IMG_TO_CLIENT FAMILY_ID: 123 ANIMAL: ido
                name = splitted[4];
                family_id = int.Parse(splitted[2]);
                imgLoc = sqling.getAnimalPath(family_id, name, false);
            }
            else if (data.Contains("USER"))
            {
                // ex: SEND_IMG_TO_CLIENT FAMILY_ID: 123 USER: yamit 
                name = splitted[4];
                family_id = int.Parse(splitted[2]);
                imgLoc = sqling.getUserPath(family_id, name, false);
            }
            else if (data.Contains("FAMILY"))
            {
                // ex: SEND_IMG_TO_CLIENT FAMILY_ID: 123 
                family_id = int.Parse(splitted[2]);
                imgLoc = sqling.getFamilyPath(family_id, false);
            }

            client.Send(Encoding.ASCII.GetBytes("beseder" + " <EOF>"));
            if (imgLoc != DEFAULT_LOC)
            {
                byte[] img = null;
                byte[] data1 = new byte[1024];

                try
                {
                    Form1 imgctrl = new Form1();
                    int temp = imgctrl.ImageControl(imgLoc, client);
                    // ... copied to ImageControl so i can use BitMapImage
                    //returni = "OK";
                    Console.Out.WriteLine("image sent");
                }
                catch (Exception e)
                {
                    client.Send(Encoding.ASCII.GetBytes("NO" + " <EOF>"));
                    Console.Out.WriteLine("no such image...");
                    returni = "NO";
                }
            }
            else
            {
                returni = "231";
            }
            return returni;
        }

        // SendVarData copied to ImageControl so i can use BitMapImage

        public static string UploadToServer(string data)
        {
            Console.Out.WriteLine(data);
            string returni = "";
            byte[] bytes = null;
            string imgLoc = DEFAULT_LOC;
            byte[] data1 = new byte[1024];

            string[] splitted = data.Split(' ');
            string name;
            int family_id;
            
            if (splitted[3].Contains("USER"))
            {
                // ex: RECIEVE_IMG_FROM_CLIENT FAMILY_ID: 123 USER: yamit LENGTH: 10000
                family_id = int.Parse(splitted[2]);
                name = splitted[4];
                imgLoc = sqling.getUserPath(family_id, name, true);
            }
            else if (data.Split(' ')[3].Contains("ANIMAL"))
            {
                // ex: RECIEVE_IMG_FROM_CLIENT FAMILY_ID: 123 ANIMAL: ido  LENGTH: 10000
                family_id = int.Parse(splitted[2]);
                name = splitted[4];
                imgLoc = sqling.getAnimalPath(family_id, name, true);
            }
            else if (data.Split(' ')[3].Contains("FAMILY"))
            {
                // ex: RECIEVE_IMG_FROM_CLIENT FAMILY_ID: 123 LENGTH: 10000
                family_id = int.Parse(splitted[2]);
                imgLoc = sqling.getFamilyPath(family_id, true);
            }

            // make sure the path exists:
            string[] temp = imgLoc.Split('\\');
            string directory = "";
            for (int i=0; i<temp.Length-1; i++)
            {
                directory = directory + temp[i] + "\\";
            }
            string subdirectory = directory.Remove(directory.Length - 2);
            try
            {
                if (!Directory.Exists(subdirectory))
                {
                    Directory.CreateDirectory(subdirectory);
                }
            }
            catch
            {
                // directory doesnt exist and can't create one.
                return "NO";
            }

            client.Send(Encoding.ASCII.GetBytes("beseder" + " <EOF>"));

            byte[] data2 = new byte[1024];
            data2 = ReceiveVarData(client);
            if (ByteArrayToFile(imgLoc, data2))
            {
                returni = imgLoc;
            }
            else
            {
                try
                {
                    File.Delete(imgLoc);
                }
                catch { }
                returni = "NO" + imgLoc;
            }
            return returni;
        }

        public static byte[] ReceiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            recv = s.Receive(datasize, 0, 4, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {
                Thread.Sleep(1000);
                recv = s.Receive(data, total, dataleft, 0);
                Console.Out.WriteLine("249");
                if (recv == 0)
                {
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }


        public static bool ByteArrayToFile(string imgLoc, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(imgLoc, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CloseConnection()
        {
            //server.Shutdown(SocketShutdown.Both);
            client.Shutdown(SocketShutdown.Both);

            //client.Close();
            //server.Close();
            num_of_connections--;
        }
    }
}