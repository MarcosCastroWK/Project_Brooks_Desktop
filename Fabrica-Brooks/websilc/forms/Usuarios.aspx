<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Account_Entrar" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2
        {
            height: 100%;
        }
        .auto-style3
        {
            width: 447px;
            font-size: 9px;
            height: 1%;
        }
        .Menu
        {
            font-size:11px;
            height:1%;
            width:100%;
        }
        .auto-style6
        {
            width: 90px;
            font-size: 10px;
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
                    <td colspan="2" class="LetrasTD" valign="top" style="height:1%">
                        <uc1:cabecalho ID="cabecalho1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" class="Menu">
                        <uc2:menu ID="menu1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" colspan="2" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&amp;nbsp;Cadastro&amp;nbsp;de&amp;nbsp;usuários" Font-Bold="True" CssClass="titulo2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" valign="top"  style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="40%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" AutoGenerateColumns="False" ClientIDMode="AutoID" Height="74px" OnRowCommand="Grade_RowCommand" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" OnRowDataBound="Grade_RowDataBound">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudar.png" ToolTip="Alterar senha" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" ReadOnly="True" SortExpression="Codigo" >
                                    <ItemStyle HorizontalAlign="Right" Width="30px" CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" ReadOnly="True" SortExpression="Nome" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" />
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
                    <td id="dadosForm" valign="top" class="LetrasTD" style="height:100%;">
                        <table style="height:1%;vertical-align:top">
                            <tr>
                                <td class="LetrasTD">
                                    &nbsp;<asp:Label ID="Label1" runat="server" AssociatedControlID="UserName" Font-Bold="False" CssClass="LetrasLabel">Nome&nbsp;do&nbsp;usuário</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label2" CssClass="LetrasLabel" runat="server" AssociatedControlID="Password" Font-Bold="False">&nbsp;Senha</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" onfocus="LimpaErro()" />
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" AssociatedControlID="UserName" Font-Bold="False">&nbsp;Confirma&nbsp;Senha</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="ConfirmaSenha" onfocus="LimpaErro()" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="lblNovaSenha" CssClass="LetrasLabel" runat="server" AssociatedControlID="UserName" Visible="False" Font-Bold="False">&nbsp;Nova&nbsp;Senha</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="NovaSenha" onfocus="LimpaErro()" runat="server" TextMode="Password" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="lblConfirmaNovaSenha" CssClass="LetrasLabel" runat="server" AssociatedControlID="UserName" Visible="False" Font-Bold="False">&nbsp;Confirma&nbsp;Nova&nbsp;Senha</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="ConfirmaNovaSenha" onfocus="LimpaErro()" runat="server" TextMode="Password" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <div style="width:200px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD" ></td>
                                <td class="LetrasTD">
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
