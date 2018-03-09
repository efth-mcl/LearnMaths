<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditUsers.aspx.cs" Inherits="Editor_EditUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content_area1" Runat="Server">
    <style>
        .users table {
            border-collapse: collapse;
            width: 100%;
        }
      .users th, td {
           text-align: left;
           padding: 1%;
       }

      .users tr:nth-child(even){background-color: #f2f2f2}

      .users th {
           background-color: #344a68;
           color: white;
       }
      .UpDBt
      {
          border:0;
          padding:10px;
          cursor:pointer;
          float:right;

      }      
    </style>
    <script>
        $(document).ready(function () {
            $(".helpDiv").click(function () {
                x0p({
                    title: 'Βοήθεια',
                    text: "Ενημέρωση κατάστασης χρηστών"
                });
            });
        });
    </script>
   
            <div id="Users" runat="server" class="users">
            </div>
            <asp:Button ID="UpDStatus" runat="server" Text="Ενημέρωση" class="UpDBt" OnClick="UpDStatus_Click" />
          
</asp:Content>

