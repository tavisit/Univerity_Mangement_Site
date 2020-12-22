using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SINU.Pages
{
    public partial class MyAccount : System.Web.UI.Page
    {
        SINU.User myProfile;
        private bool unchangedTextBoxesUpdate()
        {
            if (TextBox9.Text != myProfile.username && TextBox9.Text != "")
            {
                return false;
            }
            else if (TextBox10.Text != myProfile.password && TextBox10.Text != "")
            {
                return false;
            }
            else if (TextBox11.Text != myProfile.surname && TextBox11.Text != "")
            {
                return false;
            }
            else if (TextBox12.Text != myProfile.lastname && TextBox12.Text != "")
            {
                return false;
            }
            else if (TextBox13.Text != myProfile.photo_url && TextBox13.Text != "")
            {
                return false;
            }
            else if (TextBox14.Text != myProfile.birth_date.ToString() && TextBox14.Text != "")
            {
                return false;
            }

            return true;
        }
        private void ViewProfile()
        {
            Login_Table.Visible = false;
            myProfile = SQLOperations.GetUserByEmail(Session["email"].ToString().Replace(" ", ""));
            SessionDiv.Visible = true;

            //Fill the Literal with info
            MyProfileLiteral.Text = "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
            MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
            MyProfileLiteral.Text += "<br><h1 style=\"text-align:center;\">" + myProfile.surname + " " + myProfile.lastname + "</h1>";
            if (myProfile.id > 200000) MyProfileLiteral.Text += "</br><h1 style=\"text-align:center;\">Specialization: " + myProfile.Student.Univ_info.specialization + "</h1></br>";
            MyProfileLiteral.Text += "<img style = \"margin-left: auto;margin-right: auto;display: block;\" border=\"0\" src=" + myProfile.photo_url + " width = \"250px\" height = \"250px\">";
            MyProfileLiteral.Text += "<br><br></div>";
            MyProfileLiteral.Text += "</div>";

            //Fill the courses literal
            if (myProfile.id > 200000)
            {
                
                List<SQLOperations.SubjectGrades> mySubjects = SQLOperations.getSubjectsByIdStudent(myProfile);
                ViewInformationLiteral.Text = "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";

                ViewInformationLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
                ViewInformationLiteral.Text += "<br><br><h2>Current Exams</h2>";
                foreach (var subject in mySubjects)
                {
                    if (subject.grade == 0)
                    {
                        ViewInformationLiteral.Text += "<h4> At " + subject.subjectName + " with " + subject.credits + " credits, the mark is: Currently Taking It</h4>";
                    }
                }
                ViewInformationLiteral.Text += "<br><br></div>";

                ViewInformationLiteral.Text += "<div style = \"height:30px;background-color:purple;\"></div>";

                ViewInformationLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
                ViewInformationLiteral.Text += "<br><br><h2>Taken and passed exams</h2>";
                foreach (var subject in mySubjects)
                {
                    if (subject.grade > 0)
                    {
                        ViewInformationLiteral.Text += "<h4> At " + subject.subjectName + " with " + subject.credits + " credits, the mark was: " + subject.grade + "</h4>";
                    }
                }
                ViewInformationLiteral.Text += "<br><br></div>";

                ViewInformationLiteral.Text += "</div>";
            }
            else if(myProfile.id>=100000)
            {
                updatingstudentgrade.Visible = true;
                List<SQLOperations.SubjectGrades> mySubjects = SQLOperations.getSubjectsByIdTeacher(myProfile);
                ViewInformationLiteral.Text = "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";

                ViewInformationLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
                ViewInformationLiteral.Text += "<br><br><h2>Students to be graded</h2>";
                foreach (var subject in mySubjects)
                {
                    if(subject.grade==0)
                    {
                        ViewInformationLiteral.Text += "<h4> Student with id: " + subject.name +
                                                    " has at " + subject.subjectName + " the grade " + subject.grade.ToString() + "</h4>";
                    }
                    
                }
                ViewInformationLiteral.Text += "<br><br><h2>Students already graded</h2>";
                foreach (var subject in mySubjects)
                {
                    if (subject.grade > 0)
                    {
                        ViewInformationLiteral.Text += "<h4> Student with id: " + subject.name +
                                                    " has at " + subject.subjectName + " the grade " + subject.grade.ToString() + "</h4>";
                    }

                }
                ViewInformationLiteral.Text += "<br><br></div>";
                ViewInformationLiteral.Text += "</div>";

            }
            else
            {
                
                ViewInformationLiteral.Text = "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";

                ViewInformationLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;text-align:center\">";

                    ViewInformationLiteral.Text += "<br><h2>IDs of current students</h2>";
                    List<int> allStudents = SQLOperations.getAllStudentsID();
                    int counter = 0;

                    ViewInformationLiteral.Text += "<table>";
                    foreach (var item in allStudents)
                    {
                        if (counter % 5 == 0) ViewInformationLiteral.Text += "<tr>";
                    ViewInformationLiteral.Text += "<td>";
                    ViewInformationLiteral.Text += "<h3>"+item.ToString()+ "</h3>";
                    ViewInformationLiteral.Text += "</td>";
                        if (counter % 5 == 4) ViewInformationLiteral.Text += "</tr>";
                        counter++;
                    }
                    if(counter%5!=0) ViewInformationLiteral.Text += "</tr>";
                    ViewInformationLiteral.Text += "</table>";

                    ViewInformationLiteral.Text += "<br><h2>IDs of current Teachers</h2>";
                    List<int> allTeachers = SQLOperations.getAllTeachersID();
                    counter = 0;

                    ViewInformationLiteral.Text += "<table>";
                    foreach (var item in allStudents)
                    {
                        if (counter % 5 == 0) ViewInformationLiteral.Text += "<tr>";
                        ViewInformationLiteral.Text += "<td>";
                        ViewInformationLiteral.Text += "<h3>" + item.ToString() + "</h3>";
                        ViewInformationLiteral.Text += "</td>";
                        if (counter % 5 == 4) ViewInformationLiteral.Text += "</tr>";
                        counter++;
                    }
                    if (counter % 5 != 0) ViewInformationLiteral.Text += "</tr>";
                    ViewInformationLiteral.Text += "</table>";

                    ViewInformationLiteral.Text += "<br><h2>IDs of current working staff</h2>";
                    List<int> allWorkingStaff = SQLOperations.getAllWorkingStaffID();
                    counter = 0;

                    ViewInformationLiteral.Text += "<table>";
                    foreach (var item in allWorkingStaff)
                    {
                        if (counter % 5 == 0) ViewInformationLiteral.Text += "<tr>";
                        ViewInformationLiteral.Text += "<td>";
                        ViewInformationLiteral.Text += "<h3>" + item.ToString() + "</h3>";
                        ViewInformationLiteral.Text += "</td>";
                        if (counter % 5 == 4) ViewInformationLiteral.Text += "</tr>";
                        counter++;
                    }
                    if (counter % 5 != 0) ViewInformationLiteral.Text += "</tr>";
                    ViewInformationLiteral.Text += "</table>";


                ViewInformationLiteral.Text += "<br><br></div>";
                ViewInformationLiteral.Text += "</div>";
            }

            //Fill the update TextBoxes
            if (unchangedTextBoxesUpdate())
            {
                TextBox9.Text = myProfile.username;
                TextBox10.Text = myProfile.password;
                TextBox11.Text = myProfile.surname;
                TextBox12.Text = myProfile.lastname;
                TextBox13.Text = myProfile.photo_url;
                TextBox14.Text = myProfile.birth_date.ToString();
                
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            updatingstudentgrade.Visible = false;
            Label1.Text = "";
            Label4.Text = "";
            Label2.Text = "";
            Register_Panel.Visible = false;
            MyProfileLiteral.Text = "";
            RegisterTeacherBtn.Visible = false;

            if (Session["email"] != null)
            {
                if(SQLOperations.GetIdFromEmail(Session["email"].ToString())<100000)
                {

                    Login_Table.Visible = false;
                    SessionDiv.Visible = false;
                    Register_Panel.Visible = true;
                    Label2.Text = "Enroll a new Teacher";
                    RegisterRegisterPanelBtn.Visible = false;
                    CancelRegisterPanelBtn.Visible = false;
                    RegisterTeacherBtn.Visible = true;

                    Label5.Text = "Type of Teacher:<br />" +
                        "1. Lecturer<br />" +
                        "2. Assistant Professor<br />" +
                        "3. Associate Professor<br />" +
                        "4. Professor<br />" +
                        "5. Dean<br />" +
                        "6. Chancellor<br />";

                    //View all the teachers in a table
                    //View all the Students in a table
                    ViewProfile();

                    
                }
                else
                {
                    ViewProfile();
                }
            }
            else
            {
                SessionDiv.Visible = false;
                Login_Table.Visible = true;
                Log_Out_Btn.Visible = false;
                MyProfileLiteral.Text = "";
                LogInBtn.Focus();
            }
        }

        protected void HomeImageButton_click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void MyAccount_click(object sender, EventArgs e)
        {
            Response.Redirect("MyAccount.aspx");
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        protected void AboutUs_Click(object sender, EventArgs e)
        {
            Response.Redirect("AboutUs.aspx");
        }
        private bool ValidateCredentials(string myCredential)
        {
            var myRegex = new System.Text.RegularExpressions.Regex(@"[a-zA-z0-9]*$");
            if (!myRegex.IsMatch(myCredential))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            //Textboxes from 1 and 2
            SqlConnection con = new SqlConnection(SQLOperations.connectionString);
            SqlCommand cmd = new SqlCommand("select * from Users where email LIKE @email and password LIKE @word", con);

            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox2.Text.Length < 8)
                return;

            if (!(ValidateCredentials(TextBox2.Text)))
            {
                Label1.Text = "Your email and password is incorrect";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            cmd.Parameters.AddWithValue("@email", TextBox1.Text + " %");
            cmd.Parameters.AddWithValue("@word", TextBox2.Text + " %");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (dt.Rows.Count > 0)
            {
                Session.Add("email", TextBox1.Text);
                myProfile = SQLOperations.GetUserByEmail(TextBox1.Text);
                Response.Redirect("MyAccount.aspx");
            }
            else
            {
                TextBox1.Text = "";
                TextBox2.Text = "";
                Label1.Text = "Your email and password is incorrect";
                Label1.ForeColor = System.Drawing.Color.Red;

            }
        }

        protected void RegisternBtn_Click(object sender, EventArgs e)
        {
            Login_Table.Visible = false;
            Label5.Text = "Year:";
            Register_Panel.Visible = true;
            RegisterFormBtn.Focus();
        }

        protected void Log_Out_Btn_Click(object sender, EventArgs e)
        {
            Session["email"] = null;
            Response.Redirect("Home.aspx");
        }

        protected void CancelRegisterPanelBtn_Click(object sender, EventArgs e)
        {
            Login_Table.Visible = true;
            Register_Panel.Visible = false;
        }

        private void ErrorRegister()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            Label2.Text = "Something went wrong!";
            Label2.ForeColor = System.Drawing.Color.Red;
        }
        private int GeneratedID(int min, int max)
        {
            SqlConnection con = new SqlConnection(SQLOperations.connectionString);
            con.Open();
            int generatedID;
            bool isUnique;
            Random newRandom = new Random();
            do
            {
                generatedID = newRandom.Next(min, max);
                SqlCommand cmd_search = new SqlCommand("select * from Users where id = @id", con);
                cmd_search.Parameters.AddWithValue("@id", generatedID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd_search);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cmd_search.ExecuteNonQuery();

                if (dt.Rows.Count > 0)
                {
                    isUnique = false;
                }
                else
                {
                    isUnique = true;
                }
            } while (isUnique == false);
            con.Close();
            return generatedID;
        }
        protected void RegisterRegisterPanelBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(SQLOperations.connectionString);
            con.Open();
            SqlCommand cmd_email = new SqlCommand("select * from Users where email = @email", con);
            cmd_email.Parameters.AddWithValue("@email", TextBox3.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd_email);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmd_email.ExecuteNonQuery();

            if (dt.Rows.Count > 0)
            {
                ErrorRegister();
                return;
            }

            SqlCommand cmd = new SqlCommand("insert into Users ([id], [username],[password],[surname],[lastname],[email],[photo_url]) values(@id,@username,@password,@surname,@lastname,@email,@photo_url)", con);

            if (TextBox3.Text == "" || TextBox4.Text == "" || TextBox5.Text == "" ||
                TextBox6.Text == "" || TextBox7.Text.Length < 8 || TextBox7.Text != TextBox8.Text)
            {

                ErrorRegister();
                return;
            }


            if (!(ValidateCredentials(TextBox6.Text) && ValidateCredentials(TextBox7.Text)))
            {
                ErrorRegister();
                return;
            }

            cmd.Parameters.AddWithValue("@email", TextBox3.Text);
            cmd.Parameters.AddWithValue("@surname", TextBox4.Text);
            cmd.Parameters.AddWithValue("@lastname", TextBox5.Text);
            cmd.Parameters.AddWithValue("@username", TextBox6.Text);
            cmd.Parameters.AddWithValue("@password", TextBox7.Text);
            cmd.Parameters.AddWithValue("@photo_url","https://image.flaticon.com/icons/png/512/21/21294.png");


            var newRandom = new Random();
            int generatedID = GeneratedID(200000,999999);
            
            cmd.Parameters.AddWithValue("@id", generatedID);

            int rowsAdded = cmd.ExecuteNonQuery();
            con.Close();

            if (rowsAdded > 0)
            {
                con.Open();
                //add to student table
                int student_year = 1;
                if (TextBox18.Text != "") student_year = int.Parse(TextBox18.Text.ToString());
                cmd = new SqlCommand("Insert into Student (id,year,info) values (@id,"+student_year+",1)", con);
                cmd.Parameters.AddWithValue("@id", generatedID);
                cmd.ExecuteNonQuery();

                //add subjects to student
                SqlCommand cmd_search = new SqlCommand("select TOP 1 id from Subjects_list order by id desc", con);
                sda = new SqlDataAdapter(cmd_search);
                dt = new DataTable();
                sda.Fill(dt);
                cmd_search.ExecuteNonQuery();
                int nr_id_subjects_list = (int)dt.Rows[0][0]+1;
                List<int> allSubjects = SQLOperations.getAllSubjectsIDByYear(student_year);
                List<int> allTeachers = SQLOperations.getAllTeachersID();

                for (int i=0;i<Math.Min(allSubjects.Count,allTeachers.Count); i++)
                {
                    cmd = new SqlCommand("insert into Subjects_list ([id], [id_student],[id_subject],[grade],[id_teacher]) values(@id,@id_student,@id_subject,NULL,@id_teacher)", con);
                    cmd.Parameters.AddWithValue("@id_student", generatedID);
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
                cmd = new SqlCommand("insert into Series ([id], [id_student],[id_head_teacher],[id_series]) values(@id,@id_student,@id_teacher,@id_series)", con);
                cmd.Parameters.AddWithValue("@id", id_Series_id);
                cmd.Parameters.AddWithValue("@id_student", generatedID);
                cmd.Parameters.AddWithValue("@id_teacher", 112345);
                cmd.Parameters.AddWithValue("@id_series", newRandom.Next(1,3));
                cmd.ExecuteNonQuery();

                con.Close();

                //make the session and redirect
                Session.Add("email", TextBox3.Text);
                myProfile = SQLOperations.GetUserByEmail(TextBox3.Text);
                Response.Redirect("MyAccount.aspx");
            }
            else
            {
                ErrorRegister();
                return;
            }
        }

        protected void UpdateProfileBtn_Click(object sender, EventArgs e)
        {
            if (TextBox9.Text == "" ||
                TextBox11.Text == "" ||
                TextBox12.Text == "")
                return;

            SINU.User modelUser = SQLOperations.GetUserByEmail(Session["email"].ToString());
            if (TextBox9.Text != modelUser.username)
            {
                modelUser.username = TextBox9.Text;
            }
            if (TextBox10.Text != modelUser.password && TextBox10.Text != "")
            {
                modelUser.password = TextBox10.Text;
            }
            if (TextBox11.Text != modelUser.surname)
            {
                modelUser.surname = TextBox11.Text;
            }
            if (TextBox12.Text != modelUser.lastname)
            {
                modelUser.lastname = TextBox12.Text;
            }
            if (TextBox13.Text != modelUser.photo_url)
            {
                modelUser.photo_url = TextBox13.Text;
            }
            if (TextBox14.Text != modelUser.birth_date.ToString())
            {
                modelUser.birth_date = DateTime.Parse(TextBox14.Text);
            }
            SQLOperations.UpdateUser(modelUser);
            if (myProfile.username != modelUser.username)
            {
                TextBox9.Text = modelUser.username;
            }
            if (myProfile.password != modelUser.password)
            {
                TextBox10.Text = modelUser.password;
            }
            if (myProfile.surname != modelUser.surname)
            {
                TextBox11.Text = modelUser.surname;
            }
            if (myProfile.lastname != modelUser.lastname)
            {
                TextBox12.Text = modelUser.lastname;
            }
            if (myProfile.photo_url != modelUser.photo_url)
            {
                TextBox13.Text = modelUser.photo_url;
            }
            if (myProfile.birth_date.ToString() != modelUser.birth_date.ToString())
            {
                TextBox14.Text = modelUser.birth_date.ToString();
            }
            myProfile = modelUser;
            Response.Redirect("MyAccount.aspx");
        }

        protected void DeleteAccountBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(SQLOperations.connectionString);
            SqlCommand cmd_email = new SqlCommand("select * from Users where email = @email", con);
            cmd_email.Parameters.AddWithValue("@email", Session["email"].ToString());
            SqlDataAdapter sda = new SqlDataAdapter(cmd_email);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd_email.ExecuteNonQuery();
            con.Close();
            if (dt.Rows.Count == 1)
            {
                bool deletion_completed = SQLOperations.DeleteByID((int)dt.Rows[0][0]);
                if (deletion_completed == true)
                {
                    Session["email"] = null;
                    Response.Redirect("MyAccount.aspx");
                }
            }

        }
        protected void UpdateGradesBtn_Click(object sender, EventArgs e)
        {
            //update the grade for the student with ID and Subject known
            try
            {

                Label4.ForeColor = System.Drawing.Color.Black;
                bool done = SQLOperations.UpdateStudentGrade(myProfile.id, Int32.Parse(TextBox15.Text.ToString()), TextBox16.Text, Int32.Parse(TextBox17.Text.ToString()));
            }
            catch
            {
                Label4.Text = "Something went wrong";
                Label4.ForeColor = System.Drawing.Color.Red;
            }
            Response.Redirect("MyAccount.aspx");
        }

        protected void RegisterTeacherBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(SQLOperations.connectionString);
            con.Open();
            SqlCommand cmd_email = new SqlCommand("select * from Users where email = @email", con);
            cmd_email.Parameters.AddWithValue("@email", TextBox3.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd_email);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmd_email.ExecuteNonQuery();

            if (dt.Rows.Count > 0)
            {
                ErrorRegister();
                return;
            }

            SqlCommand cmd = new SqlCommand("insert into Users ([id], [username],[password],[surname],[lastname],[email],[photo_url]) values(@id,@username,@password,@surname,@lastname,@email,@photo_url)", con);

            if (TextBox3.Text == "" || TextBox4.Text == "" || TextBox5.Text == "" ||
                TextBox6.Text == "" || TextBox7.Text.Length < 8 || TextBox7.Text != TextBox8.Text)
            {

                ErrorRegister();
                return;
            }


            if (!(ValidateCredentials(TextBox6.Text) && ValidateCredentials(TextBox7.Text)))
            {
                ErrorRegister();
                return;
            }

            cmd.Parameters.AddWithValue("@email", TextBox3.Text);
            cmd.Parameters.AddWithValue("@surname", TextBox4.Text);
            cmd.Parameters.AddWithValue("@lastname", TextBox5.Text);
            cmd.Parameters.AddWithValue("@username", TextBox6.Text);
            cmd.Parameters.AddWithValue("@password", TextBox7.Text);
            cmd.Parameters.AddWithValue("@photo_url", "https://image.flaticon.com/icons/png/512/21/21294.png");


            var newRandom = new Random();
            int generatedID = GeneratedID(100000,199999);

            cmd.Parameters.AddWithValue("@id", generatedID);

            int rowsAdded = cmd.ExecuteNonQuery();
            con.Close();

            if (rowsAdded > 0)
            {
                con.Open();
                int type_of_teacher;
                if (TextBox18.Text == "" || int.Parse(TextBox18.Text.ToString()) > 6) type_of_teacher = 1;
                else type_of_teacher = int.Parse(TextBox18.Text.ToString());
                cmd = new SqlCommand("Insert into Employees (id,salary, status, seniority) values (@id, @salary,@status,1)", con);
                cmd.Parameters.AddWithValue("@id", generatedID);
                cmd.Parameters.AddWithValue("@salary", (type_of_teacher-1)*1500+3500);
                cmd.Parameters.AddWithValue("@status", type_of_teacher);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Insert into Teachers (id,job, publications,PhD,Phd_Certificate) values (@id,1,NULL,NULL,NULL)", con);
                cmd.Parameters.AddWithValue("@id", generatedID);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("MyAccount.aspx");
            }
            else
            {
                ErrorRegister();
                return;
            }
        }
    }
}