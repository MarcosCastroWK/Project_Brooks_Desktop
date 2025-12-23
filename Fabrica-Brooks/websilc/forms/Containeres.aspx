<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Containeres.aspx.cs" Inherits="forms_containeres" %>
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
            width: 59px;
            font-size: 10px;
            }
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            width: 59px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style12
        {
            width: 59px;
            font-size: 9px;
        }
        .auto-style13 {
            width: 300px;
        }
        </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Containeres" Font-Bold="True" CssClass="titulo2"></asp:Label>
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
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Numero" HeaderText="Número/Identificação" SortExpression="Numero">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataCadastro" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Cadastro" SortExpression="DataCadastro" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Inativo" HeaderText="Inativo" SortExpression="Inativo" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Capacidade" HeaderText="Capacidade" DataFormatString="{0:n2}" SortExpression="Capacidade">
                                    <HeaderStyle CssClass="padItemGrade"></HeaderStyle>
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cor" HeaderText="Cor" SortExpression="Cor">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EhLocal" HeaderText="Local Armazenamento" SortExpression="EhLocal">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EhTerceiro" HeaderText="É Terceiro" SortExpression="EhTerceiro">
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
                                       <asp:ListItem Value="Numero">Número/Identificação</asp:ListItem>
                                       <asp:ListItem>Tipo</asp:ListItem>
                                       <asp:ListItem Value="Cor">Cor</asp:ListItem>
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
                        <table class="auto-style13">
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label CssClass="LetrasLabel" ID="Label16" runat="server" Width="100px" Font-Bold="False">Data do cadastro</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <uc6:DATA ID="datDataCadastro" runat="server" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkInativo" runat="server" Text="Inativo" Width="200px" onclick="TiraCheckTerceiro();" Font-Bold="False" Font-Names="Arial"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label ID="lblNovaSenha" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Número/Identificação</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <asp:TextBox ID="txtNumero" runat="server" TabIndex="1" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkLocalArmazenamento" runat="server" Text="É local de Armazenamento" TabIndex="1" Width="200px" onclick="TiraCheckProprio();" Font-Bold="False" CssClass="LetrasLabel" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;Tipo</asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <asp:TextBox ID="txtTipo" onfocus="LimpaErro()" runat="server" TabIndex="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkTerceiro" runat="server" Text="Terceiro" TabIndex="1" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False">&nbsp;Capacidade m3</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <uc5:MOEDA ID="valCapacidade" runat="server" IndiceTab="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False">&nbsp;Cor</asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <asp:TextBox ID="txtCor" onfocus="LimpaErro()" runat="server" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="auto-style12">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
