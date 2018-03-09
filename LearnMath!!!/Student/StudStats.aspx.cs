using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudStats : System.Web.UI.Page
{
    

    static string PathDB;
    static OleDbConnection conn;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        PathDB = @"" + Server.MapPath("~/ ") + (char)92 + "App_Data" + (char)92 + "Database1.mdb";
        if (Session["ID"] == null)
        {
            Response.Redirect("../MainPage.aspx");
        }
        else
        {
            string Uid = Session["ID"].ToString();
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
            string QLogin = "SELECT Lessons.LessonName FROM Lessons ORDER BY Lessons.LessonID " +
                            "UNION ALL " +
                            "SELECT MAX(Stats.LessonID)FROM Stats WHERE Stats.UserEmail = '" + Uid + "';";
            OleDbCommand myAccessCommand = new OleDbCommand(QLogin, conn);
            conn.Open();
            using (OleDbDataReader reader = myAccessCommand.ExecuteReader())
            {
                string X = "";
                while (reader.Read())
                {
                    X = X + reader[0].ToString() + ",";
                }
                X = X.Remove(X.Length - 1);
                if (!ClientScript.IsStartupScriptRegistered("LessonPermit"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "LessonPermit", "LessonPermit('" + X + "');", true);
                }
            }
            conn.Close();
        }
    }
    [System.Web.Services.WebMethod]
    public static object[] GraphMethod(string MethodID)
    {
        conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
        if (MethodID == "MaxHist")
        {
            
            ///Hist//////////////////////////////////////////////////////////////////////////////////////////////////////
            string Q = "SELECT Lessons.LessonName,Stats.Grade " +
                       "FROM Lessons LEFT JOIN Stats ON " +
                       "(Lessons.LessonID = Stats.LessonID AND Stats.UserEmail = '" + (string)HttpContext.Current.Session["ID"] + "') "+
                       "UNION ALL "+
                       "SELECT 'Τελικό Τεστ',max(TimeSiries.Grade) FROM TimeSiries Where TimeSiries.UserEmail = '" + (string)HttpContext.Current.Session["ID"] + "' AND TimeSiries.LessonID=-1;";
            conn.Open();
            
            OleDbCommand Command = new OleDbCommand(Q, conn);
            
            List<string> HistDataX = new List<string>();
            List<int> HistDataY = new List<int>();
            Debug.WriteLine(Q);
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
            if(HistDataX.Count!=HistDataY.Count)
            {
                HistDataX.RemoveAt(HistDataX.Count - 1);
            }
            object[] Hist = new object[2];
            Hist[0] = HistDataX.ToArray();
            Hist[1] = HistDataY.ToArray();
            conn.Close();
            return Hist;
        }
        else if(MethodID.Substring(0,1)=="L")
        {
            string Q = "SELECT TimeSiries.TimeFinish,TimeSiries.Grade FROM TimeSiries "+
            "WHERE TimeSiries.UserEmail='" + (string)HttpContext.Current.Session["ID"] + "' AND TimeSiries.LessonID="+ int.Parse(MethodID.Substring(1)) + ";";
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
        return new object[] { "error" };
        ///TIMESIRIES//////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }


    //[System.Web.Services.WebMethod]
    //public static void TimeLesson(string MethoID)
    //{
    //    string Q = "";
    //    OleDbCommand myAccessCommand = new OleDbCommand(Q, conn);
    //}
}