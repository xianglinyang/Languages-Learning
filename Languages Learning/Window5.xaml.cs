using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Languages_Learning
{
    /// <summary>
    /// Window5.xaml 的交互逻辑
    /// </summary>
    public partial class Window5 : Window
    {
        //动态链接库
        bool tag = true;

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
        StreamReader sr=new StreamReader(".//my_dream.txt");
        int number = 1;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//my_dream.txt");
            file = ".//my_dream.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//my_dream.txt");
            current_sentence = sr.ReadLine();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//cherish_the_time.txt");
            file = ".//cherish_the_time.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//cherish_the_time.txt");
            current_sentence = sr.ReadLine();

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//For.txt");
            file = ".//For.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//For.txt");
            current_sentence = sr.ReadLine();
        }

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

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = ComboBox1.SelectedIndex;
            PickPack(i);
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)slider2.Value;
            SetRate(i);
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)slider1.Value;
            SetVolumn(i);
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

        public Window5()
        {
            InitializeComponent();
            slider1.Value = 50;
            slider2.Value = 0;
            GetPack();
            PickPack(1);
            StreamReader sr = new StreamReader(".//pack.txt");
            string s;
            while (sr.Peek() >= 0)
            {
                s = sr.ReadLine();
                ComboBox1.Items.Add(s);
            }
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

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//search.txt");
            file = ".//search.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//search.txt");
            current_sentence = sr.ReadLine();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//love_bud.txt");
            file = ".//love_bud.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//love_bud.txt");
            current_sentence = sr.ReadLine();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//had_i_not_seen_the_sun.txt");
            file = "./had_i_not_seen_the_sun.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//had_i_not_seen_the_sun.txt");
            current_sentence = sr.ReadLine();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            sr.Close();
            sr = new StreamReader(".//a_grain_of_sand.txt");
            file = ".//a_grain_of_sand.txt";
            number = 1;
            textbox.Text = "";
            while (sr.Peek() >= 0)
            {
                current_sentence = sr.ReadLine();
                textbox.Text += current_sentence;
                textbox.Text += "\r\n";
            }
            sr.Close();
            sr = new StreamReader(".//a_grain_of_sand.txt");
            current_sentence = sr.ReadLine();
        }

        private void last_Click(object sender, RoutedEventArgs e)
        {
            if (number == 1) MessageBox.Show("This is the first sentence.");
            else
            {
                number -= 1;
                StreamReader sr1 = new StreamReader(file);
                for (int i = 1; i <= number; i++)
                    current_sentence = sr1.ReadLine();
                sr = sr1;
                play_Click(sender, e);
            }
            
        }
    }
}
