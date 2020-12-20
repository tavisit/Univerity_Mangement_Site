using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

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
            MyProfileLiteral.Text += "<h1 style=\"text-align:center;\">" + myProfile.surname + " " + myProfile.lastname + "</br>Specialization: " + myProfile.Student.Univ_info.specialization + "</h1></br>";
            MyProfileLiteral.Text += "<img style = \"margin-left: auto;margin-right: auto;display: block;\" border=\"0\" src=" + myProfile.photo_url + " width = \"250px\" height = \"250px\">";
            MyProfileLiteral.Text += "</div>";
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
            Label1.Text = "";
            Label2.Text = "";
            Register_Panel.Visible = false;
            MyProfileLiteral.Text = "";

            if (Session["email"] != null)
            {

                ViewProfile();
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
        protected void RegisterRegisterPanelBtn_Click(object sender, EventArgs e)
        {
            //Textboxes from 3 to 8
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

            SqlCommand cmd = new SqlCommand("insert into Users ([id], [username],[password],[surname],[lastname],[email]) values(@id,@username,@password,@surname,@lastname,@email)", con);

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


            var newRandom = new Random();
            int generatedID;

            bool isUnique;
            do
            {
                generatedID = newRandom.Next(200000, 999999);
                SqlCommand cmd_search = new SqlCommand("select * from Users where id = @id", con);
                cmd_search.Parameters.AddWithValue("@id", generatedID);
                sda = new SqlDataAdapter(cmd_search);
                dt = new DataTable();
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
            cmd.Parameters.AddWithValue("@id", generatedID);

            int rowsAdded = cmd.ExecuteNonQuery();
            con.Close();

            if (rowsAdded > 0)
            {
                //add subjects to student

                //add to student table

                //add the student to series table

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
    }
}