<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loginaplicativo.aspx.cs" Inherits="brooks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <title></title>
     <style>
        .body 
        {              
            background-image:url('fundobrooks.jpg');
        }
        .divcentral
        {
            background-image: url('sedebrooks.jpg'); width: 1000px; height:310px;margin:auto; 
        }
        .login 
        {
            width: 360px;
            background: white;
            position:relative;
            left: 30%;
            top: 26%;
            color:black;
            font-size:8pt;
            font-family:Arial;
            color: black;
            border:ridge 8px ActiveBorder;
        }
        .cabecbrooks
        {
            background: transparent center center repeat-y; width: 1000px; auto;margin: auto;
            color:black;            
            font-size:11pt;
        }
        .rodapebrooks
        {
            background: transparent center center repeat-y; width: 1000px;margin: auto;
            color:black;            
            font-size:11pt;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body class="body">
    <form id="form1" runat="server">
        <div class="cabecbrooks">
            <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/Images/cabecbrooks.jpg" />
        </div>
        <div class="divcentral">
            <table id="table1" class="login">
                <tr><td><br /></td></tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName" CssClass="LetrasLabel">Login</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="UserName" runat="server" Width="185px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="O nome do usuário é obrigatório" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" AssociatedControlID="Password" CssClass="LetrasLabel">Senha</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" Width="185px" TabIndex="0" />
                    <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="A senha é obrigatória" />
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                        <asp:Button runat="server" CommandName="Login" Font-Size="10pt" Text="Entrar" ID="Login" OnClick="Login_Click" TabIndex="0" />
                        <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <div class="rodapebrooks">
        <asp:Image ID="Image2" runat="server" ImageAlign="Middle" ImageUrl="~/Images/rodapebrooks.jpg" />
    </div>
</body>
</html>
