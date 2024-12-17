// IM THE ONE RECIEVING INFO

// TODO renew connection automatically if lost

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ClientProject
{
    public class Connections
    {
        private static Socket sender;
        private static string DEFAULT_LOC = "C:\\yk2020client";
        private static int PORT = 10001;
        private static string hosting = "192.168.1.23";

        public static String OpenConnection()
        {
            String returni;

            try
            {
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                IPHostEntry host = Dns.GetHostEntry(hosting);
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);

                // Create a TCP/IP  socket.    
                sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                
                sender.Connect(remoteEP);
                returni = "ok";
            }
            catch (SocketException se)
            {
                returni = "SocketException : " + se.ToString();
            }
            catch (Exception e)
            {
                returni = "Unexpected Exception : " + e.ToString();
            }
            return returni;
        }

        public static String reOpenConnection()
        {
            PORT = PORT + 1;
            string temp = Connections.SendRequest("esc1");
            if (temp != "bye <EOF")
            {
                ;
            }
            Thread.Sleep(1000);

            //Connections.CloseConnection();
            sender.Disconnect(true);
            Thread.Sleep(2000);

            String returni;
            try
            {
                IPHostEntry host = Dns.GetHostEntry(hosting);
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);
 
                sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                
                sender.Connect(remoteEP);
                returni = "ok";
            }
            catch (SocketException se)
            {
                returni = "SocketException : " + se.ToString();
            }
            catch (Exception e)
            {
                returni = "Unexpected Exception : " + e.ToString();
            }
            return returni;
        }
        
        public static String SendRequest(String send)
        {
            byte[] bytes = new byte[1024];
            String returni;

            try
            {
                // Send the byte-array data through the socket.    
                int bytesSent = sender.Send(Encoding.ASCII.GetBytes(send + " <EOF>"));

                // Receive the response from the remote device.    
                int bytesRec = sender.Receive(bytes);
                returni = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                if (returni == "esc")
                {
                    CloseConnection();
                    returni = "bye";
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show("Connections dissconnected.");
                Environment.Exit(0);
                return "";
            }
            catch (Exception e)
            {
                returni = "Unexpected exception : " + e.ToString();
            }
            return returni;
        }

        public static String RecieveMessage()
        {
            byte[] bytes = new byte[1024];
            String returni;
            try
            {
                // Receive the response from the remote device.    
                int bytesRec = sender.Receive(bytes);
                returni = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                if (returni == "esc")
                {
                    CloseConnection();
                    returni = "bye";
                }
            }
            catch (SocketException se)
            {
                returni = "SocketException : " + se.ToString();
            }
            catch (Exception e)
            {
                returni = "Unexpected exception : " + e.ToString();
            }
            return returni;
        }

        public static string RequestImage(int family_id1, string type_in_caps, string rest, string family_name) // if file already exists, the function doesnt replace it
        {

            // ex: SEND_IMG_TO_CLIENT FAMILY_ID: 123 USER: yamit

            string imgLoc = DEFAULT_LOC + "\\" + family_name + "\\" + type_in_caps + "\\" + rest + ".jpg";
            if (type_in_caps == "FAMILY")
            {
                imgLoc = DEFAULT_LOC + "\\" + family_name + "\\" + family_name + ".jpg";
            }
            if (File.Exists(imgLoc))
            {
                // that means the picture already exists on this computer. no need to ask the server
                return imgLoc;
            }
            string directory = DEFAULT_LOC + "\\" + family_name + "\\" + type_in_caps;
            if (type_in_caps == "FAMILY")
            {
                directory = DEFAULT_LOC + "\\" + family_name;
            }
            if (!Directory.Exists(directory))
            {
                DirectoryInfo di = Directory.CreateDirectory(directory);
            }

            string returni;
            string response = SendRequest("SEND_IMG_TO_CLIENT FAMILY_ID: " + family_id1.ToString() + " " + type_in_caps + ": " + rest + " ");
            byte[] data = new byte[1024];
            try
            {
                data = ReceiveVarData(sender);
                if (data.Length != 0 && ByteArrayToFile(imgLoc, data))
                {
                    returni = imgLoc;
                }
                else
                {
                    returni = "NO" + imgLoc;
                }
            }
            catch(SocketException e)
            {
                MessageBox.Show("Connections dissconnected.");
                returni = "";
            }
            catch
            {
                returni = "NO 161";
            }
            
            return returni;
        }

        public static string newRequestImage(int family_id1, string type_in_caps, string rest, string family_name) //this request image ignores the fact that a file with that name exists already,                                                                                                  // used for replaced images
        {
            // ex: SEND_IMG_TO_CLIENT FAMILY_ID: 123 USER: yamit
            Thread.Sleep(1000);

            string returni = "NO";
            string imgLoc = DEFAULT_LOC + "\\" + family_name + "\\" + type_in_caps + "\\" + rest + ".jpg";
            if (type_in_caps == "FAMILY")
            {
                imgLoc = DEFAULT_LOC + "\\" + family_name + "\\" + family_name + ".jpg";
            }
            string directory = DEFAULT_LOC + "\\" + family_name + "\\" + type_in_caps;
            if (type_in_caps == "FAMILY")
            {
                directory = DEFAULT_LOC + "\\" + family_name;
            }
            if (!Directory.Exists(directory))
            {
                DirectoryInfo di = Directory.CreateDirectory(directory);
            }
            string response = SendRequest("SEND_IMG_TO_CLIENT FAMILY_ID: " + family_id1.ToString() + " " + type_in_caps + ": " + rest + " ");

            byte[] data = new byte[1024];
            try
            {
                data = ReceiveVarData(sender);
                if (data.Length != 0 && ByteArrayToFile(imgLoc, data))
                {
                    returni = imgLoc;
                }
                else
                {
                    returni = "NO" + imgLoc;
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show("Connections dissconnected.");
                returni = "";
            }
            catch
            {
                returni = "NO 161";
            }
            return returni;
        }

        private static byte[] ReceiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            byte[] data;

            byte[] bytes = new byte[1024];
            int bytesRec = sender.Receive(bytes);
            string response = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            if (response.Contains("OK"))
            {
                Thread.Sleep(100);

                recv = s.Receive(datasize, 0, 4, 0);
                int size = BitConverter.ToInt32(datasize, 0);
                int dataleft = size;
                data = new byte[size];


                while (total < size)
                {
                    recv = s.Receive(data, total, dataleft, 0);
                    Thread.Sleep(100);
                    if (recv == 0)
                    {
                        break;
                    }
                    total += recv;
                    dataleft -= recv;
                }
                return data;
            }
            else
            {
                data = new byte[0];
                return data;
            }
        }

        public static string SendImage(int family_id1, string type_in_caps, string rest, string imgLoc)
        {
            string response = SendRequest("RECIEVE_IMG_FROM_CLIENT FAMILY_ID: " + family_id1.ToString() + " " + type_in_caps + ": " + rest);
            if (response.Contains("beseder"))
            {
                ;
            }
            try
            {
                Uri uriSource = new Uri(imgLoc);
                BitmapImage image = new BitmapImage(uriSource);
                JpegBitmapEncoder JpegEncoder = new JpegBitmapEncoder();
                JpegEncoder.Frames.Add(BitmapFrame.Create(image));
                MemoryStream MS = new MemoryStream();
                JpegEncoder.Save(MS);
                byte[] Data = MS.ToArray();
                // read to end
                byte[] bmpBytes = MS.ToArray();
                MS.Close();
                int total = SendVarData(sender, bmpBytes);
                return "OK";
            }
            catch (SocketException e)
            {
                MessageBox.Show("Connections dissconnected.");
                return "";
            }
            catch
            {
                return "NO";
            }
        }
        private static int SendVarData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = s.Send(datasize);

            while (total < size)
            {
                Thread.Sleep(1000);
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            return total;
        }

        public static bool ByteArrayToFile(string imgLoc, byte[] byteArray)
        {
            try
            {
                using (FileStream fsi = new FileStream(imgLoc, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) {
                    using (BinaryWriter writeri = new BinaryWriter(fsi, Encoding.UTF8))
                    {
                        writeri.Write(byteArray, 0, byteArray.Length);
                        writeri.Close();
                        writeri.Dispose();
                    }
                    fsi.Close();
                    fsi.Dispose();
                    return true;
                }

                //StreamWriter sw = new StreamWriter(myfile, false);
                // now i cannot change the file bc it is "being used by another proccess", so i create a copy of it and replace it afterwards
                
            }
            catch
            {
                var temp = FileUtil.WhoIsLocking(imgLoc);
                Console.WriteLine(temp);
                return false;
            }
        }

        public static void CloseConnection()
        {
            // Release the socket.
            string response = Connections.SendRequest("esc1");
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}