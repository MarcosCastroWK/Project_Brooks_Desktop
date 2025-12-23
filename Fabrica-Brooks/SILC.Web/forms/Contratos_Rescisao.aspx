<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contratos_Rescisao.aspx.cs" Inherits="SILC.Web.forms.Contratos_Rescisao" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc7" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc8" %>
<%@ Register Src="~/forms/INTEIRO7.ascx" TagPrefix="uc1" TagName="INTEIRO7" %>
<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function NovoRescisao() {
        var r = confirm("Confirma?");
        if (r == true) {
            window.location = 'Contratos_Rescisao.aspx';
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
                    <td class="Menu">
                        <uc2:menu ID="menu1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Rescisão&amp;nbsp;de&amp;nbsp;Contratos" Font-Bold="True" Font-Names="Arial"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <asp:GridView ID="Grade" runat="server" CellPadding="0" Width="1200px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="Grade_PageIndexChanging" >
                            <AlternatingRowStyle BackColor="AliceBlue" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibnMudar" runat="server" ImageUrl="~/Images/selecionar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Seq" SortExpression="Codigo">
                                <HeaderStyle CssClass="padItemGrade" />
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="36px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CodigoCliente" HeaderText="Cliente" SortExpression="CodigoCliente" >
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeFantasia" HeaderText="Nome Fantasia" SortExpression="NomeFantasia">
                                <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                <ItemStyle CssClass="padItemGrade" />
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="280px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social" SortExpression="Nome">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="padItemGrade" Width="350px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataRecisao" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Recisão" SortExpression="DataRecisao">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SituacaoRecisao" HeaderText="Situação">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MotivoRescisao" HeaderText="Motivo">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="200px" />
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
                <tr>
                    <td valign="center" style="height:1%;">
                        <table>
                            <tr>
                                <td><asp:Label ID="lblFiltro" CssClass="LetrasLabel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Procurar:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFiltro" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                                        <asp:ListItem>NomeFantasia</asp:ListItem>
                                        <asp:ListItem Value="Nome"></asp:ListItem>
                                        <asp:ListItem>CNPJ_CPF</asp:ListItem>
                                        <asp:ListItem>Código Cliente</asp:ListItem>
                                        <asp:ListItem>Cancelados</asp:ListItem>
                                        <asp:ListItem>Não Cancelados</asp:ListItem>
                                        <asp:ListItem>Código Contrato</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFiltro" runat="server" Width="400px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnContratos" runat="server" Text="Total contratos" Width="119px" BorderStyle="None"/>
                                </td>
                                <td style="width:84px; text-align: right; font-size:12px;"><asp:Label ID="lblTotal" runat="server" Text="0,00"></asp:Label></td>
                            </tr>
                        </table>
                     </td>   
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:90%">
                        <table>
                            <tr><td colspan="4" style="border-top:1px solid;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodigoCliente" CssClass="LetrasLabel" runat="server" Width="84px" Font-Bold="False" Height="16px" >Código Cliente</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCNPJ_CPF" CssClass="LetrasLabel" runat="server" Font-Bold="False">CNPJ/CPF:</asp:Label>                                    
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNome" CssClass="LetrasLabel" runat="server" Width="104px" Font-Bold="False" >&nbsp;Nome/Razão Social</asp:Label>
                                            </td>
                                            <td>                                    
                                                <asp:Label ID="lblNomeFantasia" CssClass="LetrasLabel" runat="server" Width="80px" Font-Bold="False" >&nbsp;Nome Fantasia</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intCodigoCliente" IndiceTab="1" Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCNPJ_CPF" runat="server" Enabled="False" onfocus="LimpaErro()" TabIndex="1" Width="140px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNome" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>                                    
                                                <asp:TextBox ID="txtNomeFantasia" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Width="84px" Font-Bold="False" CssClass="LetrasLabel">Data Rescisão</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" CssClass="LetrasLabel" runat="server" Width="71px" Font-Bold="False" Visible="False">Data Registro</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Width="99px" CssClass="LetrasLabel">Documento Recisão</asp:Label>
                                                </td>
                                            <td>
                                                <asp:Label ID="lblMotivoRecisao" CssClass="LetrasLabel" runat="server" Width="120px" Font-Bold="False" >Motivo/Descrição Recisão</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc6:DATA ID="datDataRescisao" runat="server" />
                                            </td>
                                            <td>
                                                <uc6:DATA ID="datDataRegistro" runat="server" Width="125px" Visible="False"/>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDocumentoRecisao" runat="server" Width="300px">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>TERMO RESCISÃO ASSINADO</asp:ListItem>
                                                    <asp:ListItem>CANCELAMENTO VERBAL</asp:ListItem>
                                                    <asp:ListItem>CARTA DE RESCISÃO</asp:ListItem>
                                                    <asp:ListItem>AGUARDANDO TERMO ASSINADO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMotivoRecisao" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <div style="width:300px;text-align:right;">
                                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="width:300px;text-align:left;" >
                                                    <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Salvar" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                                    <input id="btnCancelar" type="button" value="Voltar a tela inicial" onclick="NovoRescisao();" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr><td colspan="4" style="border-top:1px solid;">
                                                <asp:HiddenField ID="hifCodigo" runat="server" />
                                            </td></tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
