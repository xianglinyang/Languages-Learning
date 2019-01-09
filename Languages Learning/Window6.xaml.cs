using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;


namespace Languages_Learning
{
    /// <summary>
    /// Window6.xaml 的交互逻辑
    /// </summary>
    public partial class Window6 : Window
    {
        //动态链接库
        public bool tag = true;

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
        string current_sentence, file;
        StreamReader sr = new StreamReader(".//my_dream.txt", System.Text.Encoding.Default);
        int number = 1;

        private void speak()
        {
            string[] s = current_sentence.Split(new char[] { '.' });
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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)slider1.Value;
            SetVolumn(i);
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)slider2.Value;
            SetRate(i);
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void _return_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Hide();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (sr.Peek() < 0) MessageBox.Show("There's no more sentence.");
            else
            {
                number += 1;
                current_sentence = sr.ReadLine();
                play_Click(sender, e);
            }
        }

        private void last_Click(object sender, RoutedEventArgs e)
        {
            if (number == 1) MessageBox.Show("This is the first sentence.");
            else
            {
                number -= 1;
                sr.Close();
                sr = new StreamReader(file, System.Text.Encoding.Default);
                for (int i = 1; i <= number; i++)
                    current_sentence = sr.ReadLine();
                play_Click(sender, e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//guanju.txt", System.Text.Encoding.Default);
            file = ".//guanju.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//guanju.txt",System.Text.Encoding.Default);
            current_sentence = sr.ReadLine();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//hanshi.txt", System.Text.Encoding.Default);
            file = ".//hanshi.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//hanshi.txt", System.Text.Encoding.Default);
            current_sentence = sr.ReadLine();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//jingyesi.txt", System.Text.Encoding.Default);
            file = ".//jingyesi.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//jingyesi.txt", System.Text.Encoding.Default);
            current_sentence = sr.ReadLine();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//qingming.txt", System.Text.Encoding.Default);
            file = ".//qingming.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//qingming.txt", System.Text.Encoding.Default);
            current_sentence = sr.ReadLine();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//yuanri.txt", System.Text.Encoding.Default);
            file = ".//yuanri.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//yuanri.txt", System.Text.Encoding.Default);
            current_sentence = sr.ReadLine();
        }

        public Window6()
        {
            InitializeComponent();
            slider1.Value = 50;
            slider2.Value = 0;
            GetPack();
            PickPack(0);
        }
    }
}
