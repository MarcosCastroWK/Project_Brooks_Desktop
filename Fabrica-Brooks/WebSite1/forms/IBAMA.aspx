<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IBAMA.aspx.cs" Inherits="IBAMA" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

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
            width: 90px;
            font-size: 10px;
            height: 26px;
        }
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            height: 1%;
        }
    </style>
    <script src="geral.js" lang="javascript" type="text/javascript"></script>
    <script src="../Scripts/funcaoGeral.js"></script>
</head>
<body>
     <form id="form1" runat="server">
        <div id="Operacoes">
            <table class="form2">
                <tr>
                    <td class="auto-style11">
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;IBAMA" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td id="dadosCab2" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Button ID="btnVisualizacaoSimples" runat="server" style="width: 110px; font-size:8pt;" Text="visualização simples" OnClick="btnVisualizacaoSimples_Click" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Cadastro do IBAMA');" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%; min-height: 20vh; max-height: 40vh; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="99%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Cadastro do IBAMA');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Cadastro do IBAMA');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoIBAMA" HeaderText="Código IBAMA" SortExpression="CodigoIBAMA" >
                                    <ItemStyle CssClass="padItemGrade" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="80%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="Ativo" >
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
                                <td class="LetrasLabel">
                                    <asp:Label CssClass="LetrasLabel" ID="Label3" runat="server" Font-Bold="False">&nbsp;Procurar:</asp:Label>
                                    </td>
                                <td>
                                    <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                       <asp:ListItem>Descrição</asp:ListItem>
                                       <asp:ListItem Value="Ativo">Ativo</asp:ListItem>
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
                        <table style="width:300px">
                            <tr>
                                <td class="auto-style10">
                                    &nbsp;</td>
                                <td class="auto-style10">
                                    <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" TabIndex="2" CssClass="LetrasLabel"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;Descrição</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtDescricao" runat="server" TabIndex="1" Height="111px" TextMode="MultiLine" Width="785px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False">&nbsp;Código IBAMA</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtCodigoIBAMA" onfocus="LimpaErro()" runat="server" Width="85px" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="1" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Cadastro do IBAMA');" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" TabIndex="1" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Cadastro do IBAMA');" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divImprimir" style="top:30%; left:30%; position:absolute">
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden;" />
            <asp:Panel ID="Panel1" runat="server" Height="500px"></asp:Panel>
        </div>
    </form>
</body>
</html>
