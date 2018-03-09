using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profesor_StudentProgress : System.Web.UI.Page
{
    static string PathDB;
    static OleDbConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
        {
            Response.Redirect("../MainPage.aspx");
        }
        else
        {
            PathDB = @"" + Server.MapPath("~/ ") + (char)92 + "App_Data" + (char)92 + "Database1.mdb";
            string Query = "SELECT Email, UserName FROM Users Where Role='Stud'";
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
            OleDbCommand Command = new OleDbCommand(Query, conn); ;
            conn.Open();
            string HTML = "";
            List<string> UsersList = new List<string>();
            using (OleDbDataReader reader = Command.ExecuteReader())
            {
                int i = 0;
                
                while (reader.Read())
                {
                    UsersList.Add(reader[0].ToString());
                    HTML = HTML + "<input type='radio' name='radio' id='stud_" + i + "' value='" + reader[0].ToString() + "'/> " +
                                     "<label for= 'stud_" + i + "' >" + reader[1].ToString() + "</label >" +
                                     "<ul id='stats_stud_" + i + "' class='stats'><li class='MaxHist statsli'>Καλύτερες Επιδόσεις ανά τεστ</li><li class='L-1 statsli'>Πρόοδος Τελικού Τεστ</li><li class='TimePerLesson'>Πρόοδος ανά Κεφαλαίο<ul class='LessonsStatTime'>###" + i + "</ul></li></ul>"; 
                    i++;
                }
            }


            for (int i = 0; i < UsersList.Count; i++)
            {
                Query = "SELECT  Lessons.LessonName,Stats.LessonID " +
                       "FROM Lessons Left JOIN Stats ON " +
                       "(Lessons.LessonID = Stats.LessonID AND Stats.UserEmail = '" + UsersList[i] + "');";
                Command = new OleDbCommand(Query, conn);
                using (OleDbDataReader reader = Command.ExecuteReader())
                {
                    string X = "";
                    int j = 1;
                    while (reader.Read())
                    {
                        if (reader[1].ToString() =="")
                        {
                            X = X + "<li class='L" + j + " statsli'>" + (string)reader[0] + "</li>";
                        }
                        else
                        {
                            X = X + "<li class='L" +j+" statsli OK'>" + (string)reader[0] + "</li>";
                        }
                        j++;
                    }
                    HTML = HTML.Replace("###" + i, X);
                }
            }
            StudsList.InnerHtml = HTML;
            conn.Close();

        }
    }
    [System.Web.Services.WebMethod]
    public static object[] MaxHist(string StudID)
    {
        ///Hist//////////////////////////////////////////////////////////////////////////////////////////////////////
        string Q = "SELECT Lessons.LessonName,Stats.Grade " +
                   "FROM Lessons LEFT JOIN Stats ON " +
                   "(Lessons.LessonID = Stats.LessonID AND Stats.UserEmail = '" + StudID + "') " +
                   "UNION ALL " +
                   "SELECT 'Τελικό Τεστ',max(TimeSiries.Grade) FROM TimeSiries Where TimeSiries.UserEmail = '" + StudID + "' AND LessonID=-1;";
        conn.Open();
        OleDbCommand Command = new OleDbCommand(Q, conn);
        List<string> HistDataX = new List<string>();
        List<int> HistDataY = new List<int>();
        using (OleDbDataReader reader = Command.ExecuteReader())
        {
            int i = 0;
            while (reader.Read())
            {
                HistDataX.Add(reader[0].ToString());
                if (reader[1].ToString() == "-1")
                    HistDataY.Add(0);
                else if (reader[1].ToString() != "")
                    HistDataY.Add((int)reader[1]);
                i++;
            }


        }
        if (HistDataX.Count != HistDataY.Count)
        {
            HistDataX.RemoveAt(HistDataX.Count - 1);
        }
        object[] Hist = new object[2];
        Hist[0] = HistDataX.ToArray();
        Hist[1] = HistDataY.ToArray();
        conn.Close();
        return Hist;
    }
    [System.Web.Services.WebMethod]
    public static object[] TimeSiries(string StudID ,int LID)
    {
        conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
        string Q = "SELECT TimeSiries.TimeFinish,TimeSiries.Grade FROM TimeSiries " +
                   "WHERE TimeSiries.UserEmail='" + StudID + "' AND LessonID= "+LID+";";
        conn.Open();

        OleDbCommand Command = new OleDbCommand(Q, conn);
        List<double> TimeDataX = new List<double>();
        List<int> TimeDataY = new List<int>();
        using (OleDbDataReader reader = Command.ExecuteReader())
        {

            while (reader.Read())
            {
                TimeDataX.Add((double)reader[0]);
                TimeDataY.Add((int)reader[1]);
            }
        }
        conn.Close();
        object[] TimeData = new object[2];

        TimeData[0] = TimeDataX.ToArray();
        TimeData[1] = TimeDataY.ToArray();
        return TimeData;
    }
}