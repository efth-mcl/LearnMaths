<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProfesorMain.aspx.cs" Inherits="Profesor_ProfesorMain" %>

<asp:Content ID="JSc" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/ProfMainScript.js"></script>
    <script>
        $(document).ready(function () {
            $(".helpDiv").click(function () {
                x0p({
                    title: 'Βοήθεια',
                    text: "Μπορείτε να δείτε όλους τους νέους χρήστες στην αρχική σελίδα.",
                });
            });
            UserNews("ProfesorMain.aspx");
        });
    </script>
 </asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="content_area1" Runat="Server">
   <h2>Τελευταία νέα</h2>
          <div id="news" runat="server" class="News"></div>
</asp:Content>

