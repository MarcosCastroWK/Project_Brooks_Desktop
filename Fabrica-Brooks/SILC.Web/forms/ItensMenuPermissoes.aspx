<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItensMenuPermissoes.aspx.cs" Inherits="SILC.Web.forms.ItensMenuPermissoes" %>

<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>

<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc3" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/forms/table.css" type="text/css" rel="stylesheet" />
    <link href="~/forms/aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
         .CabecalhoMenu
         {
             height: 50px;
             width:100%;
         }
        </style>
</head>
 <body>
     <form id="form1" runat="server">
        <div style="position:relative; top:0px;">
            <table class="form2">
                <tr>
                    <td class="CabecalhoMenu">
                        <uc1:cabecalho ID="cabecalho1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="height:1%;">
                        <uc2:menu ID="menu1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 0px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height:1%;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Permissões de acesso do Menu Principal" CssClass="titulo2"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    &nbsp;<asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                                </td>
                                <td>
                                    &nbsp;<asp:Button ID="btnLiberarTodas" runat="server" Text="Liberar todas permissões-excluir" OnClick="btnLiberarTodas_Click" Width="216px" />
                                </td>
                                <td>
                                    &nbsp;<asp:Button ID="btnTirarTodas" runat="server" Text="Tirar todas permissões -excluir" Width="205px" OnClick="btnTirarTodas_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="2">
                        <table>
                            <tr>
                                <td valign="top">
                                    <asp:GridView ID="GradeUsuarios" runat="server" CellPadding="0" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" Font-Bold="False" Width="254px" OnRowCommand="GradeUsuarios_RowCommand">
                                        <AlternatingRowStyle BackColor="AliceBlue" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibnMudar" runat="server" ImageUrl="~/Images/selecionar.png" ToolTip="Ver/Alterar" CommandArgument='<%# Container.DisplayIndex %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                            <HeaderStyle CssClass="padItemGrade" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nome" HeaderText="Nome do Usuário" SortExpression="Nome">
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                            <ItemStyle CssClass="padItemGrade" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="280px" />
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
                                </td>
                                <td valign="top">
                                    <asp:GridView ID="Grade" runat="server" CellPadding="2" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="false" ForeColor="Black" GridLines="Vertical" Font-Bold="False" OnRowDataBound="Grade_RowDataBound" >
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cód.Item">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCodigoItemMenu" runat="server" Enabled="false" CssClass="LetrasLabel" BackColor="#ffffcc" BorderStyle="None" MaxLength="6" Text='<%# Bind("CodigoItemMenu") %>' Width="50px" style="text-align:right"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descrição item menu">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItem" runat="server" Enabled="false" Width="400px" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Text='<%# Bind("Item") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" Width="400px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Consultar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkConsultar" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="49px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Incluir">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIncluir" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="49px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAlterar" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="49px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>                            
                                            <asp:TemplateField HeaderText="Excluir">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkExcluir" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="49px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>                            
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>                
            </table>
        </div>
        <asp:HiddenField ID="hifCodigoUsuario" runat="server" />
    </form>
</body>
</html>