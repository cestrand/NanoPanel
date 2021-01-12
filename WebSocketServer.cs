using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace NanoPanel
{
    public class WebSocketServer
    {
        HttpListener httpListener;
        private List<Task> tasks = new();
        private bool _quit = false;
        private List<HttpListenerWebSocketContext> connected = new();

        public bool IsRunning
        {
            get
            {
                return httpListener.IsListening && !_quit;
            }
        }

        public WebSocketServer()
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:7775/");
        }

        public void Start()
        {
            httpListener.Start();
            Task.Factory.StartNew(AcceptConnections);
        }

        public void Stop()
        {
            _quit = true;
            Task.Factory.ContinueWhenAll(tasks.ToArray(), tasks =>
            {
                httpListener.Stop();
            });
        }

        public async void Send(ArraySegment<byte> bytes)
        {
            foreach(HttpListenerWebSocketContext ctx in connected)
            {
                WebSocket ws = ctx.WebSocket;
                ws.SendAsync(bytes, WebSocketMessageType.Binary, true, new());
            }
        }

        public async void Send(string text) {
            Send(Encoding.ASCII.GetBytes(text));
        }


        public async void AcceptConnections()
        {
            while(!_quit)
            {
                HttpListenerContext context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;
                    connected.Add(webSocketContext);
                }
            }
            
        }
    }
}
