using System;
using System.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace ServerProject
{
    public class Sqling
    {
        private DBAccess objectdbAcess = new DBAccess();
        private string returni;
        private static string DEFAULT_LOC = "C:\\yk2020";
        //private static string DEFAULT_LOC = "C:\\Users\\Teacher\\Documents\\yserver";


        public string CreateAccount(string username, string password1, string family)
        {
            returni = "OK";
            //inserting the new info in the database
            DataTable dtFamily = new DataTable();
            string query = "Select * from Family Where name= '" + family + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtFamily);
            if (dtFamily.Rows.Count != 0) //making sure there is a family with this name
            {
                int Family_ID = 0;
                Family_ID = int.Parse(dtFamily.Rows[0]["Family_ID"].ToString());
                Console.Out.WriteLine("Familt_ID = " + Family_ID.ToString());
                SqlCommand insertCommand = new SqlCommand("insert into Users(Name,Password,Family_ID,image) values(@username, @password, @family, @image)");
                insertCommand.Parameters.AddWithValue("@username", username); //securing the data sent
                insertCommand.Parameters.AddWithValue("@password", password1);
                insertCommand.Parameters.AddWithValue("@family", Family_ID);
                insertCommand.Parameters.AddWithValue("@image", DEFAULT_LOC + "\\" + Family_ID.ToString() + "\\USERS\\" + username + ".jpg");
                int row = objectdbAcess.executeQuery(insertCommand);
                if (row == 1)
                {
                    SqlCommand updateCommand = new SqlCommand("UPDATE Family SET Members = Members + 1 WHERE Family_ID = @ParameterID");
                    updateCommand.Parameters.AddWithValue("@ParameterID", Family_ID);
                    int row2 = objectdbAcess.executeQuery(updateCommand);
                    if (row2 == 1)
                    {
                        returni = "OK";
                    }
                    else
                    {
                        returni = "problem adding a member to the family";
                    }
                }
                else
                {
                    returni = "problem creating this account";
                }
            }
            else
            {
                returni = "this family doesn't exist in the system yet. Add it to the system and try again.";
            }

            objectdbAcess.closeConn();
            return returni;
        }

        public string LogIn(string username, string password1)
        {
            DataTable dtUsers = new DataTable();

            string query = "Select * from Users Where name= '" + username + "' AND password= '" + password1 + "'";
            objectdbAcess.readDatathroughAdapter(query, dtUsers);
            if(dtUsers.Rows.Count != 0) // that means there is a user with this information
            {
                returni = "OK";
            }
            else
            {
                returni = "Username and or password are wrong. Please try again.";
            }
            objectdbAcess.closeConn();
            return returni;
        }

        public string CreateFamily(string name)
        {
            DataTable dtFamily = new DataTable();

            string query = "Select * from Family Where name= '" + name + "'";
            objectdbAcess.readDatathroughAdapter(query, dtFamily);
            if (dtFamily.Rows.Count == 0) // that means there is no such family with same name already
            {
                try
                {
                    SqlCommand insertCommand = new SqlCommand("insert into Family(Name,Members,Animals) values(@name, @members, @animals)");
                    insertCommand.Parameters.AddWithValue("@name", name); //securing the data sent
                    insertCommand.Parameters.AddWithValue("@members", 0);
                    insertCommand.Parameters.AddWithValue("@animals", 0);
                    int row = objectdbAcess.executeQuery(insertCommand);
                    if (row == 1)
                    {
                        
                        // get the family id so the server will create folder for its images
                        query = "Select * from Family Where name= '" + name + "'";
                        objectdbAcess.readDatathroughAdapter(query, dtFamily);
                        if (dtFamily.Rows.Count != 0) //making sure there is a family with this name
                        {
                            int Family_ID = int.Parse(dtFamily.Rows[0]["Family_ID"].ToString());
                            Console.Out.WriteLine("108: " + Family_ID);
                            DirectoryInfo di1 = Directory.CreateDirectory(DEFAULT_LOC + "\\" + Family_ID.ToString());
                            DirectoryInfo di2 = Directory.CreateDirectory(DEFAULT_LOC + "\\" + Family_ID.ToString() + "\\ANIMALS");
                            DirectoryInfo di3 = Directory.CreateDirectory(DEFAULT_LOC + "\\" + Family_ID.ToString() + "\\USERS");
                            if (di1.Exists && di2.Exists && di3.Exists)
                            {
                                SqlCommand updateCommand = new SqlCommand("UPDATE Family SET image = @image WHERE Family_ID = @ParameterID");
                                updateCommand.Parameters.AddWithValue("@image", DEFAULT_LOC + "\\" + Family_ID.ToString() + "\\" + name + ".jpg");
                                updateCommand.Parameters.AddWithValue("@ParameterID", Family_ID);
                                int row2 = objectdbAcess.executeQuery(updateCommand);
                                if (row2 == 1)
                                {
                                    returni = "OK";
                                }
                                else
                                {
                                    returni = "problem updating image source";
                                }
                            }
                            else returni = "problem creating directories";
                        }
                        else
                        {
                            returni = "problem creating family";
                        }
                        
                    }
                    else
                    {
                        returni = "problem in 122 sqling";
                    }
                }
                catch (Exception e)
                {
                    returni = e.Message;
                }
            }
            else
            {
                returni = "family already exists. try again.";
            }
            objectdbAcess.closeConn();
            return returni;
        }
        
        public string CreateAnimal(int family_id, string name, string type) 
        {
            SqlCommand insertCommand = new SqlCommand("insert into Animal(Name,Family_ID,type,image,other_notes) values(@name, @family, @type, @image, @othernotes)");
            insertCommand.Parameters.AddWithValue("@name", name); //securing the data sent
            insertCommand.Parameters.AddWithValue("@family", family_id);
            insertCommand.Parameters.AddWithValue("@type", type);
            insertCommand.Parameters.AddWithValue("@image", DEFAULT_LOC + "\\" + family_id.ToString() + "\\ANIMALS\\" + name + ".jpg");
            insertCommand.Parameters.AddWithValue("@othernotes", "FOOD:N%OUT:N%");

            int row = objectdbAcess.executeQuery(insertCommand);
            if (row == 1)
            {
                // adding the new animal to its family
                SqlCommand updateCommand = new SqlCommand("UPDATE Family SET animals = animals+1 WHERE Family_ID = @ParameterID");
                updateCommand.Parameters.AddWithValue("@ParameterID", family_id);
                int row2 = objectdbAcess.executeQuery(updateCommand);
                if (row2 == 1)
                {
                    returni = "OK";
                }
                else
                {
                    returni = "ERROR 172";
                }
            }
            else
            {
                returni = "ERROR 177";
            }
            return returni;
        }

        public string NumberAnimals(string family_ID)
        {
            DataTable dtFamilys = new DataTable();
            string query = "Select * from Family Where Family_ID= '" + family_ID + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtFamilys);
            if (dtFamilys.Rows.Count != 0) //making sure there is a family with this name
            {
                returni = dtFamilys.Rows[0]["animals"].ToString();
            }
            else
            {
                returni = "error occured. family doesnt exist.";
            }
            return returni;
        }
        public string AnimalNames(string family_ID)
        {
            DataTable dtAnimals = new DataTable();
            string query = "Select * from Animal Where Family_ID= '" + family_ID + "'";
            objectdbAcess.readDatathroughAdapter(query, dtAnimals);
            if (dtAnimals.Rows.Count != 0) //making sure there is a family with this name
            {
                for (int i=0; i<dtAnimals.Rows.Count; i++)
                {
                    returni = returni + dtAnimals.Rows[i]["name"].ToString() + "&";
                }
            }
            else
            {
                returni = "ERROR";
            }
            return returni;
        }

        public string WhichFamily(string user, string password) //generates family_id from user information
        {
            DataTable dtUsers = new DataTable();
            string query = "Select * from Users Where name= '" + user + "' AND password= '" + password + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtUsers);
            if (dtUsers.Rows.Count != 0)
            {
                returni = dtUsers.Rows[0]["Family_ID"].ToString() + " ";
            }
            else
            {
                returni = "ERROR";
            }
            return returni;
        }

        public string FamilyName(string id)
        {
            DataTable dtUsers = new DataTable();
            string query = "Select * from Family Where Family_ID= '" + id + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtUsers);
            if (dtUsers.Rows.Count != 0)
            {
                returni = dtUsers.Rows[0]["name"].ToString();
            }
            else
            {
                returni = "error occured. there is no family with this id";
            }
            return returni;
        }

        /* TODO
        public string DelAnimal(int id, string animalname)
        {
            DataTable dtAnimal = new DataTable();
            string query = "Select * from Animal Where Family_ID= '" + id + "' AND name= '" + animalname + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtAnimal);
            if (dtAnimal.Rows.Count != 0)
            {
                returni = dtAnimal.Rows[0]["name"].ToString();
            }
            else
            {
                returni = "error occured. there is no family with this id";
            }
            return returni;
        }
        */

        public string updateUser(int family_id, string oldname, string newname, int age, string gender)
        {
            // TODO handle nulls
            SqlCommand updateCommand = new SqlCommand("UPDATE Users SET name = @name, age = @age, gender = @gender WHERE Family_ID = @family_ID AND name = @oldname");
            updateCommand.Parameters.AddWithValue("@name", newname);
            updateCommand.Parameters.AddWithValue("@age", age);
            updateCommand.Parameters.AddWithValue("@gender", gender);
            updateCommand.Parameters.AddWithValue("@family_ID", family_id);
            updateCommand.Parameters.AddWithValue("@oldname", oldname);

            int row2 = objectdbAcess.executeQuery(updateCommand);
            if (row2 == 1)
            {
                returni = "OK";
            }
            else
            {
                returni = "ERROR no user found.";
            }
            return returni;
        }
        public string updateAnimal(int family_id, string oldname, string age, string gender, string lastimeaten, string howoftenfood, string lastimeout, string howoftenout, string type, string othernotes)
        {
            try
            {
                string[] notes = infoAnimal(family_id, oldname).Split('&')[8].Split('%');
                if (othernotes.Contains("FOOD:Y") && notes[0].Contains("N"))
                {
                    returni = AddNote(family_id, oldname + " needs food%");
                }
                if (othernotes.Contains("FOOD:N") && notes[0].Contains("Y"))
                {
                    returni = RemoveNote(family_id, oldname + " needs food%");
                }
                if (othernotes.Contains("OUT:Y") && notes[1].Contains("N"))
                {
                    returni = AddNote(family_id, oldname + " needs to go out%");
                }
                if (othernotes.Contains("OUT:N") && notes[1].Contains("Y"))
                {
                    returni = RemoveNote(family_id, oldname + " needs to go out%");
                }
                SqlCommand updateCommand = new SqlCommand("UPDATE Animal SET age = @age, gender = @gender, last_time_eaten = @eaten, how_often_food = @food, last_time_went = @went, how_often_out = @out, type = @type, other_notes = @other WHERE Family_ID = @family_ID AND name = @oldname");
                updateCommand.Parameters.AddWithValue("@age", age);
                updateCommand.Parameters.AddWithValue("@gender", gender);
                updateCommand.Parameters.AddWithValue("@eaten", lastimeaten);
                updateCommand.Parameters.AddWithValue("@food", howoftenfood);
                updateCommand.Parameters.AddWithValue("@went", lastimeout);
                updateCommand.Parameters.AddWithValue("@out", howoftenout);
                updateCommand.Parameters.AddWithValue("@type", type);
                updateCommand.Parameters.AddWithValue("@other", othernotes);
                updateCommand.Parameters.AddWithValue("@family_ID", family_id);
                updateCommand.Parameters.AddWithValue("@oldname", oldname);
                int row2 = objectdbAcess.executeQuery(updateCommand);
                if (row2 == 1)
                {
                    returni = "OK";
                }
                else
                {
                    returni = "ERROR no animal found.";
                }
                return returni;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        
        public string infoUser(int family_id, string name)
        {
            // familyname&password&age&gender
            DataTable dtUsers = new DataTable();
            string query = "Select * from Users Where Family_ID= '" + family_id + "' AND name= '" + name + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtUsers);
            
            if (dtUsers.Rows.Count != 0)
            {
                try
                {
                    returni = FamilyName(family_id.ToString()) +"&"+ dtUsers.Rows[0]["password"].ToString() +"&"+ dtUsers.Rows[0]["age"].ToString() +"&"+ dtUsers.Rows[0]["gender"].ToString() + "&" + dtUsers.Rows[0]["other_notes"].ToString() + '&';
                }
                catch (Exception e) // in case the user doesnt exist / it exists but doesnt have an image
                {
                    returni = e.ToString();
                }
            }
            else
            {
                returni = "";
            }
            return returni;
        }
        public string infoFamily(int family_id)
        {
            // familyname&members&animals&memberslist&animalslist
            DataTable dtFamily = new DataTable();
            string query = "Select * from Family Where Family_ID= '" + family_id + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtFamily);

            if (dtFamily.Rows.Count != 0)
            {
                try
                {
                    returni = FamilyName(family_id.ToString()) +"&"+ dtFamily.Rows[0]["members"].ToString() + "&" + dtFamily.Rows[0]["animals"].ToString() +"&";
                    // TODO add list of animals and list of members
                }
                catch (Exception e) // in case the animal doesnt exist / it exists but doesnt have an image
                {
                    returni = e.ToString();
                }
            }
            else
            {
                returni = "";
            }
            return returni;
        }
        public string infoAnimal(int family_id, string name)
        {
            // familyname&age&gender&eaten&food&went&out&type&othernotes
            DataTable dtAnimals = new DataTable();
            string query = "Select * from Animal Where Family_ID= '" + family_id + "' AND name= '" + name + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtAnimals);

            if (dtAnimals.Rows.Count != 0)
            {
                try
                {
                    // info = name, age, gender, lastimeeaten, howoftenfood, lastimewent, howoftenout, type, othernotes
                    returni = dtAnimals.Rows[0]["name"].ToString() + "&" + dtAnimals.Rows[0]["age"].ToString() + "&" + dtAnimals.Rows[0]["gender"].ToString() + "&" + dtAnimals.Rows[0]["last_time_eaten"].ToString() + "&" + dtAnimals.Rows[0]["how_often_food"].ToString() + "&" + dtAnimals.Rows[0]["last_time_went"].ToString() + "&" + dtAnimals.Rows[0]["how_often_out"].ToString() + "&" + dtAnimals.Rows[0]["type"].ToString() + "&" + dtAnimals.Rows[0]["other_notes"].ToString() +"&";
                }
                catch (Exception e) // in case the animal doesnt exist / it exists but doesnt have an image
                {
                    returni = e.ToString();
                }
            }
            else
            {
                returni = "";
            }
            return returni;
        }
        
        public string GetNotes(int family_id)
        {
            DataTable dtFamily = new DataTable();
            string query = "Select * from Family Where Family_ID= '" + family_id + "'";
            objectdbAcess.readDatathroughAdapter(query, dtFamily);

            if (dtFamily.Rows.Count != 0)
            {
                try
                {
                    returni = dtFamily.Rows[0]["other_notes"].ToString();
                }
                catch (Exception e)
                {
                    returni = e.ToString();
                }
            }
            else
            {
                returni = "";
            }
            return returni;
        }

        public string AddNote(int family_id, string note)
        {
            string notes = GetNotes(family_id) + note;

            SqlCommand updateCommand = new SqlCommand("UPDATE Family SET other_notes=@note WHERE Family_ID = @family_ID");
            updateCommand.Parameters.AddWithValue("@note", notes);
            updateCommand.Parameters.AddWithValue("@family_ID", family_id);

            int row2 = objectdbAcess.executeQuery(updateCommand);
            if (row2 == 1)
            {
                returni = "OK";
            }
            else
            {
                returni = "ERROR no family found.";
            }
            return returni;
        }
        public string RemoveNote(int family_id, string note)
        {
            string notes = GetNotes(family_id);

            notes = notes.Replace(note, "");

            SqlCommand updateCommand = new SqlCommand("UPDATE Family SET other_notes=@note WHERE Family_ID = @family_ID");
            updateCommand.Parameters.AddWithValue("@note", notes);
            updateCommand.Parameters.AddWithValue("@family_ID", family_id);

            int row2 = objectdbAcess.executeQuery(updateCommand);
            if (row2 == 1)
            {
                returni = "OK";

                // now remove from the animal
                int temp = note.IndexOf(" needs");
                string name = note.Remove(temp);

                string[] note2 = infoAnimal(family_id, name).Split('&')[8].Split('%');
                if (note.Contains("food"))
                {
                    note2[0] = "FOOD:N";
                }
                else // contains "out"
                {
                    note2[1] = "OUT:N";
                }
                string newnote = note2[0] + "%" + note2[1] + "%" + note2[2];

                updateCommand = new SqlCommand("UPDATE Animal SET other_notes=@note WHERE name = @name AND Family_ID = @family_ID");
                updateCommand.Parameters.AddWithValue("@note", newnote);
                updateCommand.Parameters.AddWithValue("@name", name);
                updateCommand.Parameters.AddWithValue("@family_ID", family_id);
                row2 = objectdbAcess.executeQuery(updateCommand);
                if (row2 == 1)
                {
                    returni = "OK";
                }
                else
                {
                    returni = "ERROR no family found.";
                }
            }
            else
            {
                returni = "ERROR no family found.";
            }
            return returni;
        }
        public string EditNotes(int family_id, string animalname, string note) // for animal 
        {
            // TODO
            return "";
        }


        public string getAnimalPath(int family_id, string name, Boolean create)
        {
            DataTable dtAnimals = new DataTable();
            string query = "Select * from Animal Where Family_ID= '" + family_id + "' AND name= '" + name + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtAnimals);
            
            if (dtAnimals.Rows.Count != 0)
            {
                try
                {
                    returni = dtAnimals.Rows[0]["image"].ToString();
                }
                catch (Exception e) // in case the animal doesnt exist / it exists but doesnt have an image
                {
                    /*
                    if (create)
                    {
                        returni = family_id.ToString() + "\\animals\\" + name + ".jpg";
                        SqlCommand updateCommand = new SqlCommand("UPDATE Animals SET image = @imagepath WHERE Family_ID = @ParameterID");
                        updateCommand.Parameters.AddWithValue("@ParameterID", family_id);
                        updateCommand.Parameters.AddWithValue("@imagepath", returni);
                        int row2 = objectdbAcess.executeQuery(updateCommand);
                        if (row2 != 1)
                        {
                            returni = "";
                        }
                        // else: path created successfully
                    }
                    else returni = "";
                    */
                    returni = "";
                }
            }
            else
            {
                returni = "";
            }

            return returni;
        }
        public string getUserPath(int family_id, string name, Boolean create)
        {
            DataTable dtUsers = new DataTable();
            string query = "Select * from Users Where Family_ID= '" + family_id + "' AND name= '" + name + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtUsers);

            if (dtUsers.Rows.Count != 0)
            {
                try
                {
                    returni = dtUsers.Rows[0]["image"].ToString();
                }
                catch (Exception e) // in case the animal doesnt exist / it exists but doesnt have an image
                {
                    if (create)
                    {
                        returni = family_id.ToString() + "\\users\\" + name + ".jpg";
                        SqlCommand updateCommand = new SqlCommand("UPDATE Users SET image = @imagepath WHERE Family_ID = @ParameterID AND Name = @name");
                        updateCommand.Parameters.AddWithValue("@ParameterID", family_id);
                        updateCommand.Parameters.AddWithValue("@name", name);
                        updateCommand.Parameters.AddWithValue("@imagepath", returni);
                        int row2 = objectdbAcess.executeQuery(updateCommand);
                        if (row2 != 1)
                        {
                            returni = "";
                        }
                        // else: path created successfully
                    }
                    else returni = "";
                }
            }
            else
            {
                returni = "";
            }

            return returni;
        }
        public string getFamilyPath(int family_id, Boolean create)
        {
            DataTable dtFamily = new DataTable();
            string query = "Select * from Family Where Family_ID= '" + family_id + "'";
            // שם בטלבלה חדשה את הפריט בדטאבייס משפחות ששמו זהה לשם המשפחה שהוכנס
            objectdbAcess.readDatathroughAdapter(query, dtFamily);

            if (dtFamily.Rows.Count != 0)
            {
                try
                {
                    returni = dtFamily.Rows[0]["image"].ToString();
                }
                catch (Exception e) // in case the animal doesnt exist / it exists but doesnt have an image
                {
                    if (create)
                    {
                        returni = family_id.ToString() + "\\" + family_id.ToString() + ".jpg";
                        SqlCommand updateCommand = new SqlCommand("UPDATE Family SET image = @imagepath WHERE Family_ID = @ParameterID");
                        updateCommand.Parameters.AddWithValue("@ParameterID", family_id);
                        updateCommand.Parameters.AddWithValue("@imagepath", returni);
                        int row2 = objectdbAcess.executeQuery(updateCommand);
                        if (row2 != 1)
                        {
                            returni = "";
                        }
                        // else: path created successfully
                    }
                    else returni = "";
                }
            }
            else
            {
                returni = "";
            }

            return returni;
        }
    }
}