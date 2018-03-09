<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StudentMain.aspx.cs" Inherits="Default" %>

<%-- Add content controls here --%>
<asp:Content ID="JQ" ContentPlaceHolderID="head" runat="server">
     <script>
            $(document).ready(function () {
                $(".helpDiv").click(function () {
                    x0p({
                        title: 'Βοήθεια',
                        text: "Στα τελευταία νέα μπορείτε να ενημερωθείτε για θέματα όπως η διαθεσιμότητα κεφαλαίων ή την τυχόν αδυναμία σε κάποιο κεφάλαιο.",
                    });
                });
            });
        </script>
        <%--Help Button --%>
    <script>
        $(document).ready(function () {
            ///NEWS/////
            $(".rmM").click(function () {
                var parent = $(this).parent();
                parent.remove();
                $.ajax({
                    type: "POST",
                    url: "StudentMain.aspx/RmMessage",
                    data: '{MessID: "' + parent.attr("id") + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if ($(".News").html() == "<table><tbody></tbody></table>") {
                            $(".News").html("Δεν υπάρχει κάποια καινούργια είδηση.");
                        }
                    }
                });
                //NEWS////
                $("input[type=radio]").click(function () {
                    var val = $(this).val();
                    $.ajax({
                        type: "POST",
                        url: "StudentMain.aspx/GoToStadyThisLesson",
                        data: '{LessonID: "' + val + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            window.location.href = "Lessons.aspx";
                        },
                        failure: function (response) {
                            console.log("Error calling Ajax");
                        }
                    });
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="StudentMain" ContentPlaceHolderID="content_area1" Runat="Server">
    <%--////////////////////////////// STYLE BIGIN //////////////////////// --%>
     <style>
        .HelpDiv h1
        {
            font-family: "Roboto", sans-serif;
            padding:20px;
            background-color:#473FAF;
            text-align:center;
            margin:0;
            color:white;
        }
        .HelpDiv
        {
            background-color:#808080;
        }
        .HelpDiv input[type=radio]
        {
            display:none;
        }
        .HelpDiv label:hover
        {
            background-color:azure;
        }
        .HelpDiv input[type=radio]:checked + label
        {
            background-color:#608dce;
            color: #f2f2f2;
        }
        .HelpDiv label
        {
            font-family: "Roboto", sans-serif;
            position:relative;
            display:block;
            padding:3%;
            font-size: 15px;
            color: #1e324e;
            text-decoration: none;
            cursor:pointer;
        }
    </style>
    <%--////////////////////////////// END STYLE //////////////////////// --%>
    <%--////////////////////////////// SCRIPT BIGIN //////////////////////// --%>
    <%--////////////////////////////// SCRIPT END //////////////////////// --%>
     <h2>Τελευταία νέα</h2>
          <div id="news" runat="server" class="News"></div>
    <br />
    <br />
    <div class="HelpDiv" id="HelpDiv" runat="server">
    </div>
    <div class="check"></div>
</asp:Content>