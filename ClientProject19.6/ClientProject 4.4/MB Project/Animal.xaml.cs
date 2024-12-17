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
    public partial class Animal : Window
    {
        // TDDO add remove Button
        private int family1;
        private string animalname;
        private string imgLoc;
        private Home HomeWindow; // so i can send info to it when this window is closed
        private Boolean isChanged;
        private string[] info;
        private Boolean oldfood, oldout;

        public Animal(int Family_ID, string animalname, Home home)
        {
            InitializeComponent();
            this.animalname = animalname;
            this.family1 = Family_ID;
            this.HomeWindow = home;
            this.isChanged = false;

            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight) - 40;
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth) - 40;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            try
            {
                family.Content = Connections.SendRequest("FAMILY_NAME FAMILY_ID: " + family1);
            }
            catch
            {
                // TODO restart connection

            }

            family.Content = family.Content.ToString().Remove(family.Content.ToString().Length - 6).Trim();
            string imgLoc = Connections.RequestImage(Family_ID, "ANIMAL", animalname, family.ContentStringFormat);
            if (!imgLoc.Contains("NO"))
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imgLoc.Remove(imgLoc.Length - 4) + "2.jpg");
                    bitmap.EndInit();
                    animalimage.Source = bitmap;
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
                        animalimage.Source = bitmap;
                    }
                    catch
                    {
                        ;
                    }
                }
            }
            info = Connections.SendRequest("INFO_ANIMAL FAMILY: " + Family_ID + " NAME: " + animalname + " ").Split('&');
            // info = name, age, gender, lastimeeaten, lastimeeaten, lastimewent, howoftenout, type, othernotes

            name.Content = info[0];
            age.Text = info[1];
            if (info[2].Contains("Female")) gender.SelectedIndex = 0;
            if (info[2].Contains("Male")) gender.SelectedIndex = 1;
            else gender.SelectedIndex = 2;
            lastimeaten.Text = info[3].Trim();
            howoteneats.Text = info[4].Trim();
            lastimewent.Text = info[5].Trim();
            howoftenout.Text = info[6].Trim();
            try
            {
                if (info[7] != "") type.SelectedValue = int.Parse(info[7]);
            }
            catch
            {
                type.SelectedIndex = 3;
            }
            othernotes.Text = info[8].Split('%')[2];
            type.SelectedIndex = int.Parse(info[7].Trim());
            if (info[8].Split('%')[0].Contains("Y"))
            {
                needsfood.IsChecked = true;
            }
            if (info[8].Split('%')[1].Contains("Y"))
            {
                needsout.IsChecked = true;
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

                    ImageSource imageSource = new BitmapImage(new Uri(imgLoc));
                    animalimage.Source = imageSource;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ":" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string response = Connections.SendImage(family1, "ANIMAL", animalname, imgLoc);
            if (response.Contains("OK"))
            {
                MessageBox.Show("Image uploaded succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.isChanged = true;
            }
            else
            {
                MessageBox.Show("I'm sorry, but this image is problematic for me to save. Please go ahead and save another one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Saveall_Click(object sender, RoutedEventArgs e)
        {
            string newage, howoftenfood2, howoftenout2;
            try
            {
                 newage = int.Parse(age.Text).ToString();
            }
            catch
            {
                if (age.Text != "")
                {
                    MessageBox.Show("don't try to save a non-number at age", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else { newage = ""; }
            }
            try
            {
                howoftenfood2 = int.Parse(howoteneats.Text).ToString();
            }
            catch
            {
                if (howoteneats.Text != "")
                {
                    MessageBox.Show("don't try to save a non-number at how often eats", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else { howoftenfood2 = ""; }
            }
            try
            {
                howoftenout2 = int.Parse(howoftenout.Text).ToString();
            }
            catch
            {
                if (howoftenout.Text != "")
                {
                    MessageBox.Show("don't try to save a non-number at how often out", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else { howoftenout2 = ""; }
            }

            try
            {
                string needsfood2="N", needsout2="N";
                if (needsfood.IsChecked == true)
                {
                    needsfood2 = "Y";
                }
                if (needsout.IsChecked == true)
                {
                    needsout2 = "Y";
                }
                string del = type.SelectedIndex.ToString();
                ;
                string response = Connections.SendRequest("UPDATE_ANIMAL FAMILY_ID: "+family1+" NAME: "+animalname +" &" + newage.ToString()+"&"+gender.SelectedItem.ToString().Substring(38)+"&"+lastimeaten.Text+"&"+ howoftenfood2 + "&"+lastimewent.Text+"&"+howoftenout2+"&"+type.SelectedIndex.ToString()+"&FOOD:"+ needsfood2 + "%OUT:"+ needsout2 + "%"+othernotes.Text+"%");
                if (response.Contains("OK"))
                {
                    isChanged = true;
                    MessageBox.Show("changes made succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // adding notes to the family:
                    if (needsfood.IsChecked == true && oldfood == false)
                    {
                        //Connections.SendRequest("ADD_NOTE " + family1 + " NOTE: &" + animalname + " needs food%");
                    }
                    if (needsout.IsChecked == true && oldout == false)
                    {
                        //Connections.SendRequest("ADD_NOTE " + family1 + " NOTE: &" + animalname + " needs to go out%");
                    }
                    // TODO removing notes from the family:
                    if (needsfood.IsChecked == false && oldfood == true)
                    {
                        //Connections.SendRequest("REMOVE_NOTE " + family1 + " NOTE: &" + animalname + " needs food%");
                    }
                    if (needsout.IsChecked == false && oldout == true)
                    {
                        //SendRequest("REMOVE_NOTE " + family1 + " NOTE: &" + animalname + " needs to go out%");
                    }
                }
                else
                {
                    MessageBox.Show("problem saving changes: " + response, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        /*
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("changes you made will be visible next time you sign in", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Visibility = Visibility.Hidden;
        }
        */
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("changes you made (and saved) will be visible next time you sign in", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Visibility = Visibility.Hidden;
        }
    }
}
