<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioConferenciaDiariaPorPeriodo.aspx.cs" Inherits="SILC.Web.Relatorios.Relatorios_RelatorioConferenciaDiariaPorPeriodo" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>

<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
        function AbrePesquisaClientes() {
            var navegador = navigator.appName.toLowerCase();
            if (navegador.indexOf("internet") == -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                window.open('../forms/PesquisaClientes.aspx');
            }
            else if (navegador.indexOf("internet") > -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                location = '../forms/PesquisaClientes.aspx?explorer=../Relatorios/RelatorioConferenciaDiariaPorPeriodo.aspx';
            }
        }
        function TiraOrdemCaminhao() {
            if (document.getElementById('rdbOrdemCaminhao').checked) {
                document.getElementById('rdbOrdemData').checked = false;
            }
            else if (document.getElementById('rdbOrdemCaminhao').checked == false) {
                document.getElementById('rdbOrdemData').checked = true;
            }
        }
        function TiraOrdemData() {
            if (document.getElementById('rdbOrdemData').checked) {
                document.getElementById('rdbOrdemCaminhao').checked = false;
            }
            else if (document.getElementById('rdbOrdemData').checked == false) {
                document.getElementById('rdbOrdemCaminhao').checked = true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">

            <uc1:cabecalho ID="cabecalho1" runat="server" />
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Conferência Diária Por Período"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Período:" Width="30px"></asp:Label>
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
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:RadioButton ID="rdbOrdemCaminhao" runat="server" CssClass="LetrasLabel" Text="Ordem Caminhão" onclick="TiraOrdemCaminhao();" Checked="True"/>
                    </td>
                    <td></td>
                    <td>
                        <asp:RadioButton ID="rdbOrdemData" runat="server" CssClass="LetrasLabel" Text="Ordem Data" onclick="TiraOrdemData();"/>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panTipo" runat="server" GroupingText="Tipo" CssClass="titulo2" Width="300px" BackColor="White" Height="40px">
                <asp:CheckBox ID="chkComValores" runat="server" Checked="True" CssClass="LetrasLabel" Text="Com Valores" />
                <asp:CheckBox ID="chkSemSubtotal" runat="server" Checked="True" CssClass="LetrasLabel" Text="Sem Subtotal" />
            </asp:Panel>
            <table>
                <tr>
                    <td>
                        <uc2:CLIENTESCONTROL ID="CLIENTESCONTROL1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem" alt="x" src="../Images/procura.png" onclick="AbrePesquisaClientes()"/>
                    </td>
                    <td>
                        <asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="1px" />            
                    </td>
                </tr>
            </table>
            <table style="padding: 0; word-spacing: 0;">
                <tr>
                    <td>
                        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório de Conferência Diária Por Período');" />
                    </td>
                    <td>
                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="lblTitulo0" runat="server" BackColor="White" CssClass="titulo2" Text="Relatório de "></asp:Label>
            <br />
            <br />
            <asp:GridView ID="Grade" runat="server" PageSize="45" Width="1400px" CssClass="LetrasLabel" OnRowDataBound="Grade_RowDataBound" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblTotalMovimentacoes" runat="server" CssClass="LetrasLabel" Text="Total de movimentações:"></asp:Label>
            <br />
        </asp:Panel>
        
    </form>
</body>
</html>
