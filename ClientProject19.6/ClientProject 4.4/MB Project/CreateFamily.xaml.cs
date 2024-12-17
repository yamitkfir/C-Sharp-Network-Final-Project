using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class CreateFamily : Window
    {
        private static string DEFAULT_LOC = "C:\\yk2020client";
        public CreateFamily()
        {
            InitializeComponent();
        }

        private void Createf_Click(object sender, RoutedEventArgs e)
        {
            string family_name = fname.Text;
            if (!family_name.Equals(""))
            {
                string response = Connections.SendRequest("CREATEFAMILY: &" + family_name + "& ");
                if (response.Contains("OK"))
                {

                    DirectoryInfo di1 = Directory.CreateDirectory(DEFAULT_LOC + "\\" + family_name);
                    DirectoryInfo di2 = Directory.CreateDirectory(DEFAULT_LOC + "\\" + family_name + "\\ANIMALS");
                    DirectoryInfo di3 = Directory.CreateDirectory(DEFAULT_LOC + "\\" + family_name + "\\USERS");

                    if (di1.Exists && di2.Exists && di3.Exists)
                    {
                        MessageBox.Show("Family created succesfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("problem creating the needed folders", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(response.Remove(response.Length-5), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("family name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
