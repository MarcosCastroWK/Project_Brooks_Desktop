<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MTR.aspx.cs" Inherits="MTR" %>

<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %><%@ Register src="CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %><%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc3" %><%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc4" %>

<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <style>
        .tituloRelNegrito
        {
            font-family: Tahoma;
            font-size: 11pt;
            font-weight: bold;
        }
        .destinoRel
        {
            font-family: Tahoma;
            font-size: 11pt;
            font-weight: bold;
        }
        .invisivel
        {
            background-color: white;
            color: white;
            width: 1px;
            font-size: 1px;
            visibility: hidden;
            font-weight: bold;
        }
        .bordaTD
        {
            font-family:Tahoma;
            font-size: 11px;
            border: solid 1px;
            height: 40px;
            vertical-align: top;
            font-weight: bold;
        }
        .semBorda
        {
            font-family:Tahoma;
            font-size: 11px;
            height: 40px;
            vertical-align: top;
            border: none;
            border-radius:unset;
            background-color:beige;
            font-weight: bold;
        }
        .LetrasMTR_Right
        {
            font-family:Tahoma;
            font-size: 11px;
            font-weight: bold;
            text-align: right;     
        }
        .LetrasMTR
        {
            font-family:Tahoma;
            font-size: 11px;
            font-weight: bold;
        }
        .w10 {
            width: 10%;
            border-left: 1px solid;
        }
        .w20 {
            width: 20%;
            border-left: 1px solid;
        }
        .w30{
            width: 30%;
            border-left: 1px solid;
        }
        .w40 {
            width: 40%;
            border-left: 1px solid;
        }
        .w50{
            width: 50%;
            border-left: 1px solid;
        }    
        .styleddl {
            border-style: none;
            font-family: Tahoma;
            font-size: 11px;
            vertical-align: top;
            border-radius: unset;
            background-color:beige;
            font-weight: bold;
        }
    </style>
    <script>
        function Imprime()
        {
            if (document.getElementById('ddlMotorista').value == '')
            {
                alert('Campo Motorista com nome inválido!');
                return;
            }
            else if (document.getElementById('ddlPlacas').value == '')
            {
                alert('Campo Placa do Veículo com valor inválido!');
                return;
            }
            try {
                document.getElementById('Operacoes').style.visibility = "hidden"; 
                document.getElementById('Panel1').style.position = "absolute";
                document.getElementById('Panel1').style.top = 0;
                var _vias = 0;
                _vias = document.getElementById('intVias_txtInteiro').value;
                var i = 0;
                for (i = 1; i <= _vias; i++) {
                    window.print();
                }
                SalvarImprimido();
            }
            finally {
                document.getElementById('Operacoes').style.visibility = "visible";
                document.getElementById('Panel1').style.position = "initial";
            }
        }
        function SalvarImprimido() {
            document.getElementById("<%=btnSalvar.ClientID%>").click();
        }

        function Continuar() {
        var r = confirm("Continuar?");
        if (r == true) {
            window.location = 'DTR.aspx?Continuar=1';
        }
        else {
            window.location = 'DTR.aspx?Continuar=0';
        }
    }

    </script>
