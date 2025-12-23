<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DTR_Enviados.aspx.cs" Inherits="DTR_Enviados" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>
<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>

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
        .auto-style13
        {
            width: 411px;
            font-size: 9px;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function AbrePesquisaMotoristas() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('PesquisaMotoristas.aspx', '_blank', 'modalDialog');
    }
    function AbrePesquisaCaminhoes() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('PesquisaCaminhoes.aspx', '_blank', 'modalDialog');
    }
    function CheckTransbordo()
    {
        if (document.getElementById('chkTransbordo').checked) {
            document.getElementById('chkTijucas').checked = false;
        }
        else if (document.getElementById('chkTransbordo').checked == false) {
            document.getElementById('chkTijucas').checked = true;
        }
    }
    function CheckTijucas() {
        if (document.getElementById('chkTijucas').checked) {
            document.getElementById('chkTransbordo').checked = false;
        }
        else if (document.getElementById('chkTijucas').checked == false) {
            document.getElementById('chkTransbordo').checked = true;
        }
    }
    function Novo() {
        var r = confirm("Confirma?");
        if (r == true) {
            window.location = 'DTR_Enviados.aspx';
        }
    }
</script>
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
                        <uc2:menu ID="menu1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="DTR - Enviados" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:1500px;height:240px; overflow-y:scroll;border:ridge 5px;font-size:10pt;">
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="1480px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudar" runat="server" ImageUrl="~/Images/procura0.png" ToolTip="Ver relatório e MTR" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Retorna para Armazenados" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Imprimir">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnImprimir" runat="server" ImageUrl="~/Images/impressora.png" ToolTip="Imprimir/ver Relação e MTR novamente"  CommandArgument='<%# Container.DataItemIndex %>' OnClick="ibnImprimir_Click" />
                                        </ItemTemplate>
                                        <ItemStyle Width="42px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Sequencial" HeaderText="Sequencial" SortExpression="Sequencial">
                                    <HeaderStyle CssClass="padItemGrade" Width="50px" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataColeta" HeaderText="Data Coleta" SortExpression="DataColeta" DataFormatString=" {0: dd/MM/yyyy}">
                                    <HeaderStyle CssClass="padItemGrade" Width="60px" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeCliente" HeaderText="Cliente" SortExpression="NomeCliente">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Residuo" HeaderText="Resíduo" SortExpression="Residuo">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" DataFormatString=" {0:n2}">
                                    <ItemStyle CssClass="padItemGrade" Width="60px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="UN" SortExpression="Unidade" DataField="Unidade">
                                    <ItemStyle CssClass="padItemGrade" Width="14px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataSaida" HeaderText="Data Saída" SortExpression="DataSaida" DataFormatString="{0: dd/MM/yyyy}">
                                        <ItemStyle CssClass="padItemGrade" Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DestinoFinal" HeaderText="Destino final" SortExpression="DestinoFinal" />
                                    <asp:BoundField DataField="Imprimido" HeaderText="Imprimido" SortExpression="Imprimido">
                                        <ItemStyle CssClass="padItemGrade" Width="50px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Lote" HeaderText="Lote" SortExpression="Lote" />
                                    <asp:BoundField DataField="NumeroImpressao" HeaderText="Nº Impressão" SortExpression="NumeroImpressao">
                                        <ItemStyle CssClass="padItemGrade" Width="48px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeMotorista" HeaderText="Motorista" />
                                    <asp:BoundField DataField="Placas" HeaderText="Placas" />
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
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label2" runat="server" Font-Bold="False" Width="105px">&nbsp;Período de saída</asp:Label>
                                </td>
                                <td>
                                    <div style="width:270px;border:0">
                                        <uc6:DATA ID="txtDataInicial" runat="server" />
                                        <uc6:DATA ID="txtDataFinal" runat="server" />
                                    </div>
                                </td>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Ok" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="lblLote" runat="server" Font-Bold="False" Width="32px">&nbsp;Lote</asp:Label>
                                    <asp:Label CssClass="LetrasLabel" ID="lblLote0" runat="server" Font-Bold="False" Width="32px">&nbsp;Lote</asp:Label>
                                </td>
                                <td colspan="2">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAnterior" runat="server" OnClick="btnAnterior_Click" Text="Anterior" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnProximo" runat="server" OnClick="btnProximo_Click" Text="Proximo" />
                                            </td>
                                            <td>
                                                &nbsp;<asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                                            </td>
                                            <asp:HiddenField ID="hifNumeroLancamento" runat="server" />
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Button ID="btnConfirma" runat="server" Text="Confirma retorno?" Visible="false" OnClick="btnConfirma_Click" />
                                </td>
                                <td class="auto-style13">
                                    <div style="width:160px">
                                        <input id="inpCancelar" type="button" value="Voltar a tela inicial" onclick="Novo();" />
                                    </div>
                                </td>
                                <td class="LetrasTD">
                                    <asp:HiddenField ID="hifNumeroMTR" runat="server" />
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td class="auto-style12">
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td style="width:100%">
                                    <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px"/>
                                    <asp:HiddenField ID="hifCodigoResiduo" runat="server" />
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
