using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profesor_ProfesorMain : System.Web.UI.Page
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
            
        }
    }
    [System.Web.Services.WebMethod]
    public static void RmMessage(string MessID)
    {
        string Query = "DELETE FROM Messages WHERE [UserID] = '"+ (string)HttpContext.Current.Session["ID"] + "' AND [Time] = "+ double.Parse(MessID) + "; ";
        conn.Open();
        OleDbCommand Qcomand = new OleDbCommand(Query, conn);
        Qcomand.ExecuteNonQuery();
        conn.Close();
    }
}