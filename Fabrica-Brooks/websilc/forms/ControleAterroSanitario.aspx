<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ControleAterroSanitario.aspx.cs" Inherits="ControleAterroSanitario" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>
<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>
<%@ Register src="CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc10" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <script src="geral.js" lang="javascript" type="text/javascript"></script>
    <script src="../Scripts/funcaoGeral.js"></script>
    <style type="text/css">
        .LetrasAlinhamentoEsquerda
        {
            font-family: Tahoma;
            font-size: 10px;
            text-align: right;
        }
        .auto-style1 {
            width: 382px;
        }
        </style>
</head>
<script src="~/Scripts/funcaoGeral.js"></script>
<script>
    function AbrePesquisaMotoristas() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('PesquisaMotoristas.aspx', '_blank', 'modalDialog');
    }
    function AbrePesquisaCaminhoes() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('PesquisaCaminhoes.aspx', '_blank', 'modalDialog');
    }
    function AbrePesquisaClientes() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('PesquisaClientes.aspx', '_blank', 'modalDialog');
    }
    function MostraCliente() {
        document.getElementById("<%=btnMostraCliente.ClientID%>").click();
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
    function CheckAmbos() {
        document.getElementById('chkTijucas').checked = false;
        document.getElementById('chkTransbordo').checked = false;
    }
</script>
<body>
     <form id="form1" runat="server">
        <div id="Operacoes">
            <table class="form2">
                <tr>
                    <td>
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Cadastro Descarga Peso Total" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:90%; height:400px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
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
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" DataFormatString=" {0: dd/MM/yyyy}">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Hora" HeaderText="Hora" SortExpression="Hora">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LocalAterro" HeaderText="Local Aterro" SortExpression="LocalAterro">
                                    <ItemStyle CssClass="padItemGrade" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroTicket" HeaderText="Ticket" SortExpression="NumeroTicket">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeMotorista" HeaderText="Nome  Motorista" SortExpression="NomeMotorista" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Caminhão" SortExpression="Modelo" />
                                    <asp:BoundField DataField="NumeroCaixa" HeaderText="Container" SortExpression="NumeroCaixa">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalPeso" HeaderText="Peso" SortExpression="TotalPeso" DataFormatString=" {0:n2}">
                                    <ItemStyle CssClass="padItemGrade" Width="70px" HorizontalAlign="Right"/>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Peso Individual ou Médio" ItemStyle-HorizontalAlign="Right"> 
                                        <ItemTemplate>
                                            <asp:Button ID="btnPesoIndividual" runat="server" Text='<%# Bind("PesoIndividual")%>' ToolTip="Lista de Pesos Individuais" CommandArgument='<%# Container.DataItemIndex %>' Width="100px" CssClass="LetrasAlinhamentoEsquerda" OnClick="btnPesoIndividual_Click" />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NomeCliente" HeaderText="Cliente" SortExpression="NomeCliente">
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
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label2" runat="server" Font-Bold="False" Width="110px">&nbsp;Período de mostragem</asp:Label>
                                </td>
                                <td>
                                    <div style="width:200px;">
                                        <uc6:DATA ID="txtDataInicial" runat="server" />
                                        <uc6:DATA ID="txtDataFinal" runat="server" />
                                    </div>
                                </td>
                                <td class="auto-style1">
                                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Ok" />
                                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" OnClientClick="DesabilitaOperacoes(this.id, 'txtDataInicial', 'txtDataFinal', 'Relatório Controle de Aterro');" />
                                </td>
                                <td rowspan="9" style="vertical-align:top;">
                                    <table>
                                        <tr>
                                            <td class="tituloFundoBranco">
                                                <asp:Label ID="lblTotalPeso" runat="server" Text="Total peso:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label CssClass="LetrasLabel" ID="lblTituloTicketPeso" runat="server" Font-Bold="False" Width="600px"></asp:Label>
                                                <asp:GridView ID="GradeTicket" runat="server" CellPadding="3" Width="650px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" AllowSorting="True" Font-Bold="False" OnRowDataBound="GradeTicket_RowDataBound">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:BoundField DataField="xCodigoCliente" HeaderText="Código">
                                                        <HeaderStyle CssClass="padItemGrade" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NomeFantasia" HeaderText="Cliente">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Quantidade" HeaderText="Peso" DataFormatString=" {0:n2}">
                                                        <ItemStyle CssClass="padItemGrade" Width="70px" HorizontalAlign="Right"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Franquia" HeaderText="Peso Individual" DataFormatString=" {0:n2}">
                                                        <ItemStyle CssClass="padItemGrade" Width="70px" HorizontalAlign="Right"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumeroCaixa" HeaderText="Container">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DataDescarga" HeaderText="Data Descarga" DataFormatString=" {0: dd/MM/yyyy}">
                                                        <HeaderStyle CssClass="padItemGrade" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
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
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label7" runat="server" Font-Bold="False" Width="40px">&nbsp;Cliente:</asp:Label>
                                    &nbsp;<asp:TextBox CssClass="LetrasLabel" ID="txtCodigoCliente" runat="server" Font-Bold="False" Width="40px" onblur="MostraCliente();"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="lblNomeCliente" runat="server" Font-Bold="False"></asp:Label>
                                </td>
                                <td>
                                    <img id="imgProcuraCliente" alt="x" src="../Images/procura.png" style="cursor: pointer;" onclick="AbrePesquisaClientes();"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style ="border: 0;">
                                    <table style="padding:0; word-spacing:0;">
                                        <tr>
                                            <td style="height:22px;">
                                                <asp:Label CssClass="LetrasLabel" ID="Label5" runat="server" Font-Bold="False" Width="80px">Local Aterro</asp:Label>
                                            </td>
                                            <td style="height:22px;">
                                                <asp:CheckBox ID="chkTransbordo" runat="server" CssClass="LetrasLabel" Text="Transbordo - 2" onclick="CheckTransbordo();" />
                                            </td>
                                            <td style="height:22px;">
                                                <asp:CheckBox ID="chkTijucas" runat="server" CssClass="LetrasLabel" Text="Tijucas - 13" onclick="CheckTijucas();" />
                                            </td>
                                            <td style="height:12px;">
                                                <input id="chkAmbos" type="button" class="LetrasLabel" value="Ambos" onclick="CheckAmbos();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD" style="height:36px;">
                                    <asp:Label CssClass="LetrasLabel" ID="Label3" runat="server" Font-Bold="False" Width="80px">&nbsp;Data</asp:Label>
                                </td>
                                <td>
                                    <uc6:DATA ID="txtData" runat="server" IndiceTab="0" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label4" runat="server" Font-Bold="False" Width="80px">&nbsp;Hora</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHora" runat="server" Width="62px" onblur="CompletaHHMMss(this.id);" onkeypress="return isNumberKeyHora(event);" style="margin-bottom: 6px"></asp:TextBox>
                                </td>
                                <td class="auto-style1">
                                    <table>
                                        <tr>
                                            <td><uc7:MOTORISTA ID="ctlMotorista" runat="server" IndiceTab="2" /></td>
                                            <td><img id="imagem" alt="x" src="../Images/procura.png" style="cursor:pointer;" onclick="AbrePesquisaMotoristas();"/></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label CssClass="LetrasLabel" ID="Label6" runat="server" Font-Bold="False" Width="100px">&nbsp;Ticket</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTicket" runat="server" TabIndex="0" Width="62px" MaxLength="8"></asp:TextBox>
                                </td>
                                <td class="auto-style1">
                                    <table>
                                        <tr>
                                            <td>
                                                <uc8:CAMINHAO ID="ctlCaminhao" runat="server" IndiceTab="2" />
                                            </td>
                                            <td><img id="Img1" alt="x" src="../Images/procura.png" style="cursor:pointer;" onclick="AbrePesquisaCaminhoes();"/></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False" Width="100px">&nbsp;Peso Total</asp:Label>
                                </td>
                                <td>
                                    <uc5:MOEDA ID="moePesoTotal" runat="server" IndiceTab="0" />
                                </td>
                                <td class="auto-style1">
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td>
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="0" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" TabIndex="0" />
                                    </div>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:Button ID="btnProcurar" TabIndex="10" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px" OnClick="btnProcurar_Click" />
                                    <asp:Button ID="btnMostraCliente" TabIndex="10" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px" OnClick="btnMostraCliente_Click" />
                                </td>
                                <td style="width:32%">
                                    &nbsp;</td>
                                <td>
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
