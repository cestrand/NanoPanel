using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NanoPanel
{
    class AnalogPinState
    {

        private int _A0 = 0;
        public int A0
        {
            get => _A0;
            set
            {
                int prev = _A0;
                _A0 = value;
                OnAnalogPinChanged(new(0, _A0, prev));
            }
        }
        private int _A1 = 0;
        public int A1
        {
            get => _A1;
            set
            {
                int prev = _A1;
                _A1 = value;
                OnAnalogPinChanged(new(1, _A1, prev));
            }
        }
        private int _A2 = 0;
        public int A2
        {
            get => _A2;
            set
            {
                int prev = _A2;
                _A2 = value;
                OnAnalogPinChanged(new(2, _A2, prev));
            }
        }
        private int _A3 = 0;
        public int A3
        {
            get => _A3;
            set
            {
                int prev = _A3;
                _A3 = value;
                OnAnalogPinChanged(new(3, _A3, prev));
            }
        }
        private int _A4 = 0;
        public int A4
        {
            get => _A4;
            set
            {
                int prev = _A4;
                _A4 = value;
                OnAnalogPinChanged(new(4, _A4, prev));
            }
        }
        private int _A5 = 0;
        public int A5
        {
            get => _A5;
            set
            {
                int prev = _A5;
                _A5 = value;
                OnAnalogPinChanged(new(5, _A5, prev));
            }
        }
        private int _A6 = 0;
        public int A6
        {
            get => _A6;
            set
            {
                int prev = _A6;
                _A6 = value;
                OnAnalogPinChanged(new(6, _A6, prev));
            }
        }
        private int _A7 = 0;
        public int A7
        {
            get => _A7;
            set
            {
                int prev = _A7;
                _A7 = value;
                OnAnalogPinChanged(new(7, _A7, prev));
            }
        }

        public event EventHandler<AnalogPinChangedArgs> AnalogPinChanged;

        public AnalogPinState()
        {

        }

        public AnalogPinState(Serial serial)
        {
            connectEvents(serial);
        }

        public void connectEvents(Serial serial)
        {
            serial.ReceivedLine += Serial_ReceivedLine;
        }

        static Regex re = new Regex(@"^1\s+A(?<pin>\d+)\s+(?<value>\d+)");
        private void Serial_ReceivedLine(object sender, ReceivedLineArgs e)
        {
            MatchCollection matches = re.Matches(e.line);
            foreach(Match match in matches)
            {
                int pin = int.Parse(match.Groups["pin"].Value);
                int value = int.Parse(match.Groups["value"].Value);
                switch (pin)
                {
                    case 0: A0 = value; break;
                    case 1: A1 = value; break;
                    case 2: A2 = value; break;
                    case 3: A3 = value; break;
                    case 4: A4 = value; break;
                    case 5: A5 = value; break;
                    case 6: A6 = value; break;
                    case 7: A7 = value; break;
                }
            }
        }

        public virtual void OnAnalogPinChanged(AnalogPinChangedArgs e)
        {
            AnalogPinChanged?.Invoke(this, e);
        }

    }
    class AnalogPinChangedArgs : EventArgs
    {
        public int previous;
        public int next;
        public int pin;

        public AnalogPinChangedArgs(int pin, int next, int prev = -1)
        {
            this.pin = pin;
            this.next = next;
            this.previous = prev;
        }
    }
}
