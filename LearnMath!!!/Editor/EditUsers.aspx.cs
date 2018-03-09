using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Editor_EditUsers : System.Web.UI.Page
{
    static string PathDB;
    static OleDbConnection conn;
    public static List<string> UsersList;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
        {
            Response.Redirect("../MainPage.aspx");
        }
        else
        {
            PathDB = @"" + Server.MapPath("~/ ") + (char)92 + "App_Data" + (char)92 + "Database1.mdb";
            string Query = "Select Email,UserName,Status From Users Where Role='Stud'";
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
            OleDbCommand Command = new OleDbCommand(Query, conn); ; 
            conn.Open();
            string HTML = "<table><tr><th>Επώνυμο και Όνομα</th><th>E-Mail</th><th>Ενεργοί</th><th>Αδρανείς</th><th>Αποκλεισμένοι</th></tr>";
              
            UsersList = new List<string>();
            using (OleDbDataReader reader = Command.ExecuteReader())
            {
                while(reader.Read())
                {
                    UsersList.Add((string)reader[0]);
                    HTML = HTML + "<tr><td>" + (string)reader[1] + "</td><td>" + (string)reader[0] + "</td>" +
                        "<td><input type = 'radio' name = '"+ (string)reader[0] + "' value = 'active' 1#"+ (string)reader[0] + "#1></td>" +
                        "<td><input type = 'radio' name = '"+ (string)reader[0] + "' value = 'inactive' 2#"+ (string)reader[0] + "#2></td>" +
                        "<td><input type = 'radio' name = '"+ (string)reader[0] + "' value = 'ban' 3#"+ (string)reader[0] + "#3></td>";
                    if((string)reader[2]== "active")
                    {
                        HTML = HTML.Replace("1#"+ (string)reader[0] + "#1", "checked");
                        HTML = HTML.Replace("2#"+ (string)reader[0] + "#2", "");
                        HTML = HTML.Replace("3#"+ (string)reader[0] + "#3", "");
                    }
                    else if((string)reader[2] == "inactive")
                    {
                        HTML = HTML.Replace("1#"+ (string)reader[0] + "#1", "");
                        HTML = HTML.Replace("2#"+ (string)reader[0] + "#2", "checked");
                        HTML = HTML.Replace("3#"+ (string)reader[0] + "#3", "");
                    }
                    else if((string)reader[2] == "ban")
                    {
                        HTML = HTML.Replace("1#"+ (string)reader[0] + "#1", "");
                        HTML = HTML.Replace("2#"+ (string)reader[0] + "#2", "");
                        HTML = HTML.Replace("3#"+ (string)reader[0] + "#3", "checked");
                    }
                }
            }
            conn.Close();
            Users.InnerHtml = HTML+ "</table>";
        }
    }

    protected void UpDStatus_Click(object sender, EventArgs e)
    {
        OleDbCommand Qcomand;
        string Query;
        conn.Open();
        for (int i=0;i<UsersList.Count;i++)
        {

            string X = Request.Form[UsersList[i]];
            Debug.WriteLine(i+" X="+X);
            Query = "UPDATE Users Set Users.Status = @0 WHERE Users.Email = @1";
            Qcomand = new OleDbCommand(Query,conn);
            Qcomand.Parameters.AddRange(new OleDbParameter[]
            {
                    new OleDbParameter("@0",X),
                    new OleDbParameter("@1",  UsersList[i]),
                   
            });
            Qcomand.ExecuteNonQuery();
        }
        conn.Close();
        Response.Redirect("~/Editor/EditUsers.aspx");


    }
}