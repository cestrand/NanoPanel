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
        public MainWindow()
        {
            InitializeComponent();
            serial = new();
            analogPinState = new(serial);
            digitalPinState = new(serial);
            serial.Open();
            analogPinState.AnalogPinChanged += AnalogPinState_AnalogPinChanged;
            digitalPinState.DigitalPinChanged += DigitalPinState_DigitalPinChanged;
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
                case  2: setDigitalPinControl( D2_status, e.next); break;
                case  3: setDigitalPinControl( D3_status, e.next); break;
                case  4: setDigitalPinControl( D4_status, e.next); break;
                case  5: setDigitalPinControl( D5_status, e.next); break;
                case  6: setDigitalPinControl( D6_status, e.next); break;
                case  7: setDigitalPinControl( D7_status, e.next); break;
                case  8: setDigitalPinControl( D8_status, e.next); break;
                case  9: setDigitalPinControl( D9_status, e.next); break;
                case 10: setDigitalPinControl(D10_status, e.next); break;
                case 11: setDigitalPinControl(D11_status, e.next); break;
                case 12: setDigitalPinControl(D12_status, e.next); break;
                case 13: setDigitalPinControl(D13_status, e.next); break;
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
    }
}
