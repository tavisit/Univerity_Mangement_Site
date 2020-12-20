using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SINU.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private void readFile()
        {
            List<ScrappedInfo> scrappedInfos = new List<ScrappedInfo>();
            scrappedInfos = Scrapper.MainScrapper();
            ListNews.Text = "<table style = \"margin-left: 5%;margin-right:5%;\">";
            int counter = 0;
            foreach (var n in scrappedInfos)
            {
                if (counter % 2 == 0) ListNews.Text += "<tr>";
                ListNews.Text += "<td >";
                 ListNews.Text += "<a  href = \"" + n.link + "\" ><h3>" + n.title + " </h3>";
                ListNews.Text += "<img border=\"0\" src=" + n.image_url + " width = \"300px\" height = \"300px\"></a>";
                ListNews.Text += "</td>";
                if (counter % 2 == 1) ListNews.Text += "</tr>";
                counter++;
                if (counter >= 10) break;

            }
            ListNews.Text += "</table>";
        }
        private void ViewProfile()
        {
            SINU.User myProfile = SQLOperations.GetUserByEmail(Session["email"].ToString().Replace(" ", ""));
            MyProfileLiteral.Text = "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
            MyProfileLiteral.Text += "<div style = \"margin-left: 5%;margin-right:5%;\">";
            MyProfileLiteral.Text += "<h1 style=\"text-align:center;\">" + myProfile.surname + " " + myProfile.lastname + "</h1></br>";
            MyProfileLiteral.Text += "<img style = \"margin-left: auto;margin-right: auto;display: block;\" border=\"0\" src=" + myProfile.photo_url + " width = \"250px\" height = \"250px\">";
            MyProfileLiteral.Text += "</div>";
            MyProfileLiteral.Text += "</div>";

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Scrapper.MainScrapper();
            readFile();
            if (Session["email"] != null)
            {

                ViewProfile();
            }
            else
            {
                MyProfileLiteral.Text = "";
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
    }
}