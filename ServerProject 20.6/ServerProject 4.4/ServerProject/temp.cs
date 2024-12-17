/*
IN CONNECTIONS: 

public static string SendMSG(String send) // can be used if i want the server to send a msg first without expecting response
{
    string newsend = ConnectionsTat.CreateMessage(send);
    client.Send(Encoding.ASCII.GetBytes(newsend + "<EOF>"));

    return "NULL"; //data= info sent by client
}

 IN CONNC+ECTIONS: SendMSG():
{
...
string data = null;
byte[] bytes = null;
bytes = new byte[1024];
while (true){
                int bytesRec = client.Receive(bytes); // now we recive info as ByteSecs instead of full strings
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
} //recieving info in bytes til the end of msg
Console.WriteLine("Text received : {0}", data);
}

            System.Diagnostics.Debug.WriteLine("File does not exist.");

*/