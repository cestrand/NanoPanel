using System;
using System.Collections.Generic;
using System.Text;

namespace NanoPanel
{
    class WebSocketService
    {
        WebSocketServer webSocketServer;
        AnalogPinState _analogPinState;
        DigitalPinState _digitalPinState;

        public AnalogPinState AnalogPinState
        {
            get => _analogPinState;
            set
            {
                if(_analogPinState is not null)
                {
                    _analogPinState.AnalogPinChanged -= _analogPinState_AnalogPinChanged;
                }
                _analogPinState = value;
                _analogPinState.AnalogPinChanged += _analogPinState_AnalogPinChanged;
            }
        }

        public DigitalPinState DigitalPinState
        {
            get => _digitalPinState;
            set
            {
                if(_digitalPinState is not null)
                { 
                    _digitalPinState.DigitalPinChanged -= _digitalPinState_DigitalPinChanged;
                }
                _digitalPinState = value;
                _digitalPinState.DigitalPinChanged += _digitalPinState_DigitalPinChanged;
            }
        }

        private void _digitalPinState_DigitalPinChanged(object sender, DigitalPinChangedArgs e)
        {
            byte[] data = { 2, (byte)e.pin, e.next ? 1 : 0 };
            webSocketServer.Send(data);
        }

        private void _analogPinState_AnalogPinChanged(object sender, AnalogPinChangedArgs e)
        {
            List<byte> data = new() { 1, (byte)e.pin };
            data.AddRange(BitConverter.GetBytes(e.next));
            webSocketServer.Send(data.ToArray());
        }

        public WebSocketService()
        {
            webSocketServer = new();
            webSocketServer.Start();
        }



        
    }
}
