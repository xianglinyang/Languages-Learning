using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace Languages_Learning
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        //动态链接库
        bool tag = true;
        string str, str1;
        [DllImport("DLL2.dll", EntryPoint = "speak", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Speak([MarshalAs(UnmanagedType.LPWStr)] string speakContent); //DllImport请参照MSDN

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

        public Window1()
        {
            InitializeComponent();
            Slider1.Value = 0;
            Slider2.Value = 50;
            GetPack();
            StreamReader sr = new StreamReader(".//pack.txt");
            string s;
            while (sr.Peek() >= 0)
            {
                s = sr.ReadLine();
                ComboBox1.Items.Add(s);
            }
        }

        private void pause_or_continue_Click(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            str = textbox.Text;
        }

        private void speak()
        {
            string[] s = str.Split(new char[] { '.' });
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

        private void speak_Click(object sender, RoutedEventArgs e)
        {
            /*ThreadDelegate backWorkDel = new ThreadDelegate(prcessStart);//创建一个ThreadDelegate的实例，调用准备在后台运行的函数
            backWorkDel.BeginInvoke(null, null);使用异步的形式开始执行这个委托*/
            Thread t = new Thread(speak);
            t.Start();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = ComboBox1.SelectedIndex;
            PickPack(i);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)Slider1.Value;
            SetRate(i);
            
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int i = (int)Slider2.Value;
            SetVolumn(i);
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text = "";
        }


        private void _return_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            Hide();
        }

        //把流绑回来
        private void GetWave_Click(object sender, RoutedEventArgs e)
        {
            GetWave(str1);
            Thread t = new Thread(speak);
            t.Start();
            GetSpeak();
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            str1 = TextBox2.Text;
        }
    }
}
