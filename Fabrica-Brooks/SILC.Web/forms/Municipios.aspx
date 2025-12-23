<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Municipios.aspx.cs" Inherits="SILC.Web.forms.Municipios" %>
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
        .auto-style8
        {
            width: 10px;
            font-size: 9px;
            height: 24px;
        }
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            width: 90px;
            top: 1px;
            font-size: 9px;
            height: 26px;
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Municípios" Font-Bold="True" CssClass="titulo2"></asp:Label>
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="80%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" AllowPaging="True" PageSize="6" OnPageIndexChanging="Grade_PageIndexChanging" OnRowCommand="Grade_RowCommand" Font-Bold="False" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="400px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoIPM" HeaderText="Código IPM" SortExpression="CodigoIPM" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoIBGE" HeaderText="Código IBGE" SortExpression="CodigoIBGE" >
                                    <ItemStyle CssClass="padItemGrade" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AliquotaISS" HeaderText="Alíquota ISS" SortExpression="AliquotaISS" />
                                    <asp:BoundField DataField="UF" HeaderText="UF" SortExpression="UF" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <PagerSettings PageButtonCount="20" />
                                <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                     </td>
                </tr>
                <tr>
                    <td valign="center" style="height:1%;">
                        <table>
                            <tr>
                                <td class="LetrasLabel">Procurar:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                       <asp:ListItem Value="Nome">Nome</asp:ListItem>
                                       <asp:ListItem>UF</asp:ListItem>
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
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;Nome</asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtNome" runat="server" TabIndex="1" Width="311px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label2" runat="server" Width="100px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Código IBGE</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtCodigoIBGE" onfocus="LimpaErro()" runat="server" Width="85px" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style11">
                                    <asp:Label ID="Label3" runat="server" Width="100px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Alíquota ISS</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <uc5:MOEDA ID="moeAliquotaISS" onfocus="LimpaErro()" runat="server" IndiceTab="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="Label4" runat="server" Width="100px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Código IPM</asp:Label>
                                    </td>
                                <td class="auto-style8">
                                    <uc4:INTEIRO7 ID="intCodigoIPM"  onfocus="LimpaErro()" runat="server" IndiceTab="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="Label5" runat="server" Width="100px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;UF/Estado</asp:Label>
                                    </td>
                                <td class="auto-style8">
                                    <asp:TextBox ID="txtUF"  onfocus="LimpaErro()" runat="server" Width="25px" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="1" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" TabIndex="1" />
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
    </form>
</body>
</html>
