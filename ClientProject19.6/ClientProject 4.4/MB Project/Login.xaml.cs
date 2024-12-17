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
using System.Windows.Shapes;

namespace ClientProject
{
    public partial class Login : Window
    {
        private static string account1;
        private static string password1;

        public Login()
        {
            InitializeComponent();
            this.Closing += Window_Closing;
            account1 = ""; password1 = "";
            enter.Background = (Brush)new BrushConverter().ConvertFromString("#edcf97"); //change backgorund of label
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            account1 = account.Text;
            password1 = password.Password.ToString();
            if (account1 == "" && password1 == "") MessageBox.Show("please enter account and password before trying to log in", "Error");
            else if (account1 == "") MessageBox.Show("please enter account before trying to log in", "Error");
            else if (password1 == "") MessageBox.Show("please enter password before trying to log in", "Error");
            else
            {
                string response = Connections.SendRequest("LOGINACCOUNT: " + account1 + " PASSWORD: " + password1 + " ");
                if (response.Contains("OK")) next_window();
                else MessageBox.Show("Username and or password are wrong. Please try again.", "Problem");
            }
        }

        private void next_window()
        {
            this.Visibility = Visibility.Hidden;
            Home home_window = new Home(account1, password1);
            home_window.Show();
        }
        
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount register_window = new CreateAccount();
            register_window.Show();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Connections.SendRequest(account1 + " esc <EOF>");
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //added this on every window so the x button will end the program running
        {
            Connections.SendRequest(account1 + " esc <EOF>");
            System.Windows.Application.Current.Shutdown();
        }

        private void bye(object sender, RoutedEventArgs e)
        {
            Connections.SendRequest(account1 + " esc <EOF>");
            System.Windows.Application.Current.Shutdown();
        }
    }
}
