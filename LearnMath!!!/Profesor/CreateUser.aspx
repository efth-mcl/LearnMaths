<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Profesor_CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content_area1" Runat="Server">
    <script src="../Scripts/CreateUserScript.js"></script>
    <link href="../Styles/CreateUserStyle.css" rel="stylesheet" />
    <asp:ScriptManager ID="CreateUserScriptManager" runat="server" />
    <%--Help Button --%>
    <script>
        $(document).ready(function () {
            $(".helpDiv").click(function () {
                x0p({
                    title: 'Βοήθεια',
                    text: "Σε αυτή τη σελίδα μπορείτε να δημιουργείτε τους δικους σας χρήστες.",
                });
            });
        });
    </script>
    <%--Help Button --%>
    <%-- /////////////////////////////////// --%>
 <asp:UpdatePanel ID="CreateUser_UpdatePanel" runat="server">
    <ContentTemplate>
        <div class="register-form">
            <table>
             <tr>
                 <td><input class="input" id="RegSurName" runat="server" type="text" placeholder="Surname"  value=""/></td>
                 <td><asp:Label ID="ErrorSurName" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label></td>
             </tr>
             <tr>
                 <td><input class="input" id="RegName" runat="server" type="text" placeholder="Name"  value=""/></td>
                 <td><asp:Label ID="ErrorName" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label></td>
             </tr
             <tr>
             </tr>
                 <td>
                     <input class="input" id="RegEmail" runat="server" type="text" placeholder="email address"  value="" visible="True" />
                </td>
                <td><asp:Label ID="ErrorEmail" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label></td>
             </tr>
                 <td>
                     <input class="input" id="RegPass" runat="server" type="password" placeholder="Password"  value="" visible="True" />
                </td>
                <td><asp:Label ID="ErrorPass" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label></td>
             </tr>
                <td>
                    <input class="input" id="RegPassRe" runat="server" type="password" placeholder="Repeat Password"  value="" visible="True" />
                </td>
                <td><asp:Label ID="ErrorRePass" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label></td>
                <tr>
                    <td>
                      <div class="Radio">
                       <input type="radio" name="radio" id="RegEditor" value="Editor"/>
                          <label for="RegEditor">Editor</label>
                       <input type="radio" name="radio" id="RegStud" value="Stud"/>
                         <label for="RegStud">Student</label>
                      </div>
                    </td>
                    <td><asp:Label ID="ErrorRadio" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Button class="Button" ID="Register_Button" runat="server" Text="Register" OnClick="Register_Button_Click"/></td>
                    <td><asp:Label ID="OK_orNot_Reg" runat="server" Text="Επιτυχής έγγραφη νέου χρήστη." Visible="False" ForeColor="Green"></asp:Label></td>
                </tr>
            </table>
            
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

