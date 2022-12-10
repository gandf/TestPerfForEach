using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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

namespace testperf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SpeedText.Text = string.Empty;
            /*
            SpeedTest(60000);
            SpeedTest(600000);
            SpeedTest(6000000);
            SpeedTest(60000000);
            */
            /*
            SpeedTestSimple(60000);
            SpeedTestSimple(60000);
            SpeedTestSimple(60000);
            SpeedTestSimple(600000);
            SpeedTestSimple(600000);
            */
            SpeedTestSimple(6000000);
            SpeedTestSimple(6000000);
            SpeedTestSimple(6000000);
            SpeedTestSimple(6000000);
        }
        private void SpeedTest(int nbdata)
        {
            SpeedText.Text += string.Format("Test with {0} NbData", nbdata) + Environment.NewLine;
            List<int> list = new(nbdata);
            var rnd = RandomNumberGenerator.Create();
            Random rand = new(12345);
            for (int i = 0; i < nbdata; i++)
            {
                var rnddata = new byte[sizeof(int)];
                rnd.GetBytes(rnddata);
                list.Add(BitConverter.ToInt32(rnddata));
            }
            int[] arr = list.ToArray();

            //Begin test
            int chk = 0;
            Stopwatch watch = Stopwatch.StartNew();
            for (int rpt = 0; rpt < 100; rpt++)
            {
                int k = list.Count;
                for (int j = 0; j < k; j++)
                {
                    chk += list[j];
                }
            }
            watch.Stop();
            SpeedText.Text += string.Format("List/for: {0}ms ({1})", watch.ElapsedMilliseconds, chk) + Environment.NewLine;

            chk = 0;
            watch = Stopwatch.StartNew();
            for (int rpt = 0; rpt < 100; rpt++)
            {
                foreach (int i in list)
                {
                    chk += i;
                }
            }
            watch.Stop();
            SpeedText.Text += string.Format("List/foreach: {0}ms ({1})", watch.ElapsedMilliseconds, chk) + Environment.NewLine;

            chk = 0;
            watch = Stopwatch.StartNew();
            for (int rpt = 0; rpt < 100; rpt++)
            {
                list.ForEach(i => chk += i);
            }
            watch.Stop();
            SpeedText.Text += string.Format("List/foreach function: {0}ms ({1})", watch.ElapsedMilliseconds, chk) + Environment.NewLine;
        }
        private void SpeedTestSimple(int nbdata)
        {
            SpeedText.Text += string.Format("Test with {0} NbData", nbdata) + Environment.NewLine;
            List<int> list = new(nbdata);
            var rnd = RandomNumberGenerator.Create();
            Random rand = new(12345);
            for (int i = 0; i < nbdata; i++)
            {
                var rnddata = new byte[sizeof(int)];
                rnd.GetBytes(rnddata);
                list.Add(BitConverter.ToInt32(rnddata));
            }
            int[] arr = list.ToArray();

            //Begin test
            int chk = 0;
            Stopwatch watch = Stopwatch.StartNew();
            for (int rpt = 0; rpt < 100; rpt++)
            {
                foreach (int i in list)
                {
                    chk += i;
                }
            }
            watch.Stop();
            SpeedText.Text += string.Format("List/foreach: {0}ms ({1})", watch.ElapsedMilliseconds, chk) + Environment.NewLine;

            chk = 0;
            watch = Stopwatch.StartNew();
            for (int rpt = 0; rpt < 100; rpt++)
            {
                list.ForEach(i => chk += i);
            }
            watch.Stop();
            SpeedText.Text += string.Format("List/foreach function: {0}ms ({1})", watch.ElapsedMilliseconds, chk) + Environment.NewLine;
        }
    }
}
