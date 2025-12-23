<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hidrojateamento.aspx.cs" Inherits="SILC.Web.forms.brooks.hidrojateamento" %>

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
            background-image: url('sedehidrojateamento.jpg'); width: 1000px; height:336px;margin:auto;
        }
        .divmenuservicos
        {
            background-image: url('menuservicos.jpg'); width: 1000px; height:175px;margin:auto;
        }
        .divinstalacoes
        {
            background-image: url('hidrojateamento2.jpg'); width: 1000px; height:551px;margin:auto;
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
    <script type="text/javascript" src="imagemvermelha.js"> </script>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body class="body">
    <form id="form1" runat="server">
        <div class="cabecbrooks">
            <table style="text-align:center;" border:"0"; cellpadding="0" cellspacing="0">
                <tr>
                    <td><asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/Images/cabec1brooks.jpg" /></td>
                    <td style="position: relative; width: 280px; background-image: url('../../Images/backareacliente.jpg')" >
                        <asp:HyperLink ID="lnkAreaCliente" runat="server" NavigateUrl="~/forms/brooks/login.aspx" Font-Bold="true" ForeColor="Brown">Area do Cliente</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divcentral">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td style="position:relative; left:0px; top:-3px">
                        <a href="default.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="~/forms/brooks/home.jpg" BorderWidth="0" /></a>
                    </td>
                    <td style="position:relative; left:81px; top:-3px">
                        <a href="brooksambiental.aspx"><img id="Image6" src="brooksambiental.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:150px; top:-3px">
                        <a href="equipamentos.aspx"><img id="Img1" src="equipamentos.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:240px; top:-3px">
                        <a href="legislacao.aspx"><img id="Img2" src="legislacao.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:312px; top:-3px">
                        <a href="perguntasfrequentes.aspx"><img id="Img3" src="perguntasfrequentes.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:374px; top:-3px">
                        <a href="contato.aspx"><img id="Img4" src="contato3.jpg" style="border:0px;" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divmenuservicos">
            <table>
                <tr>
                    <td style="position:relative; top: 35px; left: 62px;">
                        <a href="assessoria.aspx"><img id="Img5" src="assessoriafundo.jpg" onmouseover="assessoria();" style="border:0px;"/></a>
                    </td>
                    <td style="position:relative; top: 26px; left:62px;">
                        <a href="coletaseletiva.aspx"><img id="Img6" src="coletaseletiva.jpg" onmouseover="coletaseletiva();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 34px; left:70px;">
                        <a href="coletaresiduos.aspx"><img id="Img7" src="coletaresiduosfundo.jpg" onmouseover="coletaresiduos();" style="border:0px;" /></a>
                    </td>                
                    <td style="position:relative; top: 36px; left:74px;">
                        <a href="RCD.aspx"><img id="Img8" src="RCDfundo.jpg" onmouseover="RCD();" style="border:0px;" /></a>
                    </td>                
                    <td style="position:relative; top: 36px; left:76px;">
                        <a href="RSS.aspx"><img id="Img9" src="RSSfundo.jpg" onmouseover="RSS();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 32px; left:68px;">
                        <a href="hidrojateamento.aspx"><img id="Img10" src="hidrojateamentofundo.jpg" onmouseover="hidrojateamento();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 32px; left:64px;">
                        <a href="gestaoglobal.aspx"><img id="Img11" src="gestaoglobalfundo.jpg" onmouseover="gestaoglobal();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 30px; left:60px;">
                        <a href="USTE.aspx"><img id="Img12" src="USTEfundo.jpg" onmouseover="USTE();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 28px; left:62px;">
                        <a href="DTR.aspx"><img id="Img13" src="dtrfundo.jpg" onmouseover="DTR();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 28px; left:62px;">
                        <a href="destinacaofinal.aspx"><img id="Img14" src="destinacaofinalfundo.jpg" onmouseover="destinacaofinal();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 34px; left:67px;">
                        <a href="logistica.aspx"><img id="Img15" src="logisticafundo.jpg" onmouseover="logistica();" style="border:0px;" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <div id="instalacoes" class="divinstalacoes">
        </div>
    </form>
    <div class="rodapebrooks">
        <asp:Image ID="Image2" runat="server" ImageAlign="Middle" ImageUrl="~/Images/rodapebrooks.jpg" />
    </div>
</body>
</html>
