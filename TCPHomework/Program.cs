using ScreenShotExample;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	public class Server
	{
		static Thread thread;
		static public void Main()
		{
            ScreenCapture sc = new ScreenCapture();
			Console.ReadLine();
            Console.WriteLine("Working");
            TcpListener listener = new TcpListener(IPAddress.Parse("192.168.0.108"), 11000);
            try
            {
                listener.Start();
                do
                {
                    if (listener.Pending())
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        byte[] buffer = new byte[1024];
                        string str;
                        NetworkStream ns = client.GetStream();
                        int len = ns.Read(buffer, 0, buffer.Length);
                        StringBuilder sb = new StringBuilder();
                        str = Encoding.UTF8.GetString(buffer, 0, len);
                        if(str == "Screenshot")
                        {
                            sc.CaptureScreenToFile("test.jpg", ImageFormat.Jpeg);
                        }
                        sb.AppendLine($"{len} byte was received fron {client.Client.RemoteEndPoint}");
                        sb.AppendLine(str);
                        Console.WriteLine(sb.ToString());
                        client.Client.Shutdown(SocketShutdown.Receive);
                    }
                } while (true);
            }
            catch (SystemException ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                listener.Stop();
            }
        }

        async static void ServerFunc()
        {
			await Task.Run(() =>
			{
                
            });
			
        }
    }
}