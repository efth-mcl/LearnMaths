using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
  public static  string News(OleDbConnection conn)
    {
        string Query = "Select Message,Time From Messages Where UserID='" + (string)HttpContext.Current.Session["ID"] + "';";
        conn.Open();
        OleDbCommand Qcomand = new OleDbCommand(Query, conn);
        OleDbDataReader reder = Qcomand.ExecuteReader();
        string HTML = "<table>";
        while (reder.Read())
        {
            HTML = HTML + "<tr id='" + ((double)reder[1]).ToString() + "'><td><p>" + (string)reder[0] + "</p></td><td class='rmM'></td></tr>";
        }
        conn.Close();
        HTML = HTML + "</table>";
        return HTML;
    }



 }
