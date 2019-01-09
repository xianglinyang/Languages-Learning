using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Windows.Controls;

namespace Languages_Learning
{
    /// <summary>
    /// Window4.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        //动态链接库
        bool tag = true;
        string str;
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


        private delegate void ThreadDelegate(); //申明一个专用来调用更改线程函数的委托
        public static string ss = string.Empty.PadRight(50);
        public MainWindow parent;

        public string current_word;
        public int number = 1;
        public StreamReader sr = new StreamReader(".//sougou.txt",System.Text.Encoding.Default);

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            str = textbox.Text;

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

        private void play_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(speak);
            t.Start();
        }

        private void Get_Answer_Click(object sender, RoutedEventArgs e)
        {
            textbox2.Text = current_word;
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            if (str == current_word) MessageBox.Show("You're right.");
            else MessageBox.Show("You're wrong.");
        }

        private void Last_One_Click(object sender, RoutedEventArgs e)
        {
            if (number == 1) MessageBox.Show("This is the first one.");
            else
            {
                number -= 1;
            StreamReader sr1 = new StreamReader(".//sougou.txt",System.Text.Encoding.Default);
            for (int i = 1; i <= number; i++)
                current_word = sr1.ReadLine();
            sr = sr1;
            play_Click(sender, e);
            }
            
        }

        private void Next_One_Click(object sender, RoutedEventArgs e)
        {
            if (sr.Peek() < 0) MessageBox.Show("There's no more word.");
            else
            {
                number += 1;
                current_word = sr.ReadLine();
                play_Click(sender, e);
            }
            
        }

        private void _return_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Hide();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)slider1.Value;
            SetVolumn(i);
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)slider2.Value;
            SetRate(i);
        }

        public Window4()
        {
            InitializeComponent();
            GetPack();
            PickPack(0);
            slider1.Value = 50;
            slider2.Value = 0;
            current_word = sr.ReadLine();
            number = 1;
        }

    }
}
