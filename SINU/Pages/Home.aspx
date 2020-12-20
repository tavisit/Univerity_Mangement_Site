<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SINU.Pages.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
            height: 100px;
        }
        .auto-style2 {
            height: 100px;
        }
        .auto-style3 {
            position: fixed;
            width: 100%;
            height: -20px;
            left: 0px;
            top: 0px;
        }
        tr
        {
            text-align:center;
            background-color:ghostwhite;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style3" style="background-color: #800080;">
            <asp:ImageButton ID="HomeImageButton" runat="server" Height="50px" ImageUrl="https://www.utcluj.ro/static/images/logo_site_centenar.png" OnClick="HomeImageButton_click" Width="232px" />
            <div style="width: 100%; height: 50px">
                <asp:Button ID="Button1" runat="server" Height="50px" OnClick="HomeButton_Click" Text="Home" Width="33%" BackColor="White" BorderStyle="Groove" Font-Bold="True" />
                <asp:Button ID="Button4" runat="server" Height="50px" OnClick="AboutUs_Click" Text="About Us" Width="33%" BackColor="White" BorderStyle="Groove" Font-Bold="True" />
                <asp:Button ID="Button2" runat="server" Height="50px" OnClick="MyAccount_click" Text="MyAccount" Width="33%" BackColor="White" BorderStyle="Groove" Font-Bold="True" />
            </div>
        </div>
        
        <div style="margin-left:auto;margin-right:auto;background-color: #800080; width:60%; height:100%;" class="auto-style1">
            <div class="auto-style2" style="background-color: #FFFFFF">
            </div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Literal ID="MyProfileLiteral" runat="server"></asp:Literal>
                <br />
            <br />
            <br />
            <asp:Literal ID="ListNews" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
