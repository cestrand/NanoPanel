using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Linq;

namespace NanoPanel
{
    public class WebSocketServer
    {
        HttpListener httpListener;
        private List<Task> tasks = new();
        List<Task<(WebSocket ws, byte[] buffer, WebSocketReceiveResult result)>> receiveTasks = new();
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
            Task.Factory.StartNew(ReceiveData);
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


        private async void AcceptConnections()
        {
            while(!_quit)
            {
                HttpListenerContext context = await httpListener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;
                    connected.Add(webSocketContext);
                    receiveTasks.Add(makeReceiveTask(webSocket, new byte[128]));
                }
            }
            
        }

        private async Task<(WebSocket ws, byte[] buffer, WebSocketReceiveResult result)> makeReceiveTask(WebSocket ws, byte[] buffer)
        {
            return (ws, buffer, await ws.ReceiveAsync(buffer, new()));
        }

        private async void ReceiveData()
        {
            while(!_quit)
            {
                try
                {
                    if (receiveTasks.Count == 0)
                    {
                        Thread.Sleep(20);
                        continue;
                    }

                    await Task.Factory.ContinueWhenAny(receiveTasks.ToArray(), finishedTask =>
                    {
                        var task = finishedTask as Task<(WebSocket ws, byte[] buffer, WebSocketReceiveResult result)>;
                        WebSocket ws = task.Result.ws;
                        byte[] buffer = task.Result.buffer;
                        WebSocketReceiveResult result = task.Result.result;
                        ProcessResult(buffer, ws, result);
                        receiveTasks.Remove(finishedTask);
                        if (ws.State == WebSocketState.Open || ws.State == WebSocketState.CloseSent)
                        {
                            receiveTasks.Add(makeReceiveTask(ws, buffer));
                        }
                    });
                }
                catch (System.ArgumentException) { }
            }
            
        }

        private void ProcessResult(byte[] buffer, WebSocket ws, WebSocketReceiveResult result)
        {
            if (result.MessageType == WebSocketMessageType.Close)
            {
                ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", new()).Wait();
                return;
            }
            if(result.MessageType == WebSocketMessageType.Text)
            {
                MessageBox.Show(Encoding.ASCII.GetString(buffer));
            }
            if (result.MessageType == WebSocketMessageType.Binary)
            {
                byte cmd = buffer[0];
                switch(cmd)
                {
                    case 3:
                        byte pin = buffer[1];
                        bool value = buffer[2] == 0 ? false : true;
                       // SetDigitalPin(pin, value);
                        break;
                }
            }


        }
    }
}
