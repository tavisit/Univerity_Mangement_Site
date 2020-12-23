using System;
using System.Collections.Generic;
using System.Web.UI;
using System.IO;

namespace SINU.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private void ChangeDormPayment()
        {
            int today = DateTime.Now.Day;
            string location = AppDomain.CurrentDomain.BaseDirectory+@"DormInformation.txt";

            string text;
            if (!(File.Exists(location)))
            {
                File.CreateText(location);
                text = "unpayed";
            }
            else
            {
                text = File.ReadAllText(location);

                if(text == "unpayed"&& today>=2)
                {
                    return;
                }
            }

            if (today == 1 && text=="unpayed")
            {
                SQLOperations.IncreaseByOneMonthDorm();
                text = "payed";
            }
            else if(today >= 2)
            {
                text = "unpayed";
            }

            File.WriteAllText(location, text);

        }
        private void ViewProfile()
        {
            SINU.User myProfile = SQLOperations.GetUserByEmail(Session["email"].ToString().Replace(" ", ""));
            //general information
            MyProfileLiteral.Text = "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
            MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
            MyProfileLiteral.Text += "<h1 style=\"text-align:center;\">" + myProfile.surname + " " + myProfile.lastname + "</h1></br>";
            MyProfileLiteral.Text += "<img style = \"margin-left: auto;margin-right: auto;display: block;\" border=\"0\" src=" + myProfile.photo_url + " width = \"250px\" height = \"250px\">";
            MyProfileLiteral.Text += "<br><br></div>";
            MyProfileLiteral.Text += "</div>";
            ListNews.Text = "<table style = \"margin-left: 5%;margin-right:5%;\">";

            //divider
            MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;height:50px;background-color:purple;\">";
            MyProfileLiteral.Text += "</div>";

            // teacher/student special info
            int my_id = SQLOperations.GetIdFromEmail(Session["email"].ToString());
            if(my_id<100000)
            {
                return;
            }
            else if (my_id > 200000)
            {
                //add collegues from series
                List<User> myCollegues = SQLOperations.GetStudentsFromSeriesByStudentId(my_id);

                MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
                MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
                int counter = 0;
                MyProfileLiteral.Text += "<br><h2 style = \"text-align:center;\">My collegues</h2><br>";
                MyProfileLiteral.Text += "<table>";
                foreach (var myCollegue in myCollegues)
                {   
                    if(myCollegue.id!=my_id)
                    {
                        if (counter % 3 == 0) MyProfileLiteral.Text += "<tr>";
                        MyProfileLiteral.Text += "<td >";
                        MyProfileLiteral.Text += "<h3>" + myCollegue.surname + " " + myCollegue.lastname + " </h3>";
                        MyProfileLiteral.Text += "<img border=\"0\" src=" + myCollegue.photo_url + " alt=" + myCollegue.surname + " " + myCollegue.lastname + "width = \"300px\" height = \"300px\"></a>";
                        MyProfileLiteral.Text += "</td>";
                        if (counter % 3 == 2) MyProfileLiteral.Text += "</tr>";
                        counter++;
                    }

                }
                if (counter % 3 !=0) MyProfileLiteral.Text += "</tr>";
                MyProfileLiteral.Text += "</table>";
                MyProfileLiteral.Text += "</div>";
                MyProfileLiteral.Text += "</div>";
            }
            else
            {
                // add teacher information
                Employee currentEmployee = SQLOperations.GetEmployeesInformationById(my_id);
                MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
                MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
                MyProfileLiteral.Text += "<br><br><h2> My current status<br><h2>";
                MyProfileLiteral.Text += "<h4> My id is: "+currentEmployee.id+"<br></h4>";
                MyProfileLiteral.Text += "<h4> A seniority of: "+currentEmployee.seniority+" years</h4>";
                MyProfileLiteral.Text += "<h4> My current working status is: "+currentEmployee.Status_Job.description+ "</h4><br><br>";

                MyProfileLiteral.Text += "<div style = \"height:10px;background-color:purple;\">";
                MyProfileLiteral.Text += "</div>";

                MyProfileLiteral.Text += "<br><br><h2> Teaching status</h2>";
                MyProfileLiteral.Text += "<h4> I am a "+currentEmployee.Teacher.Teaching_Types.name+"</h4>";
                MyProfileLiteral.Text += "<h4> That means I have to do: "+currentEmployee.Teacher.Teaching_Types.description+"</h4>";
                MyProfileLiteral.Text += "<h4> My Phd is located here: <a target=\"_blank\" href = \"http://" + currentEmployee.Teacher.PhD_Certificate+"\">PhD site</a></h4>";
                MyProfileLiteral.Text += "<h4> I have a number of "+currentEmployee.Teacher.publications+" publications</h4><br><br>";

                MyProfileLiteral.Text += "<div style = \"height:10px;background-color:purple;\">";
                MyProfileLiteral.Text += "</div>";

                MyProfileLiteral.Text += "<br><br><h2> The departemnt where I work</h2>";
                foreach (var item in currentEmployee.Teacher.Info_department)
                {
                    Info_department myDepartment = item;
                    if(myDepartment.id_head!=myProfile.id)MyProfileLiteral.Text += "<h4> The head of the department has the id "+myDepartment.id_head.ToString()+"</h4>";
                    else MyProfileLiteral.Text += "<h4> I am the head of the department</h4>";
                    MyProfileLiteral.Text += "<h4> "+myDepartment.description+"</h4>";
                    MyProfileLiteral.Text += "<h4> I have " + myDepartment.building+ " students enrolled in this department</h4>";
                }
                               
                MyProfileLiteral.Text += "<br><br></div>";
                MyProfileLiteral.Text += "</div>";
            }


        }
        private void readFile()
        {
            List<string> scrappedAnn = new List<string>();
            scrappedAnn = Scrapper.AnnouncementScrapper(10);
            ListNews.Text = "<br><table style = \"margin: 0 auto; background-color:white;\">";
            ListNews.Text += "<tr><td><h2>Announcements</h2></td></tr>";
            foreach (var n in scrappedAnn)
            {
                ListNews.Text += "<tr>";
                ListNews.Text += "<td>";
                ListNews.Text += n.ToString();
                ListNews.Text += "</td>";
                ListNews.Text += "</tr>";
            }
            ListNews.Text += "</table><br>";

            List<ScrappedInfo> scrappedNews = new List<ScrappedInfo>();
            scrappedNews = Scrapper.NewsScrapper(10);
            ListNews.Text += "<br><table style = \"margin-left: 5%;margin-right:5%;\">";
            ListNews.Text += "<tr><td colspan=2><h2>News</h2></td></tr>";
            int counter = 0;
            foreach (var n in scrappedNews)
            {
                if (counter % 2 == 0) ListNews.Text += "<tr>";
                ListNews.Text += "<td >";
                ListNews.Text += "<a  target=\"_blank\" href = \"" + n.link + "\" ><h3>" + n.title + " </h3>";
                ListNews.Text += "<img border=\"0\" src=" + n.image_url + " width = \"300px\" height = \"300px\"></a>";
                ListNews.Text += "</td>";
                if (counter % 2 == 1) ListNews.Text += "</tr>";
                counter++;

            }
            ListNews.Text += "</table><br>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ChangeDormPayment();
            if (Session["email"] != null)
            {

                ViewProfile();
            }
            else
            {
                MyProfileLiteral.Text = "";
            }
            readFile();
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
    }
}