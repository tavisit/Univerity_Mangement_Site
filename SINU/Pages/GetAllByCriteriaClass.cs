using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SINU.Pages
{
    public class GetAllByCriteriaClass : SQLOperations
    {

        public static List<int> getAllTeachersID()
        {
            List<int> allTeachers = new List<int>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("select id from Teachers", con);
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
                allTeachers.Add((int)dt.Rows[i][0]);
            }

            return allTeachers;
        }

        public static List<int> getAllWorkingStaffID()
        {
            List<int> allWorkingStaff = new List<int>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("select id from Working_staff", con);
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
                allWorkingStaff.Add((int)dt.Rows[i][0]);
            }

            return allWorkingStaff;
        }
        public static List<int> getAllStudentsID()
        {
            List<int> allStudents = new List<int>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("select id from Student", con);
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
                allStudents.Add((int)dt.Rows[i][0]);
            }

            return allStudents;
        }

        public static List<int> getAllSubjectsIDByYear(int year)
        {
            List<int> allSubjects = new List<int>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlConn = new SqlCommand("select id from Subjects where year = @year", con);
            sqlConn.Parameters.AddWithValue("@year", year);
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
                allSubjects.Add((int)dt.Rows[i][0]);
            }

            return allSubjects;
        }
    }
}