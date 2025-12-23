<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatorioConferenciaDiaria.aspx.cs" Inherits="Relatorios_RelatorioConferenciaDiaria" %>

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
                location = '../forms/PesquisaClientes.aspx?explorer=../Relatorios/RelatorioConferenciaDiaria.aspx';
            }
        }
        function Imprime()
        {
            try {
                //alert(window.navigator.userAgent);
                if (window.navigator.userAgent.indexOf('Chrome/117') == -1) {
                    document.getElementById('Operacoes').style.visibility = "hidden";
                    document.getElementById('Panel1').style.position = "absolute";
                    document.getElementById('Panel1').style.top = 0;
                }
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
            width: 28px;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Operacoes">

            <uc1:cabecalho ID="cabecalho1" runat="server" />
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Conferência Diária"></asp:Label>    
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Data:" Width="30px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data1" runat="server" />
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
                        <img id="imagem" alt="x" src="../Images/procura.png" onclick="AbrePesquisaClientes()" class="auto-style1"/>
                    </td>
                    <td>
                        <asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="1px" />
                    </td>
                </tr>
            </table>
            <table style="word-spacing: 0; padding: 0;"> 
                <tr>
                    <td>
                        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', '', 'Relatório de Conferência Diária');"/>
                    </td>
                    <td>
                        <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
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
            <asp:GridView ID="Grade" runat="server" Width="1280px" CssClass="LetrasLabel" OnRowDataBound="Grade_RowDataBound" CellPadding="1" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Font-Size="11px">
                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="12px" BorderWidth="1" BorderStyle="Groove" />
                <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" Font-Size="12px" BorderWidth="1" BorderStyle="Groove" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" />
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
