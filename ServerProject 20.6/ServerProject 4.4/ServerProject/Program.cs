using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Connections.StartServer();
            string data="hey 19";
            while (true) // the server runs until programmer closes it
            {
                try
                {
                    data = Connections.RecieveRequest();
                    if (data.Contains("esc2"))
                    {
                        Console.Out.WriteLine(data.Split(' ')[1] + "left");
                        //Console.In.ReadLine();
                        Environment.Exit(0); // Once I make the server listen to multiple, remobve line
                    }
                    else if (data.Contains("esc1"))
                    {
                        Connections.newStartServer();
                    }
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.ToString());
                    ;
                    Connections.CloseConnection();
                    Environment.Exit(0);
                }
                Console.WriteLine(data);
            }

        }
    }
}