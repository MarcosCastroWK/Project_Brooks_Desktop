<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SILC.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="forms/aspx.css" type="text/css" rel="stylesheet" />
    <title></title>
     <style>
        .body 
        {              
            background-image:url('forms/brooks/fundobrooks.jpg');
        }
        .divcentral
        {
            background-image: url('forms/brooks/sede.jpg'); width: 1000px; height:334px;margin:auto;
        }
        .divmenuservicos
        {
            background-image: url('forms/brooks/menuservicos.jpg'); width: 1000px; height:185px;margin:auto;
        }
        .divinstalacoes
        {
            background-image: url('forms/brooks/instalacoes.jpg'); width: 1000px; height:1418px;margin:auto;
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
        .objectImage 
        {
            width: 70px;
            height: 80px;
            overflow: hidden;
        }
    </style>
    <script type="text/javascript" src="imagemvermelha.js"></script>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body class="body">
    <form id="form1" runat="server">
        <div class="cabecbrooks">
            <table style="text-align:center;" border:"0"; cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/Images/cabec1brooks.jpg" />
                    </td>
                    <td style="position: relative; width: 280px; background-image: url('Images/backareacliente.jpg')" >
                        <asp:HyperLink ID="lnkAreaCliente" runat="server" NavigateUrl="~/forms/brooks/login.aspx" Font-Bold="true" ForeColor="Brown">BROOKS</asp:HyperLink>
                        <br />
                        <asp:HyperLink ID="lnkFastCompost" runat="server" NavigateUrl="~/forms/fastcompost/login.aspx" Font-Bold="true" ForeColor="Brown">FastCompost</asp:HyperLink>
                    </td>                  
                </tr>
            </table>
        </div>
        <div class="divcentral">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/forms/brooks/home.jpg" />
                    </td>
                    <td style="position:relative; left:84px;">
                        <a href="forms/brooks/brooksambiental.aspx"><img id="Image6" src="forms/brooks/brooksambiental.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:155px;">
                        <a href="forms/brooks/equipamentos.aspx"><img id="Img1" src="forms/brooks/equipamentos.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:240px;">
                        <a href="forms/brooks/legislacao.aspx"><img id="Img2" src="forms/brooks/legislacao.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:314px;">
                        <a href="forms/brooks/perguntasfrequentes.aspx"> <img id="Img3" src="forms/brooks/perguntasfrequentes.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:384px;">
                        <a href="forms/brooks/contato.aspx"><img id="Img4" src="forms/brooks/contato.jpg" style="border:0px;" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divmenuservicos">        
            <table>
                <tr>
                    <td style="position:relative; top: 35px; left: 62px;">
                        <a href="forms/brooks/assessoria.aspx"><img id="Img5" src="forms/brooks/assessoriafundo.jpg" onmouseover="assessoria();" style="border:0px;"/></a>
                    </td>
                    <td style="position:relative; top: 26px; left:62px;">
                        <a href="forms/brooks/coletaseletiva.aspx"><img id="Img6" src="forms/brooks/coletaseletiva.jpg" onmouseover="coletaseletiva();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 34px; left:70px;">
                        <a href="forms/brooks/coletaresiduos.aspx"><img id="Img7" src="forms/brooks/coletaresiduosfundo.jpg" onmouseover="coletaresiduos();" style="border:0px;" /></a>
                    </td>                
                    <td style="position:relative; top: 36px; left:74px;">
                        <a href="forms/brooks/RCD.aspx"><img id="Img8" src="forms/brooks/RCDfundo.jpg" onmouseover="RCD();" style="border:0px;" /></a>
                    </td>                
                    <td style="position:relative; top: 36px; left:76px;">
                        <a href="forms/brooks/RSS.aspx"><img id="Img9" src="forms/brooks/RSSfundo.jpg" onmouseover="RSS();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 32px; left:68px;">
                        <a href="forms/brooks/hidrojateamento.aspx"><img id="Img10" src="forms/brooks/hidrojateamentofundo.jpg" onmouseover="hidrojateamento();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 32px; left:64px;">
                        <a href="forms/brooks/gestaoglobal.aspx"><img id="Img11" src="forms/brooks/gestaoglobalfundo.jpg" onmouseover="gestaoglobal();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 30px; left:60px;">
                        <a href="forms/brooks/USTE.aspx"><img id="Img12" src="forms/brooks/USTEfundo.jpg" onmouseover="USTE();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 28px; left:62px;">
                        <a href="forms/brooks/DTR.aspx"><img id="Img13" src="forms/brooks/dtrfundo.jpg" onmouseover="DTR();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 28px; left:62px;">
                        <a href="forms/brooks/destinacaofinal.aspx"><img id="Img14" src="forms/brooks/destinacaofinalfundo.jpg" onmouseover="destinacaofinal();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 34px; left:60px;">
                        <a href="forms/brooks/logistica.aspx"><img id="Img15" src="forms/brooks/logisticafundo.jpg" onmouseover="logistica();" style="border:0px;" /></a>
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
