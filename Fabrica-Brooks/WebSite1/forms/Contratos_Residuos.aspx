<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contratos_Residuos.aspx.cs" Inherits="Contratos_Residuos" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
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
<script>
    function isNumberKeyInteiro2Local(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode >= 48 && charCode <= 57 || charCode == 8 || charCode == 46)
            return true;
        if (charCode >= 96 && charCode <= 105)
            return true;
        return false;
    }

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
            document.getElementById('txtTipoNegociacao1').value = 'REAJUSTE';
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
            document.getElementById('txtTipoNegociacao1').value = 'REPACTUAÇÃO';
        }
    }
    function CalcularDataTerminoReajuste() {
        var _dia = document.getElementById('datDataInicio').value[0] + document.getElementById('datDataInicio').value[1];
        var _mes = document.getElementById('datDataInicio').value[3] + document.getElementById('datDataInicio').value[4];
        var _ano4 = parseInt(document.getElementById('datDataInicio').value[6] + document.getElementById('datDataInicio').value[7] + document.getElementById('datDataInicio').value[8] + document.getElementById('datDataInicio').value[9]) + 1;
        var _str_data = _dia + '/' + _mes + '/' + _ano4;
        if (document.getElementById('datDataTermino').value == '' && document.getElementById('datDataInicio').value != '')
            document.getElementById('datDataTermino').value = _str_data;
        if (document.getElementById('datDataReajuste').value == '' && document.getElementById('datDataInicio').value != '')
            document.getElementById('datDataReajuste').value = _str_data;
        if (document.getElementById('txtAniversarioReajuste').value == '' && document.getElementById('datDataInicio.value') != '')
            document.getElementById('txtAniversarioReajuste').value = _dia + "/" + _mes;
    }
    function Novo() {
        var r = confirm("Confirma?");
        if (r == true) {
            window.location = 'Contratos_Cadastro.aspx';
        }
    }

    function EscondePesquisaResiduos() {
        document.getElementById('PanelResiduos').style.visibility = 'hidden';
    }
    function PesquisaResiduosGrade(evt, pId) {
        var ptxt = pId.replace("txtCodigoResiduo", "txtDescricaoResiduo");
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode == 113 && document.getElementById('PanelResiduos').style.visibility == 'hidden') {
            document.getElementById('PanelResiduos').style.visibility = 'visible';
            document.getElementById(pId).value = '';
            document.getElementById(ptxt).value = 'F2 - Pesquisa';
        }
        else if (charCode == 113) {
            document.getElementById('PanelResiduos').style.visibility = 'hidden';
        }
            
    }

    function MostraResiduo(pId) {        
        var x = document.getElementById('hifResiduos').value;
        var _cdresiduo = document.getElementById(pId).value;        
        var sp = x.split('>' + _cdresiduo + '|');
        var ptxt = pId.replace("txtCodigoResiduo", "txtDescricaoResiduo");
        if (sp.length > 1) {
            sp = x.split('>' + _cdresiduo + '|')[1];
            var spAchado = sp.split("<--");
            document.getElementById(ptxt).value = spAchado[0];
            EscondePesquisaResiduos();
        }
        else {
            document.getElementById(pId).value = '';
            document.getElementById(ptxt).value = 'F2 - Pesquisa';
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
                    <td id="dadosCab" valign="top" style="font-size:9pt;">
                        &nbsp;&nbsp;<asp:Label ID="lblTitulo" runat="server" Text="Inclusão base do contrato incluída com sucesso!" Font-Bold="True" Font-Names="Arial"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:90%">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" CssClass="LetrasLabel" runat="server" Font-Bold="False" >Código gerado</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" CssClass="LetrasLabel" runat="server" Font-Bold="False" >Código Cliente</asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="Label6" CssClass="LetrasLabel" runat="server" Width="104px" Font-Bold="False" >Nome/Razão Social</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" CssClass="LetrasLabel" runat="server" Width="80px" Font-Bold="False" >Nome Fantasia</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" CssClass="LetrasLabel" runat="server" Font-Bold="False">CNPJ/CPF:</asp:Label>                                    
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDataInicio" CssClass="LetrasLabel" runat="server"  Font-Bold="False" >Data Início</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNumeroContrato" runat="server" Font-Bold="False" Text="Número Contrato" Width="60px" CssClass="LetrasLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblValorContrato" runat="server" Font-Bold="False" Text="&nbsp;Valor Contrato" Width="84px" CssClass="LetrasLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc1:INTEIRO7 ID="intCodigoGerado" runat="server" Enabled="False" IndiceTab="0" />
                                            </td>
                                            <td style="margin-left: 40px">
                                                <uc1:INTEIRO7 runat="server" ID="intCodigoCliente" IndiceTab="0" Enabled="False" />
                                            </td>
                                            <td>                                    
                                                <asp:TextBox ID="txtNome" runat="server" Width="259px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNomeFantasia" runat="server" Width="220px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCNPJ_CPF" runat="server" Enabled="False" Width="140px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <uc6:DATA ID="datDataInicio" runat="server" Enabled="false" />                                                
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intNumeroContrato" IndiceTab="0" Enabled="false" />
                                            </td>
                                            <td>
                                                <uc5:MOEDA ID="moeValorContrato" runat="server" IndiceTab="0" Enabled="false" />
                                            </td>    
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hifCodigo" runat="server" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hifValorContrato" runat="server" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hifCodigoResiduo" runat="server" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hifResiduos" runat="server" />
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td id="dadosCab" valign="top" style="font-size:9pt;" colspan="10">
                                    <asp:Label ID="Label1" runat="server" Text="Incluir resíduos" Font-Bold="True" Font-Names="Arial"></asp:Label>
                                 </td>
                            </tr>
                            <tr>
                                <td valign="top">                                    
                                    <table>
                                        <tr><td style="border-top:1px solid; height:10px;">
                                            <asp:GridView ID="GradeResiduos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ClientIDMode="AutoID" Font-Bold="False" Font-Names="verdana" Font-Size="9px" ForeColor="Black" GridLines="Vertical" OnRowCommand="GradeResiduos_RowCommand" OnRowCreated="GradeResiduos_RowCreated" OnRowDataBound="GradeResiduos_RowDataBound" PageSize="5" Width="1800px">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnSalvar" runat="server" CommandArgument="<%# Container.DisplayIndex %>" ImageUrl="~/Images/salvar.png" OnClick="ibnSalvar_Click" ToolTip="Salvar" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnExcluirResiduo" runat="server" CommandArgument="<%# Container.DisplayIndex %>" ImageUrl="~/Images/excluir2.png" OnClick="ibnExcluirResiduo_Click" ToolTip="Excluir" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Código Resíduo" SortExpression="CodigoResiduo">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCodigoResiduo" runat="server" onfocusout="MostraResiduo(this.id);" onkeyup="return PesquisaResiduosGrade(event, this.id);" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" MaxLength="6" Text='<%# Bind("CodigoResiduo") %>' Width="50px" style="text-align:right"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Descrição Resíduo" SortExpression="DescricaoReduzidaResiduo">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDescricaoResiduo" runat="server" BackColor="#FFFFCC" BorderStyle="None" Enabled="false" CssClass="LetrasLabel" Text='<%# Bind("DescricaoReduzidaResiduo") %>' Width="200px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cx Disp" SortExpression="CaixaDisponivel">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCxDisp" runat="server" onkeypress="return isNumberKeyInteiro2Local(event);" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" MaxLength="2" Width="20px" style="text-align:right" Text='<%# Bind("CaixaDisponivel") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo Cx" SortExpression="TipoCaixa">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hifTipoCx" runat="server" Value='<%# Bind("TipoCaixa") %>'></asp:HiddenField>
                                                            <asp:DropDownList ID="ddlTipoCx" runat="server" BackColor="#ffffcc" CssClass="LetrasLabel" BorderStyle="None">
                                                                <asp:ListItem>-</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Frequência Coleta">
                                                        <ItemTemplate>
                                                            <div style="width:140px">
                                                                <asp:TextBox ID="txtQtFrequenciaColeta" runat="server" BackColor="#ffffcc" CssClass="LetrasLabel" BorderStyle="None" MaxLength="2" Width="20px" style="text-align:right" onkeypress="return isNumberKeyInteiro2Local(event);" Text='<%# Bind("FrequenciaColeta") %>'></asp:TextBox>
                                                                <asp:DropDownList ID="ddlFrequenciaColeta" runat="server" BackColor="#ffffcc" CssClass="LetrasLabel" BorderStyle="None">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>-</asp:ListItem>
                                                                    <asp:ListItem>DEMANDA</asp:ListItem>
                                                                    <asp:ListItem>DIÁRIA</asp:ListItem>
                                                                    <asp:ListItem>SEMANAL</asp:ListItem>
                                                                    <asp:ListItem>QUINZENAL</asp:ListItem>
                                                                    <asp:ListItem>MENSAL</asp:ListItem>
                                                                    <asp:ListItem>BIMESTRAL</asp:ListItem>
                                                                    <asp:ListItem>TRIMESTRAL</asp:ListItem>
                                                                    <asp:ListItem>QUADRIMESTRAL</asp:ListItem>
                                                                    <asp:ListItem>SEMESTRAL</asp:ListItem>
                                                                    <asp:ListItem>ANUAL</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Roteiro">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlRoteiro" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="80px" Text='<%# Bind("Roteiro") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>MENSAL</asp:ListItem>
                                                                <asp:ListItem>SEMANAL</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao1CobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="44px" Text='<%# Bind("expressao1CobrancaMensal") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>ATÉ</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Franquia">
                                                        <ItemTemplate>
                                                            <uc5:MOEDA ID="moeFranquiaCobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Valor='<%# Bind("Franquia1CobrancaMensal", "{0:n2}") %>'/>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao2CobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="99px" Text='<%# Bind("expressao2CobrancaMensal") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>COLETA(S) POR</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Periodicidade">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPeriodicidadeCobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="108px" Text='<%# Bind("PeriodicidadeCobrancaMensal") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>DIA</asp:ListItem>
                                                                <asp:ListItem>SEMANA</asp:ListItem>
                                                                <asp:ListItem>QUINZENA</asp:ListItem>
                                                                <asp:ListItem>MÊS</asp:ListItem>
                                                                <asp:ListItem>BIMESTRE</asp:ListItem>
                                                                <asp:ListItem>TRIMESTRE</asp:ListItem>
                                                                <asp:ListItem>QUADRIMESTRE</asp:ListItem>
                                                                <asp:ListItem>SEMESTRE</asp:ListItem>
                                                                <asp:ListItem>ANO</asp:ListItem>
                                                                <asp:ListItem>DEMANDA</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" Width="80px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField>
                                                        <HeaderStyle BackColor="Black" BorderWidth="0" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderStyle BackColor="Black" Width="1px" Font-Size="1"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valor/Excedente">
                                                        <ItemTemplate>
                                                            <uc5:MOEDA ID="moeValorExcedenteCobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Valor='<%# Bind("ValorExcedenteCobrancaMensal", "{0:n2}") %>'/>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" Width="80px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                                <asp:DropDownList ID="ddlexpressao1CobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="78px" Text='<%# Bind("expressao1CobrancaPeso") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>COBRAR</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="54px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valor&nbsp;Unitário" SortExpression="ValorUnitario">
                                                        <ItemTemplate>
                                                            <uc5:MOEDA ID="moeValorUnitario" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Valor='<%# Bind("ValorUnitario", "{0:n2}") %>'/>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="80px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao2CobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="44px" Text='<%# Bind("expressao2CobrancaPeso") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>POR</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Und" SortExpression="Unidade">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnidade" runat="server" BackColor="#ffffcc" onkeyup="Maiusculas(this.id);" BorderStyle="None" CssClass="LetrasLabel" Width="20px" Text='<%# Bind("Unidade") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="20px" />
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="condição">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlcondicaoCobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Text='<%# Bind("condicaoCobrancaPeso") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>EXCEDENTE A</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Franquia">
                                                        <ItemTemplate>
                                                            <uc5:MOEDA ID="moeFranquiaCobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Valor='<%# Bind("FranquiaCobrancaPeso", "{0:n2}") %>'/>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="60px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Und">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnidadeCobrancaPeso" runat="server" onkeyup="Maiusculas(this.id);" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="20px" Text='<%# Bind("UnidadeCobrancaPeso") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="20px" />
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao3CobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="46px" Text='<%# Bind("expressao3CobrancaPeso") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>POR</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="20px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao4CobrancaPeso" runat="server" BackColor="#ffffcc" style="border:none;" BorderStyle="None" CssClass="LetrasLabel" Text='<%# Bind("expressao4CobrancaPeso") %>'>
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>CX</asp:ListItem>
                                                                <asp:ListItem>COLETA</asp:ListItem>
                                                                <asp:ListItem>MÊS</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OBS" SortExpression="OBS">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOBS" runat="server" onkeyup="Maiusculas(this.id);" BackColor="#ffffcc" BorderStyle="None" MaxLength="50" CssClass="LetrasLabel" Width="180px" Text='<%# Bind("OBS") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="180px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dias&nbsp;de&nbsp;Coleta" SortExpression="DiasColeta">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDiasColeta" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="80px" Text='<%# Bind("DiasColeta") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particularidade" SortExpression="Particularidade">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtParticularidade" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="80px" Text='<%# Bind("Particularidade") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mês/Ano Base" SortExpression="MesAnoBase">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMesAnoBase" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="40px" Text='<%# Bind("MesAnoBase") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
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
                                                <div style="width:400px;text-align:left;">
                                                    <input id="btnCancelar" type="button" value="Voltar a tela inicial" onclick="Novo();" />
                                                    <asp:Label ID="lblMensagemResiduos" runat="server" CssClass="LetrasLabel" Font-Bold="true"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td>                                                
                                                <asp:Panel ID="PanelResiduos" runat="server" style="visibility: hidden;">
                                                    <table>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="contentTopRightDiv" style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;">
                                                                    <asp:GridView ID="Grade" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ClientIDMode="AutoID" Font-Bold="False" Font-Names="verdana" Font-Size="9px" ForeColor="Black" GridLines="Vertical" PageSize="6" TabIndex="1" OnRowCommand="Grade_RowCommand" OnSorting="Grade_Sorting" OnPageIndexChanging="Grade_PageIndexChanging1">
                                                                        <AlternatingRowStyle BackColor="AliceBlue" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Selecionar Resíduo">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ibnConsultar" runat="server" CommandArgument="<%# Container.DisplayIndex%>" ImageUrl="~/Images/selecionar.png" ToolTip="Selecionar" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Codigo" HeaderText="Código Resíduo" SortExpression="Codigo">
                                                                            <HeaderStyle CssClass="padItemGrade" />
                                                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição Resíduo" SortExpression="DescricaoReduzida">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="padItemGrade" Width="350px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Grupo" HeaderText="Grupo Resíduo" SortExpression="Grupo">
                                                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="CodigoIBAMA_Analitico" HeaderText="Código IBAMA" SortExpression="CodigoIBAMA_Analitico">
                                                                            <HeaderStyle Width="120px" />
                                                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Classe" HeaderText="Classe" SortExpression="Classe">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="60px" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <FooterStyle BackColor="#CCCCCC" />
                                                                        <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
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
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblFiltro" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px">&nbsp;Procurar:</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                                                                <asp:ListItem Value="Codigo">Código</asp:ListItem>
                                                                                <asp:ListItem Value="DescricaoReduzida">Residuo</asp:ListItem>
                                                                                <asp:ListItem>Grupo</asp:ListItem>
                                                                                <asp:ListItem>Ativos</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtFiltro" runat="server" Width="400px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Ok" />
                                                                        </td>
                                                                    </tr>
                                                                </table>                                                    
                                                            </td>
                                                        </tr>                                                        
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>    
                               </td>
                                <td>
                                    <asp:DropDownList ID="p_ddlTiposDeCaixas" runat="server" style="visibility:hidden">
                                    </asp:DropDownList>
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
