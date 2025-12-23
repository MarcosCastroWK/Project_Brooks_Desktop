<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contratos_Reajustes.aspx.cs" Inherits="Contratos_Reajustes" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc7" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc8" %>
<%@ Register Src="~/forms/INTEIRO7.ascx" TagPrefix="uc1" TagName="INTEIRO7" %>
<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc3" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc9" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>

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
    function InabilitarBotaoSalvar(pBotao) {
        document.getElementById(pBotao).style.visibility = 'hidden';
    }
    function PegaDataProximoReajuste(pId) {
        var sCampoTipoNegociacao = pId.replace('ddlTipoNegociacao', 'datDataReajuste_txtData');
        var sCampoProximoReajuste = pId.replace('ddlTipoNegociacao', 'ProximoReajuste_txtData');
        if (document.getElementById(pId).value.indexOf('REAJUSTE') > -1) {
            document.getElementById(sCampoTipoNegociacao).value = document.getElementById('hifDataReajusteSelecionado').value;
            var _ano = document.getElementById('hifDataReajusteSelecionado').value.substring(6);
            var _dtreaj = document.getElementById('hifDataReajusteSelecionado').value.substring(0, 6) + (parseFloat(_ano) + 1);
            document.getElementById(sCampoProximoReajuste).value = _dtreaj;
        }
        else {
            var data = new Date();
            dia = data.getDate().toString();
            diaF = (dia.length == 1) ? '0' + dia : dia;
            mes = (data.getMonth() + 1).toString(); //+1 pois no getMonth Janeiro começa com zero.
            mesF = (mes.length == 1) ? '0' + mes : mes;
            anoF = data.getFullYear();
            document.getElementById(sCampoTipoNegociacao).value = diaF + '/' + mesF + '/' + anoF;
            document.getElementById(sCampoProximoReajuste).value = document.getElementById('hifDataReajuste').value;
        }            
    }
    function Maiusculas(textbox) {
        var _str = '';
        _str = document.getElementById(textbox).value;     
        document.getElementById(textbox).value = _str.toUpperCase();
    }
    function Novo() {
        var r = confirm("Confirma?");
        if (r == true) {
            window.location = 'Contratos_ReajustarRepactuar.aspx';
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
    function HabilitaSalvarReajuste(pId) {
        if (pId.substring(0, 21) + 'ibnSalvarReajuste' != "") {
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').src = '../Images/salvar.png';
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').disabled  = false;
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
                        
                        <uc2:menu ID="menu1" runat="server" />
                        
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="Reajustar/Repactuar" Font-Bold="True" Font-Names="Arial"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <asp:GridView ID="Grade" runat="server" CellPadding="0" Width="1300px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="Grade_PageIndexChanging" >
                            <AlternatingRowStyle BackColor="AliceBlue" />
                            <Columns>
                                <asp:TemplateField HeaderText="Selecionar Contrato">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibnSelecionar" runat="server" ImageUrl="~/Images/selecionar.png" ToolTip="Selecionar" OnClick="ibnSelecionar_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
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
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social" SortExpression="Nome">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="padItemGrade" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ValorContrato" HeaderText="Valor Contrato" SortExpression="ValorContrato" DataFormatString="{0:n2}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DiaVencimento" HeaderText="Dia Vencimento" SortExpression="DiaVencimento">
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataReajuste" DataFormatString="{0: dd/MM/yyyy}" HeaderText="Data Reajuste" SortExpression="DataReajuste">
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IndiceReajuste" HeaderText="Indice Reajuste" SortExpression="IndiceReajuste">
                                <ItemStyle CssClass="padItemGrade" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataTermino" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Vencimento" SortExpression="DataTermino">
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataRecisao" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Recisão" SortExpression="DataRecisao" />
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
                                    <asp:Button ID="btnContratos" runat="server" Text="Total contratos" Width="109px" BorderStyle="None"/>
                                </td>
                                <td style="width:84px; text-align: right; font-size:12px;"><asp:Label ID="lblTotal" runat="server" Text="0,00"></asp:Label></td>
                            </tr>
                        </table>
                     </td>   
                </tr>
                <tr><td colspan="6" style="border-top:1px solid;"></td></tr>
                <tr>
                    <td id="dadosForm" valign="top">
                        <table>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <asp:HiddenField ID="hifValorContrato" runat="server" />                                                
                                </td>
                                <td>
                                    <asp:HiddenField ID="hifCodigoCliente" runat="server" />
                                </td>
                                <td>
                                    <asp:HiddenField ID="hifCodigoResiduo" runat="server" />
                                </td>
                                <td>
                                    <asp:HiddenField ID="hifResiduos" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left; vertical-align:top;">
                                    <asp:GridView ID="GradeReajustes" runat="server" CellPadding="0" Width="400px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" AllowSorting="True" Font-Bold="False" PageSize="5" OnRowCommand="GradeReajustes_RowCommand" OnSorting="GradeReajustes_Sorting" OnRowDataBound="GradeReajustes_RowDataBound" OnRowCreated="GradeReajustes_RowCreated">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgResiduosContratado" runat="server" ImageUrl="~/Images/mudarmostrar2.png" ToolTip="Resíduos contratado" OnClick="ibnMudar1_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibnSalvarReajuste" runat="server" ImageUrl="~/Images/salvarcinza.png" ToolTip="Salvar" OnClick="ibnSalvarReajuste_Click" Enabled="false" CommandArgument='<%# Container.DisplayIndex %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="1px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibnExcluirReajustes" runat="server" ImageUrl="~/Images/excluir2.png" ToolTip="Excluir" OnClick="ibnExcluirReajustes_Click" />
                                                </ItemTemplate>
                                                <ItemStyle Width="1px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Data Reajuste">
                                                <ItemTemplate>
                                                    <uc6:DATA ID="datDataReajuste" runat="server" Style="width: 60px; text-align: center;" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Data='<%# Bind("Data", "{0:dd/MM/yyyy}") %>'/>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo de Negociação">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hifTipoNegociacao" runat="server" Value='<%# Bind("TipoNegociacao") %>'></asp:HiddenField>
                                                    <asp:DropDownList ID="ddlTipoNegociacao" runat="server" Width="304px" style="border: 0px; border-spacing: 2px;" CssClass="LetrasLabel" onfocus="HabilitaSalvarReajuste(this.id);" onchange="PegaDataProximoReajuste(this.id);">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="padItemGrade" Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contrato" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <uc5:MOEDA ID="moePercentualContrato" runat="server" BorderStyle="None" CssClass="LetrasLabel" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Preços Unitários" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <uc5:MOEDA ID="moePercentualUnitarios" runat="server" BorderStyle="None" CssClass="LetrasLabel" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor&nbsp;Contrato">
                                                <ItemTemplate>
                                                    <uc5:MOEDA ID="moeValorContrato" runat="server" BorderStyle="None" CssClass="LetrasLabel" Valor='<%# Bind("Valor", "{0:n2}") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contrato Atual">
                                                <ItemTemplate>
                                                    <uc1:INTEIRO7 ID="intNumeroContrato" runat="server" BorderStyle="None" Style="text-align: right;" CssClass="LetrasLabel" Valor='<%# Bind("NumeroContrato") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Situação">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hifSituacao" runat="server" Value='<%# Bind("Situacao") %>'></asp:HiddenField>
                                                    <asp:DropDownList ID="ddlSituacao" runat="server" BorderStyle="none" CssClass="LetrasLabel" Width="220px" onfocus="HabilitaSalvarReajuste(this.id);">                                                   
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="padItemGrade" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Sequencial" HeaderText="Seq">
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"  />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Próximo Reajuste">
                                                <ItemTemplate>
                                                    <uc6:DATA ID="ProximoReajuste" runat="server" Style="width: 60px; text-align: center;" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Observação">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtObservacao" runat="server" BackColor="#FFFFCC" BorderStyle="None" MaxLength="50" onfocus="HabilitaSalvarReajuste(this.id);" CssClass="LetrasLabel" Text='<%# Bind("Observacao") %>' Width="300px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
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
                                <td style="text-align: left; vertical-align:top;">
                                    <asp:HiddenField ID="hifSequencialReajuste" runat="server" />                                                
                                    <asp:HiddenField ID="hifDataReajuste" runat="server" />                                                
                                    <asp:DropDownList ID="p_ddlSituacao" runat="server" style="visibility:hidden"> 
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="p_ddlTiposNegociacao" runat="server" style="visibility:hidden"> 
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <table>
                                        <tr>
                                            <td style="height:10px;"> 
                                                <asp:Label ID="lblTituloResiduos" runat="server" CssClass="LetrasLabel" Font-Bold="True" Width="400px">Resíduos Contratados</asp:Label>
                                                <asp:GridView ID="GradeResiduos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ClientIDMode="AutoID" Font-Bold="False" Font-Names="verdana" Font-Size="9px" ForeColor="Black" GridLines="Vertical" OnRowCommand="GradeResiduos_RowCommand" OnRowCreated="GradeResiduos_RowCreated" OnRowDataBound="GradeResiduos_RowDataBound" PageSize="5" Width="1800px">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnSalvar" runat="server" CommandArgument="<%# Container.DisplayIndex %>" ImageUrl="~/Images/salvar.png" OnClick="ibnSalvar_Click" ToolTip="Salvar" OnClientClick="InabilitarBotaoSalvar(this.id);" />
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
                                                            <asp:TextBox ID="txtCxDisp" runat="server" onkeypress="return isNumberKeyInteiro7(event);" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" MaxLength="2" Width="20px" style="text-align:right" Text='<%# Bind("CaixaDisponivel") %>'></asp:TextBox>
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
                                                                    <asp:ListItem>-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Roteiro" SortExpression="Roteiro">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlRoteiro" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="80px">
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>MENSAL</asp:ListItem>
                                                                <asp:ListItem>SEMANAL</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hifRoteiro" runat="server" Value='<%# Bind("Roteiro") %>'></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao1CobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="44px" Text='<%# Bind("expressao1CobrancaMensal") %>'>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>ATÉ</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Franquia">
                                                        <ItemTemplate>
                                                            <uc5:MOEDA ID="moeFranquiaCobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Valor='<%# Bind("QuantidadeFranquia", "{0:n2}") %>'/>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao2CobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="110px" Text='<%# Bind("expressao2CobrancaMensal") %>'>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>COLETA(S) POR</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Periodicidade">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPeriodicidadeCobrancaMensal" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="104px" Text='<%# Bind("PeriodicidadeCobrancaMensal") %>'>
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
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="Black" Width="80px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField>
                                                    <HeaderStyle BackColor="Black" Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle BackColor="Black" Width="1px" />
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
                                                                <asp:DropDownList ID="ddlexpressao1CobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="68px" Text='<%# Bind("expressao1CobrancaPeso") %>'>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>COBRAR</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
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
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>POR</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Und" SortExpression="Unidade">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnidade" runat="server" onkeyup="Maiusculas(this.id);" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="20px" Text='<%# Bind("Unidade") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="20px" />
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="condição">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlcondicaoCobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Text='<%# Bind("condicaoCobrancaPeso") %>'>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>EXCEDENTE A</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
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
                                                            <asp:TextBox ID="txtUnidadeCobrancaPeso" runat="server" BackColor="#ffffcc" onkeyup="Maiusculas(this.id);" BorderStyle="None" CssClass="LetrasLabel" Width="20px" Text='<%# Bind("UnidadeCobrancaPeso") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="20px" />
                                                        <ItemStyle CssClass="padItemGrade" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao3CobrancaPeso" runat="server" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Width="46px" Text='<%# Bind("expressao3CobrancaPeso") %>'>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>POR</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="20px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlexpressao4CobrancaPeso" runat="server" BackColor="#ffffcc" style="border:none;" BorderStyle="None" CssClass="LetrasLabel" Text='<%# Bind("expressao4CobrancaPeso") %>'>
                                                                <asp:ListItem>-</asp:ListItem>
                                                                <asp:ListItem>CX</asp:ListItem>
                                                                <asp:ListItem>COLETA</asp:ListItem>
                                                                <asp:ListItem>MÊS</asp:ListItem>
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#FF9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OBS" SortExpression="OBS">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOBS" runat="server" BackColor="#ffffcc" onkeyup="Maiusculas(this.id);" MaxLength="50" BorderStyle="None" CssClass="LetrasLabel" Width="180px" Text='<%# Bind("OBS") %>'></asp:TextBox>
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
                                                            <asp:TextBox ID="txtParticularidade" runat="server" BackColor="#ffffcc" onkeyup="Maiusculas(this.id);" BorderStyle="None" CssClass="LetrasLabel" Width="80px" Text='<%# Bind("Particularidade") %>'></asp:TextBox>
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
                                                <input id="btnCancelar" type="button" value="Voltar a tela inicial" onclick="Novo();" />
                                                <asp:Label ID="lblMensagemResiduos" runat="server" CssClass="LetrasLabel" Font-Bold="True" Height="20px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Panel ID="PanelResiduos" runat="server" style="visibility: hidden;">
                                                    <table valign="top">
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="contentTopRightDiv" style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;">
                                                                    <asp:GridView ID="GradePesquisa" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ClientIDMode="AutoID" Font-Bold="False" Font-Names="verdana" Font-Size="9px" ForeColor="Black" GridLines="Vertical" PageSize="6" TabIndex="1" OnPageIndexChanging="GradePesquisa_PageIndexChanging1" OnSorting="GradePesquisa_Sorting" OnRowCommand="GradePesquisa_RowCommand">
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
                                                                            <asp:Label ID="lblFiltroPesquisa" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px">&nbsp;Procurar:</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlFiltroPesquisa" runat="server" Width="200px">
                                                                                <asp:ListItem Value="Codigo">Código</asp:ListItem>
                                                                                <asp:ListItem Value="DescricaoReduzida">Residuo</asp:ListItem>
                                                                                <asp:ListItem>Grupo</asp:ListItem>
                                                                                <asp:ListItem>Ativos</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtFiltroPesquisa" runat="server" Width="400px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnOkPesquisa" runat="server" OnClick="btnOkPesquisa_Click" Text="Ok" />
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
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="p_ddlTiposDeCaixas" runat="server" style="visibility:hidden">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="p_ddlFrequenciaColeta" runat="server" style="visibility:hidden">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hifDataReajusteSelecionado" runat="server" />
        </div>
    </form>
</body>
</html>
