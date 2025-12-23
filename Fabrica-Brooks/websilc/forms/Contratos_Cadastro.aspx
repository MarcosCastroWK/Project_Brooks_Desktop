<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contratos_Cadastro.aspx.cs" Inherits="Contratos_Cadastro" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc7" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc8" %>
<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc3" %>

<!DOCTYPE html> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            height: 11px;
        }
        .auto-style5 {
            width: 364px;
        }
    </style>
</head>
<script>
    function Maiusculas(textbox) {
        var _str = '';
        _str = document.getElementById(textbox).value;     
        document.getElementById(textbox).value = _str.toUpperCase();
    }
    function CalcularPercentual() {
        var vc = document.getElementById('hifValorContrato').value.replace(".", "").replace(",", ".");
        var vca = document.getElementById('moeValorContrato1').value.replace(".", "").replace(",", ".");
        document.getElementById('moePercentualReajuste').value = (parseFloat((vca - vc) * 100) / vc).toFixed(6).replace(".", ",");
    }
    function CalcularValorContrato() {
        var vc = document.getElementById('hifValorContrato').value.replace(".", "").replace(",", ".");
        var vp = document.getElementById('moePercentualReajuste').value.replace(".", "").replace(",", ".") / 100 + 1;
        var vr = vc * vp ;
        document.getElementById('moeValorContrato1').value = parseFloat(vr).toFixed(2).replace(".", ",");

        if (document.getElementById('moePercentualReajuste').value.replace(",", ".") > 0) {
            //document.getElementById('txtTipoNegociacao1').value = 'REAJUSTE';
            document.getElementById('datDataReajuste1').value = document.getElementById('datDataReajuste').value;     
            var _data = document.getElementById('datDataReajuste').value;
            var _dia  = _data.substring(0, 6);
            var _ano4 = parseInt(_data.substring(6)) + 1;
            var _str_data = _dia + _ano4;
            document.getElementById('datDataReajuste2').value = _str_data;
        }
        else {
            var _data = new Date();
            var _dia  = _data.getDate();           // 01/01/2021
            var _mes  = _data.getMonth() + 1;      // 0-11 (zero=janeiro)
            var _ano4 = _data.getFullYear();       // 4 dígitos
            var _str_data = '';
            if (_dia < 10 && _mes < 10)
                _str_data = '0' + _dia + '/0' + _mes + '/' + _ano4;
            else if (_dia >= 10 && _mes < 10)
                _str_data = _dia + '/0' + _mes + '/' + _ano4;
            else if (_dia >= 10 && _mes >= 10)
                _str_data = _dia + '/' + _mes + '/' + _ano4;
            else if (_dia < 10 && _mes >= 10)
                _str_data = '0' + _dia + '/'+ _mes + '/' + _ano4;
            document.getElementById('datDataReajuste1').value = _str_data;
            document.getElementById('datDataReajuste2').value = document.getElementById('datDataReajuste').value;
            //document.getElementById('txtTipoNegociacao1').value = 'REPACTUAÇÃO';
        }
    }
    function Novo() {
        var r = confirm("Confirma?");
        if (r == true) {
            window.location = 'Contratos_Cadastro.aspx';
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Incluir&nbsp;Contratos" Font-Bold="True" Font-Names="Arial"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                    <asp:GridView ID="Grade" runat="server" CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" AllowPaging="True" PageSize="6" OnPageIndexChanging="Grade_PageIndexChanging" OnRowCommand="Grade_RowCommand" Font-Bold="False" TabIndex="1">
                        <AlternatingRowStyle BackColor="AliceBlue" />
                        <Columns>
                            <asp:TemplateField HeaderText="Selecionar Cliente">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibnConsultar" runat="server" ImageUrl="~/Images/selecionar.png" ToolTip="Selecionar" CommandArgument='<%# Container.DisplayIndex%>' />
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codigo" HeaderText="Código Cliente" SortExpression="Codigo">
                            <HeaderStyle CssClass="padItemGrade" />
                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social" SortExpression="Nome">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="padItemGrade" Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NomeFantasia" HeaderText="Nome Fantasia" SortExpression="NomeFantasia">
                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DataCadastro" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Cadastro" SortExpression="DataCadastro" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CNPJ_CPF" HeaderText="CNPJ/CPF" SortExpression="CNPJ_CPF" >
                            <HeaderStyle Width="120px" />
                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Existe" HeaderText="Existe Contrato" SortExpression="Existe" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="60px" />
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
                                <td><asp:Label ID="lblFiltro" CssClass="LetrasLabel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Procurar/Clientes:</asp:Label>
                                </td>
                                <td>
                                <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px" TabIndex="1">
                                    <asp:ListItem>NomeFantasia</asp:ListItem>
                                    <asp:ListItem Value="Nome"></asp:ListItem>
                                    <asp:ListItem Value="CNPJ_CPF">CNPJ/CPF</asp:ListItem>
                                    <asp:ListItem Value="Codigo">Código</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFiltro" runat="server" Width="400px" TabIndex="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" Height="26px" TabIndex="1" />
                                </td>
                            </tr>
                        </table>
                     </td>   
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:90%">
                        <table>
                            <tr><td style="border-bottom:1px solid; height:10px;">&nbsp;</td></tr>
                            <tr><td style="height:6px;"></td></tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" colspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDataRegistro" CssClass="LetrasLabel" runat="server" Font-Bold="False">&nbsp;Data Registro</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDataInicio" CssClass="LetrasLabel" runat="server"  Font-Bold="False" >Data Início</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNumeroContrato" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="70px">Nº Contrato</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblValorContrato" runat="server" Font-Bold="False" Text="&nbsp;Valor Contrato" Width="84px" CssClass="LetrasLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDataTermino" CssClass="LetrasLabel" runat="server" Width="70px" Font-Bold="False" >Data Término</asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblSituacao" runat="server" Font-Bold="False" Text="Situação:" Width="100px" CssClass="LetrasLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl0" CssClass="LetrasLabel" runat="server" Width="70px" Font-Bold="False" >Próximo Reajuste</asp:Label>
                                            </td>
                                            <td>
                                                
                                                <asp:Label ID="lblIndiceReajuste2" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >Indice Reajuste</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" CssClass="LetrasLabel" runat="server" Width="60px" Font-Bold="False" >Aniversário Dia/Mês</asp:Label>
                                                </td>
                                            <td>
                                                 <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Width="70px" Font-Bold="False">Dia Vcto Boleto</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label CssClass="LetrasLabel" ID="Label33" runat="server" Font-Bold="False">&nbsp;Observação</asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc6:DATA ID="datDataRegistro" runat="server" IndiceTab="0" Enabled="False" />
                                            </td>
                                            <td>
                                                <uc6:DATA ID="datDataInicio" runat="server" onfocusout="CalcularDataTerminoReajuste();" IndiceTab="0" />                                                
                                            </td>
                                            <td>
                                                <uc4:INTEIRO7 runat="server" ID="intNumeroContrato" IndiceTab="0" />
                                            </td>
                                            <td>
                                                <uc5:MOEDA ID="moeValorContrato" runat="server" IndiceTab="0" />
                                            </td>
                                            <td>
                                                <uc6:DATA ID="datDataTermino" runat="server" IndiceTab="0" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSituacao" runat="server" Width="308px">
                                                    <asp:ListItem> </asp:ListItem>
                                                    <asp:ListItem>SEM CONTRATO</asp:ListItem>
                                                    <asp:ListItem>EM ANÁLISE/REDAÇÃO</asp:ListItem>
                                                    <asp:ListItem>AGUARDANDO ASSINATURA CLIENTE</asp:ListItem>
                                                    <asp:ListItem>ASSINADO/ARQUIVADO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <uc6:DATA ID="datDataReajuste" runat="server" IndiceTab="0" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIndiceReajuste" runat="server" onkeyup="Maiusculas(this.id);" Width="100px" ></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAniversarioReajuste" runat="server" Width="60px" ></asp:TextBox>
                                            </td>
                                            <td>
                                                 <uc4:INTEIRO7 runat="server" ID="intDiaVencimento" IndiceTab="0" />
                                            </td>
                                            <td rowspan="2">                                                
                                                <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" onkeyup="Maiusculas(this.id);" Width="240px" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hifCodigo" runat="server" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hifNome" runat="server" />
                                                <asp:HiddenField ID="hifNomeFantasia" runat="server" />
                                                <asp:HiddenField ID="hifCNPJ_CPF" runat="server" />
                                                <div style="width:700px;text-align:right;">
                                                    <asp:HiddenField ID="hifValorContrato" runat="server" />
                                                    <asp:Label ID="lblMensagem" ForeColor="Red" CssClass="LetrasLabel" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="width:300px;text-align:left;" >
                                                    <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Salvar" ID="Salvar" OnClick="Salvar_Click" Enabled="False" TabIndex="1" />
                                                    <input id="btnCancelar" type="button" value="Voltar a tela inicial" onclick="Novo();" />
                                                    </div>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr><td style="border-top:1px solid;"></td></tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
