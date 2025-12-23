<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlocosMTR.aspx.cs" Inherits="SILC.Web.forms.BlocosMTR" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style6
        {
            width: 59px;
            font-size: 10px;
            }
        .auto-style12
        {
            width: 59px;
            font-size: 9px;
        }
        </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function AbrePesquisaMotoristas() {
        var navegador = navigator.appName.toLowerCase();
        if (navegador.indexOf("internet") == -1) {
            document.getElementById("<%=btnProcurar.ClientID%>").click();
            window.open('../forms/PesquisaMotoristas.aspx');
        }
        else if (navegador.indexOf("internet") > -1) {
            document.getElementById("<%=btnProcurar.ClientID%>").click();
            location = '../forms/PesquisaMotoristas.aspx?explorer=../forms/BlocosMTR.aspx';
        }
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td style="height:1%;">
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>
                <tr>
                    <td class="Menu">
                        <uc2:menu ID="menu1" runat="server" Visible="false" />
                        
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Blocos MTR" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="90%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Sequencial" HeaderText="Sequencial" SortExpression="Sequencial">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Data" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data" SortExpression="Data" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeMotorista" HeaderText="Nome Motorista" SortExpression="NomeMotorista">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoMotorista" HeaderText="Código Motorista" SortExpression="CodigoMotorista" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroBloco" HeaderText="Número Bloco" SortExpression="NumeroBloco">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NuMTRInicial" HeaderText="nº MTR Inicial" SortExpression="NuMTRInicial">
                                    <HeaderStyle CssClass="padItemGrade"></HeaderStyle>
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NuMTRFinal" HeaderText="Nº MTR Final" SortExpression="NuMTRFinal">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CoeficienteNumeracao" HeaderText="Coeficiente Numeração" SortExpression="CoeficienteNumeracao">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="center" style="height:1%;">
                        <table>
                            <tr>
                                <td class="LetrasLabel">Procurar:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                       <asp:ListItem Value="Nome">Motorista</asp:ListItem>
                                       <asp:ListItem Value="NumeroBloco">Bloco</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFiltro" runat="server" Width="400px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table style="width:730px">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;<asp:Label CssClass="LetrasLabel" ID="Label16" runat="server" Width="100px" Font-Bold="False">Data</asp:Label>
                                </td>
                                <td>
                                    <uc6:DATA ID="datData" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table cellpadding="0">
                                        <tr>
                                            <td cellpadding="0">
                                                <uc7:MOTORISTA ID="ctlMotorista" runat="server" />
                                            </td>
                                            <td>
                                                <img id="imagem" alt="x" src="../Images/procura.png" onclick="AbrePesquisaMotoristas();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False" Width="100px">&nbsp;Número Bloco</asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <uc4:INTEIRO7 ID="intNumeroBloco" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False">&nbsp;nº MTR Inicial</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <uc4:INTEIRO7 ID="intNuMTRInicial" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    &nbsp;&nbsp;<asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="100px">&nbsp;nº MTR Final</asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <uc4:INTEIRO7 ID="intNuMTRFinal" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    &nbsp;&nbsp;<asp:Label ID="Label17" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="120px">&nbsp;Coeficiente Numeração</asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <uc4:INTEIRO7 ID="intCoeficienteNumeracao" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="auto-style12">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                                <td class="auto-style12">
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px" OnClick="btnProcurar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
