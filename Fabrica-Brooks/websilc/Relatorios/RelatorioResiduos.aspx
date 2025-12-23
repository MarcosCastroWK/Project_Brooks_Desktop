<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatorioResiduos.aspx.cs" Inherits="RelatorioResiduos" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

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
                document.getElementById('Panel1').style.position = '';
            }
        }
        function AbrePreview()
        {
            var strWindowFeatures = "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes";
            window.open('RelatorioContaineres.aspx', strWindowFeatures);
        }
        function TiraLocadas() {
            if (document.getElementById('rdbLocadas').checked) {
                document.getElementById('rdbDisponiveis').checked = false;
            }
            else if (document.getElementById('rdbLocadas').checked == false) {
                document.getElementById('rdbDisponiveis').checked = true;
            }
            document.getElementById('rdbDataLocacao').disabled = false;
            document.getElementById('rdbNomeFantasia').disabled = false;
            document.getElementById('rdbNumeroCaixa').checked = true;
        }
        function TiraDisponiveis() {
            document.getElementById('rdbDataLocacao').disabled = true;
            document.getElementById('rdbNomeFantasia').disabled = true;
            if (document.getElementById('rdbDisponiveis').checked) {
                document.getElementById('rdbLocadas').checked = false;
            }
            else if (document.getElementById('rdbDisponiveis').checked == false) {
                document.getElementById('rdbLocadas').checked = true;
            }
            document.getElementById('rdbNumeroCaixa').checked = true;
            document.getElementById('rdbDataLocacao').checked = false;
            document.getElementById('rdbNomeFantasia').checked = false;
        }
        function TiraDataLocacao() {
            if (document.getElementById('rdbDataLocacao').checked) {
                document.getElementById('rdbNumeroCaixa').checked = false;
                document.getElementById('rdbNomeFantasia').checked = false;
            }
            else if (document.getElementById('rdbDataLocacao').checked == false) {
                document.getElementById('rdbNumeroCaixa').checked = true;
                document.getElementById('rdbNomeFantasia').checked = true;
            }
        }
        function TiraNumeroCaixa() {
            if (document.getElementById('rdbNumeroCaixa').checked) {
                document.getElementById('rdbDataLocacao').checked = false;
                document.getElementById('rdbNomeFantasia').checked = false;
            }
            else if (document.getElementById('rdbNumeroCaixa').checked == false) {
                document.getElementById('rdbDataLocacao').checked = true;
                document.getElementById('rdbNomeFantasia').checked = true;
            }
        }
        function TiraNomeFantasia() {
            if (document.getElementById('rdbNomeFantasia').checked) {
                document.getElementById('rdbDataLocacao').checked = false;
                document.getElementById('rdbNumeroCaixa').checked = false;
            }
            else if (document.getElementById('rdbNomeFantasia').checked == false) {
                document.getElementById('rdbDataLocacao').checked = true;
                document.getElementById('rdbNumeroCaixa').checked = true;
            }
        }
    </script>
    <style type="text/css">
        #btnCancelar {
            width: 68px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">
            <uc1:cabecalho ID="cabecalho1" runat="server" />
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Resíduos"></asp:Label>
    
            <br />
            <table style="padding:0; word-spacing:0;">
                <tr>
                    <td>
                        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Relatório de Containeres');" />
                    </td>
                    <td>
                        <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Voltar" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                    </td>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" Visible="False" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>

        <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server">
                <asp:Label ID="lblTitulo0" runat="server" BackColor="White" CssClass="titulo2" Text="Relatório de Resíduos"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="Grade" runat="server" PageSize="45" Width="1450px" CssClass="LetrasLabel" OnRowDataBound="Grade_RowDataBound" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
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
            </asp:Panel>
        </div>
    </form>
</body>
</html>
