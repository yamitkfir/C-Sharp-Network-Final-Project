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
    public partial class Home : Window
    {
        private string account1;
        private string password1;
        private int family1_ID;
        private int num_of_animals1;
        private string[] animalnames = new string[9];
        private string[] animalIMGlocations = new string[9];
        private string temp;
        Button[] animals;
        Image[] images;
        Label[] names;

        public Home(string acc, string pass)
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight) - 30;
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth) - 20;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            this.Closing += Window_Closing;
            temp = "";

            this.account1 = acc;
            this.password1 = pass;

            animals = new Button[] { animal1, animal2, animal3, animal4, animal5, animal6, animal7, animal8, animal9 };
            images = new Image[] { image1, image2, image3, image4, image5, image6, image7, image8, image9 };
            names = new Label[] { nameanimal1, nameanimal2, nameanimal3, nameanimal4, nameanimal5, nameanimal6, nameanimal7, nameanimal8, nameanimal9 };
            
            string str_Family_ID = Connections.SendRequest("WHICH_FAMILY NAME= " + acc + " PASSWORD: " + pass + " ");
            str_Family_ID = Connections.SendRequest("WHICH_FAMILY NAME= " + acc + " PASSWORD: " + pass + " ");

            try
            {
                this.family1_ID = int.Parse(str_Family_ID.Split(' ')[0]);
                title.Content = "Welcome to the server, " + acc + "! :) your family ID is " + this.family1_ID.ToString();
            }
            catch
            {
                title.Content = "Welcome to the server, " + acc + "! :) there was a problem finding your family: " + str_Family_ID;
            }

            nameuser.Content = account1;
            namefamily.Content = Connections.SendRequest("FAMILY_NAME ID: " + family1_ID).Split(' ')[0];
            string animalnaimes = "";
            try
            {
                animalnaimes = Connections.SendRequest("ANIMAL_NAMES FAMILY_ID: " + family1_ID);
                if (!animalnaimes.Contains("ERROR"))
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (animalnaimes.Split('&')[i].Contains("<EOF>"))
                        {
                            break;
                        }
                        animalnames[i] = animalnaimes.Split('&')[i].Trim();
                    }
                }
            }
            catch
            {
                string temp = Connections.reOpenConnection();
                Thread.Sleep(2000);

                try
                {
                    namefamily.Content = Connections.SendRequest("FAMILY_NAME ID: " + family1_ID).Split(' ')[0];
                    str_Family_ID = Connections.SendRequest("WHICH_FAMILY NAME= " + acc + " PASSWORD: " + pass + " ");
                    
                    this.family1_ID = int.Parse(str_Family_ID.Split(' ')[0]);
                    title.Content = "Welcome to the server, " + acc + "! :) your family ID is " + this.family1_ID.ToString();
                    animalnaimes = Connections.SendRequest("ANIMAL_NAMES FAMILY_ID: " + family1_ID);
                    if (!animalnaimes.Contains("ERROR"))
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (animalnaimes.Split('&')[i].Contains("<EOF>"))
                            {
                                break;
                            }
                            animalnames[i] = animalnaimes.Split('&')[i].Trim();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.ToString());
                    ;
                    Connections.CloseConnection();
                    Environment.Exit(0);
                }
            }

            string str_num_of_animals = Connections.SendRequest("TELL_ME_NUM_OF_ANIMALS " + this.family1_ID + " ");
            this.num_of_animals1 = 0;
            this.num_of_animals1 = int.Parse(str_num_of_animals.Split(' ')[0]);
            title2.Content = "you have " + this.num_of_animals1.ToString() + " animals";

            string family_name = Connections.SendRequest("FAMILY_NAME ID: " + family1_ID + " ").Split(' ')[0];

            temp = Connections.newRequestImage(family1_ID, "USER", this.account1, family_name).Split(' ')[0];


            if (!temp.Contains("NO"))
            {
                File.Delete(temp.Remove(temp.Length - 4)+"2.jpg");
                File.Copy(temp, temp.Remove(temp.Length - 4) + "2.jpg");
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(temp);
                bitmap.EndInit();
                userimage.Source = bitmap;
            }

            temp = Connections.newRequestImage(family1_ID, "FAMILY", "", family_name).Split(' ')[0];
            if (!temp.Contains("NO"))
            {
                File.Delete(temp.Remove(temp.Length - 4) + "2.jpg");
                File.Copy(temp, temp.Remove(temp.Length - 4) + "2.jpg");
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(temp);
                familyimage.Source = bitmap;
                bitmap.EndInit();
            }

            temp = Connections.SendRequest("GET_NOTES " + family1_ID + " " + acc + " ");
            if (temp.Contains("%"))
            {
                String[] notes = temp.Split('%');
                for (int i=0; i<notes.Length-1; i++)
                {
                    MessageBoxResult result = MessageBox.Show(notes[i]+". Did you do it?", "An animal of yours needs you", MessageBoxButton.YesNo);  
                    if (result == MessageBoxResult.Yes) {
                        string response = Connections.SendRequest("REMOVE_NOTE " + family1_ID + " &" + notes[i] + "%&");
                        if (!response.Contains("OK"))
                        {
                            MessageBox.Show(response);
                        }
                    }
                }

            } // else: no notes to show

            for (int i = 0; i<=num_of_animals1-1; i++) 
            {
                // initiating animals buttuns, images and names
                animals[i].IsEnabled = true;
                names[i].Content = animalnames[i];
                animalIMGlocations[i] = Connections.newRequestImage(family1_ID, "ANIMAL", this.animalnames[i], family_name);
                if (animalIMGlocations[i]!=null && !animalIMGlocations[i].Contains("NO"))
                {
                    try
                    {
                        File.Delete(animalIMGlocations[i].Remove(animalIMGlocations[i].Length - 4) + "2.jpg");
                        File.Copy(animalIMGlocations[i], animalIMGlocations[i].Remove(animalIMGlocations[i].Length - 4) + "2.jpg");
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(animalIMGlocations[i], UriKind.Absolute);
                        bitmap.EndInit();
                        images[i].Source = bitmap;
                        bitmap.UriSource = null;
                        bitmap = null;
                    }
                    catch { }
                   
                }
            }


        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            EditUser edit_user = new EditUser(this.family1_ID, this.account1, this);
            edit_user.Show();
        }
        public void RecieveEditedUserFromWindow(string newname)
        {
            nameuser.Content = newname;
        }

        private void EditAnimal_Click(object sender, RoutedEventArgs e)
        {
            // I made this function general, it works on all 7 buttons of animals.
            Button temp = (Button)sender;
            int animal_number = int.Parse(temp.Name.Substring(temp.Name.Length - 1));
            string animalname = animalnames[animal_number - 1];
            Animal animal_page = new Animal(this.family1_ID, animalname, this);
            animal_page.Show();
        }

        private void NewAnimal_Click(object sender, RoutedEventArgs e)
        {
            CreateAnimal create_animal = new CreateAnimal(this.family1_ID);
            create_animal.Show();
        }

        private void EditFamily_Click(object sender, RoutedEventArgs e)
        {
            Family family = new Family(this.family1_ID);
            family.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //added this on every window so the x button will end the program running
        {
            Connections.SendRequest(account1 + " esc2 <EOF>");
            System.Windows.Application.Current.Shutdown();
        }

        private void bye(object sender, RoutedEventArgs e)
        {
            Connections.SendRequest(account1 + " esc2 <EOF>");
            System.Windows.Application.Current.Shutdown();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Thread.Sleep(3000);
            String temp = Connections.reOpenConnection();

            if (temp.Equals("ok"))
            {
                Home home2 = new Home(this.account1, this.password1);
                Thread.Sleep(5000);
                home2.Visibility = Visibility.Visible;
            }
        }

        /*
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Login login = new Login();
            login.Show();
        }
        */
    }
}
