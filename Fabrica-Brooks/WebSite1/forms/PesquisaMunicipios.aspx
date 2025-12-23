<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PesquisaMunicipios.aspx.cs" Inherits="PesquisaMunicipios" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 1%;
        }
    </style>
</head>
<script>
    function FechaJanela()
    {
        window.opener.location.reload();
        window.close();
    }
    function VoltarHomeOrigem(pHomeOrigem)
    {
        location = pHomeOrigem;
    }
</script>
<script src="../Scripts/funcaoGeral.js"></script>
<body class="body">
     <form id="form1" runat="server">
        <div id="Operacoes">
            <table class="form2">
                <tr>
                    <td class="auto-style1">
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Consulta&nbsp;Municípios" CssClass="titulo2" Font-Bold="True"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td id="dadosCab2" valign="top" style="height:1%; font-size:9pt;">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblFiltro" CssClass="LetrasLabel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Procurar:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                    <asp:ListItem Value="Nome"></asp:ListItem>
                                    <asp:ListItem Value="Codigo">Código</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFiltro" runat="server" Width="280px"></asp:TextBox>
                            </td>
                            <td class="auto-style1">
                                <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Pesquisa Municípios');" />
                            </td>
                        </tr>
                    </table>

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
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" Width="300px" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="UF" HeaderText="UF">
                                    <ItemStyle Width="60px" />
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
                                <td>&nbsp;</td>
                                <td>
                                    <br />
                                    <asp:Label ID="lblErro" CssClass="LetrasLabel" runat="server" Text=""></asp:Label>
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
