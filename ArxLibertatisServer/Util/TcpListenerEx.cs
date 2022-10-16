using System.Net;
using System.Net.Sockets;

namespace ArxLibertatisServer.Util
{
    /// <summary>
    /// because Active is not exposed on TcpListener, we have to use this class
    /// </summary>
    public class TcpListenerEx : TcpListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener"/> class with the specified local endpoint.
        /// </summary>
        /// <param name="localEP">An <see cref="T:System.Net.IPEndPoint"/> that represents the local endpoint to which to bind the listener <see cref="T:System.Net.Sockets.Socket"/>. </param><exception cref="T:System.ArgumentNullException"><paramref name="localEP"/> is null. </exception>
        public TcpListenerEx(IPEndPoint localEP) : base(localEP)
        {
        }

        public new bool Active
        {
            get { return base.Active; }
        }
    }
}
