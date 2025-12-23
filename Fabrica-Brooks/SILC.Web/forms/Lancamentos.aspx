<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lancamentos.aspx.cs" Inherits="SILC.Web.forms.Lancamentos" %>
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
        .auto-style2 {
            width: 1%;
            height: 16px;
        }
        .auto-style3 {
            height: 16px;
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
                        <uc2:menu ID="menu" runat="server" Visible="false" />
                        
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Locações" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" OnSelectedIndexChanged="Grade_SelectedIndexChanged" >
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
                                    <asp:BoundField DataField="NumeroLancamento" HeaderText="Nº Lançamento" SortExpression="NumeroLancamento">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeCliente" HeaderText="Cliente" SortExpression="NomeCliente">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="400px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Código" SortExpression="CodigoCliente" />
                                    <asp:BoundField DataField="NumeroCaixa" HeaderText="Container" SortExpression="NumeroCaixa" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataColocacao" HeaderText="Data Colocação" SortExpression="DataColocacao" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataRetirada" HeaderText="Data Retirada" SortExpression="DataRetirada">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NuLancColocacao" HeaderText="Nº Troca" SortExpression="NuLancColocacao">
                                    <HeaderStyle CssClass="padItemGrade"></HeaderStyle>
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroMTR" HeaderText="Nº MTR" SortExpression="NumeroMTR">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DescricaoResiduo" HeaderText="Descrição Resíduo" SortExpression="DescricaoResiduo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="400px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unidade" HeaderText="Un" SortExpression="Unidade" />
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
                    <td id="dadosForm" style="height:100%">
                        <table style="width:600px">
                            <tr>
                                <td rowspan="13" style="vertical-align:top">

                                    <div id="Div1" style="width:360px;height:300px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                                        <asp:TextBox ID="txtBuscar" runat="server" Width="301px"></asp:TextBox>
                                        <asp:Button ID="btnBusca" runat="server" Text="Ok" OnClick="btnBusca_Click" />
                                        <asp:GridView ID="GradeClientes" runat="server" CellPadding="3" Width="340px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="GradeClientes_RowCommand" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" AllowSorting="True" Font-Bold="False" Height="66px" >
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
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label16" runat="server" Width="100px" Font-Bold="False">Data Lançamento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="datData" runat="server" Width="125px" OnDataBinding="datDataCadastro_DataBinding" TextMode="Date"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblNovaSenha" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px" >&nbsp;Nº Lançamento</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNrLancamento" runat="server" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False" Width="80px">&nbsp;Código Cliente</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intCodigoCliente" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Container</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContainer" runat="server" TabIndex="1" Width="60px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" CssClass="LetrasLabel" runat="server" Font-Bold="False">&nbsp;Data Colocação</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="datDataColocacao" runat="server" Width="125px" OnDataBinding="datDataCadastro_DataBinding" TextMode="Date"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Data Retirada</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="datDataRetirada" runat="server" Width="125px" OnDataBinding="datDataCadastro_DataBinding" TextMode="Date"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label18" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Nº Troca</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNrLancTroca" runat="server" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <div style="width:200px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    </td>
                                <td class="auto-style2">
                                    </td>
                                <td class="auto-style3"></td>
                                <td class="auto-style3"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="LetrasLabel" ID="Label19" runat="server" Font-Bold="False" Width="110px">&nbsp;Locações a partir de</asp:Label>
                                                <asp:TextBox ID="txtDataInicial" runat="server" TabIndex="1" Width="125px" TextMode="Date"></asp:TextBox>
                                                <asp:Button ID="btnOk" runat="server" Text="Ok" />
                                            </td>
                                        </tr>
                                        <tr>
                                           <td colspan="2">
                                                <asp:GridView ID="GradeMTR" runat="server" CellPadding="3" Width="600px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" OnSelectedIndexChanged="Grade_SelectedIndexChanged" >
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibnMudarSernha0" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibnExcluir0" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NumeroLancamento" HeaderText="Nº Lançamento" SortExpression="NumeroLancamento">
                                                        <HeaderStyle CssClass="padItemGrade" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumeroMTR" HeaderText="Nº MTR" SortExpression="NumeroMTR" >
                                                        <HeaderStyle CssClass="padItemGrade" />
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodigoResiduo" HeaderText="Código Resíduo" SortExpression="CodigoResiduo" />
                                                        <asp:BoundField DataField="DescricaoResiduo" HeaderText="Descrição Resíduo" SortExpression="DescricaoResiduo">
                                                        <HeaderStyle CssClass="padItemGrade" Width="400px"/>
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Unidade" HeaderText="Un" SortExpression="Unidade" />
                                                        <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" >
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorUnitario" DataFormatString="{0:n2}" HeaderText="Valor Unitário" SortExpression="ValorUnitario">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorTotal" DataFormatString="{0:n2}" HeaderText="Valor Total" SortExpression="ValorTotal">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Observacao" HeaderText="Observação" SortExpression="Observacao" />
                                                        <asp:BoundField DataField="Deposito" HeaderText="Destino" SortExpression="Deposito" />
                                                        <asp:BoundField DataField="DataDescarga" HeaderText="Data Descarga" SortExpression="DataDescarga"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Hora Descarga" SortExpression="HoraDescarga" />
                                                        <asp:BoundField DataField="Ticket" HeaderText="Ticket" SortExpression="Ticket" />
                                                        <asp:BoundField DataField="NumeroMTRFatima" HeaderText="Número MTR-e" SortExpression="NumeroMTRFatima" />
                                                        <asp:BoundField DataField="Motivo" HeaderText="Motivo MTR-e" SortExpression="Motivo" />
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
