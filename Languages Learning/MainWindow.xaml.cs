using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;

namespace Languages_Learning
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //动态链接库


        //三个独立界面
        Window1 Listening;
        Window2 Speaking_English;
        Window3 Dictation_English;
        Window4 Dictation_Chinese;
        Window5 Poetry_Reading_English;
        Window6 Poetry_Reading_Chinese;
        Window7 Speaking_Chinese;
        public bool language=true;//默认设置英语

        public MainWindow()
        {
            InitializeComponent();
            Listening = new Window1();
            Speaking_English = new Window2();
            Dictation_English = new Window3();
            Dictation_Chinese = new Window4();
            Poetry_Reading_English = new Window5();
            Poetry_Reading_Chinese = new Window6();
            Speaking_Chinese = new Window7();
        }


        public void listenling_Click(object sender, RoutedEventArgs e)
        {
            Listening.parent = this;
            Listening.Show();
            Hide();
        }

        private void speaking_Click(object sender, RoutedEventArgs e)
        {
            if (language)
            {
                Speaking_English.parent = this;
                Speaking_English.Show();
                Hide();
            }
            else
            {
                Speaking_Chinese.parent = this;
                Speaking_Chinese.Show();
                Hide();
            }
        }

        private void dictation_Click(object sender, RoutedEventArgs e)
        {
            if(language)
            {
                Dictation_English.parent = this;
                Dictation_English.Show();
                Hide();
            }
            else
            {
                Dictation_Chinese.parent = this;
                Dictation_Chinese.Show();
                Hide();

            }
        }

        private void english_Checked(object sender, RoutedEventArgs e)
        {
            language = true;
        }

        private void chinese_Checked(object sender, RoutedEventArgs e)
        {
            language = false;
        }

        private void poetry_reading_Click(object sender, RoutedEventArgs e)
        {
            if (language)
            {
                Poetry_Reading_English.parent = this;
                Poetry_Reading_English.Show();
                Hide();
            }
            else
            {
                Poetry_Reading_Chinese.parent = this;
                Poetry_Reading_Chinese.Show();
                Hide();
            }
        }
    }
}
