<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SILC.Web.forms.fastcompost.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <title></title>
     <style>
        .body 
        {       
            background-image:url('fundo.jpg');
        }
        .cabec
        {
            background: transparent center center repeat-y; width: 74%; margin-left:10%;
            color:black;            
            font-size:11pt;
        }
        .divcentral
        {
            background-image: url('fundo.jpg'); width: 74%; height:310px;margin-left:10%; 
        }
        .login 
        {
            width: 42%;
            background: white;
            position:relative;
            left: 30%;
            top: 12%;
            color:black;
            font-size:8pt;
            font-family:Arial;
            color: black;
            border:ridge 8px ActiveBorder;
        }
        .rodape
        {
            background: transparent center center repeat-y; width: 74%; margin-left:10%;
            color:black;            
            font-size:11pt;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body class="body">
    <form id="form1" runat="server">
        <div class="cabec">
            <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/forms/fastcompost/logocabec.jpg" />
		</div>
        <div class="divcentral">
			<a href="..\..\default.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="~/forms/fastcompost/home.jpg" BorderWidth="0" /></a>
			<table class="login">
                <tr>			
					<td>&nbsp;</td>
				</tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName" CssClass="LetrasLabel">Login</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="UserName" onfocus="LimpaErro()" runat="server" Width="185px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="O nome do usuario e obrigatorio" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" AssociatedControlID="Password" CssClass="LetrasLabel">Senha</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" onfocus="LimpaErro()" Width="185px" TabIndex="0" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="A senha e obrigatoria" />
                    </td>
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
        <div class="rodape">
            <asp:Image ID="Image2" runat="server" ImageAlign="Middle" ImageUrl="~/forms/fastcompost/logorodape.jpg" />
        </div>
    </form>
</body>
</html>
