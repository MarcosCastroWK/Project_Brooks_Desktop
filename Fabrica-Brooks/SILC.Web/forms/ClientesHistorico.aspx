<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientesHistorico.aspx.cs" Inherits="SILC.Web.forms.webClientesHistorico" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc7" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc8" %>
<%@ Register src="~/forms/INTEIRO7.ascx" TagPrefix="uc1" TagName="INTEIRO7" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc9" %>

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
            border:solid 0px black;
            padding:10px;
            background-color:white;
            width: 745px;
            height: 1600px;
        }
        .tabContents2
        {
            left: 746px;
            top:300px;
            border:solid 0px black;
            padding:10px;
            background-color:white;
            width: 745px;
            height: 1600px;
        }
        .auto-style6
        {
            width: 90px;
            font-size: 10px;
            height: 26px;
        }
        .auto-style8
        {
            width: 10px;
            font-size: 9px;
            height: 24px;
        }
        .auto-style12
        {
            width: 90px;
            top: 1px;
            font-size: 9px;
            height: 26px;
        }
        .LetrasLabel2
        {
            top: 1px;
            font-size: 9px;
            font-family: Verdana;
            color: blue;
        }
        .auto-style17 {
            width: 1%;
        }
        .auto-style18 {
            height: 1%;
            width: 1250px;
        }
        .auto-style19 {
            top: 1px;
            font-size: 9px;
            font-family: Verdana;
            color: blue;
            margin-top: 20px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" class="form2">
            <tr>
                <td>
                    <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:menu ID="menucabec1" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td id="dadosCab" valign="top" style="font-size:9pt;" class="auto-style18">
                    <asp:Label ID="Label58" runat="server" CssClass="LetrasLabel" Text="Histórico&amp;nbsp;de&amp;nbsp;alteração"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:GridView ID="GradeHistorico" runat="server" AutoGenerateColumns="False" Font-Size="10px" Font-Names="Verdana" OnRowDataBound="GradeHistorico_RowDataBound" OnRowCommand="GradeHistorico_RowCommand" Width="400px" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibnConsultaHistorico" runat="server" ImageUrl="~/Images/mostrarsmall.png" ToolTip="Consultar" CommandArgument='<%# Container.DisplayIndex%>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DataAlteracao" HeaderText="Data" SortExpression="Data">
                            <HeaderStyle CssClass="padItemGrade" />
                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="padItemGrade" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="padItemGrade" Width="150px" />
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
                <td>
                    <asp:Label ID="Label102" runat="server" Style="text-align: center" CssClass="LetrasLabel" Font-Bold="False" Width="766px" Height="18px" BackColor="Aqua" Font-Size="12pt">&nbsp;ALTERADO</asp:Label>
                    <asp:Label ID="Label103" runat="server" Style="text-align: center" CssClass="LetrasLabel2" Font-Bold="False" Width="760px" Height="18px" BackColor="LightBlue" Font-Size="12pt">&nbsp;CADASTRO ANTERIOR</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tabContents">
                        <asp:Label ID="lblTituloCodigoCliente" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="90px">&nbsp;&nbsp;&nbsp;Código&nbsp;Cliente:</asp:Label>
                        <asp:Label ID="lblCodigoCliente" runat="server" CssClass="LetrasLabel" BorderWidth="1px" BorderColor="DimGray" Width="50px" Height="20px" Font-Names="Arial" Font-Size="14px">&nbsp;</asp:Label>
                        <table style="width:400px;">
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2">
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDataCadastro" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="100px">&nbsp;Data Cadastro</asp:Label>
                                </td>
                                <td>
                                    <div style="width:380px;">
                                        <uc6:DATA ID="datDataCadastro" runat="server" />
                                        <asp:CheckBox ID="chkNaoAceitaDTRComPesoDiferente" runat="server" CssClass="LetrasLabel" Font-Bold="False" onclick="TiraCheckTerceiro();" style="margin-top: 0px" TabIndex="3" Text="Não aceita DTR peso diferente" Width="170px" />
                                        <asp:ImageButton ID="imbInativo" runat="server" ImageUrl="~/Images/selecionar.png" OnClick="imbInativo_Click1" />
                                        &nbsp;<asp:Label ID="lblInativo" runat="server" CssClass="LetrasLabel" Text="Inativo" Font-Size="9pt" Height="20px"></asp:Label>
                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoCadastro" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Tipo Cadastro</asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoCadastro" runat="server" TabIndex="3" Width="120px">
                                        <asp:ListItem>Mensal</asp:ListItem>
                                        <asp:ListItem>Eventual</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblDataCadastroAnterior" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="80px">&nbsp;Data Cadastro</asp:Label>
                                </td>
                                <td>
                                    <div style="width:350px;">
                                        <uc6:DATA ID="datDataCadastro0" runat="server" />
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" onclick="TiraCheckTerceiro();" style="margin-top: 0px" TabIndex="3" Text="Não aceita DTR peso diferente" Width="175px" />
                                        <asp:ImageButton ID="imbInativo0" runat="server" ImageUrl="~/Images/selecionar.png" OnClick="imbInativo_Click1" />
                                        &nbsp;<asp:Label ID="Label38" runat="server" CssClass="LetrasLabel2" Text="Inativo" Font-Size="9pt" Height="20px"></asp:Label>
                                    </div>

                                </td>
                                <td>
                                    <asp:Label ID="Label41" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="67px">Tipo Cadastro</asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoCadastro0" runat="server" CssClass="LetrasLabel2" TabIndex="3" Width="120px">
                                        <asp:ListItem>Mensal</asp:ListItem>
                                        <asp:ListItem>Eventual</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNome" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Nome/Razão Social</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNome" runat="server" Font-Bold="true" TabIndex="1" Width="360px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label52" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Código Func_coml:"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <uc3:INTEIRO ID="intCodigoFuncionarioComercial" runat="server" IndiceTab="3" />
                                    <asp:Label ID="lblNomeFuncionario" runat="server" Text="nome funcionário"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNome0" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" >&nbsp;Nome/Razão Social</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNome0" runat="server" Font-Bold="true" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label59" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Text="Código Func_coml:"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <uc3:INTEIRO ID="intCodigoFuncionarioComercial0" runat="server" IndiceTab="3" />
                                    <asp:Label ID="lblNomeFuncionario0" runat="server" Height="16px" Text="nome funcionário" CssClass="LetrasLabel2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNomeFantasia" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Nome Fantasia</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomeFantasia" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblNovaSenha2" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="67px" Font-Bold="False" >Nova Senha</asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtNovaSenha" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="3" Width="134px" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblNomeFantasia0" onfocus="LimpaErro()" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" >&nbsp;Nome Fantasia</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomeFantasia0" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblNovaSenha5" onfocus="LimpaErro()" CssClass="LetrasLabel2" runat="server" Width="67px" Font-Bold="False" >Nova Senha</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtNovaSenha0" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="3" Width="125px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;CNPJ/CPF</asp:Label>
                                </td>
                                <td style="border-width: 0px">
                                    <table border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCNPJ_CPF" runat="server" TextMode="SingleLine" TabIndex="1" Width="120px" onblur="OnColocaPontosIfemBarra(this.id);" onfocus="return OnRetiraPontosIfemBarra(this.id);" onkeypress="return SoNumeros(event);" MaxLength="14"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="txtAlfaCNPJ_CPF" runat="server" TabIndex="1" MaxLength="1" Width="18px" onkeyup="ParaMaiusculas(this.id)" onkeypress="return SoLetras(event, this.id);"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="lblNovaSenha3" CssClass="LetrasLabel" runat="server" Width="67px" Font-Bold="False" >Data Senha</asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <uc6:DATA ID="datDataNovaSenha" runat="server" IndiceTab="3" />
                                </td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel2" ID="Label60" runat="server" Font-Bold="False">&nbsp;CNPJ/CPF</asp:Label>
                                </td>
                                <td>
                                    <table border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCNPJ_CPF0" runat="server" MaxLength="14" onblur="OnColocaPontosIfemBarra(this.id);" onfocus="return OnRetiraPontosIfemBarra(this.id);" onkeypress="return SoNumeros(event);" TabIndex="1" TextMode="SingleLine" Width="120px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAlfaCNPJ_CPF0" runat="server" MaxLength="1" onkeypress="return SoLetras(event, this.id);" onkeyup="ParaMaiusculas(this.id)" TabIndex="1" Width="18px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="lblNovaSenha4" CssClass="LetrasLabel2" runat="server" Width="67px" Font-Bold="False" >Data Senha</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <uc6:DATA ID="datDataNovaSenha0" runat="server" IndiceTab="3" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14">
                                    <asp:Label ID="Label2" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;RG/Inscrição Estadual</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtRG_IE" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="200px" ></asp:TextBox>
                                </td>
                                <td class="auto-style6">
                                    <asp:Label ID="lblSolicitadaPor" CssClass="LetrasLabel" runat="server" Width="67px" Font-Bold="False" >Solicitado Por</asp:Label>
                                </td>
                                <td class="auto-style6">&nbsp;</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtSolicitadaPor" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="3" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label61" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel2">&nbsp;RG/Inscrição Estadual</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRG_IE0" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="200px" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblSolicitadaPor0" CssClass="LetrasLabel2" runat="server" Width="67px" Font-Bold="False" >Solicitado Por</asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtSolicitadaPor0" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="3" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Classificação</asp:Label>
                                </td>
                                <td colspan="4">
                                    <table>
                                        <tr>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intClassificacao" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Filial</asp:Label>
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 ID="intFilial" runat="server" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" CssClass="LetrasLabel" runat="server" Width="76px" Font-Bold="False">&nbsp;Conta Gerencial</asp:Label>
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intContaGerencial" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" CssClass="LetrasLabel" runat="server" Width="45px" Font-Bold="False">&nbsp;Código Exp p/NF</asp:Label>
                                            </td>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoExpNF" runat="server" IndiceTab="2" />
                                            </td>
                                            <td>   
                                                <asp:Label ID="Label9" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Código&amp;nbsp;no&amp;nbsp; aterro"></asp:Label>
                                            </td>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoAterro" runat="server" IndiceTab="3" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="5">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label62" CssClass="LetrasLabel2" runat="server" Font-Bold="False">&nbsp;Classificação</asp:Label>
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intClassificacao0" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label42" runat="server" Font-Bold="False" CssClass="LetrasLabel2">&nbsp;Filial</asp:Label>
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 ID="intFilial0" runat="server" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label44" CssClass="LetrasLabel2" runat="server" Width="76px" Font-Bold="False">&nbsp;Conta Gerencial</asp:Label>
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intContaGerencial0" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label45" CssClass="LetrasLabel2" runat="server" Width="64px" Font-Bold="False">&nbsp;Código Exp p/NF</asp:Label>
                                            </td>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoExpNF0" runat="server" IndiceTab="2" />
                                            </td>
                                            <td>   
                                                <asp:Label ID="Label46" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Text="Código&amp;nbsp;no&amp;nbsp; aterro"></asp:Label>
                                            </td>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoAterro0" runat="server" IndiceTab="3" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                
                                <td></td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                        <asp:CheckBox ID="chkEmiteDDR" runat="server" Text="Emitir DDR" TabIndex="1" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                    </td>
                                <td>
                                        <asp:CheckBox ID="chkEmitirCDF" runat="server" Text="Emitir CDF" TabIndex="1" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                    </td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                        <asp:CheckBox ID="chkEmiteDDR0" runat="server" Text="Emitir DDR" TabIndex="1" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel2"/>
                                    </td>
                                <td>
                                        <asp:CheckBox ID="chkEmitirCDF0" runat="server" Text="Emitir CDF" TabIndex="1" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel2"/>
                                    </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label55" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="130px">&nbsp;Aliquota Imposto Federal:</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:DropDownList ID="ddlAliquotasFederais" runat="server" CssClass="LetrasLabel" Height="16px" Width="400px">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label63" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="138px">&nbsp;Aliquota Imposto Federal:</asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlAliquotasFederais0" runat="server" CssClass="LetrasLabel2" Height="16px" Width="400px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table style="width:340px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEndereco0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="50px">&nbsp;Endereço</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndereco0" runat="server" TabIndex="1" onkeyup="ParaMaiusculas(this.id)" Width="387px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblNumero0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Nº"></asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNumero0" runat="server" IndiceTab="1" />
                                </td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblEndereco1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="50px">&nbsp;Endereço</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndereco3" runat="server" TabIndex="1" onkeyup="ParaMaiusculas(this.id)" Width="387px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblNumero1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Text="Nº"></asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNumero3" runat="server" IndiceTab="1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblComplemento0" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplemento0" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblFone10" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()">Fone 1</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD10" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone10" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="154px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblComplemento1" CssClass="LetrasLabel2" runat="server" Width="77px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplemento3" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblFone21" runat="server" CssClass="LetrasLabel2" Font-Bold="False" onfocus="LimpaErro()">Fone&nbsp;1</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD23" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone23" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="145px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblBairro0" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBairro0" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblFone20" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="38px">Fone 2</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD20" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone20" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="154px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblBairro1" onfocus="LimpaErro()" CssClass="LetrasLabel2" runat="server" Width="61px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBairro3" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblFone22" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="38px">Fone 2</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD24" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone24" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="145px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="lblCEP0" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                </td>
                                <td>
                                    <div style="width:300px;vertical-align:central;">
                                        <asp:TextBox ID="txtCEP0" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>

                                        <asp:Label ID="lblUF0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtUF0" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>

                                        <asp:Label ID="lblCodigoIBGE0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtCodigoIBGE0" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>

                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="lblCelular0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="29px" Height="16px">Celular</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDC0" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCelular0" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="154px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel2" ID="lblCEP1" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                </td>
                                <td>
                                    <div style="width:300px;vertical-align:central;">
                                        <asp:TextBox ID="txtCEP3" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>

                                        <asp:Label ID="lblUF1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtUF3" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>

                                        <asp:Label ID="lblCodigoIBGE1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtCodigoIBGE3" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>

                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="lblCelular1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="29px" Height="16px">Celular</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDF3" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCelular3" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="145px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14">
                                    <asp:Label ID="lblCidade0" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Cidade</asp:Label>
                                </td>
                                <td>
                                    <table style="width:400px;border-spacing:0;padding:0;word-spacing: 0;">
                                        <tr>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoClidade0" runat="server" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lbtNomeCidade0" runat="server" Enabled="False" TabIndex="1" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="auto-style6">
                                    <asp:Label ID="lblFax0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="27px">Fax</asp:Label>
                                    </td>
                                <td class="auto-style6">
                                    <uc9:INTEIRO2 ID="intDDDF0" runat="server" IndiceTab="2" />
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtFax0" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="154px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblCidade1" runat="server" Width="82px" Font-Bold="False" CssClass="LetrasLabel2">&nbsp;Cidade</asp:Label>
                                </td>
                                <td>
                                    <table style="width:400px;border-spacing:0;padding:0;word-spacing: 0;">
                                        <tr>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoClidade3" runat="server" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lbtNomeCidade3" runat="server" Enabled="False" TabIndex="1" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="lblFax1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="27px">Fax</asp:Label>
                                    </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDF4" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFax3" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="145px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblemail0" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtemail0" runat="server" onblur="Validar_email(this.id);" onfocus="LimpaErro()" TabIndex="1" Width="400px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblemail1" CssClass="LetrasLabel2" runat="server" Width="65px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtemail3" runat="server" onblur="Validar_email(this.id);" onfocus="LimpaErro()" TabIndex="1" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="lblContato0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContato0" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="180px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblContato1" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="65px">&nbsp;Contato</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContato3" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="180px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <asp:Label ID="lblMensagem0" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Endereço</asp:Label>
                                </td>
                                <td>
                                    <div>
                                        <asp:TextBox ID="txtEndereco1" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </div>
                                </td>
                                <td class="auto-style12">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Nº"></asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNumero1" runat="server" IndiceTab="1" />
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label64" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Endereço</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndereco4" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="Label65" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Text="Nº"></asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNumero5" runat="server" IndiceTab="1" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplemento1" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()">Fone 1</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD11" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone11" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="145px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label66" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplemento4" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label67" runat="server" CssClass="LetrasLabel2" Font-Bold="False" onfocus="LimpaErro()">Fone&nbsp;1</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD25" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone25" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBairro1" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" CssClass="LetrasLabel" Font-Bold="False">Fone 2</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD21" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone21" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label68" onfocus="LimpaErro()" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBairro4" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label69" runat="server" CssClass="LetrasLabel2" Font-Bold="False">Fone 2</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD26" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone26" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label14" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                </td>
                                <td>
                                    <div style="width:300px;vertical-align:central;">
                                        <asp:TextBox ID="txtCEP1" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>

                                        <asp:Label ID="Label15" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtUF1" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>

                                        <asp:Label ID="Label16" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtCodigoIBGE1" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>

                                    </div>
                                </td>

                                <td>
                                    <asp:Label ID="Label17" runat="server" CssClass="LetrasLabel" Font-Bold="False">Celular</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDC1" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCelular1" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel2" ID="Label70" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                </td>
                                <td>
                                    <div style="width:300px;vertical-align:central;">
                                        <asp:TextBox ID="txtCEP4" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>

                                        <asp:Label ID="Label71" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtUF4" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>

                                        <asp:Label ID="Label72" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtCodigoIBGE4" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>

                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="Label73" runat="server" CssClass="LetrasLabel2" Font-Bold="False">Celular</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDC3" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCelular4" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14">
                                    <asp:Label ID="Label18" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Cidade</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <table style="border-spacing:0;padding:0;word-spacing: 0;">
                                        <tr>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoClidade1" runat="server" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lbtNomeCidade1" runat="server" Enabled="False" TabIndex="1" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" CssClass="LetrasLabel" Font-Bold="False">Fax</asp:Label>
                                    </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDF1" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFax1" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label74" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel2">&nbsp;Cidade</asp:Label>
                                </td>
                                <td>
                                    <table style="border-spacing:0;padding:0;word-spacing: 0;">
                                        <tr>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoClidade4" runat="server" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lbtNomeCidade4" runat="server" Enabled="False" TabIndex="1" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:Label ID="Label75" runat="server" CssClass="LetrasLabel2" Font-Bold="False">Fax</asp:Label>
                                    </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDF5" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFax4" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label20" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                </td>
                                <td colspan="4">
                                    <div style="height:24px">
                                        <asp:TextBox ID="txtemail1" runat="server" onfocus="LimpaErro()" onblur="Validar_email(this.id);" TabIndex="1" Width="200px"></asp:TextBox>
                                        <asp:Label ID="Label37" CssClass="LetrasLabel" runat="server" Font-Bold="False" Height="22px">&nbsp;Cobrança</asp:Label>
                                        &nbsp;<asp:DropDownList ID="ddlTipoCobranca" runat="server" Width="204px" CssClass="LetrasLabel" Height="16px">
                                            <asp:ListItem Value="01">BOLETO SEM INSTRUÇÃO DE PROTESTO</asp:ListItem>
                                            <asp:ListItem Value="02">EM CARTEIRA</asp:ListItem>
                                            <asp:ListItem Value="03">DEPÓSITO C/C</asp:ListItem>
                                            <asp:ListItem Value="04">BOLETO COM INSTRUÇÃO DE PROTESTO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label76" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtemail4" runat="server" onfocus="LimpaErro()" onblur="Validar_email(this.id);" TabIndex="1" Width="200px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label77" CssClass="LetrasLabel2" runat="server" Font-Bold="False">&nbsp;Cobrança</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoCobranca0" runat="server" Width="204px" CssClass="LetrasLabel2" Height="16px">
                                        <asp:ListItem Value="01">BOLETO SEM INSTRUÇÃO DE PROTESTO</asp:ListItem>
                                        <asp:ListItem Value="02">EM CARTEIRA</asp:ListItem>
                                        <asp:ListItem Value="03">DEPÓSITO C/C</asp:ListItem>
                                        <asp:ListItem Value="04">BOLETO COM INSTRUÇÃO DE PROTESTO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label54" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="140px">&nbsp;Instruções para faturamento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInstrucoesFaturamento" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label53" runat="server" CssClass="LetrasLabel" Font-Bold="False">CNPJ Famento</asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtCNPJ_CPF_Faturamento" runat="server" onblur="OnColocaPontosIfemBarra(this.id);" onfocus="return OnRetiraPontosIfemBarra(this.id);" onkeypress="return SoNumeros(event);" TabIndex="1" TextMode="SingleLine" Width="120px" MaxLength="14"></asp:TextBox>
                                    <asp:TextBox ID="txtAlfaCNPJ" runat="server" MaxLength="1" Width="12px" onkeyup="ParaMaiusculas(this.id)" onkeypress="return SoLetras(event, this.id);" TabIndex="1"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label78" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="106px">Instruções para faturamento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInstrucoesFaturamento0" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label79" runat="server" CssClass="LetrasLabel2" Font-Bold="False">CNPJ Famento</asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtCNPJ_CPF_Faturamento0" runat="server" onblur="OnColocaPontosIfemBarra(this.id);" onfocus="return OnRetiraPontosIfemBarra(this.id);" onkeypress="return SoNumeros(event);" TabIndex="1" TextMode="SingleLine" Width="120px" MaxLength="14"></asp:TextBox>
                                    <asp:TextBox ID="txtAlfaCNPJ0" runat="server" MaxLength="1" Width="12px" onkeyup="ParaMaiusculas(this.id)" onkeypress="return SoLetras(event, this.id);" TabIndex="1"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="Label21" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContato1" runat="server" onfocus="LimpaErro()" onkeyup="ParaMaiusculas(this.id)" TabIndex="1" Width="180px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label80" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContato4" runat="server" onfocus="LimpaErro()" onkeyup="ParaMaiusculas(this.id)" TabIndex="1" Width="180px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <asp:Label ID="lblMensagem1" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 180px; padding:0; word-spacing: 0;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label23" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Endereço</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <div style="width:300px;">
                                        <asp:TextBox ID="txtEndereco2" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </div>
                                </td>
                                <td class="auto-style12">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="Label24" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Nº"></asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNumero2" runat="server" IndiceTab="1" />
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label81" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Endereço</asp:Label>
                                </td>
                                <td>
                                        <asp:TextBox ID="txtEndereco5" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label82" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Text="Nº"></asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intNumero6" runat="server" IndiceTab="1" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label25" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplemento2" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label26" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()" Width="50px">Fone 1</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD12" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone12" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label83" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplemento5" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label84" runat="server" CssClass="LetrasLabel2" Font-Bold="False" onfocus="LimpaErro()" Width="50px">Fone 1</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD27" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone27" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label27" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBairro2" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label28" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="37px">Fone 2</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD22" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone22" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label85" onfocus="LimpaErro()" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBairro5" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label86" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="37px">Fone 2</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDD28" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFone28" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="Label29" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                </td>
                                <td>
                                    <div style="width:300px; vertical-align:central;">
                                        <asp:TextBox ID="txtCEP2" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>
                                        <asp:Label ID="Label30" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtUF2" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>
                                        <asp:Label ID="Label31" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtCodigoIBGE2" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="Label32" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="39px">Celular</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDC2" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCelular2" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel2" ID="Label87" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCEP5" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="70px" ></asp:TextBox>
                                        <asp:Label ID="Label47" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtUF6" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>
                                        <asp:Label ID="Label48" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                        &nbsp;<asp:TextBox ID="txtCodigoIBGE6" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>
                                </td>
                                <td>                                    
                                    <asp:Label ID="Label88" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="39px">Celular</asp:Label>
                                </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDC4" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCelular5" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14">
                                    <asp:Label ID="Label33" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Cidade</asp:Label>
                                </td>
                                <td>
                                    <table style="width:300px;border-spacing:0;padding:0;word-spacing: 0;">
                                        <tr>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoClidade2" runat="server" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lbtNomeCidade2" runat="server" Enabled="False" TabIndex="1" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="auto-style6">
                                    <asp:Label ID="Label34" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="29px">Fax</asp:Label>
                                    </td>
                                <td class="auto-style6">
                                    <uc9:INTEIRO2 ID="intDDDF2" runat="server" IndiceTab="2" />
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtFax2" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label89" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel2">&nbsp;Cidade</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intCodigoClidade5" runat="server" IndiceTab="1" />
                                    <asp:TextBox ID="lbtNomeCidade5" runat="server" Enabled="False" TabIndex="1" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label90" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="29px">Fax</asp:Label>
                                    </td>
                                <td>
                                    <uc9:INTEIRO2 ID="intDDDF6" runat="server" IndiceTab="2" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFax5" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label35" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtemail2" runat="server" onfocus="LimpaErro()" onblur="Validar_email(this.id);" TabIndex="1" Width="300px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label91" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtemail5" runat="server" onfocus="LimpaErro()" onblur="Validar_email(this.id);" TabIndex="1" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                        <asp:Label ID="Label36" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                </td>
                                <td colspan="3">
                                    <div style="width: 400px; vertical-align: central;">
                                        <asp:TextBox ID="txtContato2" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="180px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                        <asp:Label ID="Label56" Height="18px" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="40px">&nbsp;Cargo:</asp:Label>
                                        <asp:TextBox ID="txtCargoContato" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="140px"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                        <asp:Label ID="Label92" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                </td>
                                <td colspan="2">
                                        <asp:TextBox ID="txtContato5" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                        <asp:Label ID="Label93" Height="22px" runat="server" CssClass="LetrasLabel2" Font-Bold="False">&nbsp;Cargo:</asp:Label>
                                        <asp:TextBox ID="txtCargoContato0" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="100px" Height="20px"></asp:TextBox>
                                    </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <asp:Label ID="lblMensagem2" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;<asp:Label ID="Label39" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()" Width="67px">Km média</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intKmMedia" runat="server" IndiceTab="1" />
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label94" runat="server" Font-Bold="False" CssClass="LetrasLabel2" Width="67px" Height="16px">Km média</asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intKmMedia0" runat="server" IndiceTab="1" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label22" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="100px">&nbsp;Ponto de Referência</asp:Label>
                                </td>
                                <td>
                                    <div style="width: 600px">
                                        <asp:TextBox ID="txtPontoDeReferencia" runat="server" Height="50px" TabIndex="1" TextMode="MultiLine" Width="340px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </div>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label95" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="100px">&nbsp;Ponto de Referência</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPontoDeReferencia0" runat="server" Height="50px" TabIndex="1" TextMode="MultiLine" Width="340px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label40" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Observação</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtObservacao" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="340px" Height="100px" TextMode="MultiLine" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label96" onfocus="LimpaErro()" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False" >&nbsp;Observação</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtObservacao0" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="340px" Height="100px" TextMode="MultiLine" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td style="width:300px">
                                    <asp:CheckBox ID="chkIncluirAtualizarDados" runat="server" CssClass="LetrasLabel" Text="Incluir/Atualizar dados" Width="550px" TabIndex="1" />
                                </td>
                                <td></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="chkIncluirAtualizarDados0" runat="server" CssClass="auto-style19" Text="Incluir/Atualizar dados" Width="390px" TabIndex="1" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td colspan="5" style="text-align: center;">
                                                            <asp:Label ID="lblTituloFatma" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Size="9pt" Width="745px" BackColor="#FFCCFF">&nbsp;Sistema MTR-e IMA</asp:Label>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td colspan="4" style="text-align: center;width:600px">
                                                            <asp:Label ID="lblTituloFatma1" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Size="9pt" Width="700px" BackColor="#FFCCFF">&nbsp;Sistema MTR-e IMA</asp:Label>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label43" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="100px">&nbsp;Senha Master</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSenhaMaster" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSenhaAcesso" runat="server" Width="70px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Senha Acesso</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSenhaAcesso" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="Label97" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="76px">&nbsp;Senha Master</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSenhaMaster0" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="148px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSenhaAcesso0" runat="server" Width="70px" Font-Bold="False" CssClass="LetrasLabel2">&nbsp;Senha Acesso</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSenhaAcesso0" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="175px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblObsMTRe" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Observação</asp:Label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtObsMTRFatima" runat="server" TabIndex="2" Width="140px" maxlength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="http://mtr.ima.sc.gov.br/" Font-Size="11pt">http://mtr.ima.sc.gov.br/</asp:HyperLink>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblObsMTRe0" CssClass="LetrasLabel2" runat="server" Width="100px" Font-Bold="False">&nbsp;Observação</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtObsMTRFatima0" runat="server" TabIndex="2" Width="140px" maxlength="20"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label49" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContatoFatma" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label50" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="50px">&nbsp;Telefone</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFoneFatma" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="Label98" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContatoFatma0" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label99" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="50px">&nbsp;Telefone</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFoneFatma0" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label51" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="50px">&nbsp;e-mail</asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtemailFatma" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="400px"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="Label100" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="50px">&nbsp;e-mail</asp:Label>
                                                        </td>
                                                        <td colspan="7">
                                                            <asp:TextBox ID="txtemailFatma0" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="400px"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label57" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="106px">&nbsp;Código Unidade IMA:</asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <uc1:INTEIRO7 ID="intCodigoUnidadeIMA" runat="server" />
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="Label101" runat="server" CssClass="LetrasLabel2" Font-Bold="False" Width="115px">&nbsp;Código Unidade IMA:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <uc1:INTEIRO7 ID="intCodigoUnidadeIMA0" runat="server" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkClienteExigeMTReParaRCD" runat="server" CssClass="LetrasLabel" TabIndex="1" Text="Cliente Exige emissão da MTR-e para RCD" Width="230px" />
                                                        </td>
                                                        <td>
                                                            <div style="width:540px;text-align: right;">
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkClienteExigeMTReParaRCD0" runat="server" CssClass="LetrasLabel2" TabIndex="1" Text="Cliente Exige emissão da MTR-e para RCD" Width="240px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMensagem5" runat="server" CssClass="LetrasLabel"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width:1%">
                                                &nbsp;</td>
                                            <td class="auto-style17">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
