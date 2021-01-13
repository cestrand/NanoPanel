using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Drawing;

namespace NanoPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Serial serial;
        AnalogPinState analogPinState;
        DigitalPinState digitalPinState;

        ImageSource redIcon;
        ImageSource greenIcon;
        public MainWindow()
        {
            InitializeComponent();
            redIcon = Icon;
            greenIcon = BitmapFrame.Create(new Uri("pack://application:,,,/icons/green.ico", UriKind.RelativeOrAbsolute));


            serial = new();
            analogPinState = new(serial);
            digitalPinState = new(serial);
            serial.Open();
            analogPinState.AnalogPinChanged += AnalogPinState_AnalogPinChanged;
            digitalPinState.DigitalPinChanged += DigitalPinState_DigitalPinChanged;
            serial.SerialOpenChanged += Serial_SerialOpenChanged;

            WebSocketService webSocketService = new()
            {
                AnalogPinState = analogPinState,
                DigitalPinState = digitalPinState
            };
        }

        private void Serial_SerialOpenChanged(object sender, SerialOpenChangedArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (e.IsSerialOpen)
                {
                    this.Icon = greenIcon;
                    TaskbarIcon.IconSource = greenIcon;
                }
                else
                {
                    this.Icon = redIcon;
                    TaskbarIcon.IconSource = redIcon;
                }
            }));
           
        }

        private void setDigitalPinControl(Ellipse e, bool pinState)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (pinState)
                {
                    e.Fill = System.Windows.Media.Brushes.LimeGreen;
                }
                else
                {
                    e.Fill = System.Windows.Media.Brushes.Tomato;
                }
            }));
        }

        private void DigitalPinState_DigitalPinChanged(object sender, DigitalPinChangedArgs e)
        {
            switch (e.pin)
            {
                case 2: D2_status.State = e.next; break;
                case 3: D3_status.State = e.next; break;
                case 4: D4_status.State = e.next; break;
                case 5: D5_status.State = e.next; break;
                case 6: D6_status.State = e.next; break;
                case 7: D7_status.State = e.next; break;
                case 8: D8_status.State = e.next; break;
                case 9: D9_status.State = e.next; break;
                case 10: D10_status.State = e.next; break;
                case 11: D11_status.State = e.next; break;
                case 12: D12_status.State = e.next; break;
                case 13: D13_status.State = e.next; break;
            }
        }

        private void AnalogPinState_AnalogPinChanged(object sender, AnalogPinChangedArgs e)
        {
            switch (e.pin)
            {
                case 0:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A0_bar.Value = e.next;
                        A0_number.Text = e.next.ToString();
                    }));
                    break;
                case 1:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A1_bar.Value = e.next;
                        A1_number.Text = e.next.ToString();
                    }));
                    break;
                case 2:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A2_bar.Value = e.next;
                        A2_number.Text = e.next.ToString();
                    }));
                    break;
                case 3:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A3_bar.Value = e.next;
                        A3_number.Text = e.next.ToString();
                    }));
                    break;
                case 4:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A4_bar.Value = e.next;
                        A4_number.Text = e.next.ToString();
                    }));
                    break;
                case 5:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A5_bar.Value = e.next;
                        A5_number.Text = e.next.ToString();
                    }));
                    break;
                case 6:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A6_bar.Value = e.next;
                        A6_number.Text = e.next.ToString();
                    }));
                    break;
                case 7:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        A7_bar.Value = e.next;
                        A7_number.Text = e.next.ToString();
                    }));
                    break;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //clean up notifyicon (would otherwise stay open until application finishes)
            TaskbarIcon.Dispose();

            base.OnClosing(e);
        }
    }
}
