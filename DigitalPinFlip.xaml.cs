using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NanoPanel
{
    /// <summary>
    /// Logika interakcji dla klasy DigitalPinFlip.xaml
    /// </summary>
    public partial class DigitalPinFlip : UserControl
    {
        bool _state = false;
        bool IsOutput = false;
        public bool State
        {
            get => _state;
            set
            {
                bool prev = _state;
                _state = value;
                if(prev != value)
                {
                    OnStateChanged(new(value));
                }
            }
        }

        public event EventHandler<StateChangedArgs> StateChanged;

        public virtual void OnStateChanged(StateChangedArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        public DigitalPinFlip()
        {
            InitializeComponent();
            ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
            StateChanged += DigitalPinFlip_StateChanged;
            
        }

        private void DigitalPinFlip_StateChanged(object sender, StateChangedArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (State)
                {
                    ellipse.Fill = System.Windows.Media.Brushes.LimeGreen;
                }
                else
                {
                    ellipse.Fill = System.Windows.Media.Brushes.Tomato;
                }
            }));
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(IsOutput)
            {
                Flip();
            }
            
        }

        public bool Flip()
        {
            State = !State;
            return State;
        }
    }

    public class StateChangedArgs : EventArgs
    {
        public bool state;

        public StateChangedArgs(bool state)
        {
            this.state = state;
        }
    }

}
