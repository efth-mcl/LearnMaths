<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StudStats.aspx.cs" Inherits="Student_StudStats" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content_area1" Runat="Server">
     <script src="https://cdnjs.cloudflare.com/ajax/libs/mathjs/3.3.0/math.min.js"></script>
     <script type="text/javascript" src="../Scripts/GRAPHS.js"></script>
    <script src="../Scripts/StudStatsScript.js"></script>
    <link href="../Styles/StudStatsStyle.css" rel="stylesheet" />
        <%--Help Button --%>
        <script>
            $(document).ready(function () {
                $(".helpDiv").click(function () {
                    x0p({
                        title: 'Βοήθεια',
                        text: "Μπορείτε να δείτε τις καλύτερες επιδόσεις για όλα τα test οργανωμένες σε ένα ιστόγραμμα."+
                              "Μπορείτε να δείτε όλες τις επιδόσεις σας στα τεστ ανά ημερομηνία πηγαίνωντας τον κέρσορα πάνω στα πράσινα σημεία.",
                        maxWidth: '500px',
                        maxHeight: '300px',
                    });
                });
            });
        </script>
        <%--Help Button --%>
     <script type="text/javascript">        
     </script>
     <ul class="StatsList">
         <li id="MaxHist" class="Stats OK"><div class="hvr-sweep-to-right">Καλύτερες Επιδόσεις ανά τεστ</div></li>
         <li id="L-1" class="Stats OK"><div class="hvr-sweep-to-right">Πρόοδος Τελικού Τεστ</div></li>
         <li><div  id="LessonsTime"> Πρόοδος ανά Κεφαλαίο </div>
             <ul class="Chapters">
                <li id="L1" class="OK Stats"></li>
             </ul>
        </li>
     </ul>
        <canvas id="Canvas" style="margin-right:100px;">
            </canvas>
         
</asp:Content>

