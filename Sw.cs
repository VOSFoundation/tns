using System.IO;
using System.Net.Sockets;
using System.Text;

namespace telnetserver
{
    class Sw : TextWriter
    {
        private Socket connection;

        public Sw(Socket connection)
        {
            this.connection = connection;
        }

        public override Encoding Encoding => throw new System.NotImplementedException();
        public override void WriteLine(string value)
        {
            byte[] str = Encoding.ASCII.GetBytes(value + "\r\n");
            this.connection.Send(str);
        }
        public override void Write(string value)
        {
            byte[] str = Encoding.ASCII.GetBytes(value);
            this.connection.Send(str);
        }
    }
}
