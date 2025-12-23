<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DTR_Relatorio.aspx.cs" Inherits="DTR_Relatorio" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>

<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>

<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style11
        {
            height: 1%;
        }
        .auto-style12
        {
            width: 411px;
        }
        </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td class="auto-style11">
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
                        <asp:Label ID="lblTitulo" runat="server" Text="DTR - Armazenados" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:400px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="98%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudar" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Sequencial" HeaderText="Sequencial" SortExpression="Sequencial">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Lote" HeaderText="Lote" SortExpression="Lote" />
                                    <asp:BoundField DataField="DataColeta" HeaderText="Data Coleta" SortExpression="DataColeta" DataFormatString=" {0: dd/MM/yyyy}">
                                    <HeaderStyle CssClass="padItemGrade" Width="120"/>
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeCliente" HeaderText="Cliente" SortExpression="NomeCliente">
                                    <ItemStyle CssClass="padItemGrade" Width="350px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Residuo" HeaderText="Resíduo" SortExpression="Residuo">
                                    <ItemStyle CssClass="padItemGrade" Width="300"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" DataFormatString=" {0:n2}">
                                    <ItemStyle CssClass="padItemGrade" Width="70px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="UN" SortExpression="Unidade" DataField="Unidade">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataSaida" HeaderText="Data Saída" SortExpression="DataSaida" DataFormatString=" {0: dd/MM/yyyy}" />
                                    <asp:BoundField DataField="DestinoFinal" HeaderText="Destino final" SortExpression="DestinoFinal" />
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
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table>
                            <tr>
                                <td style="width:1%">
                                    &nbsp;</td>
                                <td class="auto-style12">
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td style="width:100%">
                                    <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    &nbsp;</td>
                                <td class="auto-style12">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                    <asp:HiddenField ID="hifNumeroLancamento" runat="server" />
                                    <asp:HiddenField ID="hifNumeroMTR" runat="server" />
                                    <asp:HiddenField ID="hifCodigoResiduo" runat="server" />
                                </td>
                                <td style="width:100%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
