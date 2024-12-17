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
using System.Runtime.InteropServices;
using System.IO;

namespace ClientProject
{
    public partial class MainWindow : Window
    {
        private string temp;
        private static string DEFAULT_LOC = "C:\\yk2020client";
        //private static string DEFAULT_LOC = "C:\\Users\\Teacher\\Documents\\yclient";

        public MainWindow()
        {
            InitializeComponent();
            start.Background = (Brush)new BrushConverter().ConvertFromString("#edcf97"); //change backgorund of label
            this.Closing += Window_Closing;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            
            temp = Connections.OpenConnection();
            DirectoryInfo di = Directory.CreateDirectory(DEFAULT_LOC);
            if (temp == "ok" && di.Exists)
            {

                status.Content = "welcome to Yamit's project";
                login_window();
            }
            else
            {
                status.Content = "Unable to connect, try again.";
            }
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Info info = new Info
            {
                Visibility = Visibility.Visible
            };
        }

        private void login_window()
        {
            Login login_window = new Login();
            this.Visibility = Visibility.Hidden; //hide the current window
            login_window.Show();
        }

        private void bye(object sender, RoutedEventArgs e)
        {
            Connections.SendRequest("esc <EOF>");
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //added this on every window so the x button will end the program running
        {
            Connections.SendRequest("esc <EOF>");
            System.Windows.Application.Current.Shutdown();
        }


    }
}
