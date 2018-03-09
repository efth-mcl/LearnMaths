<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Learn Math!</title>
    <link rel="shortcut icon" href="Images/website%20icon.ico"/>
    <link rel="stylesheet" type="text/css" href="~/Styles/MainStyle.css" />
    <link href="frameworks/PopUp/x0popup.css" rel="stylesheet" />
    <script src="frameworks/JQuery/query-1.12.2.min.js"></script>
    <script src="frameworks/PopUp/x0popup.min.js"></script>
    <script src="Script1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="MainScriptManager" runat="server" />
        <div id="banner">   
        </div>
        <div class="form">
            <nav id="menu">
                <a href="#this">Create Account</a>
                <a href="#this" class="Celect">Sign In</a>
            </nav>
            <asp:UpdatePanel ID="pnlHelloWorld" runat="server">
                <ContentTemplate>
                    <div class="login-form">
                        <input class="input" id="LogUser" runat="server" type="text" placeholder="E-Mail" value=""/>
                        <input class="input" id="LogPas" runat="server" type="password"  placeholder="Password" value=""/>
                        <asp:Button class="Button" ID="LogIn_Button" runat="server" Text="Login" OnClick="LogIn"/>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="register-form">
                      <dir class="MessageEmptyBox"><p></p></dir>
                      <input class="input" id="RegSurName" runat="server" type="text" placeholder="Surname"  value=""/>
                      <input class="input" id="RegName" runat="server" type="text" placeholder="Name"  value=""/>
                      <dir class="arrowEmail"></dir>
                      <dir class="MessageEmailBox"><p></p></dir>
                      <input class="input" id="RegEmail" runat="server" type="text" placeholder="email address"  value=""/>
                      <dir class="arrowPass"></dir>
                      <dir class="MessagePassBox"><p></p></dir>
                      <input class="input" id="RegPass" runat="server" type="password" placeholder="Password"  value=""/>
                      <dir class="arrowPass"></dir>
                      <input class="input" id="RegPassRe" runat="server" type="password" placeholder="Repeat Password"  value=""/>
                      <asp:Button class="Button" ID="Register_Button" runat="server" Text="Register" OnClick="Register"/>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="footer">
             <p><br /> Email καθηγητών:
               <span style="margin-left:25px;">Χάρης Βάλσαμος: Haris@papei.com</span><span style="margin-left:35px;">Ευθύμης Μιχάλης: Efthymis@papei.com</span>
                
             </p>
        </div>
    </form>
</body>
</html>
