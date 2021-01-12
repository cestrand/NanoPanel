using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NanoPanel
{
    class DigitalPinState
    {
        private bool _D2 = false;
        public bool D2
        {
            get => _D2;
            set
            {
                bool prev = _D2;
                _D2 = value;
                OnDigitalPinChanged(new(2, _D2, prev));
            }
        }
        private bool _D3 = false;
        public bool D3
        {
            get => _D3;
            set
            {
                bool prev = _D3;
                _D3 = value;
                OnDigitalPinChanged(new(3, _D3, prev));
            }
        }
        private bool _D4 = false;
        public bool D4
        {
            get => _D4;
            set
            {
                bool prev = _D4;
                _D4 = value;
                OnDigitalPinChanged(new(4, _D4, prev));
            }
        }
        private bool _D5 = false;
        public bool D5
        {
            get => _D5;
            set
            {
                bool prev = _D5;
                _D5 = value;
                OnDigitalPinChanged(new(5, _D5, prev));
            }
        }
        private bool _D6 = false;
        public bool D6
        {
            get => _D6;
            set
            {
                bool prev = _D6;
                _D6 = value;
                OnDigitalPinChanged(new(6, _D6, prev));
            }
        }
        private bool _D7 = false;
        public bool D7
        {
            get => _D7;
            set
            {
                bool prev = _D7;
                _D7 = value;
                OnDigitalPinChanged(new(7, _D7, prev));
            }
        }
        private bool _D8 = false;
        public bool D8
        {
            get => _D8;
            set
            {
                bool prev = _D8;
                _D8 = value;
                OnDigitalPinChanged(new(8, _D8, prev));
            }
        }
        private bool _D9 = false;
        public bool D9
        {
            get => _D9;
            set
            {
                bool prev = _D9;
                _D9 = value;
                OnDigitalPinChanged(new(9, _D9, prev));
            }
        }
        private bool _D10 = false;
        public bool D10
        {
            get => _D10;
            set
            {
                bool prev = _D10;
                _D10 = value;
                OnDigitalPinChanged(new(10, _D10, prev));
            }
        }
        private bool _D11 = false;
        public bool D11
        {
            get => _D11;
            set
            {
                bool prev = _D11;
                _D11 = value;
                OnDigitalPinChanged(new(11, _D11, prev));
            }
        }

        private bool _D12 = false;
        public bool D12
        {
            get => _D12;
            set
            {
                bool prev = _D12;
                _D12 = value;
                OnDigitalPinChanged(new(12, _D12, prev));
            }
        }

        private bool _D13 = false;
        public bool D13
        {
            get => _D13;
            set
            {
                bool prev = _D13;
                _D13 = value;
                OnDigitalPinChanged(new(13, _D13, prev));
            }
        }


        public event EventHandler<DigitalPinChangedArgs> DigitalPinChanged;

        public DigitalPinState()
        {

        }

        public DigitalPinState(Serial serial)
        {
            connectEvents(serial);
        }

        public void connectEvents(Serial serial)
        {
            serial.ReceivedLine += Serial_ReceivedLine;
        }

        static Regex re = new Regex(@"^2\s+D(?<pin>\d+)\s+(?<value>\d+)");
        private void Serial_ReceivedLine(object sender, ReceivedLineArgs e)
        {
            MatchCollection matches = re.Matches(e.line);
            foreach(Match match in matches)
            {
                int pin = int.Parse(match.Groups["pin"].Value);
                bool value = int.Parse(match.Groups["value"].Value) == 0 ? false : true;
                switch (pin)
                {
                    case  2:  D2 = value; break;
                    case  3:  D3 = value; break;
                    case  4:  D4 = value; break;
                    case  5:  D5 = value; break;
                    case  6:  D6 = value; break;
                    case  7:  D7 = value; break;
                    case  8:  D8 = value; break;
                    case  9:  D9 = value; break;
                    case 10: D10 = value; break;
                    case 11: D11 = value; break;
                    case 12: D12 = value; break;
                    case 13: D13 = value; break;
                }
            }
        }

        public virtual void OnDigitalPinChanged(DigitalPinChangedArgs e)
        {
            DigitalPinChanged?.Invoke(this, e);
        }

    }

    class DigitalPinChangedArgs : EventArgs
    {
        public bool? previous;
        public bool next;
        public int pin;

        public DigitalPinChangedArgs(int pin, bool next, bool? prev=null)
        {
            this.pin = pin;
            this.next = next;
            this.previous = prev;
        }
    }
}
