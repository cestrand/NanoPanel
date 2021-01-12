using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace NanoPanel
{
    class Serial
    {
        static SerialPort _serialPort;
        static Regex regex_digital = new Regex(@"^2\s+D(?<pin>\d+)\s+(?<value>\d+)");

       

        bool _exitReadThread = false;
        Thread readThread;

        public event EventHandler<ReceivedLineArgs> ReceivedLine;
        

        public Serial()
        {
            _serialPort = new SerialPort("COM3", 9600);
            
        }

        public void Open()
        {
            readThread = new Thread(Read);
            readThread.Start();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ensureSerialIsOpen()
        {
            while (!_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Open();
                }
                catch (System.IO.FileNotFoundException)
                {

                }
                catch (System.IO.IOException)
                {

                }
                Thread.Sleep(500);
            }
        }

        private void Read()
        {
                       
            
            while (!_exitReadThread)
            {
                try
                {
                    ensureSerialIsOpen();
                    string message = _serialPort.ReadLine();
                    OnReceivedLine(new ReceivedLineArgs(message));
                }
                catch (TimeoutException) { }
                catch (System.UnauthorizedAccessException) { }
            }
        }

        public virtual void OnReceivedLine(ReceivedLineArgs e)
        {
            ReceivedLine?.Invoke(this, e);

        }


        void Close()
        {
            _serialPort.Close();
        }
       
    }

    class ReceivedLineArgs : EventArgs
    {
        public string line;

        public ReceivedLineArgs(String line) : base()
        {
            this.line = line;
        }
    }
}
