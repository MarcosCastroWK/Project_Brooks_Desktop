<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MTRsPendentes.aspx.cs" Inherits="MTRsPendentes" %>
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
            height: 13px;
        }
        .auto-style3 {
            height: 26px;
        }
        .auto-style4 {
            height: 31px;
        }
        .auto-style5 {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
    </style>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function Imprime() {
            try {
                document.getElementById('contentTopRightDiv').style.height = '100%';
                document.getElementById('contentTopRightDiv').style.border = '0px';
                document.getElementById('contentTopRightDiv').style.overflowY = 'hidden';
                document.getElementById('tableDigitacao').style.visibility = 'hidden';
                document.getElementById('tablePrincipal').style.border = '0px';
                document.getElementById('menu1_Menu1').style.visibility = 'hidden';
                window.print();
            }
            finally {
                document.getElementById('tableDigitacao').style.visibility = 'visible';
                document.getElementById('menu1_Menu1').style.visibility = 'visible';
                document.getElementById('contentTopRightDiv').style.height = '200px';
                document.getElementById('contentTopRightDiv').style.overflowY = 'scroll';
                document.getElementById('contentTopRightDiv').style.border = 'ridge 5px';
                document.getElementById('Grade').style.height = '';
            }
        }
    </script>
</head>
    <body>
     <form id="form1" runat="server">
        <div>
            <table id="tablePrincipal" class="form2">
                <tr>
                    <td style="height:1%;">
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;MTRs Pendentes" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:90%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar2.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir2.png" ToolTip="Excluir" Enabled="false" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NumeroLancamento" HeaderText="Nº Lançamento" SortExpression="NumeroLancamento">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeFantasia" HeaderText="Cliente" SortExpression="NomeFantasia">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="260px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Código" SortExpression="CodigoCliente" />
                                    <asp:BoundField DataField="DataRetirada" HeaderText="Data Retirada" SortExpression="DataRetirada" DataFormatString="{0:dd/MM/yy}">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumeroMTRFatima" HeaderText="Nº MTR e" SortExpression="NumeroMTRFatima">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoResiduo" HeaderText="Código Resíduo" SortExpression="CodigoResiduo" />
                                    <asp:BoundField DataField="DescricaoResiduo" HeaderText="Descrição Resíduo" SortExpression="DescricaoResiduo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="260px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Motivo" HeaderText="Motivo" SortExpression="Motivo" />
                                    <asp:BoundField DataField="NumeroMTR" HeaderText="Número MTR" SortExpression="NumeroMTR" />
                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                                    <asp:BoundField DataField="Motorista" HeaderText="Motorista" />
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
                    <td id="dadosForm" style="height:100%;" valign="top">
                        <table id="tableDigitacao" style="width:600px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblNovaSenha" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px" >&nbsp;Nº Lançamento</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNrLancamento" runat="server" Enabled="False" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <input id="btnImprimir" onclick="Imprime();" type="button" value="Imprimir" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False" Width="80px">&nbsp;Código Cliente</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intCodigoCliente" runat="server" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label8" runat="server" Font-Bold="False">&nbsp;Cliente:</asp:Label>
                                </td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="lblNomeCliente" runat="server" Font-Bold="False" Width="400px"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label CssClass="LetrasLabel" ID="Label6" runat="server" Font-Bold="False" Width="80px">&nbsp;Código Resíduo</asp:Label>
                                </td>
                                <td class="auto-style4">
                                    <uc4:INTEIRO7 ID="intCodigoResiduo" runat="server" Enabled="false" />
                                </td>
                                <td class="auto-style4">
                                    <asp:Label CssClass="LetrasLabel" ID="Label7" runat="server" Font-Bold="False">&nbsp;CNPJ/CPF</asp:Label>
                                    </td>
                                <td class="auto-style4">
                                    <asp:TextBox ID="txtCNPJ_CPF" runat="server" TextMode="SingleLine" TabIndex="1" Width="120px" MaxLength="14" Enabled="False"></asp:TextBox>
                                </td>
                                <td class="auto-style4">
                                    </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Data Retirada</asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <uc6:DATA ID="datDataRetirada" runat="server" />
                                </td>
                                <td class="auto-style3">
                                                    <asp:Label ID="lblSenhaAcesso" runat="server" Width="70px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Senha Acesso</asp:Label>
                                </td>
                                <td class="auto-style3">
                                                    <asp:TextBox ID="txtSenhaAcesso" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="159px" Enabled="False"></asp:TextBox>
                                </td>
                                <td class="auto-style3">
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label5" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Número MTR</asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="intNumeroMTR" runat="server" MaxLength="10" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="auto-style2">
                                    &nbsp;</td>
                                <td class="auto-style2">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="http://mtr.ima.sc.gov.br/" Font-Size="11pt">http://mtr.ima.sc.gov.br/</asp:HyperLink>
                                    </td>
                                <td class="auto-style2">
                                    </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">
                                    <asp:Label ID="Label4" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Número MTR-e</asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="intNumeroMTRe" runat="server" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="Label9" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="90px">&nbsp;Motorista:</asp:Label>
                                </td>
                                <td class="auto-style3">
                                                    <asp:TextBox ID="txtMotorista" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="159px" Enabled="False"></asp:TextBox>
                                </td>
                                <td class="auto-style3">
                                    </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <div style="width:200px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td colspan="2">
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
