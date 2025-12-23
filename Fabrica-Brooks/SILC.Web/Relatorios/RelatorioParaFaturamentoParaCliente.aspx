<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioParaFaturamentoParaCliente.aspx.cs" Inherits="SILC.Web.Relatorios.RelatorioParaFaturamentoParaCliente" %>

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
                location = '../forms/PesquisaClientes.aspx?explorer=../Relatorios/RelatorioParaFaturamentoParaCliente.aspx';
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
            width: 68px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">
            <uc1:cabecalho ID="cabecalho1" runat="server" />
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório para Faturamento - Cliente"></asp:Label>
    
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
                    <td>
                        <asp:Label ID="lblTituloCodigo" runat="server" CssClass="LetrasLabel" Text="Código: "></asp:Label>
                        <asp:TextBox ID="txtCodigo" runat="server" Width="60px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblRazaoSocial" runat="server" Text="razao social"></asp:Label>
                        &nbsp;&nbsp;&nbsp; </td>

                    <td>
                        &nbsp;<asp:ImageButton ID="imagem" runat="server" ImageUrl="~/Images/procura.png" OnClick="btnProcurar_Click" OnClientClick="AbrePesquisaClientes();" />
                    </td>
                    <td>
                        <asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="1px" OnClick="btnProcurar_Click" />            
                    </td>
                </tr>
            </table>
            <table style="padding: 0; word-spacing: 0;">
                <tr>
                    <td>
                        <asp:Button ID="btnImprimir" runat="server" Text="Ok" OnClick="btnImprimir_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório para Faturamento - Cliente');" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />   
                    </td>
                    <td class="auto-style1">
                        <input id="btnImprimir0" type="button" value="Imprimir" onclick="Imprime();" />
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

        <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="1000px">
        </asp:Panel>
        
    </form>
</body>
</html>
