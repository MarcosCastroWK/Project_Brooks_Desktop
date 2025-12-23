<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Caminhoes.aspx.cs" Inherits="Caminhoes" %>
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
        .auto-style2
        {
            height: 28px;
        }
    </style>    
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function TiraCheckProprio()
    {
        if (document.getElementById('chkProprio').checked)
        {
            document.getElementById('chkTerceiro').checked = false;
        }
        else if (document.getElementById('chkProprio').checked == false)
        {
            document.getElementById('chkTerceiro').checked = true;
        }
    }
    function TiraCheckTerceiro() {
        if (document.getElementById('chkTerceiro').checked) {
            document.getElementById('chkProprio').checked = false;
        }
        else if (document.getElementById('chkTerceiro').checked == false) {
            document.getElementById('chkProprio').checked = true;
        }
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td colspan="2" style="height:1%;width:100%;">
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>                                                                                                     
                <tr>
                    <td colspan="2" class="Menu">
                        <uc2:menu ID="menu1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" colspan="2" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label Font-Bold="True" ID="lblTitulo" Font-Names="Arial" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Caminhões" CssClass="titulo2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:406px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="80%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" AllowSorting="True" OnSorting="Grade_Sorting" ForeColor="Black" GridLines="Vertical" Font-Bold="False">
                                <AlternatingRowStyle BackColor="AliceBlue" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudar" runat="server" ImageUrl="~/Images/selecionar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DisplayIndex %>' />
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
                                    <asp:BoundField DataField="Modelo" HeaderText="Descrição" SortExpression="Modelo">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataCadastro" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Cadastro" SortExpression="DataCadastro" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Inativo" HeaderText="Inativo" SortExpression="Inativo" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoVeiculo" HeaderText="Tipo Veículo" SortExpression="TipoVeiculo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Placas" HeaderText="Placas" SortExpression="Placas">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Kilometragem" HeaderText="Km Inicial">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataAquisicao" HeaderText="Data Aquisição" DataFormatString=" {0: dd/MM/yyyy}" SortExpression="DataAquisicao">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VctoIPVA" HeaderText="Vencimento IPVA" DataFormatString=" {0: dd/MM/yyyy}">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VctoLicenciamento" HeaderText="Vcto Licenciamento" DataFormatString=" {0: dd/MM/yyyy}">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dtVctoSegr" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Seguro Obrigatório">
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VencimentoSeguroFrota" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Seguro Frota">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EhProprio" HeaderText="É Próprio" SortExpression="EhProprio">
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
                        </div>
                    </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table style="width: 894px">
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="False" CssClass="LetrasLabel" ID="Label16" runat="server" Width="100px">Data do cadastro</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <uc6:DATA ID="datDataCadastro" runat="server" IndiceTab="0" />
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label5" runat="server">Cor</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtCor" onfocus="LimpaErro()" runat="server" Width="100px" TabIndex="2"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label9" runat="server">Vencimento&nbsp;IPVA</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <uc6:DATA ID="datVencimentoIPVA" runat="server" IndiceTab="3" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    &nbsp;</td>
                                <td class="auto-style6">
                                    <asp:CheckBox ID="chkInativo" Font-Bold="false" runat="server" Text="Inativo" Width="100px" onclick="TiraCheckTerceiro();" CssClass="LetrasLabel"/>
                                </td>
                                <td class="auto-style6">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label6" runat="server">Chassi</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtChassi" onfocus="LimpaErro()" runat="server" Width="223px" TabIndex="2"></asp:TextBox>
                                </td>
                                <td class="auto-style6"><asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label10" runat="server">Vcto Licenciamento</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <uc6:DATA ID="datVencimentoLicenciamento" runat="server" IndiceTab="3" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label1" runat="server">Descrição</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtModelo" runat="server" TabIndex="1"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label7" runat="server" Enabled="False">RENAVAM</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtRenavam" onfocus="LimpaErro()" runat="server" TabIndex="2"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label11" runat="server">Vcto&nbsp;Seguro Obrigatório</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <uc6:DATA ID="datVencimentoSeguroObrigatorio" runat="server" IndiceTab="3" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label Font-Bold="False" ID="Label2" runat="server" CssClass="LetrasLabel">&nbsp;Ano&nbsp;Fabric/Modelo</asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <div style="width:100px">
                                        <uc3:INTEIRO ID="intAnoFabricacao" runat="server" IndiceTab="1" />
                                        <a>/</a><uc3:INTEIRO ID="intAnoModelo" runat="server" IndiceTab="1" />
                                    </div>
                                </td>
                                <td class="auto-style2">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label15" runat="server">Cidade emplacamento</asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtCidade" onfocus="LimpaErro()" runat="server" TabIndex="2"></asp:TextBox>
                                </td>
                                <td class="auto-style2">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label12" runat="server">Seguro&nbsp;Frota</asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <uc6:DATA ID="datSeguroFrota" runat="server" IndiceTab="3" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" ID="Label3" CssClass="LetrasLabel" runat="server">&nbsp;Marca</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtMarca" onfocus="LimpaErro()" runat="server" TabIndex="1"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label8" runat="server">Km&nbsp;Inicial</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <uc4:INTEIRO7 ID="intKmInicial" runat="server" IndiceTab="2" />
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    <asp:CheckBox ID="chkProprio" runat="server" Text="Próprio" TabIndex="3" Width="100px" onclick="TiraCheckProprio();" Font-Bold="False" CssClass="LetrasLabel" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" ID="lblNovaSenha" CssClass="LetrasLabel" runat="server" >&nbsp;Tipo&nbsp;Veículo</asp:Label>
                                </td>
                                <td class="LetrasTD" style="width:120px;">
                                    <asp:TextBox ID="txtTipoVeiculo" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" ></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label13" runat="server">Data&nbsp;Aquisição</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <uc6:DATA ID="datDataAquisicao" runat="server" IndiceTab="2" />
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    <asp:CheckBox ID="chkTerceiro" runat="server" Text="Terceiro" TabIndex="3" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" ID="lblConfirmaNovaSenha" CssClass="LetrasLabel" runat="server" >&nbsp;Placas</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtPlacas" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="80px" ></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label Font-Bold="false" CssClass="LetrasLabel" ID="Label14" runat="server">Valor&nbsp;Aquisição</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <uc5:MOEDA ID="valValorAquisicao" runat="server" IndiceTab="2" />
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style7" >
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td class="auto-style7">
                                    <asp:Label Font-Bold="False" ID="lblMensagem" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                                </td>
                                <td class="auto-style7">
                                    </td>
                                <td class="auto-style7">
                                    &nbsp;</td>
                                <td class="auto-style7">
                                    </td>
                                <td class="auto-style7">
                                    <div style="width:200px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" />
                                    </div>
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
