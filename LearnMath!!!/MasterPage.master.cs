using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    static bool i = true;
    static string PathDB;
    static OleDbConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["ID"] == null)
        {
            Response.Redirect("../MainPage.aspx");
        }
        else
        {
            if ((string)Session["Role"] == "Stud")
            {
                StudentHome.Visible = true;
                Show_Stud();
            }
            else if ((string)Session["Role"] == "Prof")
            {
                ProfHome.Visible = true;
                Show_Prof();
                Show_Editor();
            }
            else if ((string)Session["Role"] == "Editor")
            {
                EditorHome.Visible = true;
                Show_Editor();
            }
        }
    }

    

    public void Show_Stud()
    {
        
        StudentStats.Visible = true;
        Lessons_B.Visible = true;
        GotoTests.Visible = true;
        FinalTest.Visible = true;
    }
    public void Show_Prof()
    {
       
        Studens_Stats.Visible = true;
        CreateUser.Visible = true;
    }
    public void Show_Editor()
    {     
        EditUsers.Visible = true;
    }
    protected void LogOut_Click(object sender, EventArgs e)
    {
        Session["UuerName"] = null;
        Session["ID"] = null;
        Response.Redirect("~/MainPage.aspx");
    }
    ////////////////////////STUD BEGIN////////////////////////
    protected void Lesson_Click(object sender, EventArgs e)
    { 
        Response.Redirect("~/Student/Lessons.aspx");
    }


    protected void StudentStats_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Student/StudStats.aspx");
    }

    protected void StudentHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Student/StudentMain.aspx");
    }

    protected void FinalTest_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Student/FinalTest.aspx");
    }
    protected void GotoTests_Click(object sender, EventArgs e)
    {
        Session["FromTestButton"] = "true";
        Response.Redirect("Lessons.aspx");
    }
    ////////////////////////STUD END////////////////////////

    ////////////////////////PROF BEGIN//////////////////////

    protected void Studens_Stats_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Profesor/StudentProgress.aspx");
    }
    protected void ProfHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Profesor/ProfesorMain.aspx");
    }
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Profesor/CreateUser.aspx");
    }
    ////////////////////////PROF END////////////////////////

    ////////////////////////EDITOR BEGIN//////////////////////

    protected void EditUsers_Click(object sender, EventArgs e)
    {
       
        
            Response.Redirect("~/Editor/EditUsers.aspx");
        
    }

    protected void EditorHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Editor/EditorMain.aspx");
    }

    ////////////////////////EDITOR END//////////////////////



}
