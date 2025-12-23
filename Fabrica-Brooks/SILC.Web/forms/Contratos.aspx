<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contratos.aspx.cs" Inherits="SILC.Web.forms.webContratos" %>
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
    <style type="text/css">
        .auto-style2 {
            height: 11px;
        }
        .auto-style3 {
            height: 259px;
        }
        .auto-style4 {
            width: 1200px;
            height: 259px;
        }
        .auto-style5 {
            width: 364px;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function AbrePesquisaClientes() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('../forms/PesquisaClientes.aspx');
    }
    function AbrePesquisaGrupoResiduos() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('../forms/PesquisaGrupoResiduos.aspx');
    }
    function Maiusculas(textbox) {
        var _str = '';
        _str = document.getElementById(textbox).value;     
        document.getElementById(textbox).value = _str.toUpperCase();
    }
    function CalcularPercentual() {
        var vc = document.getElementById('hifValorContrato').value.replace(".", "").replace(",", ".");
        var vca = document.getElementById('moeValorContrato1').value.replace(".", "").replace(",", ".");
        moePercentualReajuste.value = (parseFloat((vca - vc) * 100) / vc).toFixed(6).replace(".", ",");
    }
    function CalcularValorContrato() {
        var vc = document.getElementById('hifValorContrato').value.replace(".", "").replace(",", ".");
        var vp = document.getElementById('moePercentualReajuste').value.replace(".", "").replace(",", ".") / 100 + 1;
        var vr = vc * vp ;
        moeValorContrato1.value = parseFloat(vr).toFixed(2).replace(".", ",");

        if (document.getElementById('moePercentualReajuste').value.replace(",", ".") > 0) {
            txtTipoNegociacao1.value = 'REAJUSTE';
            datDataReajuste1.value = datDataReajuste.value;           
            var _dia = datDataReajuste.value[8] + datDataReajuste.value[9];
            var _mes = parseInt(datDataReajuste.value[5] + datDataReajuste.value[6]);
            var _ano4 = parseInt(datDataReajuste.value[0] + datDataReajuste.value[1] + datDataReajuste.value[2] + datDataReajuste.value[3]) + 1;
            var _str_data = _ano4 + '-' + ("0" + (_mes)).slice(-2) + '-' + ("0" + _dia).slice(-2);       
            datDataReajuste2.value = _str_data;
        }
        else {
            var _data = new Date();
            var _dia  = _data.getDate();           // 1-31
            var _mes  = _data.getMonth();          // 0-11 (zero=janeiro)
            var _ano4 = _data.getFullYear();       // 4 dígitos
            var _str_data = _ano4 + '-' + ("0"+(_mes + 1)).slice(-2) + '-' + ("0"+_dia).slice(-2);       
            datDataReajuste1.value = _str_data;
            datDataReajuste2.value = datDataReajuste.value;
            txtTipoNegociacao1.value = 'REPACTUAÇÃO';
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Contratos" Font-Bold="True" Font-Names="Arial"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <asp:GridView ID="Grade" runat="server" CellPadding="0" Width="1300px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="Grade_PageIndexChanging" >
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibnMudar" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" Enabled="false" ToolTip="Excluir" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Sequencial" SortExpression="Codigo">
                                <HeaderStyle CssClass="padItemGrade" />
                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
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
                                <asp:BoundField DataField="DataReajuste" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Reajuste" SortExpression="DataReajuste">
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
                                        <asp:ListItem>Código Cliente</asp:ListItem>
                                        <asp:ListItem Value="Nome"></asp:ListItem>
                                        <asp:ListItem>NomeFantasia</asp:ListItem>
                                        <asp:ListItem>CNPJ_CPF</asp:ListItem>
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
                <tr><td colspan="6" style="border-top:1px solid;"></td></tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:90%">
                        <table>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNome0" CssClass="LetrasLabel" runat="server" Width="104px" Font-Bold="False" >&nbsp;Código Cliente</asp:Label>
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intCodigoCliente" IndiceTab="1" Enabled="False" />
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCNPJ_CPF" CssClass="LetrasLabel" runat="server" Font-Bold="False">CNPJ/CPF:</asp:Label>                                    
                                            </td>
                                            <td>                                    
                                                <asp:TextBox ID="txtCNPJ_CPF" runat="server" Enabled="False" onfocus="LimpaErro()" TabIndex="1" Width="140px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <img id="imagem" alt="x" src="../Images/procura2.png" style="cursor:pointer;" onclick="AbrePesquisaClientes()"/>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" OnClick="btnProcurar_Click1" />            
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNome" CssClass="LetrasLabel" runat="server" Width="104px" Font-Bold="False" >&nbsp;Nome/Razão Social</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNome" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNomeFantasia" CssClass="LetrasLabel" runat="server" Width="80px" Font-Bold="False" >&nbsp;Nome Fantasia</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNomeFantasia" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;Observação</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtObservacao" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="300px" ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Width="104px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Data Recisão</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="datDataRecisao" runat="server" Width="125px" TextMode="Date"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Width="99px" CssClass="LetrasLabel">Documento Recisão</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDocumentoRecisao" runat="server">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>TERMO RESCISÃO ASSINADO</asp:ListItem>
                                                    <asp:ListItem>CANCELAMENTO VERBAL</asp:ListItem>
                                                    <asp:ListItem>CARTA DE RESCISÃO</asp:ListItem>
                                                    <asp:ListItem>AGUARDANDO TERMO ASSINADO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" CssClass="LetrasLabel" runat="server" Width="71px" Font-Bold="False" Height="16px">&nbsp;Data Registro</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="datDataRegistro" runat="server" Width="125px" TextMode="Date"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMotivoRecisao" CssClass="LetrasLabel" runat="server" Width="120px" Font-Bold="False" >Motivo/Descrição Recisão</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMotivoRecisao" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                   <table>
                                       <tr>
                                            <td>
                                                <asp:Label ID="Label11" CssClass="LetrasLabel" runat="server" Width="104px" Font-Bold="False">Número Contrato</asp:Label>
                                            </td>
                                            <td>
                                                <uc1:INTEIRO7 runat="server" ID="intNumeroContrato" IndiceTab="1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl1" CssClass="LetrasLabel" runat="server" Width="60px" Font-Bold="False" >Data Início</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="datDataInicio" runat="server" Width="125px" TextMode="Date"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" CssClass="LetrasLabel" runat="server" Width="70px" Font-Bold="False" >Data Término</asp:Label></td>                                            
                                            <td>
                                                <asp:TextBox ID="datDataTermino" runat="server" Width="125px" TextMode="Date"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" CssClass="LetrasLabel" runat="server" Width="60px" Font-Bold="False" >Aniversário</asp:Label>
                                            </td>                                            
                                            <td>
                                                <asp:TextBox ID="txtAniversarioReajuste" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="60px" ></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl0" CssClass="LetrasLabel" runat="server" Width="66px" Font-Bold="False" >Próx Reajuste</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="datDataReajuste" runat="server" Width="125px" TextMode="Date"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIndiceReajuste2" CssClass="LetrasLabel" runat="server" Width="77px" Font-Bold="False" >Indice Reajuste</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIndiceReajuste" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="60px" ></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl3" CssClass="LetrasLabel" runat="server" Width="77px" Font-Bold="False" >Caixas Locadas</asp:Label>
                                            </td>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCaixasLocadas" runat="server" IndiceTab="2" />
                                            </td>
                                        </tr>
                                   </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Text="&nbsp;Valor atual Contrato" Width="104px" CssClass="LetrasLabel"></asp:Label>
                                            </td>
                                            <td>
                                                
                                                <uc5:MOEDA ID="moeValorContrato" runat="server" />
                                            </td>
                                            <td>
                                                 <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Width="80px" Font-Bold="False">Dia p/Vencimento</asp:Label>
                                            </td>
                                            <td>
                                                 <uc1:INTEIRO7 runat="server" ID="intDiaVencimento" IndiceTab="1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hifCodigo" runat="server" />
                                            </td>
                                            <td>
                                                <div style="width:700px;text-align:right;">
                                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hifValorContrato" runat="server" />
                                                </div>
                                            </td>
                                            <td>
                                                <div style="width:300px;text-align:left;" >
                                                    <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr><td colspan="6" style="border-top:1px solid;"></td></tr>
                            <tr>
                                <td colspan="4" style="text-align: left; vertical-align:top;" class="auto-style3">
                                    <asp:GridView ID="GradeReajustes" runat="server" CellSpacing="0" CellPadding="0" Width="700px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" AllowSorting="True" Font-Bold="False" PageSize="5" OnRowCommand="GradeReajustes_RowCommand" OnSorting="GradeReajustes_Sorting" OnRowDataBound="GradeReajustes_RowDataBound">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibnMudar0" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar1_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibnExcluirReajustes" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" CommandArgument='<%# Container.DisplayIndex %>' OnClick="ibnExcluirReajustes_Click" />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Data" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Reajuste">
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="90px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Valor" HeaderText="Valor Contrato" DataFormatString="{0:n2}" >
                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemStyle  CssClass="padItemGrade" HorizontalAlign="Right" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumeroContrato" HeaderText="Contrato Atual">
                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Situacao" HeaderText="Situação">
                                            <ItemStyle CssClass="padItemGrade"  Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TipoNegociacao" HeaderText="Tipo de Negociação">
                                            <ItemStyle CssClass="padItemGrade" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sequencial" HeaderText="Sequencial">
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"  />
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
                                <td colspan="4" style="text-align: left; vertical-align:top;" class="auto-style4">
                                    <asp:Panel ID="PanelReajuste" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTituloReajustes" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="110px">Alteração de Reajuste</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">Sequencial:</asp:Label>
                                                </td>
                                                <td>
                                                    <uc1:INTEIRO7 runat="server" ID="intSequencial" IndiceTab="1" Enabled="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCodigoCliente1" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">Código Cliente:</asp:Label>
                                                </td>
                                                <td>
                                                    <uc1:INTEIRO7 runat="server" ID="intCodigoCliente1" IndiceTab="1" Enabled="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDataReajuste1" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">Data Reajuste:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="datDataReajuste1" runat="server" Width="125px" TextMode="Date"></asp:TextBox>
                                                    <asp:Label ID="lblDataReajuste2" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="50px">Próximo Reajuste</asp:Label>
                                                    <asp:TextBox ID="datDataReajuste2" runat="server" TextMode="Date" Width="125px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Text="Valor Contrato:" Width="100px" CssClass="LetrasLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <div>
                                                        <asp:TextBox ID="moeValorContrato1" runat="server" Enabled="true" TextMode="SingleLine" Width="100px" onfocusout="CalcularPercentual();" ></asp:TextBox>
                                                        <asp:TextBox ID="moePercentualReajuste" runat="server" TextMode="SingleLine" Width="100px" onfocusout="CalcularValorContrato();"></asp:TextBox>
                                                        <asp:Label ID="lblPercentualReajuste" runat="server" Text="%" Height="20px"></asp:Label>
                                                        <input id="btnCalc" type="button" value="Calc" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblContratoAtual1" runat="server" Font-Bold="False" Text="Contrato atual:" Width="100px" CssClass="LetrasLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <uc1:INTEIRO7 ID="intNumeroContrato1" runat="server" IndiceTab="1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Text="Situação:" Width="100px" CssClass="LetrasLabel"></asp:Label>
                                                </td>                                                
                                                <td>
                                                    <asp:DropDownList ID="txtSituacao1" runat="server" Width="308px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>SEM CONTRATO</asp:ListItem>
                                                        <asp:ListItem>EM ANÁLISE/REDAÇÃO</asp:ListItem>
                                                        <asp:ListItem>AGUARDANDO ASSINATURA CLIENTE</asp:ListItem>
                                                        <asp:ListItem>ASSINADO/ARQUIVADO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Tipo de negociação:" Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="txtTipoNegociacao1" runat="server" Width="304px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>REAJUSTE</asp:ListItem>
                                                        <asp:ListItem>REPACTUAÇÃO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnOkReajuste" runat="server" CommandName="Salvar" Font-Size="10pt" Text="Ok" OnClick="btnOkReajuste_Click" />
                                                    <asp:Button ID="btnCancelaAlteracaoReajuste" runat="server" Text="Cancelar" OnClick="btnCancelaAlteracaoReajuste_Click" />
                                                </td>
                                            </tr>
                                        </table>                                    
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GradeResiduos" runat="server" CellPadding="1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" 
                                                    Width="1800px" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False"
                                                    ForeColor="Black" GridLines="Vertical" Font-Bold="False" PageSize="5" OnRowCreated="GradeResiduos_RowCreated" OnRowDataBound="GradeResiduos_RowDataBound" OnRowCommand="GradeResiduos_RowCommand" >
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibnMudar1" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar1_Click1" CommandArgument='<%# Container.DisplayIndex %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ibnExcluirResiduo" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" CommandArgument='<%# Container.DisplayIndex %>' OnClick="ibnExcluirResiduo_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CodigoContrato" HeaderText="Seq Contrato" SortExpression="CodigoContrato">
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodigoResiduo" HeaderText="Código Resíduo" SortExpression="CodigoResiduo">
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DescricaoReduzidaResiduo" HeaderText="Descrição Resíduo" SortExpression="DescricaoReduzidaResiduo">
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CaixaDisponivel" HeaderText="Cx Disp" SortExpression="CaixaDisponivel">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoCaixa" HeaderText="Tp Cx" SortExpression="TipoCaixa">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FrequenciaColeta" HeaderText="Frequência Coleta" SortExpression="FrequenciaColeta">
                                                        <HeaderStyle Width="120px" />
                                                        </asp:BoundField>    
                                                        <asp:BoundField DataField="Roteiro" HeaderText="Roteiro" SortExpression="Roteiro">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="expressao1CobrancaMensal"  HeaderText="expr">
                                                        <HeaderStyle BackColor="Black" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="franquia1CobrancaMensal" HeaderText="Franquia">
                                                        <HeaderStyle BackColor="Black" />
                                                        <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="expressao2CobrancaMensal" HeaderText="expressão">
                                                        <HeaderStyle BackColor="Black" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PeriodicidadeCobrancaMensal" HeaderText="Periodicidade">
                                                        <HeaderStyle BackColor="Black" Width="80px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorUnitarioCobrancaMensal" DataFormatString="{0:n2}" HeaderText="Valor&nbsp;Unitário" >
                                                            <HeaderStyle BackColor="Black" Width="100px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorContratoCobrancaMensal" DataFormatString="{0:n2}" HeaderText="Valor&nbsp;Contrato" >
                                                        <HeaderStyle BackColor="Black" Width="100px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorExcedenteCobrancaMensal" DataFormatString="{0:n2}" HeaderText="Valor&nbsp;Excedente" >
                                                            <HeaderStyle BackColor="Black" Width="10" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="expressao1CobrancaPeso" HeaderText="expressão">
                                                        <HeaderStyle BackColor="#ff9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorUnitario" DataFormatString="{0:n2}" HeaderText="Valor&nbsp;Unitário" SortExpression="ValorUnitario">
                                                            <HeaderStyle BackColor="#ff9900" Width="80px" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="expressao2CobrancaPeso" HeaderText="expressão">
                                                        <HeaderStyle BackColor="#ff9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" >
                                                            <HeaderStyle BackColor="#ff9900" Width="60px" />
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="condicaoCobrancaPeso" HeaderText="condição">
                                                        <HeaderStyle BackColor="#ff9900" Width="90px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FranquiaCobrancaPeso" HeaderText="Franquia">
                                                            <HeaderStyle BackColor="#ff9900" Width="60px" />
                                                        <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UnidadeCobrancaPeso" HeaderText="Unidade" >
                                                            <HeaderStyle BackColor="#ff9900" Width="60px" />
                                                        <ItemStyle CssClass="padItemGrade"  />
                                                        </asp:BoundField>

                                                        <asp:BoundField HeaderText="expressão" DataField="expressao3CobrancaPeso">
                                                        <HeaderStyle BackColor="#ff9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center"  />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="expressão" DataField="expressao4CobrancaPeso">
                                                        <HeaderStyle BackColor="#ff9900" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center"  />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OBS" HeaderText="OBS" SortExpression="OBS">
                                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="400px"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DataReajuste" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data&nbsp;Reajuste" SortExpression="Data">
                                                        <HeaderStyle CssClass="padItemGrade" />
                                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DiasColeta" HeaderText="Dias&nbsp;de&nbsp;Coleta" SortExpression="DiasColeta">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Particularidade" HeaderText="Particularidade" SortExpression="Particularidade">
                                                        <ItemStyle CssClass="padItemGrade" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MesAnoBase" HeaderText="Mês/Ano Base" SortExpression="MesAnoBase">
                                                        <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" />
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
                                <td colspan="8">
                                    <asp:Panel ID="PanelResiduos" runat="server">
                                        <table>
                                            <tr>
                                                <td colspan="13">
                                                    <asp:Label ID="lblTituloResiduos" runat="server" CssClass="LetrasLabel" Font-Bold="True" Width="400px">Alteração de Resíduos Contratados</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" CssClass="LetrasLabel" runat="server" Width="77px" Font-Bold="False">Código Contrato:</asp:Label>
                                                </td>
                                                <td>
                                                    <uc1:INTEIRO7 ID="intCodigoContrato_R" runat="server" IndiceTab="1" Enabled="false" />
                                                </td>
                                                <td class="auto-style5">
                                                    <uc7:GRUPORESIDUO ID="GRUPORESIDUO_R" runat="server" />
                                                </td>
                                                <td>
                                                    <img id="imagem2" alt="x" style="cursor:pointer;" onclick="AbrePesquisaGrupoResiduos()" src="../Images/procura2.png" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCxDisp" CssClass="LetrasLabel" runat="server" Font-Bold="False">Cx.Disp:</asp:Label>
                                                </td>
                                                <td>
                                                    <uc3:INTEIRO2 ID="intCxDisp" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTipoCx" CssClass="LetrasLabel" runat="server" Font-Bold="False">Tipo.Cx:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTipoCx" runat="server" onkeyup="Maiusculas('txtTipoCx');" TextMode="SingleLine" Width="50px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFrequenciColeta" CssClass="LetrasLabel" runat="server" Font-Bold="False">Frequência Coleta:</asp:Label>
                                                </td>
                                                <td>
                                                    <uc3:INTEIRO2 ID="intQtFrequenciaColeta" runat="server" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtFrequenciaColeta" runat="server" onkeyup="Maiusculas('txtFrequenciaColeta');" TextMode="SingleLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" CssClass="LetrasLabel" runat="server" Font-Bold="False">Roteiro:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoteiro" runat="server" onkeyup="Maiusculas('txtRoteiro');" TextMode="SingleLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="13" class="auto-style2">
                                                    <asp:Label ID="Label17" CssClass="LetrasLabel" runat="server" Font-Bold="true" Width="400px">DADOS PARA COBRANÇA MENSAL</asp:Label>
                                                </td>
                                            </tr>
                                            <tr> 
                                                <td colspan="13">
                                                    <table>
                                                        <tr>                                                        
                                                            <td>
                                                                <asp:DropDownList ID="ddlexpressao1CobrancaMensal" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>até</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFranquia" CssClass="LetrasLabel" runat="server" Width="60px" Font-Bold="False">Franquia:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc5:MOEDA ID="moeFranquiaCobrancaMensal" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlexpressao2CobrancaMensal" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>Coleta(s) por</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label19" CssClass="LetrasLabel" runat="server" Width="60px" Font-Bold="False">Periodicidade:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPeriodicidadeCobrancaMensal" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>dia</asp:ListItem>
                                                                    <asp:ListItem>semana</asp:ListItem>
                                                                    <asp:ListItem>quinzena</asp:ListItem>
                                                                    <asp:ListItem>mês</asp:ListItem>
                                                                    <asp:ListItem>bimestre</asp:ListItem>
                                                                    <asp:ListItem>trimestre</asp:ListItem>
                                                                    <asp:ListItem>quadrimestre</asp:ListItem>
                                                                    <asp:ListItem>semestre</asp:ListItem>
                                                                    <asp:ListItem>ano</asp:ListItem>
                                                                    <asp:ListItem>demanda</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label20" CssClass="LetrasLabel" runat="server" Font-Bold="False">Valor&nbsp;unitário:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc5:MOEDA ID="moeValorUnitarioCobrancaMensal" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label21" CssClass="LetrasLabel" runat="server" Font-Bold="False">Valor&nbsp;Contrato:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc5:MOEDA ID="moeValorContratoCobrancaMensal" runat="server" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label22" CssClass="LetrasLabel" runat="server" Font-Bold="False">Valor&nbsp;Excedente:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc5:MOEDA ID="moeValorExcedenteCobrancaMensal" runat="server" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="13" class="auto-style2">
                                                    <asp:Label ID="Label28" CssClass="LetrasLabel" runat="server" Font-Bold="true" Width="400px">DADOS DA COBRANÇA POR PESO/VOLUME</asp:Label>
                                                </td>
                                            </tr>
                                            <tr> 
                                                <td colspan="13">
                                                    <table>
                                                        <tr>                                                        
                                                            <td>
                                                                <asp:DropDownList ID="ddlexpressao1CobrancaPeso" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>cobrar</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label29" CssClass="LetrasLabel" runat="server" Font-Bold="False">Valor&nbsp;unitário:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc5:MOEDA ID="moeValorUnitarioCobrancaPeso" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlexpressao2CobrancaPeso" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>por</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label24" CssClass="LetrasLabel" runat="server" Width="60px" Font-Bold="False">Unidade:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnidadeCobrancaPeso" runat="server" onfocus="LimpaErro()" Width="30px" TextMode="SingleLine"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlcondicaoCobrancaPeso" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>excedente a</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label23" CssClass="LetrasLabel" runat="server" Width="50px" Font-Bold="False">Franquia:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc5:MOEDA ID="moeFranquiaCobrancaPeso" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnidade2CobrancaPeso" runat="server" onfocus="LimpaErro()" Width="30px" TextMode="SingleLine"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlexpressao3CobrancaPeso" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>por</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlexpressao4CobrancaPeso" runat="server">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>cx</asp:ListItem>
                                                                    <asp:ListItem>coleta</asp:ListItem>
                                                                    <asp:ListItem>mês</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="13">
                                                    <asp:Label ID="Label25" CssClass="LetrasLabel" runat="server" Font-Bold="true" Width="400px">... ... ... ...</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="13">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label26" CssClass="LetrasLabel" runat="server" Font-Bold="true">Observação</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtObservacao_R" runat="server" onfocus="LimpaErro()" Width="180px" TextMode="SingleLine"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label27" CssClass="LetrasLabel" runat="server" Font-Bold="True">Data&nbsp; Reajuste:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDataInicioReajuste" runat="server" onfocus="LimpaErro()" TextMode="Date" Enabled="False"></asp:TextBox>
                                                            </td> 
                                                            <td>
                                                                <asp:Label ID="Label30" CssClass="LetrasLabel" runat="server" Font-Bold="true">Dias de Coleta:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDiasColeta" runat="server" onfocus="LimpaErro()"></asp:TextBox>
                                                            </td> 
                                                            <td>
                                                                <asp:Label ID="Label31" CssClass="LetrasLabel" runat="server" Font-Bold="true">Particularidade:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtParticularidade" runat="server" onfocus="LimpaErro()" Width="92px"></asp:TextBox>
                                                            </td> 
                                                            <td>
                                                                <asp:Label ID="Label32" CssClass="LetrasLabel" runat="server" Font-Bold="true">Mês/Ano:</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMesAnoBase" runat="server" onfocus="LimpaErro()" Width="67px"></asp:TextBox>
                                                            </td> 
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" style="text-align:right;">
                                                    <asp:Label ID="lblMensagemResiduos" CssClass="LetrasLabel" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td colspan="6">
                                                    <div style="width:400px;text-align:left;" >
                                                        <asp:Button ID="btnOkResiduos" runat="server" Text="Ok" OnClick="btnOkResiduos_Click" />
                                                        <asp:Button ID="btnCancelarResiduos" runat="server" Text="Cancelar" OnClick="btnCancelarResiduos_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>    
                                    </asp:Panel>
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
