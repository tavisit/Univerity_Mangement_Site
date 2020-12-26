using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SINU.Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        private void ShowProfile(User myUser)
        {
            Literal1.Text = "</br></br>";
            Literal1.Text += "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
            Literal1.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
            Literal1.Text += "<h1 style=\"text-align:center;\">" + myUser.surname + " " + myUser.lastname + "</h1></br>";
            Literal1.Text += "<h1 style=\"text-align:center;\">" + myUser.Student.Univ_info.specialization + "</h1></br>";
            Literal1.Text += "<img style = \"margin-left: auto;margin-right: auto;display: block;\" border=\"0\" src=" + myUser.photo_url + " width = \"250px\" height = \"250px\">";
            Literal1.Text += "<br><br></div>";
            Literal1.Text += "</div>";

            if (myUser.id > 200000)
            {
                //add collegues from series
                List<User> myCollegues = SQLOperations.GetStudentsFromSeriesByStudentId(myUser.id);

                Literal1.Text += "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
                Literal1.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
                int counter = 0;
                Literal1.Text += "<br><h2 style = \"text-align:center;\">My collegues</h2><br>";
                Literal1.Text += "<table>";
                foreach (var myCollegue in myCollegues)
                {
                    if (myCollegue.id != myUser.id)
                    {
                        if (counter % 3 == 0) Literal1.Text += "<tr>";
                        Literal1.Text += "<td >";
                        Literal1.Text += "<a href= \"\\Pages\\Profile.aspx\\" + myCollegue.id + "\" >";
                        Literal1.Text += "<h3>" + myCollegue.surname + " " + myCollegue.lastname + " </h3>";
                        Literal1.Text += "<img border=\"0\" src=" + myCollegue.photo_url + " alt=" + myCollegue.surname + " " + myCollegue.lastname + "width = \"300px\" height = \"300px\"></a>";
                        Literal1.Text += "</td>";
                        if (counter % 3 == 2) Literal1.Text += "</tr>";
                        counter++;
                    }

                }
                if (counter % 3 != 0) Literal1.Text += "</tr>";
                Literal1.Text += "</table>";
                Literal1.Text += "</div>";
                Literal1.Text += "</div>";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            int profileID = Int32.Parse(path.Substring(path.LastIndexOf("/") + 1));
            User myUser = SQLOperations.GetUserById(profileID);
            ShowProfile(myUser);


        }
        protected void HomeImageButton_click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("\\Pages\\Home.aspx");
        }

        protected void MyAccount_click(object sender, EventArgs e)
        {
            Response.Redirect("\\Pages\\MyAccount.aspx");
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("\\Pages\\Home.aspx");
        }

        protected void AboutUs_Click(object sender, EventArgs e)
        {
            Response.Redirect("\\Pages\\AboutUs.aspx");
        }
    }
}