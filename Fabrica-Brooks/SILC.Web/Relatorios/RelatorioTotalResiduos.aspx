<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioTotalResiduos.aspx.cs" Inherits="SILC.Web.Relatorios.RelatorioTotalResiduos" %>

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
                location = '../forms/PesquisaClientes.aspx?explorer=../Relatorios/RelatorioTotalResiduos.aspx';
            }
        }
        function AbrePesquisaGrupoResiduos() {
            document.getElementById("<%=btnProcurar.ClientID%>").click();
            window.open('../forms/PesquisaResiduos.aspx');
        }
        function CheckedRadios(pId) {
            if (document.getElementById(pId).checked && document.getElementById(pId).value == 'rdbDiario') {
                document.getElementById('rdbMensal').checked = false;
                document.getElementById('rdbAnual').checked = false;
            }
            else if (document.getElementById(pId).checked && document.getElementById(pId).value == 'rdbMensal') {
                document.getElementById('rdbDiario').checked = false;
                document.getElementById('rdbAnual').checked = false;
            }
            else if (document.getElementById(pId).checked && document.getElementById(pId).value == 'rdbAnual') {
                document.getElementById('rdbDiario').checked = false;
                document.getElementById('rdbMensal').checked = false;
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
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório Total de Resíduos"></asp:Label>
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
                        <uc2:CLIENTESCONTROL ID="CLIENTESCONTROL1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem" alt="x" style="cursor:pointer;" src="../Images/procura.png" onclick="AbrePesquisaClientes()"/>
                    </td>
                    <td>
                        <asp:Button ID="btnAdicionarCliente" runat="server" Text="Adicionar" OnClick="btnAdicionarCliente_Click" />
                    </td>
                    <td>
                        <uc5:GRUPORESIDUO ID="GRUPORESIDUO1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem2" alt="x" style="cursor:pointer;" onclick="AbrePesquisaGrupoResiduos()" src="../Images/procura.png" />
                    </td>
                    <td>
                        <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="1px" />            
                    </td>
                </tr>
                <tr>
                    <td colspan="4" valign="top">
                        <table style="width:130px" class="LetrasLabel">
                            <tr>
                                <td style="text-align:center; height:22px">Visualização
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbDiario" runat="server" Text="Diário" Checked="true" onclick="CheckedRadios('rdbDiario');" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbMensal" runat="server" Text="Mensal" onclick="CheckedRadios('rdbMensal');" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbAnual" runat="server" Text="Anual" onclick="CheckedRadios('rdbAnual');" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="3" valign="top">
                        <asp:GridView ID="GradeClientes"  runat="server" CellPadding="0" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" AllowSorting="True" ForeColor="Black" GridLines="Vertical" Font-Bold="False" OnRowCommand="GradeClientes_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibnExcluir0" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Código">
                                <HeaderStyle CssClass="padItemGrade" />
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeCliente" HeaderText="Cliente">
                                <ItemStyle CssClass="padItemGrade" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                    <td colspan="2" valign="top">
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
                        <asp:Button ID="btnImprimir" runat="server" Text="Ok" OnClick="btnImprimir_Click" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Relatório Total de Resíduos');" />
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
        <asp:Panel ID="Panel1" runat="server" BorderWidth="0px">
        </asp:Panel>
        
    </form>
</body>
</html>
