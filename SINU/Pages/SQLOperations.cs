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
            public int id { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string subjectName { get; set; }
            public int credits { get; set; }
            public int grade { get; set; }
            public SubjectGrades(int id, string name, string surname, string subjectName, int credits, int grade)
            {
                this.id = id;
                this.name = name;
                this.surname = surname;
                this.subjectName = subjectName;
                this.credits = credits;
                this.grade = grade;
            }
            public override string ToString() => $"{surname} {name} scored at {subjectName} with credits {credits} the grade {grade}\n";
        }
        public static string connectionString = "Data Source=DESKTOP-FRA27F6\\SQLEXPRESS;Initial Catalog=Project_Website;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        #region Create information
        public static bool insertTeacher(User myUser, int type_of_teacher)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("insert into Users ([id], [username],[password],[surname],[lastname],[email],[photo_url]) " +
                                            "values(@id,@username,@password,@surname,@lastname,@email,@photo_url)", con);
            cmd.Parameters.AddWithValue("@email", myUser.email);
            cmd.Parameters.AddWithValue("@surname", myUser.surname);
            cmd.Parameters.AddWithValue("@lastname", myUser.lastname);
            cmd.Parameters.AddWithValue("@username", myUser.username);
            cmd.Parameters.AddWithValue("@password", myUser.password);
            cmd.Parameters.AddWithValue("@photo_url", myUser.photo_url);
            cmd.Parameters.AddWithValue("@id", myUser.id);

            con.Open();
            int rowsAdded = 0;
            try
            {
                rowsAdded = cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }


            if (rowsAdded > 0)
            {
                con.Open();
                cmd = new SqlCommand("Insert into Employees (id,salary, status, seniority) values (@id, @salary,@status,1)", con);
                cmd.Parameters.AddWithValue("@id", myUser.id);
                cmd.Parameters.AddWithValue("@salary", (type_of_teacher - 1) * 1500 + 3500);
                cmd.Parameters.AddWithValue("@status", type_of_teacher);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Insert into Teachers (id,job, publications,PhD,Phd_Certificate) values (@id,1,NULL,NULL,NULL)", con);
                cmd.Parameters.AddWithValue("@id", myUser.id);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    con.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool insertStudent(User myUser, int years, bool dorm)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("insert into Users ([id], [username],[password],[surname],[lastname],[email],[photo_url]) " +
                                            "values(@id,@username,@password,@surname,@lastname,@email,@photo_url)", con);
            cmd.Parameters.AddWithValue("@email", myUser.email);
            cmd.Parameters.AddWithValue("@surname", myUser.surname);
            cmd.Parameters.AddWithValue("@lastname", myUser.lastname);
            cmd.Parameters.AddWithValue("@username", myUser.username);
            cmd.Parameters.AddWithValue("@password", myUser.password);
            cmd.Parameters.AddWithValue("@photo_url", myUser.photo_url);
            cmd.Parameters.AddWithValue("@id", myUser.id);

            con.Open();
            int rowsAdded = 0;
            try
            {
                rowsAdded = cmd.ExecuteNonQuery();
            }
            catch
            {

                return false;
            }
            finally
            {
                con.Close();
            }
            if (rowsAdded > 0)
            {
                con.Open();
                //add to student table
                cmd = new SqlCommand("Insert into Student (id,year,info) values (@id," + years + ",1)", con);
                cmd.Parameters.AddWithValue("@id", myUser.id);
                cmd.ExecuteNonQuery();

                //add subjects to student
                SqlCommand cmd_search = new SqlCommand("select TOP 1 id from Subjects_list order by id desc", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd_search);
                DataTable dt = new DataTable();
                try
                {
                    sda.Fill(dt);
                    cmd_search.ExecuteNonQuery();
                }
                catch
                {

                    return false;
                }

                int nr_id_subjects_list = (int)dt.Rows[0][0] + 1;
                List<int> allSubjects = GetAllByCriteriaClass.getAllSubjectsIDByYear(years);
                List<int> allTeachers = GetAllByCriteriaClass.getAllTeachersID();

                for (int i = 0; i < Math.Min(allSubjects.Count, allTeachers.Count); i++)
                {
                    cmd = new SqlCommand("insert into Subjects_list ([id], [id_student],[id_subject],[grade],[id_teacher]) " +
                                         "values(@id,@id_student,@id_subject,NULL,@id_teacher)", con);
                    cmd.Parameters.AddWithValue("@id_student", myUser.id);
                    cmd.Parameters.AddWithValue("@id", ++nr_id_subjects_list);
                    cmd.Parameters.AddWithValue("@id_teacher", allTeachers[i]);
                    cmd.Parameters.AddWithValue("@id_subject", allSubjects[i]);
                    cmd.ExecuteNonQuery();

                }

                //add the student to series table
                cmd_search = new SqlCommand("select id from Series order by id desc", con);
                sda = new SqlDataAdapter(cmd_search);
                dt = new DataTable();
                sda.Fill(dt);
                cmd_search.ExecuteNonQuery();

                int id_Series_id = 0;
                if (dt.Rows.Count > 0)
                {
                    id_Series_id = (int)dt.Rows[0][0] + 1;
                }
                else
                {
                    id_Series_id = 1;
                }

                cmd = new SqlCommand("insert into Series ([id], [id_student],[id_head_teacher],[id_series]) " +
                                     "values(@id,@id_student,@id_teacher,@id_series)", con);
                cmd.Parameters.AddWithValue("@id", id_Series_id);
                cmd.Parameters.AddWithValue("@id_student", myUser.id);
                cmd.Parameters.AddWithValue("@id_teacher", 112345);
                Random newRandom = new Random();
                cmd.Parameters.AddWithValue("@id_series", newRandom.Next(1, 3));
                cmd.ExecuteNonQuery();



                // add student to dorm if exists
                if (dorm)
                {
                    cmd = new SqlCommand("insert into Dorm_payment ([id_student],[months_to_pay]) values(@id,0)", con);
                    cmd.Parameters.AddWithValue("@id", myUser.id);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Read information
        public static Employee GetEmployeesInformationById(int id)
        {
            Employee myUser = new Employee();

            string sql_command = "Select Top 1 " +
                "Employees.id, Employees.salary, Employees.seniority, " +
                "Status_Job.description, Teaching_Types.name as type_teaching,Teaching_Types.description as teaching_description, " +
                "Teachers.PhD_Certificate, Teachers.publications,Info_department.id_head, Info_department.description as department_description," +
                "(Select Count(1) from Series where Series.id_head_teacher = @id group by Series.id_head_teacher) as students " +
                "from Employees " +
                "FULL JOIN Teachers On Teachers.id = Employees.id " +
                "FULL JOIN Status_Job On Status_Job.id = Employees.status " +
                "FULL JOIN Teaching_Types On Teaching_Types.id = Teachers.job " +
                "FULL JOIN Department On Department.id_teachers = Teachers.id " +
                "FULL JOIN Info_department On Info_department.id = Department.id_department " +
                "where Employees.id = @id";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand(sql_command, con);
            sqlConn.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
            DataTable dt = new DataTable();

            try
            {
                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                return null;
            }

            if (dt.Rows.Count > 0)
            {
                myUser.Status_Job = new Status_Job();
                myUser.Teacher = new Teacher();
                myUser.Teacher.Teaching_Types = new Teaching_Types();
                Info_department info_department = new Info_department();//store in building the number of students

                for (int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
                {
                    if (dt.Rows[0][i] == null)
                    {
                        dt.Rows[0][i] = "";
                    }

                }
                try
                {

                    myUser.id = (int)dt.Rows[0][0];
                    myUser.salary = (int)dt.Rows[0][1];
                    myUser.seniority = (int)dt.Rows[0][2];
                    myUser.Status_Job.description = dt.Rows[0][3].ToString();
                    myUser.Teacher.Teaching_Types.name = dt.Rows[0][4].ToString();
                    myUser.Teacher.Teaching_Types.description = dt.Rows[0][5].ToString();
                    myUser.Teacher.PhD_Certificate = dt.Rows[0][6].ToString();
                    myUser.Teacher.publications = (int)dt.Rows[0][7];
                    info_department.id_head = (int)dt.Rows[0][8];
                    info_department.description = dt.Rows[0][9].ToString();
                    info_department.building = (int)dt.Rows[0][10];
                    myUser.Teacher.Info_department.Add(info_department);

                }
                catch
                {
                    return myUser;
                }

            }


            return myUser;
        }
        public static List<User> GetStudentsFromSeriesByStudentId(int id)
        {
            List<User> myStudents = new List<User>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("SELECT Users.id, Users.surname, Users.lastname, Users.photo_url from Users " +
                                                "JOIN Series on Series.id_student = Users.id " +
                                                "where Series.id_series = " +
                                                "(SELECT TOP 1 id_series from Series where id_student = @id_student) ", con);
            sqlConn.Parameters.AddWithValue("@id_student", id);
            SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
            DataTable dt = new DataTable();

            try
            {
                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                return null;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User currentUser = new User();
                currentUser.id = (int)dt.Rows[i][0];
                currentUser.surname = dt.Rows[i][1].ToString();
                currentUser.lastname = dt.Rows[i][2].ToString();
                if (dt.Rows[i][3].ToString() == "") dt.Rows[i][3] = "https://image.flaticon.com/icons/png/512/21/21294.png";
                currentUser.photo_url = dt.Rows[i][3].ToString();
                myStudents.Add(currentUser);
            }


            return myStudents;
        }
        public static int GetIdFromEmail(string email)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlConn = new SqlCommand("select id from Users where email LIKE @email", con);
                sqlConn.Parameters.AddWithValue("@email", email + " %");
                SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    return (int)dt.Rows[0][0];
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }

        }
        public static int getStudentDorm(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlConn = new SqlCommand("select months_to_pay from Dorm_payment where id_student = @id", con);
                sqlConn.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();

                if (dt.Rows.Count == 1)
                {
                    return (int)dt.Rows[0][0];
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }
        public static User GetUserById(int id)
        {
            User myProfileTemp = new User();
            myProfileTemp.id = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn;
            sqlConn = new SqlCommand("select * from Users where id = @id", con);
            sqlConn.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
            DataTable dt = new DataTable();

            try
            {
                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                return null;
            }

            if (dt.Rows.Count > 0)
            {
                myProfileTemp.id = (int)dt.Rows[0][0];
                myProfileTemp.username = dt.Rows[0][1].ToString();
                myProfileTemp.password = dt.Rows[0][2].ToString();
                myProfileTemp.surname = dt.Rows[0][3].ToString();
                myProfileTemp.lastname = dt.Rows[0][4].ToString();
                myProfileTemp.email = dt.Rows[0][7].ToString();
                if (dt.Rows[0][6].ToString() == "")
                    myProfileTemp.photo_url = "https://image.flaticon.com/icons/png/512/21/21294.png";
                else
                    myProfileTemp.photo_url = (string)dt.Rows[0][6].ToString().Replace(" ", "");
                if (dt.Rows[0][5].ToString() == "")
                    myProfileTemp.birth_date = DateTime.Parse("01-01-2000");
                else
                    myProfileTemp.birth_date = DateTime.Parse(dt.Rows[0][5].ToString());

                con = new SqlConnection(connectionString);
                sqlConn = new SqlCommand("Select Univ_info.specialization from Univ_info Join Student on Student.info = Univ_info.id where Student.id = @id", con);
                sqlConn.Parameters.AddWithValue("@id", myProfileTemp.id);
                sda = new SqlDataAdapter(sqlConn);
                dt = new DataTable();

                try
                {
                    sda.Fill(dt);
                    con.Open();
                    sqlConn.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    return null;
                }

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
        public static User GetUserByEmail(string conditions)
        {
            User myProfileTemp = new User();
            myProfileTemp.id = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn;
            sqlConn = new SqlCommand("select * from Users where email LIKE @email", con);
            sqlConn.Parameters.AddWithValue("@email", conditions + " %");
            SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
            DataTable dt = new DataTable();

            try
            {
                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                return null;
            }

            if (dt.Rows.Count > 0)
            {
                myProfileTemp.id = (int)dt.Rows[0][0];
                myProfileTemp.username = dt.Rows[0][1].ToString();
                myProfileTemp.password = dt.Rows[0][2].ToString();
                myProfileTemp.surname = dt.Rows[0][3].ToString();
                myProfileTemp.lastname = dt.Rows[0][4].ToString();
                myProfileTemp.email = dt.Rows[0][7].ToString();
                if (dt.Rows[0][6].ToString() == "")
                    myProfileTemp.photo_url = "https://image.flaticon.com/icons/png/512/21/21294.png";
                else
                    myProfileTemp.photo_url = (string)dt.Rows[0][6].ToString().Replace(" ", "");
                if (dt.Rows[0][5].ToString() == "")
                    myProfileTemp.birth_date = DateTime.Parse("01-01-2000");
                else
                    myProfileTemp.birth_date = DateTime.Parse(dt.Rows[0][5].ToString());

                con = new SqlConnection(connectionString);
                sqlConn = new SqlCommand("Select Univ_info.specialization from Univ_info Join Student on Student.info = Univ_info.id where Student.id = @id", con);
                sqlConn.Parameters.AddWithValue("@id", myProfileTemp.id);
                sda = new SqlDataAdapter(sqlConn);
                dt = new DataTable();

                try
                {
                    sda.Fill(dt);
                    con.Open();
                    sqlConn.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    return null;
                }

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
        public static List<int> GetTeacherByIdStudent(int id)
        {
            List<int> myTeachers = new List<int>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("Select Distinct id_teacher from Subjects_list where id_student = @id_student", con);
            sqlConn.Parameters.AddWithValue("@id_student", id);
            SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
            DataTable dt = new DataTable();

            try
            {
                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                return null;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                myTeachers.Add((int)dt.Rows[i][0]);
            }

            return myTeachers;
        }
        public static List<SubjectGrades> getSubjectsByIdStudent(SINU.User myUser)
        {

            List<SubjectGrades> mySubjects = new List<SubjectGrades>();
            try
            {
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
                    if (dt.Rows[i][0] == null)//name of Subject
                    {
                        dt.Rows[i][0] = "";
                    }
                    if (dt.Rows[i][1] == null)//credits
                    {
                        dt.Rows[i][1] = 0;
                    }
                    if (dt.Rows[i][2] == null)//grade
                    {
                        dt.Rows[i][2] = 0;
                    }
                    if (dt.Rows[i][2].ToString() == "")//grade
                    {
                        dt.Rows[i][2] = 0;
                    }

                    SubjectGrades currentSubjects = new SubjectGrades(myUser.id, myUser.lastname, myUser.surname, dt.Rows[i][0].ToString(), (int)dt.Rows[i][1], (int)dt.Rows[i][2]);
                    mySubjects.Add(currentSubjects);
                }
            }
            catch
            {
                return null;
            }

            return mySubjects;
        }
        public static List<SubjectGrades> getSubjectsByIdTeacher(User myUser)
        {
            List<SubjectGrades> mySubjects = new List<SubjectGrades>();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sqlConn = new SqlCommand("Select Subjects_list.id_student,Subjects.name, Subjects.credits,Subjects_list.grade, Subjects_list.id_teacher, Subjects_list.id_student " +
                                                    "from Subjects_list " +
                                                    "Join SUbjects on Subjects_list.id_subject = Subjects.id " +
                                                    "Join Users on Users.id = Subjects_list.id_student " +
                                                    "order by Subjects.name asc,Subjects_list.grade asc; ", con);
                SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                con.Open();
                sqlConn.ExecuteNonQuery();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((int)dt.Rows[i][4] == myUser.id)
                    {
                        if (dt.Rows[i][1] == null)//name of Subject
                        {
                            dt.Rows[i][1] = "";
                        }
                        if (dt.Rows[i][2] == null)//credits
                        {
                            dt.Rows[i][2] = 0;
                        }
                        if (dt.Rows[i][3] == null)//grade
                        {
                            dt.Rows[i][3] = 0;
                        }
                        if (dt.Rows[i][3].ToString() == "")//grade
                        {
                            dt.Rows[i][3] = 0;
                        }


                        SubjectGrades currentSubjects = new SubjectGrades((int)dt.Rows[i][5], dt.Rows[i][0].ToString(), null, dt.Rows[i][1].ToString(), (int)dt.Rows[i][2], (int)dt.Rows[i][3]);
                        mySubjects.Add(currentSubjects);
                    }

                }
            }
            catch
            {
                return null;
            }


            return mySubjects;
        }
        #endregion

        #region Update information
        public static bool IncreaseByOneMonthDorm()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("UPDATE Dorm_payment SET months_to_pay = months_to_pay+1", con);
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

                    if (profile.photo_url == null || profile.photo_url == "")
                        profile.photo_url = "https://image.flaticon.com/icons/png/512/21/21294.png";
                    command.Parameters.AddWithValue("@photo_url", profile.photo_url);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateStudentGrade(int myId, int id, string subject, int grade)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand sqlConn = new SqlCommand("Select id from Subjects where name = @subject", con);
                    sqlConn.Parameters.AddWithValue("@subject", subject);
                    SqlDataAdapter sda = new SqlDataAdapter(sqlConn);
                    DataTable dt = new DataTable();


                    try
                    {
                        sda.Fill(dt);
                        con.Open();
                        sqlConn.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {
                        return false;
                    }

                    SqlCommand command;
                    command = new SqlCommand("UPDATE Subjects_list SET grade = @grade where id_subject = @id_subject and Subjects_list.id_student = @id and Subjects_list.id_teacher = @myId", con);

                    command.Parameters.AddWithValue("@grade", grade);
                    command.Parameters.AddWithValue("@id_subject", dt.Rows[0][0]);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@myId", myId);

                    command.ExecuteNonQuery();
                    try
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {
                        return false;
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
        #endregion

        #region Delete information
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

                            command = new SqlCommand("UPDATE Series SET id_head_teacher = @new_head where id_head_teacher = " + id.ToString(), con);
                            command.Parameters.AddWithValue("@new_head", dt.Rows[0][0]);
                            command.ExecuteNonQuery();

                            command = new SqlCommand("UPDATE Subjects_list SET id_teacher = @new_head where id_teacher = " + id.ToString(), con);
                            command.Parameters.AddWithValue("@new_head", dt.Rows[0][0]);
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new Exception();
                        }

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
        #endregion

    }
}