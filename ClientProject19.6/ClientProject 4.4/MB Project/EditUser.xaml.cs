using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class EditUser : Window
    {
        private int family1;
        private string account1;
        private string imgLoc;
        private Home HomeWindow; // so i can send info to it when this window is closed
        private Boolean isChanged;
        private string[] info = { "", "", "" };


        public EditUser(int family_ID, string account, Home home)
        {
            InitializeComponent();
            this.family1 = family_ID;
            this.account1 = account;
            this.HomeWindow = home;
            this.isChanged = false;

            try
            {
                info = Connections.SendRequest("INFO_USER FAMILY_ID: " + family1 + " ACCOUNT: " + account1 + " ").Split('&');
            }
            catch
            {
                // TODO restart connection

            }
            // info= {name, family_name, age, gender}
            if (info.Contains("ERROR"))
            {
                MessageBox.Show("error showing ur info", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                name.Content = account1;
                family.Content = info[0].Trim();
                age.Text = info[2].Trim();
                if (info[2].Contains("Female")) gender.SelectedIndex = 0;
                if (info[2].Contains("Male")) gender.SelectedIndex = 1;
                else gender.SelectedIndex = 2;

                // TODO remove othernotes from "info_user"
            }
            string imgLoc = Connections.RequestImage(family_ID, "USER", account1, info[0]);
            if (!imgLoc.Contains("NO"))
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imgLoc.Remove(imgLoc.Length - 4) + "2.jpg");
                    bitmap.EndInit();
                    userimage.Source = bitmap;
                }
                catch
                {
                    try
                    {
                        File.Copy(imgLoc, imgLoc.Remove(imgLoc.Length - 4) + "2.jpg");
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imgLoc.Remove(imgLoc.Length - 4) + "2.jpg");
                        bitmap.EndInit();
                        userimage.Source = bitmap;
                    }
                    catch
                    {
                        ;
                    }
                }
            }

        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*jpg)|*.jpg| GIF Files (*.gif)|*gif|All Files (*.*)|*.*";
                dlg.Title = "Choose user pictre";
                if (dlg.ShowDialog() == true)
                {
                    imgLoc = dlg.FileName.ToString();
                    
                    ImageSource imageSource2 = new BitmapImage(new Uri(imgLoc));
                    userimage.Source = imageSource2;
                    imageSource2.Freeze();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ":" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string response = Connections.SendImage(family1, "USER", account1, imgLoc);
            if (response.Contains("OK"))
            {
                MessageBox.Show("Image uploaded succesfully", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                this.isChanged = true;
            }
            else
            {
                MessageBox.Show("I'm sorry, but this image is problematic for me to save. Please go ahead and save another one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Saveall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newage;
                try
                {
                    newage = int.Parse(age.Text).ToString();
                }
                catch
                {
                    newage = "";
                }
                string del = gender.SelectedItem.ToString().Substring(38);
                string response = Connections.SendRequest("UPDATE_USER FAMILY_ID: " + family1 + " USER: " + account1 + " &" + name.Content + "&" + newage + "&" + gender.SelectedItem.ToString().Substring(38));
                if (response.Contains("OK"))
                {
                    isChanged = true;
                    MessageBox.Show("changes made succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("problem saving changes: " + response, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("dont try to send an illegal age", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MessageBox.Show("changes you made will be visible next time you sign in", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Visibility = Visibility.Hidden;
        }
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("changes you made will be visible next time you sign in", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Visibility = Visibility.Hidden;
        }
    }
}
