using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint remoteEp = new IPEndPoint(ipAddress, 14800);// 11200


        try
		{
			Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			sender.Connect(remoteEp);

			Console.WriteLine("Conectado con el servidor");

			Console.WriteLine("Escriba un texto: ");
			string texto = Console.ReadLine();

			byte[] msg = Encoding.ASCII.GetBytes(texto + "<EOF>");

			int byteSent = sender.Send(msg);


			
			byte[] bytes = new byte[1024];
			int byteRec = sender.Receive(bytes);
			string text = Encoding.ASCII.GetString(bytes, 0, byteRec);
			Console.WriteLine("servidor: "+text);
			sender.Shutdown(SocketShutdown.Both);
			sender.Close();
		}
		catch (Exception e)
		{

			Console.WriteLine(e.ToString());
		}
    }
}