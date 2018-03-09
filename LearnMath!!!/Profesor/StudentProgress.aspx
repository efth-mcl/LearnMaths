<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StudentProgress.aspx.cs" Inherits="Profesor_StudentProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content_area1" Runat="Server">
    <link href="../Styles/StudsProgStyle.css" rel="stylesheet" />
     <script src="https://cdnjs.cloudflare.com/ajax/libs/mathjs/3.3.0/math.js"></script>
     <script type="text/javascript" src="../Scripts/GRAPHS.js"></script>
    <script src="../Scripts/StudsProgScript.js"></script>
    <%--Help Button --%>
    <script>
        $(document).ready(function () {
            $(".helpDiv").click(function () {
                x0p({
                    title: 'Βοήθεια',
                    text: "Σε αυτή τη σελίδα υπάρχουν όλες οι επιδόσεις των μαθητών στα test.",
                });
            });
            $(".goToStuds").css("height", $(".goToStuds").width() + "px");
            $(window).resize(function () { $(".goToStuds").css("height", $(".goToStuds").width() + "px"); });
        });
    </script>
    <%--Help Button --%>
    <h1>Λίστα μαθητών</h1>
    <div class="goToStuds">
    </div>
    <div class="StudsDiv"  id="StudsList" runat="server">
    </div>
         <canvas id="Canvas" style="margin-top:110px;">
         </canvas>
</asp:Content>

