<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FinalTest.aspx.cs" Inherits="Student_FinalTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content_area1" Runat="Server">
        <%--Help Button --%>
        <script>
            $(document).ready(function () {
                $(".helpDiv").click(function () {
                    x0p({
                        title: 'Βοήθεια',
                        text: "Το τελικό test αποτελείτε από 20 ερωτήσεις. Το πλήθος των ερωτήσεων ανα κρφάλαιο προσαρμόζεται ανάλογα με την επίδοση σε αυτό το κεφάλαιο.",
                    });
                });
            });
        </script>
        <%--Help Button --%>
    <style>
        .buttonS {
          border:none;
          cursor: pointer;
          height:70px;
          width:200px;
          border-radius: 0 25px;
          font: normal 50px/normal Georgia, serif;
          color: rgba(255,255,255,0.9);
          background: rgba(34,115,155,1);
          box-shadow: 0 0 1px 1px rgba(0,0,0,0.5) ;
          text-shadow: 0 0 0 rgba(15,73,168,0.66) ;
          transition: all 500ms cubic-bezier(0, -0.02, 0, 1.01);
          transform:translate(55%,25%);
        }
        .buttonS:hover
        {
            background: rgba(13,164,224,1);
            box-shadow: 0 0 9px 4px rgba(0,0,0,0.5) ;
            border-radius: 25px 0;
        }
        .start
        {
           background-color:#151e4a;
           width:40%;
           height:300px;
           margin-left:30%;
           border-radius:16px;
           padding:10px;
        }
        .Final_test
        {
            height: 100%;
            width:100%;
            position:fixed;
            z-index:1;
            top: 0;
            left: 0;
            background-color: #2dad6f;
            overflow-y:hidden;
            overflow-x:hidden;
            transition:0.4s;
            display:none;
        }
        .PAR
        {
            text-align:center;
            font-size:30px;
            color:white;
        }
    </style>
    <script>
        $(document).ready(function () {
            $(".buttonS").click(function () {
                $("#TestForm").load("Test.aspx");
                document.getElementById("TestForm").style.width = "100%";
                $.ajax({
                    type: "POST",
                    url: "FinalTest.aspx/LoadTest",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        Q = response.d;
                        Qnumb = Q.length;
                        console.log(Q);
                        console.log(Qnumb);
                        UpdateMath(Q, 0);
                        ANN[0] = Q[5];
                        St_An = new Array(Qnumb);
                        $(".CheckedNumb p").html("Έχετε απαντήσει " + 0 + " απο τις " + St_An.length + " ερωτήσεις");
                    },
                    failure: function (response) {
                        $("#a1").html("Error calling Ajax");
                    }
                })

            });
        })
    </script>
   
  


    <%--////////////////////////////////////////////////////////////////////////////--%>
    <div class="start">
        <p class="PAR" id="StartOrNot" runat="server"></p>
        <input type="button" class="buttonS" id="StartFinaleTest" runat="server"  value="Start" />
    </div>
    <link rel="stylesheet" type="text/css" href="../Styles/TestStyle.css"/>
    <div id="TestForm"  class="TestView" >
    </div> 
</asp:Content>

