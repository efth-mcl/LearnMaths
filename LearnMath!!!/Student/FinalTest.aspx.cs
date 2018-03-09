using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_FinalTest : System.Web.UI.Page
{
    static string PathDB;
    static OleDbConnection conn;
    static Random _random = new Random();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
        {
            Response.Redirect("../MainPage.aspx");
        }
        else
        {
            PathDB = @"" + Server.MapPath("~/ ") + (char)92 + "App_Data" + (char)92 + "Database1.mdb";
            string Query = "SELECT  abs((max(Lessons.LessonID)=max(Stats.LessonID) and min(Stats.Grade)>=50))" +
                           "FROM Lessons, Stats WHERE Stats.UserEmail = '" + (string)Session["ID"] + "'";
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
            OleDbCommand myAccessCommand = new OleDbCommand(Query, conn);
            conn.Open();
            using (OleDbDataReader reader = myAccessCommand.ExecuteReader())
            {
                if(reader.Read())
                {
                    if(reader[0].ToString()=="1")
                    {
                        StartOrNot.InnerHtml = "Είναι διαθέσιμο το τεστ εφόλης της ύλης με ερωτήσεις από όλα τα κεφάλαια";
                        StartFinaleTest.Visible = true;


                    }
                    else
                    {
                        StartOrNot.InnerHtml = "Δεν είστε έτοιμοι για το τελικό τεστ. Παρακαλώ ολοκληρώστε τα test των κεφαλαίων";
                        StartFinaleTest.Visible = false;
                    }
                }
            }
            conn.Close();
            }
    }
    [System.Web.Services.WebMethod]
    public static string[][] LoadTest()
    {
        
        conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
        ///Sort Lessons
        string Query = "SELECT LessonID FROM Stats WHERE UserEmail='" + (string)HttpContext.Current.Session["ID"] + "'  ORDER BY Grade;";
        OleDbCommand myAccessCommand = new OleDbCommand(Query, conn);
        conn.Open();
        OleDbDataReader reader = myAccessCommand.ExecuteReader();
        Query = "";
        List<int> list = new List<int>();
        while (reader.Read())
        {
            
            Query = Query + " SELECT * from(select top %Qnumb"+(int)reader[0]+"% Question, A, B, C, D, Answer " +
                          "FROM Tests where LessonID = "+ (int)reader[0] + "  order by rnd(-(100000 * TestID) * Time())) UNION";
            list.Add((int)reader[0]);
        }
        int Qn = 0;
        for (int j=0;j< list.Count;j++)
        {
            Query = Query.Replace("%Qnumb" + list[j] + "%", 2 + list.Count-j-1+"");
            Qn += 2 + list.Count - j - 1;
        }

        Query = Query + "##";
        Query = Query.Replace("UNION##", ";");
       
        ///

        myAccessCommand = new OleDbCommand(Query, conn);
        string[][] Q = new string[Qn][];
        reader = myAccessCommand.ExecuteReader();
        int i = 0;
        while (reader.Read())
        {
            Debug.WriteLine(i+" "+ reader[0].ToString());
            Q[i] = new string[]
            {
                (string)reader[0],
                (string)reader[1],
                (string)reader[2],
                (string)reader[3],
                (string)reader[4],
                (string)reader[5]
            };

            i++;
        }
        conn.Close();
        return Q;

    }
}