using Microsoft.Win32;
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
    public partial class Family : Window
    {
        private int family_id;
        private string imgLoc;

        public Family(int Family_ID)
        {
            InitializeComponent();
            this.family_id = Family_ID;
            string[] temp = {"","",""};
            try
            {
                temp = Connections.SendRequest("INFO_FAMILY FAMILY: " + family_id).Split('&');
            }
            catch
            {
                // TODO restart connection

            }
            name.Content = temp[0];
            members.Content = temp[1];
            animals.Content = temp[2];

            string imgLoc = Connections.RequestImage(family_id, "FAMILY", "", name.ContentStringFormat);
            if (!imgLoc.Contains("NO"))
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imgLoc.Remove(imgLoc.Length - 4) + "2.jpg");
                    bitmap.EndInit();
                    familyimage.Source = bitmap;
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
                        familyimage.Source = bitmap;
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
                dlg.Title = "Choose family pictre";
                if (dlg.ShowDialog() == true)
                {
                    imgLoc = dlg.FileName.ToString();

                    ImageSource imageSource = new BitmapImage(new Uri(imgLoc));
                    familyimage.Source = imageSource;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ":" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string response = Connections.SendImage(family_id, "FAMILY", "", imgLoc);
            if (response.Contains("OK"))
            {
                MessageBox.Show("Image uploaded succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("I'm sorry, but this image is problematic for me to save. Please go ahead and save another one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
