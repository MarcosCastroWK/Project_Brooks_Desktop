<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioVendasPorRepresentantePorPeriodo.aspx.cs" Inherits="SILC.Web.Relatorios.RelatorioVendasPorRepresentantePorPeriodo" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>

<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>

<%@ Register src="../forms/DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc4" %>

<%@ Register src="../forms/INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
        function Imprime() {
            try {
                document.getElementById('Operacoes').style.visibility = "hidden";
                document.getElementById('Panel1').style.position = "absolute";
                document.getElementById('Panel1').style.top = 0;
                window.print();
            }
            finally {
                document.getElementById('Operacoes').style.visibility = "visible";
                document.getElementById('Panel1').style.position = "";
            }
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 64px;
            height: 17px;
            font-family: Tahoma;
            font-weight: bold;
            font-size: 8pt;
        }
        .auto-style2 {
            width: 74px;
            height: 17px;
            font-family: Tahoma;
            font-weight: bold;
            font-size: 8pt;
        }
        .auto-style3 {
            width: 78px;
            height: 17px;
            font-family: Tahoma;
            font-weight: bold;
            font-size: 8pt;
        }
        .auto-style4 {
            width: 352px;
            height: 17px;
            font-family: Tahoma;
            font-weight: bold;
            font-size: 8pt;
        }
        .auto-style5 {
            width: 90px;
            height: 17px;
            font-family: Tahoma;
            font-weight: bold;
            font-size: 8pt;
        }
        .auto-style6 {
            width: 70px;
            height: 17px;
            font-family: Tahoma;
            font-weight: bold;
            font-size: 8pt;
        }
        .auto-style7 {
            width: 322px;
            height: 17px;
            font-family: Tahoma;
            font-weight: bold;
            font-size: 8pt;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">

        <div id="Operacoes">

            <uc1:cabecalho ID="cabecalho1" runat="server" />
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Vendas por Representante por Periodo"></asp:Label>
    
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Vencimento:" Width="30px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data1" runat="server" />
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="Label1" runat="server" CssClass="LetrasLabel" Text=" a " Width="20px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data2" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="LetrasLabel" Text="Código representante:"></asp:Label>
                    </td>
                    <td>
                        <uc5:INTEIRO ID="intCodigoRepresentante" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblNomeRepresentante" runat="server" CssClass="LetrasLabel" Text="..."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório de Vendas por Representante');" style="height: 26px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" Text="Voltar" PostBackUrl="~/forms/Menu.aspx" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="1300px">
                &nbsp;&nbsp;
                <asp:Label ID="lblTituloRelatorio" runat="server" style="font-weight: bold; font-family:Tahoma; font-size: 8pt;" Text=""></asp:Label>                
                <table cellspacing="1" cellpadding="1"> 
                    <tr>
                        <td class="auto-style1" style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center;">Emissão</td>
                        <td class="auto-style2" style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center;">Vencimento</td>
                        <td class="auto-style3" style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center;">Código</td>
                        <td class="auto-style4" style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center;">Cliente</td>
                        <td class="auto-style5" style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center;">Valor Bruto</td>
                        <td class="auto-style6" style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center;">Código</td>
                        <td class="auto-style7" style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center;">Representante</td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
