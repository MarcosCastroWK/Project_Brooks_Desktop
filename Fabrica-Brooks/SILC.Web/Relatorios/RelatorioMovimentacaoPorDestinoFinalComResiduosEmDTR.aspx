<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioMovimentacaoPorDestinoFinalComResiduosEmDTR.aspx.cs" Inherits="SILC.Web.Relatorios.RelatorioMovimentacaoPorDestinoFinalComResiduosEmDTR" %>
<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>
<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>
<%@ Register src="../forms/DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
        function AbrePesquisaClientes()
        {
            window.open('../forms/PesquisaClientes.aspx');
        }
        function AbrePesquisaDestinoFinal() {
            var navegador = navigator.appName.toLowerCase();
            if (navegador.indexOf("internet") == -1)
            {
                window.open('../forms/PesquisaDestinoFinal.aspx');
            }
            else if (navegador.indexOf("internet") > -1)
            {
                location = '../forms/PesquisaDestinoFinal.aspx?explorer=../Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinal.aspx';
            }
        }
        function Imprime()
        {
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
        function DischecarDivergentes(pId)
        {
        }
        function ChecaTodos() {
            document.getElementById('chkMostrarCDFe').checked = true;
        }
        function NaoChecados()
        {
        }
        function Checados()
        {
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">

            <uc1:cabecalho ID="cabecalho1" runat="server" />    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório Recebimento MTR-e e CDF-e por Destino Final"></asp:Label>
    
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Período:" Width="40px"></asp:Label>
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
                    <td colspan="2">
                        <table border="0">
                            <tr>
                                <td>
                                    <uc4:DESTINOFINAL ID="DESTINOFINAL1" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnMostraDestinoFinal" runat="server" Text="Ok" OnClick="btnMostraDestinoFinal_Click" />
                                </td>
                                <td>
                                    <img id="imagem1" alt="x" src="../Images/procura.png" onclick="AbrePesquisaDestinoFinal()"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <uc2:CLIENTESCONTROL ID="CLIENTE1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem2" alt="x" src="../Images/procura.png" onclick="AbrePesquisaClientes()"/>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkMostrarCDFe" runat="server" CssClass="LetrasLabel" Text="Mostrar CDFe" onclick="NaoChecados();" Checked="True" />
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório Recebimento MTR-e e CDF-e por Destino Final');" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnProcurar" runat="server" BackColor="White" BorderWidth="0" ForeColor="White" Text="P" Visible="true" Width="1px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">            
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <div id="divImprimir">
            <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="1750px">
            </asp:Panel>
        </div>
    </form>
</body>
</html>
