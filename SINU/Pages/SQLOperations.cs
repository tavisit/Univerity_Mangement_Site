using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SINU.Pages
{
    public class SQLOperations
    {
        public struct SubjectGrades
        {
            public string name { get; set; }
            public string surname { get; set; }
            public string subjectName { get; set; }
            public int credits { get; set; }
            public int grade { get; set; }
            public SubjectGrades(string name, string surname, string subjectName, int credits, int grade)
            {
                this.name = name;
                this.surname = surname;
                this.subjectName = subjectName;
                this.credits = credits;
                this.grade = grade;
            }
            public override string ToString() => $"{surname} {name} scored at {subjectName} with credits {credits} the grade {grade}\n";
        }
        public static string connectionString = "Data Source=DESKTOP-FRA27F6\\SQLEXPRESS;Initial Catalog=Project_Website;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static SINU.User GetUserByEmail(string conditions)
        {
            User myProfileTemp = new User();
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
                myProfileTemp.username = dt.Rows[0][1].ToString().Replace(" ", "");
                myProfileTemp.password = dt.Rows[0][2].ToString().Replace(" ", "");
                myProfileTemp.surname = dt.Rows[0][3].ToString().Replace(" ", "");
                myProfileTemp.lastname = dt.Rows[0][4].ToString().Replace(" ", "");
                myProfileTemp.email = dt.Rows[0][7].ToString().Replace(" ", "");
                if (dt.Rows[0][6].ToString() == "")
                    myProfileTemp.photo_url = "";
                else
                    myProfileTemp.photo_url = (string)dt.Rows[0][6].ToString().Replace(" ", "");
                if (dt.Rows[0][5].ToString() == "")
                    myProfileTemp.birth_date = DateTime.Parse("");
                else
                    myProfileTemp.birth_date = DateTime.Parse(dt.Rows[0][5].ToString());

                con = new SqlConnection(connectionString);
                sqlConn = new SqlCommand("Select Univ_info.specialization from Univ_info Join Student on Student.info = Univ_info.id where Student.id = @id", con);
                sqlConn.Parameters.AddWithValue("@id", myProfileTemp.id);
                sda = new SqlDataAdapter(sqlConn);
                dt = new DataTable();

                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    myProfileTemp.Student = new Student
                    {
                        Univ_info = new Univ_info()
                    };
                    myProfileTemp.Student.Univ_info.specialization = dt.Rows[0][0].ToString().Replace(" ", "");
                    // get info about the student
                }
                else
                {
                    myProfileTemp.Employee = new Employee
                    {
                        Teacher = new Teacher(),
                        Working_staff = new Working_staff()
                    };
                    //get info about the teacher/working
                }
            }
            return myProfileTemp;
        }
        public static List<SubjectGrades> getSubjectsByIdStudent(SINU.User myUser)
        {
            List<SubjectGrades> mySubjects = new List<SubjectGrades>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("Select Subjects.name, Subjects.credits, Subjects_list.grade from Subjects_list " +
                                                "JOIN Subjects on Subjects_list.id_subject = Subjects.id " +
                                                "JOIN Users on Users.id = Subjects_list.id_student " +
                                                "where Users.id = @id Order by grade desc", con);
            sqlConn.Parameters.AddWithValue("@id", myUser.id);
            SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            con.Open();
            sqlConn.ExecuteNonQuery();
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0] == null)
                {
                    dt.Rows[i][0] = "";
                }
                if (dt.Rows[i][1] == null)
                {
                    dt.Rows[i][1] = 0;
                }
                if (dt.Rows[i][2] == null)
                {
                    dt.Rows[i][2] = 0;
                }
                if (dt.Rows[i][2].ToString() == "")
                {
                    dt.Rows[i][2] = 0;
                }

                SubjectGrades currentSubjects = new SubjectGrades(myUser.lastname, myUser.surname, dt.Rows[i][0].ToString().Replace(" ", ""), (int)dt.Rows[i][1], (int)dt.Rows[i][2]);
                mySubjects.Add(currentSubjects);
            }
            return mySubjects;
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
                        "WHERE id =" + profile.id.ToString(), con);

                    command.Parameters.AddWithValue("@username", profile.username);
                    command.Parameters.AddWithValue("@password", profile.password);
                    command.Parameters.AddWithValue("@surname", profile.surname);
                    command.Parameters.AddWithValue("@lastname", profile.lastname);
                    if (profile.birth_date.ToString() == "")
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