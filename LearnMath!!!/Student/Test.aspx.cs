using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_Test : System.Web.UI.Page
{

    static string PathDB;
    static OleDbConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        PathDB = @""+Server.MapPath("~/ ")+(char)92+"App_Data"+(char)92+"Database1.mdb";
        if (!ClientScript.IsStartupScriptRegistered("Start"))
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "Start", "Start();", true);
        }
    }
    [System.Web.Services.WebMethod]
    public static string[][] LoadTest(string LessonID)
    {
        
        HttpContext context = HttpContext.Current;
        string UserID = (string)(context.Session["ID"]);
        
        conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
        string Query = "select (abs(NeedHelp * 5) + 5) from Stats where LessonID = "+ LessonID + " and UserEmail = '"+ UserID + "';";
        OleDbCommand myAccessCommand = new OleDbCommand(Query, conn);
        conn.Open();
        OleDbDataReader reader = myAccessCommand.ExecuteReader();
        int Qnumb = 5; 
        if(reader.Read())
        {
            Qnumb = Int32.Parse(reader[0].ToString());
            Debug.WriteLine("OK 1");
            Debug.WriteLine(Qnumb);
        }
        string[][] Q = new string[Qnumb][];
        //////////////////////////////
        Query = "SELECT TOP "+Qnumb+ " Tests.Question, Tests.A, Tests.B, Tests.C, Tests.D, Tests.Answer FROM Tests where LessonID = " + LessonID + " ORDER BY rnd(-(100000*TestID)*Time()) ;";
        myAccessCommand = new OleDbCommand(Query, conn);
        
        reader = myAccessCommand.ExecuteReader();
        int i = 0;
            while(reader.Read())
            {
            Debug.WriteLine("OK 2");
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
    /////
    //SaveTest
    /////
    protected void SaveTest_Click(object sender, EventArgs e)
    {
        int LeID = int.Parse(LessonID.Value);
        conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
        float score = float.Parse(Score.Value);
        string UserID = Session["ID"].ToString();
        long moment;
        OleDbCommand Qcomand;
        if (LeID != -1)
        {
            //LessonTest
            conn.Open();
            string Query = "select Grade from Stats where Stats.UserEmail = '" + UserID + "' AND Stats.LessonID = " + LeID + ";";
            Qcomand = new OleDbCommand(Query, conn);
            OleDbDataReader reader = Qcomand.ExecuteReader();
            bool help = false;
            if (reader.Read())
            {
                score = Math.Max(score, float.Parse(reader[0].ToString()));
                help = score <= 50;
            }

            string upQuery = "UPDATE Stats Set Stats.Grade = @0, Stats.NeedHelp = @1 WHERE Stats.UserEmail = @2 AND Stats.LessonID = @3";

            Qcomand = new OleDbCommand(upQuery, conn);
            Qcomand.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@0",score ),
                 new OleDbParameter("@1",Convert.ToInt32(help)*(-1)),
                new OleDbParameter("@2",UserID ),
                new OleDbParameter("@3",LeID)
            }
                );
            Qcomand.ExecuteNonQuery();
            int MaxLID = Int32.Parse((string)Session["MAX_LessonID"]);
            if (score> 50f && LeID < (int)Session["NumberOfLessons"] && LeID >= MaxLID)
            {
                Qcomand = new OleDbCommand();
                Qcomand.Connection = conn;
                Qcomand.CommandType = CommandType.Text;
                Qcomand.CommandText = "insert into Stats ([UserEmail],[LessonID],[Grade]) values (@userid,@lessonid,@grade);";
                Qcomand.Parameters.AddRange(new OleDbParameter[]
                {       new OleDbParameter("@userid", UserID),
                    new OleDbParameter("@lessonid", (LeID+1)),
                    new OleDbParameter("@grade", -1)
                }
                );
                Qcomand.ExecuteNonQuery();
                moment = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                Qcomand = new OleDbCommand();
                Qcomand.Connection = conn;
                Qcomand.CommandType = CommandType.Text;
                Qcomand.CommandText = "insert into Messages ([Time],[UserID],[Message]) values (@time,@userid,@message);";
                Qcomand.Parameters.AddRange(new OleDbParameter[]
               {
                    new OleDbParameter("@time",moment),
                    new OleDbParameter("@userid",  UserID),
                    new OleDbParameter("@message","Είναι διαθέσιμο το επόμενο κεφάλαιο,πατήστε <a href='Lessons.aspx'>εδώ</a>")
                });
                Qcomand.ExecuteNonQuery();

            }
            if(score > 50f && LeID == (int)Session["NumberOfLessons"])
            {
                moment = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                Qcomand = new OleDbCommand();
                Qcomand.Connection = conn;
                Qcomand.CommandType = CommandType.Text;
                Qcomand.CommandText = "insert into Messages ([Time],[UserID],[Message]) values (@time,@userid,@message);";
                Qcomand.Parameters.AddRange(new OleDbParameter[]
                {
                    new OleDbParameter("@time",moment),
                    new OleDbParameter("@userid",  UserID),
                    new OleDbParameter("@message","Το τελικό τεστ είναι διαθέσιμο,πατήστε <a href='http://localhost:49806/Student/FinalTest.aspx'>εδώ</a>")
                });
                Qcomand.ExecuteNonQuery();
            }


            conn.Close();
           
        }
       
            moment = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            conn.Open();
            //Final Test
            Qcomand = new OleDbCommand();
            Qcomand.Connection = conn;
            Qcomand.CommandType = CommandType.Text;
            Qcomand.CommandText = "insert into TimeSiries ([UserEmail],[TimeFinish],[Grade],[LessonID]) values (@userid,@moment,@grade,@lessonID);";
            Qcomand.Parameters.AddRange(new OleDbParameter[]
            {       new OleDbParameter("@userid", UserID),
                    new OleDbParameter("@moment", moment),
                    new OleDbParameter("@grade", score),
                    new OleDbParameter("@lessonID", LeID)
            }
            );
            Qcomand.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("StudentMain.aspx");
        }
    

   
}