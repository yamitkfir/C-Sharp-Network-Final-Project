using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    public static class ConnectionsTat
    {
        public static string CreateMessage(string data)
        {
            Sqling sqli = new Sqling();
            // CASES OF MESSAGES AND REPSONCE IN ACCORDANCE

            string send = "";
            if (data.Contains("LOGIN"))
            {
                try
                {
                    string[] split = data.Split(' ');
                    string account1 = split[1];
                    //string password1 = split[3].Substring(0, split[3].Length - 5);
                    string password1 = split[3];
                    send = sqli.LogIn(account1, password1);
                }
                catch(Exception e)
                {
                    send = e.ToString();
                }
            }
            else if (data.Contains("CREATEACCOUNT"))
            { // data example: CREATEACCOUNT:NAME: yamitk PASSWORD: 123 FAMILY: kfir
                try
                {
                    string[] split = data.Split('&');
                    string account1 = split[1];
                    string password1 = split[3];
                    string family1 = split[5];
                    send = sqli.CreateAccount(account1, password1, family1);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }
            else if (data.Contains("CREATEFAMILY"))
            {
                try
                {
                    string family1 = data.Split('&')[1];
                    send = sqli.CreateFamily(family1);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }
            else if (data.Contains("CREATEANIMAL"))
            {
                try
                {
                    string[] split = data.Split(' ');
                    int family1 = int.Parse(split[1]);
                    string type = data.Split('&')[3];
                    string name = data.Split('&')[1];
                    send = sqli.CreateAnimal(family1, name, type);
                }
                catch (Exception e)
                {
                    send = e.ToString();
                }
            }
            
            else if (data.Contains("TELL_ME_NUM_OF_ANIMALS"))
            {
                try
                {
                    string[] split = data.Split(' ');
                    string family = split[1];
                    send = sqli.NumberAnimals(family);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }
            else if (data.Contains("WHICH_FAMILY"))
            {
                try
                {
                    string[] split = data.Split(' ');
                    string name = split[2];
                    string password = split[4];
                    send = sqli.WhichFamily(name, password);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }
            else if (data.Contains("FAMILY_NAME"))
            {
                try
                {
                    string temp = sqli.FamilyName(data.Split(' ')[2]);
                    send = temp.Remove(temp.Length - 5).Trim();
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }
            else if (data.Contains("ANIMAL_NAMES"))
            {
                try
                {
                    send = sqli.AnimalNames(data.Split(' ')[2]);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }

            else if (data.Contains("INFO_FAMILY"))
            {
                try
                {
                    int family_id = int.Parse(data.Split(' ')[2]);
                    send = sqli.infoFamily(family_id);

                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }
            else if (data.Contains("INFO_USER"))
            {
                try
                {
                    int family_id = int.Parse(data.Split(' ')[2]);
                    string user = data.Split(' ')[4];
                    send = sqli.infoUser(family_id, user);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }
            else if (data.Contains("INFO_ANIMAL"))
            {
                try
                {
                    int family_id = int.Parse(data.Split(' ')[2]);
                    string animal = data.Split(' ')[4];
                    send = sqli.infoAnimal(family_id, animal);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }
            }

            else if (data.Contains("UPDATE_USER"))
            {
                // "UPDATE_USER FAMILY_ID: " + family1 + "USER: " + account1 + " &" + name + "&" + newage + "&" + gender
                try
                {
                    int family_id = int.Parse(data.Split(' ')[2]);
                    string oldname = data.Split(' ')[4];
                    string newname = data.Split('&')[1];
                    int newAge = int.Parse(data.Split('&')[2]);
                    string newGender = "";
                    newGender = data.Split('&')[3].Split(' ')[1];
                    send = sqli.updateUser(family_id, oldname, newname, newAge, newGender);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }

            }
            else if (data.Contains("UPDATE_ANIMAL"))
            {
                // UPDATE_ANIMAL FAMILY_ID: " + family1 + " ANIMALNAME: " + name + "&" + age + "&" + newgender + "&" + lastimeaten + "&" + 
                // howoftenfood + "&" + leastimeout + "&" + howoftenout + "&" + type + "&" + needsfood and needsout + othernotes + "&"
                
                try
                {
                    String[] datas = data.Split('&'); 
                    int family_id = int.Parse(data.Split(' ')[2]);
                    string animalname = data.Split(' ')[4];

                    string age = datas[1];
                    string newgender = "";
                    if (datas[2] == "Female")
                    {
                        newgender = "Female";
                    }
                    else if (datas[2] == "Male")
                    {
                        newgender = "Male";
                    }
                    else
                    {
                        newgender = "";
                    }
                    string lastimeaten = datas[4];
                    string howoftenfood = datas[5];
                    string lastimeout = datas[6];
                    string howoftenout = datas[6];
                    string type = datas[7];
                    string othernotes="";
                    try
                    {
                        othernotes = datas[8].Remove(datas[8].Length - 6);
                    }
                    catch
                    {
                        ;
                    }

                    send = sqli.updateAnimal(family_id, animalname, age, newgender, lastimeaten, howoftenfood, lastimeout, howoftenout, type, othernotes);
                }
                catch (Exception e)
                {
                    send = "ERROR connectionsTAT";
                }

            }
            

            else if (data.Contains("GET_NOTES"))
            {
                int family_id = int.Parse(data.Split(' ')[1]);
                send = sqli.GetNotes(family_id);
            }
            else if (data.Contains("EDIT_NOTE")) // for animal
            {
                int family_id = int.Parse(data.Split(' ')[1]);
                string note = data.Split(' ')[2];
                string animalname = data.Split(' ')[3];
                send = sqli.EditNotes(family_id, animalname, note);
            }
            else if (data.Contains("REMOVE_NOTE")) // for family
            {
                int family_id = int.Parse(data.Split(' ')[1]);
                string note = data.Split('&')[1];
                send = sqli.RemoveNote(family_id, note);
            }
            else if (data.Contains("ADD_NOTE"))
            {
                int family_id = int.Parse(data.Split(' ')[1]);
                string note = data.Split('&')[1];
                send = sqli.AddNote(family_id, note);
            }
            else if (data.Contains("DELETEANIMAL"))
            {
                int family_id = int.Parse(data.Split(' ')[1]);
                string animalname = data.Split(' ')[2];
                //send = sqli.DelAnimal(family_id, animalname);
            }

            /*else if (data.Contains("esc"))
            {
                Connections.CloseConnection();
                string name_left = "whoareyou";
                if (!data.Split(' ')[0].Equals("esc")) name_left = data.Split(' ')[0]; // will return the name of client disconnected 
                send =  name_left + " esc CONNECTIONSLEFT:" + Connections.num_of_connections.ToString(); 
            }
            */
            return send;
        }
    }
}
