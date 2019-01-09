using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Languages_Learning
{
    /// <summary>
    /// Window7.xaml 的交互逻辑
    /// </summary>
    public partial class Window7 : Window
    {
        public MainWindow parent;

        bool tag = true;
        string str, str1;
        Thread t;
        string current_word;

        [DllImport("DLL2.dll", EntryPoint = "speak", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Speak([MarshalAs(UnmanagedType.LPWStr)] string speakContent); //DlImport请参照MSDN

        [DllImport("DLL2.dll", EntryPoint = "check", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Check();

        [DllImport("DLL2.dll", EntryPoint = "pickPack", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PickPack(int i);

        [DllImport("DLL2.dll", EntryPoint = "getPack", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetPack();

        [DllImport("DLL2.dll", EntryPoint = "setVolumn", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetVolumn(int i);

        [DllImport("DLL2.dll", EntryPoint = "setRate", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRate(int i);

        [DllImport("DLL2.dll", EntryPoint = "getWave", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetWave([MarshalAs(UnmanagedType.LPWStr)] string path);

        [DllImport("DLL2.dll", EntryPoint = "getSpeak", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSpeak();

        [DllImport("SRDLL.dll", EntryPoint = "sr", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern double SR(string i, StringBuilder o);

        [DllImport("SRDLL.dll", EntryPoint = "tag", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TAG(string i, StringBuilder o);

        private delegate void ThreadDelegate(); //申明一个专用来调用更改线程函数的委托
        public static string ss = string.Empty.PadRight(50);

        public StreamReader sr1 = new StreamReader(".//sougou.txt");
        public int number = 1;

        public Window7()
        {
            InitializeComponent();
            Slider1.Value = 50;//音量
            Slider2.Value = 0;
            GetPack();
            PickPack(0);
            current_word = sr1.ReadLine();
            number = 1;
            TextBlock1.Text = current_word;

        }
        private void speak()
        {
            string[] s = current_word.Split(new char[] { '.' });
            int i = 0, m = s.Length;
            while (i < m)
            {
                if (tag)
                {
                    Speak(s[i]);
                    i++;
                    // DoEvents();
                }
            }
        }


        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)Slider1.Value;
            SetVolumn(i);
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)Slider2.Value;
            SetRate(i);
        }

        void Sr1()
        {
            StringBuilder ss = new StringBuilder(1024);
            double result = SR(current_word, ss);
            //TextBlock1.Text = result.ToString();
        }

        private void set_Click(object sender, RoutedEventArgs e)
        {
            t = new Thread(Sr1);
            t.Start();
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder ss = new StringBuilder(1024);
            double result = TAG(current_word, ss);
            if (result == -1 || result == -2) TextBlock1.Text = "Wrong Answer!";
            else
            {
                result = result * 100;
                TextBlock3.Text = result.ToString();
                TextBlock2.Text = ss.ToString();
            }
            t.Abort();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (sr1.Peek() < 0) MessageBox.Show("There's no more word.");
            else
            {
                number += 1;
                current_word = sr1.ReadLine();
                TextBlock1.Text = current_word;
            }
        }

        private void last_Click(object sender, RoutedEventArgs e)
        {
            if (number == 1) MessageBox.Show("This is the first one.");
            else
            {
                number -= 1;
                StreamReader sr2 = new StreamReader(".//sougou.txt");
                for (int i = 1; i <= number; i++)
                    current_word = sr2.ReadLine();
                sr1 = sr2;
                TextBlock1.Text = current_word;
            }
        }

        private void _return_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Hide();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(speak);
            t.Start();
        }
    }
}
