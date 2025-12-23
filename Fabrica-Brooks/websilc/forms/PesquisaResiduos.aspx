<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PesquisaResiduos.aspx.cs" Inherits="PesquisaResiduos" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1;" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
</head>
<script>
    function FechaJanelaGrupo()
    {
        window.opener.location.reload();
        window.close();
    }
</script>
<body class="body">
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td style="height:1%;">
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Consulta&nbsp;Grupo&nbsp;Resíduos" CssClass="titulo2" Font-Bold="True"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:100%;height:500px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="0" Width="98%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px"  AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnRowDataBound="Grade_RowDataBound" OnSorting="Grade_Sorting" OnRowCommand="Grade_RowCommand" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudar.png" ToolTip="Alterar" CommandArgument='<%# Container.DataItemIndex %>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição" SortExpression="DescricaoReduzida">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataCadastro" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Cadastro" SortExpression="DataCadastro" />
                                    <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="Ativo" >
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
                    <td valign="center" style="height:1%;">
                        <table>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <br />
                                    <asp:Label ID="lblErro" CssClass="LetrasLabel" runat="server" Text=""></asp:Label>
                                </td>
                                
                            </tr>
                        </table>
                </tr>
            </table>
        </div>
    </form>
</body>

</html>
