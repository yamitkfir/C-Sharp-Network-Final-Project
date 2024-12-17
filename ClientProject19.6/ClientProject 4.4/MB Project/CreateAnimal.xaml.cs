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
    public partial class CreateAnimal : Window
    {
        private int family_ID;
        public CreateAnimal(int Family_ID)
        {
            InitializeComponent();
            this.family_ID = Family_ID;
            type.SelectedIndex = 8;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string name1 = name.Text;
            var temp = type.SelectedIndex.ToString().Split('.');
            // temp = [Dog. cat. horse. cow. pig. snake. bird. fish. unspecified]
            string type1 = temp[temp.Length - 1]; // selected index contains irrelevant info, i take it out
            string response = "Sorry, an error occured";
            try
            {
                response = Connections.SendRequest("CREATEANIMAL " + family_ID.ToString() + " &" + name1 + "& &" + type1 + "& ").Split(' ')[0];
            }
            catch
            {
                // TODO restart connection

            }
            if (response.Contains("OK"))
            {
                MessageBox.Show("Congrats! animal saved. changes you made will be visible next time you sign in", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show(response, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
