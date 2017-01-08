using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SummitServerSample
{
    public class Manager
    {
        private Thread oThread = null;
        public Manager()
        {         
        }

        public void Start()
        {
            int t_serverPort = 4444;
            AsynchronousSocketListener ASL = new AsynchronousSocketListener(t_serverPort);
            ASL.SetUpDelegateAcc(DummyDelegateAccept);
            ASL.SetUpDelegateRec(DummyDelegateReceive);
            oThread = new Thread(new ThreadStart(ASL.StartListening));
            oThread.Start();
        }

        public int Quit()
        {
            oThread.Join();
            return 0;
        }

        //You need to store the listener and handler.  You will need the handler socket to identify which client you're receiving messages from.
        public int DummyDelegateAccept(Socket listener, Socket handler)
        {
            Console.WriteLine("Recieved client request " + handler.RemoteEndPoint.ToString());
            return 0;
        }

        public int DummyDelegateReceive(Socket handler, String a)
        {
            Console.WriteLine("Recieved client msg from " + handler.RemoteEndPoint.ToString() + " msg : "  + a);
            return 0;
        }
    }
}
