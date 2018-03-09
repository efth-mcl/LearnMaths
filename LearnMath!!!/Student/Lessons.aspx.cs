using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lessons : System.Web.UI.Page
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
                //Number Of Lessons 
                int NL = 0;
                while (reader.Read())
                {
                    X = X + reader[0].ToString() + ",";
                    NL++;
                }
                X = X.Remove(X.Length - 1);
                //Max_lessonID =lessonID can student stady
                Session["MAX_LessonID"] = X.Substring(X.Length - 1, 1);
                Session["NumberOfLessons"] = NL;
                if (!ClientScript.IsStartupScriptRegistered("LessonPermit"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "LessonPermit", "LessonPermit('" + X + "');", true);
                }
                H1class1.InnerText = "Παρακάτω μπορείτε να δείτε τη θεωρία για κάθε κεφάλαιο";
                if ((string)Session["FromTestButton"] =="true")
                {

                    if (!ClientScript.IsStartupScriptRegistered("FromTestB"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                            "FromTestB", "FromTestB();", true);
                    }
                    H1class1.InnerText = "Παρακάτω μπορείτε να δείτε τα tests για κάθε κεφάλαιο";
                    Session["FromTestButton"] = null;
                }


            }

            conn.Close();
            if(Session["LIDtoS"]!=null)
            {
                if (!ClientScript.IsStartupScriptRegistered("LoadLesson"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "LoadLesson", "LoadLesson('" + (string)Session["LIDtoS"] + "');", true);
                }
                Session["LIDtoS"] = null;
            }
        }
    }

    }
    
