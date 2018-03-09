using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PathDB = @"" + Server.MapPath("~/ ") + (char)92 + "App_Data" + (char)92 + "Database1.mdb";
    }
    static string PathDB;
    static OleDbConnection conn;

    protected void Register(object sender, EventArgs e)
    {
        string Surname = RegSurName.Value;
        string Name = RegName.Value;
        string Email = RegEmail.Value;
        string Pass = RegPass.Value;
        string RePass = RegPassRe.Value;
        int ErrorInt = 0;
        if ((Name == "" || Surname=="" || Email == "" || Pass == ""  || RePass == ""))
        {
            ErrorInt += 1;
        }
        if (Pass.Length < 8)
        {
            ErrorInt += 10;
        }
        else
        {
            if (Pass != RePass)
            {
               ErrorInt += 20;
            }
        }
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
                ErrorInt += 200;
            }
        }
       else
        {
            ErrorInt += 100;
        }
       if (ErrorInt == 0)
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
               new OleDbParameter("@role",  "Stud"),
                new OleDbParameter("@status", "inactive")

           }
           );
           myAccessCommand.ExecuteNonQuery();
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
            Query = "Select Email From Users Where Role='Prof' or Role='Editor';";
            myAccessCommand = new OleDbCommand(Query, conn);

            OleDbDataReader reader = myAccessCommand.ExecuteReader();
            List<string> Userslist = new List<string>();
            while(reader.Read())
            {
                Userslist.Add((string)reader[0]);
            }
            for(int i=0;i< Userslist.Count;i++)
            {
                long moment = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                OleDbCommand Qcomand = new OleDbCommand();
                Qcomand.Connection = conn;
                Qcomand.CommandType = CommandType.Text;
                Debug.WriteLine("OK");
                Qcomand.CommandText = "insert into Messages ([Time],[UserID],[Message]) values (@time,@userid,@message);";
                Qcomand.Parameters.AddRange(new OleDbParameter[]
                {
                    new OleDbParameter("@time",moment),
                    new OleDbParameter("@userid",  Userslist[i]),
                    new OleDbParameter("@message","Προστέθηκε νέος χρήστης με <br>email : "+Email+"<br>Επώνυμο :"+Surname+" <br> Όνομα : "+Name)
                });
                Qcomand.ExecuteNonQuery();
            }

        }
       else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mykey", "RegError("+ ErrorInt + ");", true);
            RegName.Value = Name;
            RegSurName.Value = Surname;
            RegEmail.Value = Email;
        }
        conn.Close();

    }
     private bool EmailCheck(string Email)
    {
        string[] email = Email.Split('@');
        return email.Length == 2 && (email[email.Length - 1].Contains(".com") || email[email.Length - 1].Contains(".gr"));
    }
    protected void LogIn(object sender, EventArgs e)
    {
        //////////////

        //////////////
        conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathDB + ";");
        string email = Request.Form["LogUser"];
        string Pass = Request.Form["LogPas"];
        if(email!="" && Pass!="")
        { 
        string QLogin = "SELECT * from Users where Email='" + email + "'";
        OleDbCommand myAccessCommand = new OleDbCommand(QLogin, conn);
        conn.Open();
            using (OleDbDataReader reader = myAccessCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    if ((string)reader[4] == "inactive")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mykey", "LogError('2');", true);
                    }
                    else if((string)reader[4] == "ban")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mykey", "LogError('3');", true);
                    }
                    else if ((string)reader[4] == "active")
                    {
                        MD5 md5 = MD5.Create();
                        if (VerifyMd5Hash(md5, Pass, reader[2].ToString()))
                        {
                            Session["UuerName"] = reader[1].ToString();
                            Session["ID"] = reader[0].ToString();
                            Session["Role"] = reader[3].ToString();
                            if ((string)Session["Role"] == "Prof")
                                Response.Redirect("Profesor/ProfesorMain.aspx");
                            else if ((string)Session["Role"] == "Stud")
                                Response.Redirect("Student/StudentMain.aspx");
                            else if ((string)Session["Role"] == "Editor")
                                Response.Redirect("Editor/EditorMain.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mykey", "LogError('1');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mykey", "LogError('1');", true);
                    }
                }
                
            }
            conn.Close();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mykey", "LogError('1');", true);
        }

        
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
    static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
    {
        // Hash the input.
        string hashOfInput = GetMd5Hash(md5Hash, input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        if (0 == comparer.Compare(hashOfInput, hash))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}