</head>
<body style="margin-left: 60px;">
    <form id="form1" runat="server">
        <div id="Operacoes">
            <table>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="MTR - Manifesto de Tranporte de Resíduos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="btnImprimir" type="button" value="Imprimir" class="LetrasMTR" onclick="Imprime();" />
                    </td>
                    <td>
                        <asp:Label ID="lblVias" runat="server" class="LetrasMTR" Text="Vias: "></asp:Label>
                    </td>
                    <td>
                        <uc5:INTEIRO2 ID="intVias" runat="server" class="LetrasMTR" Valor="3" />
                    </td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" CssClass="invisivel" Text="" OnClick="btnSalvar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server" Width="860px">
                <table cellspacing="0" cellpadding="0" style="width: 820px; border: solid 1px;">
                    <tr>
                        <td class="bordaTD">
                            <img id="imglogo" runat="server" src="~/Images/logotipoMTR.png"/>
                        </td>
                        <td class="bordaTD" style="width: 700px;">
                            <br />
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblTituloRel" runat="server" CssClass="tituloRelNegrito" Text="MANIFESTO DE TRANSPORTE DE RESÍDUOS - MTR"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" style="width: 820px; border: solid 1px;">
                    <tr>
                        <td class="bordaTD">
                            &nbsp;<asp:Label ID="Label1" runat="server" class="LetrasMTR" Text="1) Tipo de Resíduo"></asp:Label>
                            <br />
                            <br />
                            &nbsp;<asp:Label ID="lblTipoResiduo" runat="server" class="LetrasMTR"></asp:Label>
                        </td>
                        <td class="bordaTD">
                            &nbsp;<asp:Label ID="Label2" runat="server" class="LetrasMTR" Text="2) Código"></asp:Label>
                            <br />
                            <br />
                            &nbsp;<asp:Label ID="lblCodigo" runat="server" class="LetrasMTR_Right" Width="30px"></asp:Label>
                        </td>
                        <td class="bordaTD">
                            &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" class="LetrasMTR" Text="3) Classe"></asp:Label>
                            <br />
                            <br />
                            &nbsp;
                            <asp:Label ID="lblClasse" runat="server" class="LetrasMTR"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="bordaTD">
                            &nbsp;&nbsp;<asp:Label ID="Label4" runat="server" class="LetrasMTR" Text="4) Estado Físico"></asp:Label>
                            <br />
                            <br />
                            &nbsp; <asp:Label ID="lblEstadoFisico" runat="server" class="LetrasMTR"></asp:Label>
                        </td>
                        <td class="bordaTD" colspan="2">
                            &nbsp;<asp:Label ID="Label5" runat="server" class="LetrasMTR" Text="PESO"></asp:Label>
                            <br />
                            <br />
                            &nbsp;<asp:Label ID="lblPeso" runat="server" class="LetrasMTR"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="bordaTD" colspan="3">
                            &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" class="LetrasMTR" Text="5) Acondicionamento"></asp:Label>
                            <br />
                            <br />
                            &nbsp;&nbsp;<asp:Label ID="Label7" runat="server" style="font-size: 10px; font-weight:bold; font-family: Tahoma;" Text="EM CONTAINER"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" style="width: 820px; border: solid 1px;">
                    <tr>
                        <td style="width:12px; height: 200px; horizontal-align: central;" class="bordaTD">
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="Label8" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;6&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label9" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;C&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label10" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;L&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label11" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;&nbsp;I&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label12" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;E&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label13" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;N&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label14" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;T&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label15" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;E&nbsp;&nbsp;"></asp:Label>
                        </td>
                        <td style="width: 600px;" class="bordaTD">
                            <asp:Label ID="Label17" runat="server" class="LetrasMTR" Text="&nbsp;Empresa / Razão Social"></asp:Label>
                            <br />
                            <br />
                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="styleddl" Font-Bold="True" Font-Names="Tahoma" Font-Size="11px" Width="580px">
                                <asp:ListItem>BROOKS AMBIENTAL LTDA</asp:ListItem>
                            </asp:DropDownList>
                            ──────────────────────────────────────────────────────────────────────────────<br />&nbsp;CNPJ:<br /> &nbsp;<br /> &nbsp;03.938.048/00001-33<br /> 
                            ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;Endereço:<br /> &nbsp;<br /> &nbsp;Av. Ivo Lucchi, 729 - DISTRITO INDUSTRIAL<br /> 
                            ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;Município:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UF:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fone:<br /> &nbsp;<br /> &nbsp;PALHOÇA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SC&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 48-3344-1515<br /> 
                            ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;Responsável pela expedição do Resíduo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cargo:
                            <br />
                            <br />
                            &nbsp;CRISTIANE D. SILVEIRA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; AUX. ADM.</td>
                        <td style="width: 200px;" class="bordaTD">
                            <br />
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _____/______/_____<br /> &nbsp;
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Data da Geração<br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp; _____________________<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Assinatura </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" style="width: 820px; border: solid 1px;">
                    <tr>
                        <td style="width:12px; height: 200px; horizontal-align: central;" class="bordaTD">
                            <br />
                            <br />
                            <asp:Label ID="Label33" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;7&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label16" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;T&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label19" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;R&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label20" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;A&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label21" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;N&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label22" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;S&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label23" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;P&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label24" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;O&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label25" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;R&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label29" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;T&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label30" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;A&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label28" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;D&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label31" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;O&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label32" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;R&nbsp;&nbsp;"></asp:Label>
                        </td>
                        <td style="width: 600px;" class="bordaTD">
                            <asp:Label ID="Label26" runat="server" class="LetrasMTR" Text="&nbsp;Empresa / Razão Social"></asp:Label>
                            
                            <br />
                            <br />
                            &nbsp;<asp:Label ID="Label27" runat="server" class="LetrasMTR" Text="BROOKS AMBIENTAL LTDA"></asp:Label>
                            <br />
                            ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;CNPJ:<br /> &nbsp;<br /> &nbsp;03.938.048/00001-33<br />
                            ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;Município:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; UF:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fone:<br /> &nbsp;<br /> &nbsp;PALHOÇA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SC&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 48-3344-1515<br /> 
                            ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;Nome do Motorista<br /><asp:DropDownList ID="ddlMotorista" runat="server" CssClass="styleddl" Height="20px" Width="582px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <br /> ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;Placa do Veículo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Estado/Município<br /> 
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                            <asp:DropDownList ID="ddlPlacas" runat="server" CssClass="styleddl" Width="184px" Height="20px">
                            <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SC / Palhoça 
                        </td>
                        <td style="width: 200px;" class="bordaTD">
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _____/______/_____<br /> &nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Data da Retirada<br />
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
                            &nbsp;&nbsp;&nbsp;&nbsp; _____________________<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Assinatura </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" style="width: 820px; border: solid 1px;">
                    <tr>
                        <td style="width:12px; height: 200px; horizontal-align: central;" class="bordaTD">
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="Label34" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;8&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Label35" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;D&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label36" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;E&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label37" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;S&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label38" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;T&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label39" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;I&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label40" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;N&nbsp;&nbsp;"></asp:Label>
                            <br />
                            <asp:Label ID="Label41" runat="server" class="LetrasMTR" Text="&nbsp;&nbsp;O&nbsp;&nbsp;"></asp:Label>
                        </td>
                        <td style="width: 600px;" class="bordaTD">
                            <asp:Label ID="Label42" runat="server" class="LetrasMTR" Text="&nbsp;Empresa / Razão Social"></asp:Label>
                            
                            <br />
                            <br />
                            &nbsp;<asp:Label ID="lblDestinoFinal" runat="server" class="LetrasMTR"></asp:Label>
                            <br />
                            ──────────────────────────────────────────────────────────────────────────────<br /> &nbsp;CNPJ:<br /> &nbsp;<br /> 
                            &nbsp;<asp:Label ID="lblCNPJ_Destino" runat="server" class="LetrasMTR" Text="00.904.606/0001-51"></asp:Label><br />
                            ──────────────────────────────────────────────────────────────────────────────<br />
                            &nbsp;Endereço:<br /> &nbsp;<br />
                            &nbsp;<asp:Label ID="lblEnderecoDestino" runat="server" class="LetrasMTR" Text="RUA PAULO LITZMBERGER, 1400"></asp:Label>
                            <br />
                            <table cellspacing="0" cellpadding="0" style="width: 607px" class="bordaTD">
                                <tr>
                                    <td style="border-top: 1px solid;">
                                        &nbsp;Município:
                                    </td>
                                    <td style="border-top: 1px solid; border-left: 1px solid;">
                                        &nbsp;UF:
                                    </td>
                                    <td style="border-top: 1px solid;border-left: 1px solid;">
                                        &nbsp;Fone:
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="border-left: 1px solid;">&nbsp;</td>
                                    <td style="border-left: 1px solid;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;<asp:Label ID="lblMunicipioDestino" runat="server" class="LetrasMTR" Text="&nbsp;BLUMENAU"></asp:Label></td>
                                    <td class="w10">&nbsp;<asp:Label ID="lblUFDestino" runat="server" class="LetrasMTR" Text="&nbsp;SC"></asp:Label></td>
                                    <td class="w30">&nbsp;<asp:Label ID="lblFoneDestino" runat="server" class="LetrasMTR" Text="&nbsp;47-3378-1414"></asp:Label></td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" style="width: 607px;" class="bordaTD">
                                <tr>
                                    <td style="border-top: solid 1px;">
                                        &nbsp;Responsável pelo Recebimento do Resíduo:
                                    </td>
                                    <td style="border-left: 1px solid; border-top: solid 1px;">
                                        &nbsp;Cargo:
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="border-left: 1px solid;width: 200px;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblResponsavel" runat="server" class="LetrasMTR"></asp:Label></td>
                                    <td style="border-left: 1px solid;">
                                    </td>
                                </tr>
                            </table>

                            &nbsp;
                        </td>
                        <td style="width: 200px;" class="bordaTD">
                            <br />
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _____/______/_____<br /> &nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Data Recebimento<br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp; ______________________<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Assinatura </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" style="width: 820px; border: solid 1px;">
                    <tr>
                        <td class="bordaTD">
                            <asp:Label ID="Label48" runat="server" class="LetrasMTR" Text="9) Gerador:"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlGerador" runat="server" Width="790px" Height="20px" CssClass="semBorda">
                                <asp:ListItem>CONFORME RELAÇÃO EM ANEXO</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" style="width: 820px; border: solid 1px;">
                    <tr>
                        <td class="bordaTD">
                            <asp:Label ID="Label43" runat="server" class="LetrasMTR" Text="10) Observações:"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtObs" runat="server" BackColor="beige" BorderStyle="None" Width="790px"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>        
    </form>
</body>
</html>
