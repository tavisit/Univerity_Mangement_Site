using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINU.Pages
{
    public class UsefulHtmlStuff
    {
        // collegues from series display
        public static String colleguesInfo(int my_id)
        {
            String myHtml = "";
            //add collegues from series
            List<User> myCollegues = SQLOperations.GetStudentsFromSeriesByStudentId(my_id);

           myHtml += "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
           myHtml += "<div style = \"margin-left: 5%;margin-right:5%;\">";
            int counter = 0;
           myHtml += "<br><h2 style = \"text-align:center;\">My collegues</h2><br>";
           myHtml += "<table>";
            foreach (var myCollegue in myCollegues)
            {
                if (myCollegue.id != my_id)
                {
                    if (counter % 3 == 0)myHtml += "<tr>";
                   myHtml += "<td >";
                   myHtml += "<a href= \"Profile.aspx\\" + myCollegue.id + "\" >";
                   myHtml += "<h3>" + myCollegue.surname + " " + myCollegue.lastname + " </h3>";
                   myHtml += "<img border=\"0\" src=" + myCollegue.photo_url + " alt=" + myCollegue.surname + " " + myCollegue.lastname + "width = \"300px\" height = \"300px\"></a>";
                   myHtml += "</td>";
                    if (counter % 3 == 2)myHtml += "</tr>";
                    counter++;
                }

            }
            if (counter % 3 != 0)myHtml += "</tr>";
           myHtml += "</table>";
           myHtml += "</div>";
           myHtml += "</div>";
            return myHtml;
        }
        // general information display ( name, surname, photo etc)
        public static String generalInfo(User myUser)
        {
            String myHtml = "";
            //general information
           myHtml = "<br><br><div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
           myHtml += "<div style = \"margin-left: 5%;margin-right:5%;\">";
           myHtml += "<h1 style=\"text-align:center;\">" + myUser.surname + " " + myUser.lastname + "</h1></br>";
            if(myUser.id>200000)myHtml += "<h1 style=\"text-align:center;\">" + myUser.Student.Univ_info.specialization + "</h1></br>";
            myHtml += "<img style = \"margin-left: auto;margin-right: auto;display: block;\" border=\"0\" src=" + myUser.photo_url + " width = \"250px\" height = \"250px\">";
           myHtml += "<br><br></div>";
           myHtml += "</div>";
            
            return myHtml;
        }
        // teacher information about them
        public static String teacherInfo(User myUser)
        {
            String myHtml = "";
            // add teacher information
            Employee currentEmployee = SQLOperations.GetEmployeesInformationById(myUser.id);
            myHtml += "<div style = \"margin-left: 5%;margin-right:5%;background-color:white;\">";
            myHtml += "<div style = \"margin-left: 5%;margin-right:5%;\">";
            myHtml += "<br><br><h2> My current status<br><h2>";
            myHtml += "<h4> My id is: " + currentEmployee.id + "<br></h4>";
            myHtml += "<h4> A seniority of: " + currentEmployee.seniority + " years</h4>";
            myHtml += "<h4> My current working status is: " + currentEmployee.Status_Job.description + "</h4><br><br>";

            myHtml += "<div style = \"height:10px;background-color:purple;\">";
            myHtml += "</div>";

            myHtml += "<br><br><h2> Teaching status</h2>";
            myHtml += "<h4> I am a " + currentEmployee.Teacher.Teaching_Types.name + "</h4>";
            myHtml += "<h4> That means I have to do: " + currentEmployee.Teacher.Teaching_Types.description + "</h4>";
            myHtml += "<h4> My Phd is located here: <a target=\"_blank\" href = \"http://" + currentEmployee.Teacher.PhD_Certificate + "\">PhD site</a></h4>";
            myHtml += "<h4> I have a number of " + currentEmployee.Teacher.publications + " publications</h4><br><br>";

            myHtml += "<div style = \"height:10px;background-color:purple;\">";
            myHtml += "</div>";

            myHtml += "<br><br><h2> The departemnt where I work</h2>";
            foreach (var item in currentEmployee.Teacher.Info_department)
            {
                Info_department myDepartment = item;
                if (myDepartment.id_head != myUser.id) myHtml += "<h4> The head of the department has the id " + myDepartment.id_head.ToString() + "</h4>";
                else myHtml += "<h4> I am the head of the department</h4>";
                myHtml += "<h4> " + myDepartment.description + "</h4>";
                myHtml += "<h4> I have " + myDepartment.building + " students enrolled in this department</h4>";
            }

            myHtml += "<br><br></div>";
            myHtml += "</div>";

            return myHtml;
        }
    }
}