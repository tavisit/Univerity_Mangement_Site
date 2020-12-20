<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="SINU.Pages.AboutUs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About Us</title>
            <style type="text/css">
        .auto-style2 {
            height: 93px;
        }
        .auto-style3 {
            position: fixed;
            width: 100%;
            height: -20px;
            left: 0px;
            top: 0px;
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
        <div>
        </div>
    </form>
</body>
</html>
