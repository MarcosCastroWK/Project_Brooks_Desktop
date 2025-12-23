<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatorioReciclaveisPorDestinoFinal.aspx.cs" Inherits="RelatorioReciclaveisPorDestinoFinal" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>
<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>
<%@ Register src="../forms/DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc4" %>
<%@ Register src="../forms/GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
        function AbrePesquisaDestinoFinal() {
            var navegador = navigator.appName.toLowerCase();
            if (navegador.indexOf("internet") == -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                window.open('../forms/PesquisaClientes.aspx');
            }
            else if (navegador.indexOf("internet") > -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                location = '../forms/PesquisaDestinoFinal.aspx?explorer=../Relatorios/RelatorioReciclaveisPorDestinoFinal.aspx';
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
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Recicláveis por Destino Final"></asp:Label>
    
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
                        <uc4:DESTINOFINAL ID="DESTINOFINAL1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem" alt="x" src="../Images/procura.png" onclick="AbrePesquisaDestinoFinal()"/>
                    </td>
                    <td>
                        <uc5:GRUPORESIDUO ID="GRUPORESIDUO1" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="1px" />            
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                    <td colspan="2">
                        <asp:GridView ID="GradeResiduos"  runat="server" CellPadding="0" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" AllowSorting="True" ForeColor="Black" GridLines="Vertical" Font-Bold="False" OnRowCommand="GradeResiduos_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Código">
                                <HeaderStyle CssClass="padItemGrade" />
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeResiduo" HeaderText="Resíduo">
                                <ItemStyle CssClass="padItemGrade" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnImprimir" runat="server" Text="Ok" OnClick="btnImprimir_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório de Recicláveis por Destino Final');" Height="26px" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />    
                    </td>
                    <td>
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
        <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="1060px">
        </asp:Panel>
    </form>
</body>
</html>
