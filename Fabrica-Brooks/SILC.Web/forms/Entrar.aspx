<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entrar.aspx.cs" Inherits="SILC.Web.forms.Account_Entrar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <title></title>
     <style>
         .body 
         {
            background-color:white;
            font:10pt normal arial;
            margin:0;
            padding:0;
        }
        .login 
        {
            position:absolute;
            top:45%;
            left:45%;
            margin-top:-100px;
            margin-left:-180px;
            color:black;
            background-color: ButtonFace;
            font-size:10pt;
            border:ridge 8px ActiveBorder;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body class="body">
 
    <form id="form1" runat="server">
        <div width="100%">
            <table class="login" width="460px" height="200" cellpadding="4">
                <tr>
                    <td colspan="3"><img id="imglogo" name="imglogo" runat="server" src="../Images/logobrooks.png" tabindex="0"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName" CssClass="LetrasLabel">Nome&nbsp;do&nbsp;usuário</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="UserName" onfocus="LimpaErro()" runat="server" Width="164px"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" onfocus="LimpaErro()" Width="164px" TabIndex="0" />
                    <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="A senha é obrigatória" />
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button runat="server" CommandName="Login" Font-Size="10pt" Text="Entrar" ID="Login" OnClick="Login_Click" TabIndex="0" />
                        <asp:Button ID="btnDemonstracao" runat="server" OnClick="btnDemonstracao_Click" Text="Demonstração" Width="100px" Enabled="False" Visible="False" TabIndex="0" />
                    </td>
                    <td>Versão: 1.00</td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                    </td>
                    <td class="LetrasLabel">17/04/2017</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
