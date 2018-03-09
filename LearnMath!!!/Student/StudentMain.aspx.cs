using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    static string PathDB;
    static OleDbConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        PathDB = @"" + Server.MapPath("~/ ") + (char)92 + "App_Data" + (char)92 + "Database1.mdb";
        if (Session["ID"]==null)
        {
            Response.Redirect("../MainPage.aspx");
        }
       else
        {
            
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");

            //NEWS
            PathDB = @"" + Server.MapPath("~/ ") + (char)92 + "App_Data" + (char)92 + "Database1.mdb";
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");

            string HTML = Class1.News(conn);
            if (HTML != "<table></table>")
            {
                news.InnerHtml = HTML;
            }
            else
            {
                news.InnerHtml = "Δεν υπάρχει κάποια καινούργια είδηση.";
            }
            /////
            string Query = "SELECT Lessons.LessonID,Lessons.LessonName FROM Lessons INNER JOIN " +
                "Stats ON (Lessons.LessonID = Stats.LessonID AND Stats.UserEmail = '" + (string)Session["ID"] + "' AND Stats.NeedHelp = -1); ";
            OleDbCommand Comand = new OleDbCommand(Query, conn);
            conn.Open();

            HelpDiv.Visible = false;
            using (OleDbDataReader reader = Comand.ExecuteReader())
            {
                HTML = "";

                if (reader.Read())
                {
                    string LID = reader[0].ToString();
                    string LName= reader[1].ToString();
                    HTML = HTML + "<input type='radio' name='radio' id='Lesson"+LID+ "' value='"+LID+ "'/>" +
                                 "<label for= 'Lesson" + LID+"' >"+ LName + "</label >";
                }
                if (HTML != "")
                {
                    news.InnerHtml = "";
                    HTML = "<h1>Xρειαζεται περισσοτερη εξασκηση στο παρακάτω κεφάλαιo.</h1>" + HTML;
                    HelpDiv.Visible = true;
                    HelpDiv.InnerHtml = HTML;
                }

            }
            conn.Close();
        }
    }
    [System.Web.Services.WebMethod]
    public static void GoToStadyThisLesson(string LessonID)
    {
        HttpContext context = HttpContext.Current;
        context.Session["LIDtoS"] = LessonID;
        

    }
    [System.Web.Services.WebMethod]
    public static void RmMessage(string MessID)
    {
        string Query = "DELETE FROM Messages WHERE [UserID] = '" + (string)HttpContext.Current.Session["ID"] + "' AND [Time] = " + double.Parse(MessID) + "; ";
        conn.Open();
        OleDbCommand Qcomand = new OleDbCommand(Query, conn);
        Qcomand.ExecuteNonQuery();
        conn.Close();
    }
}