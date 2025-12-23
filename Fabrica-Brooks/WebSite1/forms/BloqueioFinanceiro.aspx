<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BloqueioFinanceiro.aspx.cs" Inherits="forms_BloqueioFinanceiro" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style13
        {
            height: 28px;
        }
        </style> 
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function AbrePesquisaClientes() {
        var navegador = navigator.appName.toLowerCase();
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        if (navegador.indexOf("internet") == -1) {
            window.open('../forms/PesquisaClientes.aspx');
        }
        else if (navegador.indexOf("internet") > -1)
        {
            location = '../forms/PesquisaClientes.aspx?explorer=../Relatorios/BloqueioFinanceiro.aspx';
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Bloqueio Financeiro" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="50%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
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
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Código cliente" SortExpression="CodigoCliente">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeFantasiaCliente" HeaderText="Nome cliente" SortExpression="NomeFantasiaCliente" />
                                    <asp:BoundField DataField="CodigoUsuario" HeaderText="Código usuário" SortExpression="CodigoUsuario" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeUsuario" HeaderText="Nome usuário" SortExpression="NomeUsuario" />
                                    <asp:BoundField DataField="DataBloqueio" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Bloqueio" SortExpression="DataBloqueio" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataDesbloqueio" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Desbloqueio" SortExpression="DataDesbloqueio" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Observacao" HeaderText="Observação" SortExpression="Observacao" />
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
                                       <asp:ListItem>Bloqueados</asp:ListItem>
                                       <asp:ListItem>Nome Fantasia Cliente</asp:ListItem>
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
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label17" runat="server" Width="90px" Font-Bold="False">Código Cliente</asp:Label>
                                </td>
                                <td colspan="2">
                                    <table style="column-span:none;padding:0;" cellspacing="0">
                                        <tr>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoCliente" runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNomeFantasiaCliente" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="200px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnOkCliente" runat="server" Text="Mostrar cliente" UseSubmitBehavior="false" style="text-align:center" OnClick="btnOkCliente_Click" Width="100px" />
                                            </td>
                                            <td>
                                                <img id="imagem" alt="x" style="cursor:pointer;" onclick="AbrePesquisaClientes()" src="../Images/procura.png" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">
                                    <asp:Label CssClass="LetrasLabel" ID="Label18" runat="server" Width="90px" Font-Bold="False">Código Usuário</asp:Label>
                                </td>
                                <td colspan="2" class="auto-style13">
                                    <uc4:INTEIRO7 ID="intCodigoUsuario" runat="server" Enabled="False" />
                                    <asp:TextBox ID="txtNomeUsuario" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="200px" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label16" runat="server" Width="90px" Font-Bold="False">Data Bloqueio</asp:Label>
                                </td>
                                <td>
                                    <uc6:DATA ID="datDataBloqueio" runat="server" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label19" runat="server" Width="90px" Font-Bold="False">Data Desbloqueio</asp:Label>
                                </td>
                                <td>
                                    <uc6:DATA ID="datDataDesbloqueio" runat="server" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False">&nbsp;Observação</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtObservacao" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td>
                                    <div style="width:140px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                                <td><asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="10px" OnClick="btnProcurar_Click" /></td>                            </tr>
                            <tr>
                                <td style="width:1%">
                                    &nbsp;</td>
                                <td  style="width:80px">
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
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
