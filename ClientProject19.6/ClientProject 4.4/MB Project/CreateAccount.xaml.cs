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
    public partial class CreateAccount : Window
    {
        private string account1;
        private string password1;

        public CreateAccount()
        {
            InitializeComponent();
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            this.account1 = name.Text;
            this.password1 = password.Password.ToString();


            if (account1 == "" && password1 == "") MessageBox.Show("please enter account and password before trying to create an account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (account1 == "") MessageBox.Show("please enter account before trying to create an account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (password1 == "") MessageBox.Show("please enter password before trying to create an account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!password_Copy.Password.ToString().Equals(password1)) MessageBox.Show("password dont match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string response = Connections.SendRequest("CREATEACCOUNT:ACCOUNT: &" + this.account1 + "& PASSWORD: &" + this.password1 + "& FAMILY: &" + this.family.Text + "& ");
                if (response.Contains("OK"))
                {
                    MessageBox.Show("yay account created seccesfully!!");
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(response.Remove(response.Length-6), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Newfamily_Click(object sender, RoutedEventArgs e)
        {
            CreateFamily new_family = new CreateFamily();
            new_family.Show();
        }


        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
