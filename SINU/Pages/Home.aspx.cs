using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace SINU.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private void ChangeDormPayment()
        {
            int today = DateTime.Now.Day;
            string location = AppDomain.CurrentDomain.BaseDirectory + @"DormInformation.txt";

            string text;
            if (!(File.Exists(location)))
            {
                File.CreateText(location);
                text = "unpayed";
            }
            else
            {
                text = File.ReadAllText(location);

                if (text == "unpayed" && today >= 2)
                {
                    return;
                }
            }

            if (today == 1 && text == "unpayed")
            {
                SQLOperations.IncreaseByOneMonthDorm();
                text = "payed";
            }
            else if (today >= 2)
            {
                text = "unpayed";
            }

            File.WriteAllText(location, text);

        }
        private void ViewProfile()
        {
            SINU.User myProfile = SQLOperations.GetUserByEmail(Session["email"].ToString().Replace(" ", ""));
            //general information
            MyProfileLiteral.Text = UsefulHtmlStuff.generalInfo(myProfile);

            //divider
            MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;height:50px;background-color:purple;\">";
            MyProfileLiteral.Text += "</div>";

            // teacher/student special info
            int my_id = SQLOperations.GetIdFromEmail(Session["email"].ToString());
            if (my_id < 100000)
            {
                return;
            }
            else if (my_id > 200000)
            {
                MyProfileLiteral.Text += UsefulHtmlStuff.colleguesInfo(my_id);

                //add teachers that teach me
                List<int> myTeachers = SQLOperations.GetTeacherByIdStudent(my_id);
                MyProfileLiteral.Text += "<br><br><div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
                MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
                int counter = 0;
                MyProfileLiteral.Text += "<br><h2 style = \"text-align:center;\">My teachers</h2><br>";
                MyProfileLiteral.Text += "<table>";
                foreach (var myTeacher in myTeachers)
                {
                    User myTeacherProfile = SQLOperations.GetUserById(myTeacher);
                    if (counter % 3 == 0) MyProfileLiteral.Text += "<tr>";
                    MyProfileLiteral.Text += "<td >";
                    MyProfileLiteral.Text += "<a href= \"Profile.aspx\\" + myTeacherProfile.id + "\" >";
                    MyProfileLiteral.Text += "<h3>" + myTeacherProfile.surname + " " + myTeacherProfile.lastname + " </h3>";
                    MyProfileLiteral.Text += "<img border=\"0\" src=" + myTeacherProfile.photo_url + " alt=" + myTeacherProfile.surname + " " + myTeacherProfile.lastname + "width = \"300px\" height = \"300px\"></a>";
                    MyProfileLiteral.Text += "</td>";
                    if (counter % 3 == 2) MyProfileLiteral.Text += "</tr>";
                    counter++;


                }
                if (counter % 3 != 0) MyProfileLiteral.Text += "</tr>";
                MyProfileLiteral.Text += "</table>";
                MyProfileLiteral.Text += "</div>";
                MyProfileLiteral.Text += "</div>";
            }
            else
            {
                MyProfileLiteral.Text += UsefulHtmlStuff.teacherInfo(myProfile);
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