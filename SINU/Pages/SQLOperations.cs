using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SINU.Pages
{
    public class SQLOperations
    {
        public static string connectionString = "Data Source=DESKTOP-FRA27F6\\SQLEXPRESS;Initial Catalog=Project_Website;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static SINU.User GetUserByEmail(string conditions)
        {
            SINU.User myProfileTemp = new SINU.User();
            myProfileTemp.id = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("select * from Users where email LIKE @email", con);
            sqlConn.Parameters.AddWithValue("@email", conditions + " %");
            SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            con.Open();
            int i = sqlConn.ExecuteNonQuery();
            con.Close();

            if (dt.Rows.Count > 0)
            {
                myProfileTemp.id = (int)dt.Rows[0][0];
                myProfileTemp.username = (string)dt.Rows[0][1];
                myProfileTemp.password = (string)dt.Rows[0][2];
                myProfileTemp.surname = (string)dt.Rows[0][3];
                myProfileTemp.lastname = (string)dt.Rows[0][4];
                myProfileTemp.email = (string)dt.Rows[0][7];
                if (dt.Rows[0][6].ToString() == "")
                    myProfileTemp.photo_url = "";
                else
                    myProfileTemp.photo_url = (string)dt.Rows[0][6];
            }
            return myProfileTemp;
        }

        public static bool DeleteByID(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand command;
                    if (id >= 200000)//student
                    {
                        command = new SqlCommand("DELETE FROM Users WHERE id= @id", con);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        
                        
                    }
                    else//teacher
                    {
                        SqlCommand cmd_email = new SqlCommand("select * from Users where id<200000 and id<>@id", con);
                        cmd_email.Parameters.AddWithValue("@id", id);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd_email);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        cmd_email.ExecuteNonQuery();

                        if (dt.Rows.Count > 0)
                        {
                            command = new SqlCommand("UPDATE Department SET id_teachers = @new_head where id_teachers = " + id.ToString(), con);
                            command.Parameters.AddWithValue("@new_head", dt.Rows[0][0]);
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new Exception();
                        }
                        command = new SqlCommand("DELETE FROM Series WHERE id_head_teacher= @id", con);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                        command = new SqlCommand("DELETE FROM Subjects_list WHERE id_teacher= @id", con);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                        command = new SqlCommand("DELETE FROM Users WHERE id= @id", con);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public static void UpdateById(int id, string newThing, string oldThing, string columnName)
        {

        }

        public static bool UpdateUser(SINU.User profile)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand command;

                    command = new SqlCommand("UPDATE Users SET " +
                        "username = @username, " +
                        "password = @password, " +
                        "surname = @surname, " +
                        "lastname = @lastname, " +
                        "birth_date = @birth_date," +
                        "photo_url = @photo_url " +
                        "WHERE id ="+profile.id.ToString(), con);

                    command.Parameters.AddWithValue("@username", profile.username);
                    command.Parameters.AddWithValue("@password", profile.password);
                    command.Parameters.AddWithValue("@surname", profile.surname);
                    command.Parameters.AddWithValue("@lastname", profile.lastname);
                    if (profile.birth_date == null)
                    {
                        profile.birth_date = new DateTime();
                        profile.birth_date = DateTime.Now.Date;
                    }
                        
                    command.Parameters.AddWithValue("@birth_date", profile.birth_date);

                    if (profile.photo_url == null)
                        profile.photo_url = "";
                    command.Parameters.AddWithValue("@photo_url", profile.photo_url);

                    command.ExecuteNonQuery();


                    con.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void DeleteByCriteria(string criteria, string columnName)
        {

        }
    }
}