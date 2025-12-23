<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DestinoFinal.aspx.cs" Inherits="SILC.Web.forms.Account_DestinoFinal" %>
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
        html
        {
            background-color:silver;
        }
        .tabs
        {
            position:relative;
            top:1px;
            left:10px;
        }
        .tab
        {
            border:solid 1px black;
            background-color:#eeeeee;
            padding:2px 10px;
        }
        .selectedTab
        {
            background-color:white;
            border-bottom:solid 1px white;
        }
        .tabContents
        {
            border:solid 1px black;
            padding:10px;
            background-color:white;
        }
        .auto-style6
        {
            width: 90px;
            font-size: 10px;
            height: 26px;
        }
        .auto-style7
        {
            width: 10px;
            font-size: 9px;
            height: 15px;
        }
        .auto-style8
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style9
        {
            width: 120px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style10
        {
            width: 90px;
            font-size: 9px;
        }
        .auto-style11
        {
            width: 90px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style12
        {
            width: 90px;
            font-size: 9px;
            height: 15px;
        }
        .auto-style13
        {
            height: 35px;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
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
                        <uc2:menu ID="menucabec1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" colspan="2" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Destino Final" Font-Bold="True" CssClass="titulo2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:180px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="90%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" AllowSorting="True" OnSorting="Grade_Sorting" ForeColor="Black" GridLines="Vertical" Font-Bold="False" Height="153px" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%#Container.DataItemIndex%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%#Container.DataItemIndex%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" Width="50px" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social" SortExpression="Nome">
                                    <HeaderStyle Width="350px" />
                                    <ItemStyle CssClass="padItemGrade" Width="350px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeFantasia" HeaderText="Nome Fantasia" SortExpression="NomeFantasia">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" Width="160px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataCadastro" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Cadastro" SortExpression="DataCadastro">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="Ativo">
                                    <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UF" HeaderText="UF" SortExpression="UF">
                                    <HeaderStyle CssClass="padItemGrade" />
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
                    <div>
                    <asp:Menu
                        id="Menu1"
                        Orientation="Horizontal"
                        StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab"
                        CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick"
                        Runat="server" StaticSubMenuIndent="10px" BackColor="#B5C7DE" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="10px" ForeColor="black">
                        <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#B5C7DE" />
                        <DynamicSelectedStyle BackColor="#507CD1" />
                        <Items>
                            <asp:MenuItem Text="Dados" Value="0" Selected="true" />
                            <asp:MenuItem Text="Licença Ambiental" Value="1" />
                        </Items>    
                        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                        <StaticMenuItemStyle CssClass="tab" HorizontalPadding="5px" VerticalPadding="2px"></StaticMenuItemStyle>
                        <StaticSelectedStyle CssClass="selectedTab" BackColor="#507CD1"></StaticSelectedStyle>
                    </asp:Menu>  
                    <div class="tabContents">
                    <asp:MultiView
                        id="MultiView1"
                        ActiveViewIndex="0"
                        Runat="server">
                        <asp:View ID="ViewDados" runat="server">
                            <table style="width: 894px">
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label CssClass="LetrasLabel" ID="Label17" runat="server" Width="100px" Font-Bold="False">Data do cadastro</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <div style="width:300px;">
                                            <uc6:DATA ID="datDataCadastro" runat="server" IndiceTab="0" />
                                            <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" TabIndex="17" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel" Checked="True"/>
                                        </div>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="False" CssClass="LetrasLabel">Endereço</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:TextBox ID="txtEndereco" onfocus="LimpaErro()" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:Label CssClass="LetrasLabel" ID="Label9" runat="server" Font-Bold="False">Bairro</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:TextBox ID="txtBairro" onfocus="LimpaErro()" runat="server" Width="140px" TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        &nbsp;<asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">Nome/Razão&nbsp;Social</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:TextBox ID="txtNome" runat="server" TabIndex="1" Width="300px"></asp:TextBox>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:Label CssClass="LetrasLabel" ID="Label15" runat="server" Font-Bold="False">CEP</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <uc4:INTEIRO7 ID="intCEP" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="LetrasTD">
                                        &nbsp;</td>
                                    <td class="LetrasTD">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="LetrasLabel">
                                        <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False">&nbsp;Nome Fantasia</asp:Label>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtNomeFantasia" onfocus="LimpaErro()" runat="server" TabIndex="1" style="margin-bottom: 0px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:Label CssClass="LetrasLabel" ID="Label10" runat="server" Font-Bold="False">Cidade</asp:Label>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtCidade" onfocus="LimpaErro()" runat="server" TabIndex="2" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:Label CssClass="LetrasLabel" ID="Label11" runat="server" Font-Bold="False">UF</asp:Label>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtUF" onfocus="LimpaErro()" runat="server" Width="24px" TabIndex="2" style="margin-top: 0px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LetrasLabel">
                                        <asp:Label ID="lblNovaSenha" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;CNPJ</asp:Label>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtCNPJ" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" ></asp:TextBox>
                                        </td>
                                    <td class="auto-style6" colspan="4">
                                        <asp:Label CssClass="LetrasLabel" ID="Label12" runat="server" Width="500px" Font-Bold="False">Nome&nbsp;do&nbsp;arquivo&nbsp;de&nbsp;logomarca com .jpg/.bmp/.png</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblConfirmaNovaSenha" CssClass="LetrasLabel" runat="server" Font-Bold="False" >Telefone</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:TextBox ID="txtTelefone" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="100px" ></asp:TextBox>
                                    </td>
                                    <td colspan="4" class="LetrasTD" style="height:1%;">
                                        <asp:TextBox ID="txtNomeArqLogo" onfocus="LimpaErro()" runat="server" Width="400px" TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6">
                                        <asp:Label CssClass="LetrasLabel" ID="Label5" runat="server" Font-Bold="False">Celular</asp:Label>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtCelular" onfocus="LimpaErro()" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td class="auto-style6" colspan="4">
                                        <asp:Label CssClass="LetrasLabel" ID="Label16" runat="server" Width="500px" Font-Bold="False">Nome&nbsp;do&nbsp;arquivo&nbsp;de&nbsp;assinatura com .jpg/.bmp/.png</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label CssClass="LetrasLabel" ID="Label6" runat="server" Font-Bold="False">e-mail</asp:Label><br />
                                        <asp:TextBox ID="txtEmail" onfocus="LimpaErro()" runat="server" Width="460px" TabIndex="1" Height="52px" MaxLength="1100" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td colspan="4" valign="top">
                                        <asp:TextBox ID="txtNomeArqAss" onfocus="LimpaErro()" runat="server" Width="400px" TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                        <asp:Label CssClass="LetrasLabel" ID="Label7" runat="server" Font-Bold="False">Local Aterro</asp:Label>
                                    </td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtLocalAterro" onfocus="LimpaErro()" runat="server" TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td class="auto-style8" colspan="4">
                                        <div style="width:350px">
                                            <asp:CheckBox ID="chkEnviarEmailCDF" runat="server" Text="Enviar e-mail de CDF para Fornecedor" TabIndex="2" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkEnviarMovResiduos" runat="server" Text="Enviar relatório de movimentação de resíduos por e-mail" TabIndex="2" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkEmiteCDF" runat="server" Text="Fornecedor Emite CDF (Brooks não gera CDF)" TabIndex="2" Width="100px" onclick="TiraCheckProprio();" Font-Bold="False" CssClass="LetrasLabel" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label21" runat="server" CssClass="LetrasLabel" Font-Bold="False">Cod.Unidade IMA</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <uc4:INTEIRO7 ID="intCodUnidadeIMA" runat="server" />
                                    </td>
                                    <td class="LetrasTD" colspan="3">
                                        &nbsp;</td>
                                    <td class="LetrasTD">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style12" >
                                        <asp:HiddenField ID="hifCodigo" runat="server" />
                                    </td>
                                    <td class="auto-style7">
                                        <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                    </td>
                                    <td class="auto-style7">
                                        </td>
                                    <td class="auto-style7">
                                        &nbsp;</td>
                                    <td class="auto-style7">
                                        </td>
                                    <td class="auto-style7">
                                        <div style="width:200px">
                                            <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="2" />
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" TabIndex="2" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ViewLicencaAmbiental" runat="server">
                            <table>
                                <tr>
                                    <td colspan="5">
                                        <div id="Div1" style="width:98%;height:100px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                                            <asp:GridView ID="GradeLAO" runat="server" CellPadding="3" Width="80%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="GradeLAO_RowCommand" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" AllowSorting="True" Font-Bold="False" PageSize="2" OnRowDataBound="GradeLAO_RowDataBound" OnSorting="GradeLAO_Sorting" >
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
                                                            <asp:ImageButton ID="ibnExcluirLao" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluirLao_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                                    <HeaderStyle CssClass="padItemGrade" />
                                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CodigoAterro" HeaderText="Código Destino" SortExpression="CodigoAterro">
                                                    <HeaderStyle CssClass="padItemGrade" />
                                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NomeDestinoFinal" HeaderText="Destino Final" SortExpression="NomeDestinoFinal" />
                                                    <asp:BoundField DataField="NumeroLicenca" HeaderText="Número Licença" SortExpression="NumeroLicenca" >
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PrazoValidade" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Prazo Validade" SortExpression="PrazoValidade" >
                                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Obs" HeaderText="Observação" SortExpression="Obs" />
                                                    <asp:BoundField DataField="Arquivo" HeaderText="Arquivo" SortExpression="Arquivo" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings PageButtonCount="5" />
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
                                    <td class="auto-style13">
                                        <asp:Label ID="Label18" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="110px">Código Destino Final</asp:Label>
                                    </td>
                                    <td class="auto-style13" colspan="2">
                                        <uc4:INTEIRO7 ID="intCodigoDestinoFinal" runat="server" />
                                        <asp:TextBox ID="txtNomeDestinoFinal" runat="server" Enabled="False" onfocus="LimpaErro()" TabIndex="1" Width="200px"></asp:TextBox>
                                        
                                    </td>
                                    <td><asp:Label ID="lblFazerUpload" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="275px">&nbsp;Escolha o documento para fazer o upload</asp:Label></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style14">
                                        <asp:Label CssClass="LetrasLabel" ID="Label4" runat="server" Width="90px" Font-Bold="False">Número Licença</asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                        <asp:TextBox ID="txtNumeroLicenca" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="200px"></asp:TextBox>
                                    </td>                                    
                                    <td>&nbsp;</td>                                    
                                    <td>
                                        <asp:FileUpload ID="UploadDocumento" runat="server" Width="359px" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="auto-style14">
                                        <asp:Label ID="Label20" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="90px">Código Atividade</asp:Label>
                                    </td>
                                    <td class="auto-style14">
                                        <asp:TextBox ID="txtCodigoAtividade" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="200px"></asp:TextBox>
                                        </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="90px">Prazo Validade</asp:Label>
                                    </td>
                                    <td>
                                        <uc6:DATA ID="datPrazoValidade" runat="server" IndiceTab="1" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="LetrasLabel" Font-Bold="False">&nbsp;Observação</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtObs" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style13">
                                        </td>
                                    <td class="auto-style13">
                                        <asp:Label ID="lblMensagemLAO" runat="server" CssClass="LetrasLabel"></asp:Label>
                                    </td>
                                    <td class="auto-style13"></td>
                                    <td class="auto-style13">
                                        <div style="width:200px">
                                            <asp:Button ID="btnOk0" runat="server" CommandName="Salvar" Font-Size="10pt" OnClick="btnOk0_Click" TabIndex="18" Text="Ok" />
                                            <asp:Button ID="Button2" runat="server" OnClick="btnCancelar_Click" Text="Novo" />
                                        </div>
                                    </td>
                                    <td class="auto-style13">
                                        <asp:HiddenField ID="hifCodigoLAO" runat="server" />
                                    </td>
                                    <td class="auto-style13"></td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="LetrasTD">&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                        &nbsp;</td>
                                    <td  style="width:80px">
                                        &nbsp;</td>
                                    <td style="width:80px">&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        
                        </asp:View>
                        </asp:MultiView>
                    </div>
                    </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
