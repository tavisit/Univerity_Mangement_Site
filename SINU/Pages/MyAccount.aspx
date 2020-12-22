<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="SINU.Pages.MyAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Account</title>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
            height: 100px;
        }

        .auto-style3 {
            position: fixed;
            width: 100%;
            height: -20px;
            left: 0px;
            top: 0px;
        }

        .style2 {
            text-align: right;
        }

        .auto-style4 {
            height: 71px;
        }

        .auto-style5 {
            text-align: right;
            height: 26px;
            width: 600px;
        }

        .auto-style8 {
            text-align: right;
            width: 600px;
        }

        .auto-style9 {
            width: 100%;
        }

        .auto-style10 {
            height: 26px;
            width: 100%;
        }

        .auto-style11 {
            height: 100%;
            margin-left: 5%;
            margin-right: 5%;
        }

        .auto-style12 {
            text-align: right;
            height: 26px;
        }

        .auto-style13 {
            height: 26px;
        }
        .auto-style14 {
            text-align: right;
        }
        .auto-style15 {
            width: 90%;
        }
        table
        {
            margin: 0 auto;
        }
        .auto-style16 {
            width: 600px;
        }
        .auto-style17 {
            height: 26px;
            width: 600px;
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
        <div style="margin-left: auto; margin-right: auto; width: 80%; background-color: purple; margin-top: 120px;">
            <br />
            <br />
            <div class="auto-style11" runat="server" id="SessionDiv">
                <asp:Literal ID="MyProfileLiteral" runat="server"></asp:Literal>
                <br />
                <br />
                <div id= "updatingstudentgrade" runat="server" style="width: 90%; margin-left: 5%; margin-right: 5%; background-color: white">
                 <table style="margin-left: 5%; margin-right: 5%;" class="auto-style15">
                      <tr>
                        <td class="auto-style14"></td>
                        <td class="auto-style9">
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                          </td>
                        <td class="auto-style9"></td>
                    </tr>
                    <tr>
                        <td class="auto-style10">Student Id:</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox15" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td class="auto-style10">Subject:</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox16" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td class="auto-style10">New Grade:</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox17" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                    
                <asp:Button ID="UpdateGradesBtn" runat="server" BackColor="White" BorderStyle="Groove" OnClick="UpdateGradesBtn_Click" Text="Update the grades" Height="60px" Width="90%" Style="margin-left: 5%; margin-right: 5%;" />
                <br />
                    </div>
                <br />
                <asp:Literal ID="ViewInformationLiteral" runat="server"></asp:Literal>
                <br />
                <br />
                <div id= "pay_the_dorm_div" runat="server" style="width: 90%; margin-left: 5%; margin-right: 5%; background-color: white">
                 <table style="margin-left: 5%; margin-right: 5%;" class="auto-style15">
                     <tr>
                        <td class="auto-style9">
                          </td>
                    </tr>
                     <tr>
                        <td class="auto-style9">
                            <h2 style =" text-align:center;">Pay the dorm</h2>
                          </td>
                    </tr>
                     <tr>
                        <td class="auto-style9">
                          </td>
                    </tr>
                      <tr>
                        <td class="auto-style9">
                            <h4 style =" text-align:center;"><asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h4>
                          </td>
                    </tr>
                     <tr>
                        <td class="auto-style9">
                          </td>
                    </tr>
                     <tr>
                        <td class="auto-style9">
                             <asp:Button ID="payTheDormBtn" runat="server" BackColor="White" BorderStyle="Groove" OnClick="payTheDormBtn_Click" Text="Pay the dorm" Height="27px" Width="60%" Style="margin-left: 20%; margin-right: 20%;font-size:20px;" />
                
                          </td>
                    </tr>
                </table>
                    
               <br />
                    </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Button ID="Log_Out_Btn" runat="server" OnClick="Log_Out_Btn_Click" Text="Log Out" BackColor="White" BorderStyle="Groove" Height="60px" Width="90%" Style="margin-left: 5%; margin-right: 5%; font-size:30px;" />
                <br />
                <br />
                <br />
                <div style="width: 90%; margin-left: 5%; margin-right: 5%; background-color: white">
                    <table style="width: 100%; margin-left: 5%; margin-right: 5%;">
                                        <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td><h1>Information settings</h1></td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style8">Username:</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox9" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style8">Password:</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox10" TextMode="Password" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style8">Surname</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox11" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style8">Lastname:</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox12" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style5">Photo:</td>
                        <td class="auto-style10">
                            <asp:TextBox ID="TextBox13" runat="server" Width="500px" TextMode="Url" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style8">Birth:</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox14" runat="server" Width="500px" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                            (Format dd-mm-yyyy)</td>
                    </tr>
                    <tr>
                        <td class="auto-style8">&nbsp;</td>
                        <td class="auto-style9">
                            <asp:Button ID="UpdateProfileBtn" runat="server" OnClick="UpdateProfileBtn_Click" Text="Update" BackColor="White" BorderStyle="Groove" Width="510px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td></td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                </table>
                </div>
                
                <br /><br />
                <div class="auto-style4" style ="width: 90%; margin-left: 5%; margin-right: 5%; background-color: white;">
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="20pt" Height="50px" Text="In order to delete the current account, press the following button:"></asp:Label>
                    <asp:Button ID="DeleteAccountBtn" runat="server" Height="30px" OnClick="DeleteAccountBtn_Click" Text="Delete" Width="100px" BackColor="Red" BorderStyle="Groove" />
                    
                <br />
                </div>
                
                <br /><br />
            </div>
            <asp:Panel ID="Login_Table" runat="server">
                <table style="width: 100%;">
                    <caption class="style1">
                        <strong>Login Form</strong>
                    </caption>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td class="style2">Email:</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">Password:</td>
                        <td>
                            <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2"></td>
                        <td>
                            <asp:Button ID="LogInBtn" runat="server" Text="Log In" OnClick="LogInBtn_Click" BackColor="White" BorderStyle="Groove" Width="100px" />
                            <asp:Button ID="RegisterFormBtn" runat="server" Text="Register" OnClick="RegisternBtn_Click" BackColor="White" BorderStyle="Groove" Width="100px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td></td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Register_Panel" runat="server">
                <table style="width: 100%;">
                    <caption class="style1">
                        <strong>Register Form</strong>
                    </caption>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style1">Characters Permited: 0-9, a-z, A-Z, *, $<br />
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">Email:</td>
                        <td class="auto-style16">
                            <asp:TextBox ID="TextBox3" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style12">Surname:</td>
                        <td class="auto-style17">
                            <asp:TextBox ID="TextBox4" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td class="auto-style13"></td>
                    </tr>
                    <tr>
                        <td class="style2">Lastname:</td>
                        <td class="auto-style16">
                            <asp:TextBox ID="TextBox5" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style12">Username:</td>
                        <td class="auto-style17">
                            <asp:TextBox ID="TextBox6" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td class="auto-style13"></td>
                    </tr>
                    <tr>
                        <td class="style2">Password:</td>
                        <td class="auto-style16">
                            <asp:TextBox ID="TextBox7" TextMode="Password" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">Confirm password:</td>
                        <td class="auto-style16">
                            <asp:TextBox ID="TextBox8" TextMode="Password" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="auto-style16">
                            <asp:TextBox ID="TextBox18" type="number" runat="server" BackColor="White" BorderStyle="Groove"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                        </td>
                        <td class="auto-style16">
                            <asp:CheckBox id="checkbox1" runat="server" Text="Dorm?" TextAlign="Right" OnCheckedChanged="Check_Clicked"/>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2"></td>
                        <td class="auto-style16">
                            <asp:Button ID="RegisterRegisterPanelBtn" runat="server" BackColor="White" BorderStyle="Groove" OnClick="RegisterRegisterPanelBtn_Click" Text="Register" Width="100px" />
                            <asp:Button ID="CancelRegisterPanelBtn" runat="server" BackColor="White" BorderStyle="Groove" OnClick="CancelRegisterPanelBtn_Click" Text="Cancel" Width="100px" />
                            <asp:Button ID="RegisterTeacherBtn" runat="server" BackColor="White" BorderStyle="Groove" OnClick="RegisterTeacherBtn_Click" Text="Register" Width="100px" />
                            <br />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style16"></td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <div style="height: 198px">
        </div>
    </form>
</body>
</html>
