<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioMovimentacaoDestinoFinal.aspx.cs" Inherits="SILC.Web.Relatorios.RelatorioMovimentacaoDestinoFinal" %>
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
        function AbrePesquisaClientes() {
            document.getElementById("<%=btnProcurar.ClientID%>").click();
            window.open('../forms/PesquisaClientes.aspx');
        }
        function AbrePesquisaDestinoFinal() {
            var navegador = navigator.appName.toLowerCase();
            if (navegador.indexOf("internet") == -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                window.open('../forms/PesquisaDestinoFinal.aspx');
            }
            else if (navegador.indexOf("internet") > -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                location = '../forms/PesquisaDestinoFinal.aspx?explorer=../Relatorios/RelatorioMovimentacaoDestinoFinal.aspx';
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
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">

            <uc1:cabecalho ID="cabecalho1" runat="server" />
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório movimentação de aterro com nº da MTRe"></asp:Label>
    
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
                        <uc4:DESTINOFINAL ID="DESTINOFINAL1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem1" alt="x" src="../Images/procura.png" onclick="AbrePesquisaDestinoFinal()"/>
                    </td>
                    <td>
                        <uc2:CLIENTESCONTROL ID="CLIENTE1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem2" alt="x" src="../Images/procura.png" onclick="AbrePesquisaClientes()"/>
                    </td>
                    <td>
                        <asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="1px" />            
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório movimentação de aterro com nº da MTRe');" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar e-mail" OnClick="btnEnviarEmail_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                                    <asp:CheckBox ID="chkNaoMostrarSemTicket" runat="server" CssClass="LetrasLabel" Height="24px" Text="Mostrar MTRe sem Ticket" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="1300px"></asp:Panel>
        </div>
        <asp:Button ID="btnConfirmar" runat="server" OnClick="btnConfirmar_Click" Text="Confirmar" Visible="False" />
    </form>
</body>
</html>
