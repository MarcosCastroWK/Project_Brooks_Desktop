<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sempermissao.aspx.cs" Inherits="sempermissao" %>

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
        <div style="width: 100%;">
            <table class="login" style="width:460px; height:200px" cellpadding="4">
                <tr>
                    <td colspan="2"><img id="imglogo" name="imglogo" runat="server" src="../Images/logobrooks.png" tabindex="0" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel">Sem permissão de acesso ou a sessão expirou, nesse caso logar novamente.</asp:Label>
                    </td>
                    <td>
			            <a href="menu.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="../Images/voltar.png" BorderWidth="0" /></a>
			        </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
