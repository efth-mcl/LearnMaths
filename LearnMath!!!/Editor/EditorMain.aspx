<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditorMain.aspx.cs" Inherits="Editor_EditorMain" %>

<asp:Content ID="EditMain" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/ProfMainScript.js"></script>
     <script>
        $(document).ready(function () {
            $(".helpDiv").click(function () {
                x0p({
                    title: 'Βοήθεια',
                    text: "Μπορείτε να δείτε όλους τους νέους χρήστες στην αρχική σελίδα.",
                });
            });
                ///NEWS/////
                $(".rmM").click(function () {
                    var parent = $(this).parent();
                    console.log(parent.attr("id"))
                    parent.remove();
                    $.ajax({
                        type: "POST",
                        url: "EditorMain.aspx/RmMessage",
                        data: '{MessID: "' + parent.attr("id") + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if ($(".News").html() == "<table><tbody></tbody></table>") {
                                $(".News").html("Δεν υπάρχει κάποια καινούργια είδηση.");

                            }
                        }
                    });
                });
                //NEWS////
            });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="content_area1" Runat="Server">
     <h2>Τελευταία νέα</h2>
          <div id="news" runat="server" class="News"></div>
</asp:Content>

