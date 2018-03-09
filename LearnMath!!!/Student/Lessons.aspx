<%@ Page Title=""  Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Lessons.aspx.cs" Inherits="Lessons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content_area1" Runat="Server">
        <%--Help Button --%>
        <script>
            $(document).ready(function () {
                $(".helpDiv").click(function () {
                    var Tip = ""
                    var TipH="";
                    var TipW = "";
                    if (fromtestb) {
                        TipW='550px';
                        TipH = '400px';
                        Tip = "Σε αυτή τη σελίδα είναι διαθέσιμα όλα τα test. Για να συνεχίσετε στο τεστ του "+
                              "επόμενου κεφαλαίου πρέπει να έχετε επίδοση τουλάχιστον 50% στις ερωτήσεις του τρέχοντος test." +
                              "και την πρώτη φορά σε κάθε test πρέπει να απαντήσετε σε πέντε ερωτήσεις. Αν απότυχετε,"+ 
                              "το test πλεον θα έχει δέκα ερωτήσεις.";
                    }
                    else
                    {
                        TipH='450px'
                        TipW='600px'
                        Tip="Σε αυτή τη σελίδα μπορείτε να βρείτε τη θεωρία για όλα τα κεφάλαια έτσι ώστε να είστε έτοιμοι για τα test."+ 
                            "Μετά από κάθε τμήμα θεωρίας υπάρχει και το αντίστοχο test για να δοκιμάσετε τις γνώσεις σας."+
                            "Για να συνεχίσετε στο επόμενο κεφάλαιο πρέπει να έχετε επίδοση τουλάχιστον 50% στις ερωτήσεις του τρέχοντος test."+
                            "και την πρώτη φορά σε κάθε test πρέπει να απαντήσετε σε πέντε ερωτήσεις. Αν απότυχετε, το test πλεον θα έχει δέκα ερωτήσεις.";
                    }
                    x0p({
                        title: 'Βοήθεια',
                        text: Tip,
                        maxWidth: TipW,
                        maxHeight: TipH

                    });
                });
            });
        </script>
        <%--Help Button --%>
  
  
    <script src="../Scripts/LessonsScr.js"></script>

    <link rel="stylesheet" type="text/css" href="../Styles/LessonsStyle.css"/>
  
        <h1 id="H1class1" runat="server"></h1>
        <ul class="Chapters">
        <li id="L1" class="OK"></li>
    </ul>
    <div class="Lesson">
    </div>
    <div class="TestB hvr-sweep-to-right">Test κεφαλαίου</div>
    <link rel="stylesheet" type="text/css" href="../Styles/TestStyle.css"/>
    <div id="TestForm"  class="TestView" >
    </div> 
</asp:Content>

