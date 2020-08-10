using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Vos;

namespace telnetserver
{
    public class TelnetServer: Application
    {
        Sr _sr;
        Sw _sw;
        Socket listener;
        Socket connection;
        public TelnetServer(Process process) : base(process) { }
        public override int Main(string[] args)
        {
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listener.Bind(new IPEndPoint(IPAddress.Parse("127.127.0.1"), 23));
            this.listener.Listen(10);
            this.connection = this.listener.Accept();
            int a = this.connection.Available;
            while(a>0)
            {
                byte[] buffer = new byte[a];
                this.connection.Receive(buffer);
                a = this.connection.Available;
            }
            _sr = new Sr(connection);
            _sw = new Sw(connection);
            Process p = this.Process.OS.CreateProcess("/bin/vosbox");
            p.Shell.In = _sr;
            p.Shell.Out = _sw;
            p.Start(null);
            while (true) System.Threading.Thread.Sleep(100);

            return 0;
        }
    }
}
