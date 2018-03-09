<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Student_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/TestScr.js"></script>
    <style type="text/css">
      body{
            overflow-y:hidden;
            overflow-x:hidden;
          }
</style>
    
    

</head>
<body>
    <form id="form1" runat="server">
        <div class="CheckedNumb">
        <p></p>
      
        </div>
        <div class="TestForm">
            <div id="Quetion"></div>
            
            <div id="A" class='block'></div>
            <div id="B" class='block'></div>
            <div id="C" class='block'></div>
            <div id="D" class='block'></div>
            
            <input  type="button" id="NQ" runat="server" value="Επόμενη ερώτηση/Επιβεβαίωση" style="font-size:24px;"/>
        </div>
        <div class="Tresult">
            <div class="Score"> </div>
             <div class="Top Qu">Εκφώνηση ερώτησης</div>
            <div class="Top Stud">Η απάντησή μου</div>
            <div class="Top Ans">Σωστή Aπάντηση</div>
            <div id="D0" class="Re r0"></div>

            <div  class="Re r0 Stud"></div>
            <div  class="Re r0 Ans"></div>

            <asp:HiddenField ID="LessonID" Value="" runat="server" />
            <asp:HiddenField ID="Score" Value="" runat="server" />
         <div class="hvr-sweep-to-right saveTest" > <asp:Button ID="SaveTest" runat="server"  Text="Πίσω στην αρχική σελίδα" OnClick="SaveTest_Click" /> </div>
        </div>
    <div id='Clock' runat="server">Time</div>
    </form>

</body>
</html>
