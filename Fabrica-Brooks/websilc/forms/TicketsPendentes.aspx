<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketsPendentes.aspx.cs" Inherits="TicketsPendentes" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            height: 13px;
        }
        .auto-style8 {
            font-size: 9px;
        }
        .auto-style9 {
            width: 200px;
        }
    </style>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function AbrePesquisaCaminhoes() {
            var navegador = navigator.appName.toLowerCase();
            if (navegador.indexOf("internet") == -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                window.open('../forms/PesquisaCaminhoes.aspx');
            }
            else if (navegador.indexOf("internet") > -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                location = '../forms/PesquisaCaminhoes.aspx?explorer=../forms/TicketsPendentes.aspx';
            }
        }
    </script>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script src="../Scripts/funcaoGeral.js"></script>
<script>
    function ImprimirRelTickets() {
        try {
            document.getElementById('tdmenu').style.visibility = "hidden";
            document.getElementById('tableBotoes').style.visibility = "hidden";
            document.getElementById('contentTopRightDiv').style = "height: 100%";
            window.print();
        }
        finally {
            document.getElementById('tdmenu').style.visibility = "visible";
            document.getElementById('tableBotoes').style.visibility = "visible";
        }
    }
</script>
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
                    <td id="tdmenu" class="Menu">
                        <uc2:menu ID="menu1" runat="server" Visible="false" />
                        
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Tickets Pendentes" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:90%;height:420px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" PageSize="50" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar2.png" Enabled="false" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir2.png" Enabled="false" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NumeroLancamento" HeaderText="Nº Lançamento" SortExpression="NumeroLancamento">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Deposito" HeaderText="Destino final" SortExpression="Deposito" />
                                    <asp:BoundField DataField="DataRetirada" HeaderText="Data" SortExpression="DataRetirada" DataFormatString="{0:dd/MM/yy}">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Caminhao" HeaderText="Caminhão" SortExpression="Caminhao" />
                                    <asp:BoundField DataField="Motorista" HeaderText="Motorista" SortExpression="Motorista" />
                                    <asp:BoundField DataField="NumeroCaixa" HeaderText="Container" SortExpression="NumeroCaixa">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Observacao" HeaderText="Observação" SortExpression="Observacao">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Código" SortExpression="CodigoCliente" />
                                    <asp:BoundField DataField="NomeFantasia" HeaderText="Cliente" SortExpression="NomeFantasia">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="260px" />
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
                    <td id="dadosForm" style="height:100%;" valign="top">
                        <table id="tableBotoes" style="width:600px">
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label8" runat="server" Font-Bold="False" Width="160px">&nbsp;Distribuição ticket(s) pendente(s)</asp:Label>
                                </td>
                                <td colspan="3">
                                    <table style="border-spacing: 0;">
                                        <tr>
                                            <td>
                                                <uc7:CAMINHAO ID="CAMINHAO1" runat="server" />
                                            </td>
                                            <td>
                                                <img id="imagem2" alt="x" onclick="AbrePesquisaCaminhoes()" src="../Images/procura2.png" style="cursor:pointer;" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="16px" OnClick="btnProcurar_Click" />            
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label6" runat="server" Font-Bold="False" Width="122px">&nbsp;Aplicar Destino Final</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDestinoFinal" runat="server" Width="205px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Data</asp:Label>
                                </td>
                                <td>
                                    <uc6:DATA ID="datDataRetirada" runat="server" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Hora</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHora" runat="server" MaxLength="8" onblur="CompletaHHMMss(this.id);" onkeypress="return isNumberKeyHora(event);"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label5" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Número Ticket</asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="intNumeroTicket" runat="server" MaxLength="10"></asp:TextBox>
                                </td>
                                <td class="auto-style2">
                                </td>
                                <td class="auto-style2">
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <div>
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Aplicar"  Text="Aplicar" ID="Aplicar" TabIndex="18" OnClick="Aplicar_Click" />
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar"   Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Cancelar" Text="Novo" ID="btnCancelar" OnClick="btnCancelar_Click" />
                                        <input id="btnImprimir" type="button" value="Imprimir" onclick="ImprimirRelTickets();" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
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
