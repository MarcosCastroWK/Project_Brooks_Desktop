<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotasFiscais.aspx.cs" Inherits="NotasFiscais" %>
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
                        <uc2:menu ID="menu" runat="server" Visible="false" />
                        
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Notas Fiscais" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:90%;height:120px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="80%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" OnSelectedIndexChanged="Grade_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mostrarsmall.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NumeroNF" HeaderText="NF nº" SortExpression="NumeroNF">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeCliente" HeaderText="Cliente" SortExpression="NomeCliente">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="400px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Código" SortExpression="CodigoCliente" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CNPJ_CPF" HeaderText="CNPJ/CPF" SortExpression="CNPJ_CPF" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataEmissao" HeaderText="Data Emissão" SortExpression="DataEmissao" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValorTotal" HeaderText="Valor Total" SortExpression="ValorTotal">
<ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cancelada" HeaderText="Situação" SortExpression="Cancelada">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroNotaFiscal" HeaderText="Seq" SortExpression="NumeroNotaFiscal" />
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
                    <td id="dadosForm" style="height:100%" valign="top">
                        <table style="width:600px">
                            <tr>
                                <td rowspan="2" style="vertical-align:top">

                                    <div id="Div1" style="width:360px;height:300px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                                        <asp:TextBox ID="txtBuscar" runat="server" Width="301px"></asp:TextBox>
                                        <asp:Button ID="btnBusca" runat="server" Text="Ok" OnClick="btnBusca_Click" />
                                        <asp:GridView ID="GradeClientes" runat="server" CellPadding="3" Width="340px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="GradeClientes_RowCommand" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" AllowSorting="True" Font-Bold="False" Height="66px" OnSelectedIndexChanged="GradeClientes_SelectedIndexChanged" >
                                            <AlternatingRowStyle BackColor="#CCCCCC" />

                                            <Columns>
                                                <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibnLancamentos" runat="server" ImageUrl="~/Images/mostrarsmall.png" ToolTip="Ver/Alterar" CommandArgument='<%# Container.DataItemIndex %>' />
                                                </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                                <asp:BoundField DataField="NomeFantasia" HeaderText="Clientes" SortExpression="NomeFantasia" />
                                                <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                                <ItemStyle Width="50px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </div>
                                </td>
                                <td colspan="4">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="titulo2" ID="Label19" runat="server" Font-Bold="False" Width="150px">&nbsp;Itens da Nota Fiscal</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td colspan="2">
                                                <asp:GridView ID="GraceItensNF" runat="server" CellPadding="3" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" Font-Bold="False" OnSelectedIndexChanged="GraceItensNF_SelectedIndexChanged">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibnMudarSernha0" runat="server" ImageUrl="~/Images/mostrarsmall.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NumeroNotaFiscal" HeaderText="Sequencial" SortExpression="NumeroNotaFiscal" />
                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao" >
                                                        <ItemStyle Width="400px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
                                                        <asp:BoundField DataField="PrecoUnitario" HeaderText="Valor Unitário" SortExpression="PrecoUnitario" />
                                                        <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" />
                                                        <asp:BoundField DataField="Valor" HeaderText="Valor Total" SortExpression="Valor" />
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

                                    <asp:HiddenField ID="hifCodigo" runat="server" />

                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    &nbsp;</td>
                                <td style="width:1%">
                                    &nbsp;</td>
                                <td>&nbsp;</td>
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
