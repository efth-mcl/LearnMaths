using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Profesor_CreateUser : System.Web.UI.Page
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
        }
    }

    protected void Register_Button_Click(object sender, EventArgs e)
    {


        string Surname = RegSurName.Value;
        string Name = RegName.Value;
        string Email = RegEmail.Value;
        string Pass = RegPass.Value;
        string RePass = RegPassRe.Value;
        string Role = Request.Form["radio"];

        bool Error = false;
        //Name
        if (Name == "")
        {
            Error = true;
            ErrorName.Text = "Συμπληρώστε αυτό το πεδίο.";
            ErrorName.Visible = true;
        }
        else
        {
            ErrorName.Visible = false;
        }
        //SurName
        if (Surname == "")
        {
            Error = true;
            ErrorSurName.Text = "Συμπληρώστε αυτό το πεδίο.";
            ErrorSurName.Visible = true;

        }
        else
        {
            ErrorSurName.Visible = false;
        }
        //Email

        conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
        conn.Open();
        string Query;
        OleDbCommand myAccessCommand;
        if (EmailCheck(Email))
        {
            Query = "SELECT Email from Users where Email='" + Email + "'";
            myAccessCommand = new OleDbCommand(Query, conn);

            OleDbDataReader reader = myAccessCommand.ExecuteReader();
            if (reader.Read())
            {
                Error = true;
                ErrorEmail.Visible = true;
                ErrorEmail.Text = "Υπάρχει ήδη χρήστης με αυτό το email.";
            }
            else
            {
                ErrorEmail.Visible = false;
            }
        }
        else
        {
            Error = true;
            ErrorEmail.Text = "Μη έγκυρο Email.";
            ErrorEmail.Visible = true;
        }
        //Pas
        if (Pass == "")
        {
            Error = true;
            ErrorPass.Text = "Συμπληρώστε αυτό το πεδίο.";
            ErrorPass.Visible = true;
        }
        else
        {
            ErrorPass.Visible = false;
        }
        if (Pass != RePass)
        {
            Error = true;
            ErrorRePass.Text = "Πρέπει οι κωδικοί να είναι ίδιοι.";
            ErrorRePass.Visible = true;
        }
        else
        {
            ErrorRePass.Visible = false;
        }
        if ((Role != "Editor" && Role != "Stud"))
        {
            Error = true;
            ErrorRadio.Text = "Επιλέξτε έναν ρόλο χρήστη.";
            ErrorRadio.Visible = true;
        }
        else
        {
            ErrorRadio.Visible = false;
        }
        if (Error == false)
        {
            Query = "insert into Users ([UserName],[Password],[Email],[Role],[Status]) values (@user,@pas,@email,@role,@status)";
            myAccessCommand = new OleDbCommand(Query, conn);
            myAccessCommand.Connection = conn;

            MD5 md5 = MD5.Create();
            Pass = GetMd5Hash(md5, Pass);
            myAccessCommand.Parameters.AddRange(new OleDbParameter[]
           {   new OleDbParameter("@user", Surname+" "+Name),
               new OleDbParameter("@pas", Pass),
               new OleDbParameter("@email", Email),
               new OleDbParameter("@role", Role),
               new OleDbParameter("@status", "active")
           }
           );
            myAccessCommand.ExecuteNonQuery();
            if (Role == "Stud")
            {
                Query = "insert into Stats ([UserEmail],[LessonID],[Grade]) values (@usermail,@lessonid,@grade)";
                myAccessCommand = new OleDbCommand(Query, conn);
                myAccessCommand.Connection = conn;
                myAccessCommand.Parameters.AddRange(new OleDbParameter[]
               {   new OleDbParameter("@usermail", Email),
               new OleDbParameter("@lessonid", 1),
               new OleDbParameter("@grade", -1)
               }
               );
                myAccessCommand.ExecuteNonQuery();
            }
            OK_orNot_Reg.Visible = true;
            RegSurName.Value = "";
            RegName.Value = "";
            RegEmail.Value = "";
        }
        else
        {
            OK_orNot_Reg.Visible = false;
        }
        conn.Close();
    }
    private bool EmailCheck(string Email)
    {
        string[] email = Email.Split('@');
        return email.Length == 2 && (email[email.Length - 1].Contains(".com") || email[email.Length - 1].Contains(".gr"));

    }
    static string GetMd5Hash(MD5 md5Hash, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
}