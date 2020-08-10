using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace telnetserver
{
    class Sr : TextReader
    {
        private byte[] _Buffer = new byte[4096];
        private Socket connection;

        public Sr(Socket connection)
        {
            this.connection = connection;
        }
        public override string ReadLine()
        {
            int d = this.connection.Available;
            while (d == 0)
            {
                Thread.Sleep(1000);
                d = this.connection.Available;
            }
            byte[] buffer = new byte[d];
            this.connection.Receive(buffer);
            return Encoding.ASCII.GetString(buffer).Trim();
        }
    }
}
