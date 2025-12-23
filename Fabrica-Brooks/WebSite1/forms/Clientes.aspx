<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="webClientes" %>
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
        .auto-style8
        {
            width: 10px;
            font-size: 9px;
            height: 24px;
        }
        .auto-style9
        {
            width: 120px;
            font-size: 9px;
            height: 24px;
        }
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            width: 10px;
            font-size: 9px;
            height: 28px;
        }
        .auto-style12
        {
            width: 90px;
            top: 1px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style13
        {
            height: 26px;
        }
        .auto-style14
        {
            top: 1px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style15
        {
            height: 19px;
        }
        .auto-style16 {
            width: 400px;
        }
        .auto-style18 {
            width: 122px;
        }
        .auto-style19 {
            width: 122px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style20 {
            width: 122px;
            font-size: 10px;
            height: 26px;
        }
        </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function SoLetras(evt, pId)
    {        
        if (pId == 'txtAlfaCNPJ_CPF') {
            if (document.getElementById('txtCNPJ_CPF').value.length == 0)
            {
                document.getElementById('txtAlfaCNPJ_CPF').value = '';
                return false;
            }
        } var charCode = (evt.which) ? evt.which : event.keyCode;
        if (pId == 'txtAlfaCNPJ')
        {
            if (document.getElementById('txtCNPJ_CPF_Faturamento').value.length == 0)
            {
                document.getElementById('txtAlfaCNPJ').value = '';
                return false;
            }
        }
        if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90))
            return true;
        return false;
    }
    function OnRetiraPontosIfemBarra(pIdCNPJ_CPF)
    {
        var CNPJ_CPF = document.getElementById(pIdCNPJ_CPF).value;
        CNPJ_CPF = CNPJ_CPF.replace('.', '');
        CNPJ_CPF = CNPJ_CPF.replace('.', '');
        CNPJ_CPF = CNPJ_CPF.replace('.', '');
        CNPJ_CPF = CNPJ_CPF.replace('-', '');
        CNPJ_CPF = CNPJ_CPF.replace('/', '');
        document.getElementById(pIdCNPJ_CPF).value = CNPJ_CPF;
    }
    function OnColocaPontosIfemBarra(pIdCNPJ_CPF) {
        var CNPJ_CPF = document.getElementById(pIdCNPJ_CPF).value;
        if (CNPJ_CPF == '')
        {
            if (pIdCNPJ_CPF == 'txtCNPJ_CPF')
                document.getElementById('txtAlfaCNPJ_CPF').value = '';
            if (pIdCNPJ_CPF == 'txtCNPJ_CPF_Faturamento')
                document.getElementById('txtAlfaCNPJ').value = '';
            return false;
        }
        if (CNPJ_CPF.length > 12)
        {
            if (validarCNPJ(CNPJ_CPF.substring(0, 14)) == false)
            {
                alert('CNPJ inválido!');
            }
            CNPJ1 = CNPJ_CPF.substring(0, 2);
            CNPJ2 = CNPJ_CPF.substring(2, 5);
            CNPJ3 = CNPJ_CPF.substring(5, 8);
            CNPJ4 = CNPJ_CPF.substring(8, 12);
            CNPJ5 = CNPJ_CPF.substring(12);
            _cnpj = CNPJ1 + '.' + CNPJ2 + '.' + CNPJ3 + '/' + CNPJ4 + '-' + CNPJ5;
            document.getElementById(pIdCNPJ_CPF).value = _cnpj;
        }
        else
        {
            if (validarCPF(CNPJ_CPF.substring(0, 11)) == false)
            {
                alert('CPF inválido!');
            }
            CPF1 = CNPJ_CPF.substring(0, 3);
            CPF2 = CNPJ_CPF.substring(3, 6);
            CPF3 = CNPJ_CPF.substring(6, 9);
            CPF4 = CNPJ_CPF.substring(9);
            _cpf = CPF1 + '.' + CPF2 + '.' + CPF3 + '-' + CPF4;
            document.getElementById(pIdCNPJ_CPF).value = _cpf;
            
        }

    }

    function validarCNPJ(cnpj) {

        cnpj = cnpj.replace(/[^\d]+/g, '');

        if (cnpj == '') return false;

        if (cnpj.length != 14)
            return false;

        // Elimina CNPJs invalidos conhecidos
        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999")
            return false;

        // Valida DVs
        tamanho = cnpj.length - 2;
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;

        return true;

    }

    function validarCPF(strCPF) {
        var Soma;
        var Resto;
        Soma = 0;
        if (strCPF == "00000000000") return false;

        for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
        Resto = (Soma * 10) % 11;

        if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(strCPF.substring(9, 10))) return false;

        Soma = 0;
        for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
        Resto = (Soma * 10) % 11;

        if ((Resto == 10) || (Resto == 11)) Resto = 0;
        if (Resto != parseInt(strCPF.substring(10, 11))) return false;
        return true;
    }

    function IgualPadrao()
    {
        if (document.getElementById('txtEndereco1') != null)
            document.getElementById('txtEndereco1').value = document.getElementById('hidEndereco0').value;
        if (document.getElementById('intNumero1_txtInteiro') != null)
            document.getElementById('intNumero1_txtInteiro').value = document.getElementById('hidNumero0').value;
        if (document.getElementById('txtComplemento1') != null)
            document.getElementById('txtComplemento1').value = document.getElementById('hidComplemento0').value;
        if (document.getElementById('intDDD11_txtInteiro') != null) {
            document.getElementById('intDDD11_txtInteiro').value = document.getElementById('hidDDD1_0').value;
        }
            
        if (document.getElementById('txtFone11') != null)
            document.getElementById('txtFone11').value = document.getElementById('hidFone1_0').value;
        if (document.getElementById('intDDD21_txtInteiro') != null)
            document.getElementById('intDDD21_txtInteiro').value = document.getElementById('hidDDD2_0').value;
        if (document.getElementById('txtFone21') != null)
            document.getElementById('txtFone21').value = document.getElementById('hidFone2_0').value;
        if (document.getElementById('intDDDC1_txtInteiro') != null)
            document.getElementById('intDDDC1_txtInteiro').value = document.getElementById('hidDDD3_0').value;
        if (document.getElementById('txtFax1') != null)
            document.getElementById('txtFax1').value = document.getElementById('hidFone3_0').value;
        if (document.getElementById('intDDDF1_txtInteiro') != null)
            document.getElementById('intDDDF1_txtInteiro').value = document.getElementById('hidDDD4_0').value;
        if (document.getElementById('txtCelular1') != null)
            document.getElementById('txtCelular1').value = document.getElementById('hidFone4_0').value;
        if (document.getElementById('txtBairro1') != null)
            document.getElementById('txtBairro1').value = document.getElementById('hidBairro0').value;
        if (document.getElementById('txtCEP1') != null)
            document.getElementById('txtCEP1').value = document.getElementById('hidCEP0').value;
        if (document.getElementById('txtUF1') != null)
            document.getElementById('txtUF1').value = document.getElementById('hidUF0').value;
        if (document.getElementById('txtCodigoIBGE1') != null)
            document.getElementById('txtCodigoIBGE1').value = document.getElementById('hidCodigoIBGE0').value;
        if (document.getElementById('intCodigoClidade1_txtInteiro') != null)
            document.getElementById('intCodigoClidade1_txtInteiro').value = document.getElementById('hidCodigoCidade0').value;
        if (document.getElementById('txtemail1') != null)
            document.getElementById('txtemail1').value = document.getElementById('hidEmail0').value;
        if (document.getElementById('txtContato1') != null)
            document.getElementById('txtContato1').value = document.getElementById('hidContato0').value;
    }
    function IgualFaturamento() {
        if (document.getElementById('txtEndereco2') != null)
            document.getElementById('txtEndereco2').value = document.getElementById('hidEndereco1').value;
        if (document.getElementById('intNumero2_txtInteiro') != null)
            document.getElementById('intNumero2_txtInteiro').value = document.getElementById('hidNumero1').value;
        if (document.getElementById('txtComplemento2') != null)
            document.getElementById('txtComplemento2').value = document.getElementById('hidComplemento1').value;
        if (document.getElementById('intDDD12_txtInteiro') != null)
            document.getElementById('intDDD12_txtInteiro').value = document.getElementById('hidDDD1_1').value;
        if (document.getElementById('txtFone12') != null)
            document.getElementById('txtFone12').value = document.getElementById('hidFone1_1').value;
        if (document.getElementById('intDDD22_txtInteiro') != null)
            document.getElementById('intDDD22_txtInteiro').value = document.getElementById('hidDDD2_1').value;
        if (document.getElementById('txtFone22') != null)
            document.getElementById('txtFone22').value = document.getElementById('hidFone2_1').value;
        if (document.getElementById('intDDDC2_txtInteiro') != null)
            document.getElementById('intDDDC2_txtInteiro').value = document.getElementById('hidDDD3_1').value;
        if (document.getElementById('txtFax2') != null)
            document.getElementById('txtFax2').value = document.getElementById('hidFone3_1').value;
        if (document.getElementById('intDDDF2_txtInteiro') != null)
            document.getElementById('intDDDF2_txtInteiro').value = document.getElementById('hidDDD4_1').value;
        if (document.getElementById('txtCelular2') != null)
            document.getElementById('txtCelular2').value = document.getElementById('hidFone4_1').value;
        if (document.getElementById('txtBairro2') != null)
            document.getElementById('txtBairro2').value = document.getElementById('hidBairro1').value;
        if (document.getElementById('txtCEP2') != null)
            document.getElementById('txtCEP2').value = document.getElementById('hidCEP1').value;
        if (document.getElementById('txtUF2') != null)
            document.getElementById('txtUF2').value = document.getElementById('hidUF1').value;
        if (document.getElementById('txtCodigoIBGE2') != null)
            document.getElementById('txtCodigoIBGE2').value = document.getElementById('hidCodigoIBGE1').value;
        if (document.getElementById('intCodigoClidade2_txtInteiro') != null)
            document.getElementById('intCodigoClidade2_txtInteiro').value = document.getElementById('hidCodigoCidade1').value;
        if (document.getElementById('txtemail2') != null)
            document.getElementById('txtemail2').value = document.getElementById('hidEmail1').value;
        if (document.getElementById('txtContato2') != null)
            document.getElementById('txtContato2').value = document.getElementById('hidContato1').value;
    }
    function InabilitarBotaoSalvar(pBotao)
    {
        document.getElementById(pBotao).style.visibility = 'hidden';
    }
    function ParaMaiusculas(pId)
    {
        document.getElementById(pId).value = document.getElementById(pId).value.toUpperCase();
    }
    function AbrePesquisaMunicipios(pTipoEndereco)
    {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('../forms/PesquisaMunicipios.aspx?TipoEndereco=' + pTipoEndereco);
    }
    function VerificarContrato()
    {
        alert('Checked');
    }
    function Validar_email(pEmail)
    {
        var email = document.getElementById(pEmail).value;
        var sp_email = email.split(";");
        var i = 0;
        var j = 0;
        var iArroba = 0;
        for (i = 0; i < sp_email.length; i++)
        {
            iArroba = 0;
            for (j = 0; j < sp_email[i].length; j++)
            {
               if (sp_email[i][j] == "@")
                   iArroba++;
            }
            if (iArroba > 1)
                alert("Existe mais de um arroba (@) em um único e-mail. É provável a falta de ; (ponto e vírgula)! ");
        }
    }
</script>
<body>
    <form id="form1" runat="server">
        -<table border="0" class="form2">
            <tr>
                <td style="height:1%;">
                    <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                </td>
            </tr>
            <tr>
                <td class="Menu">
                    <uc2:menu ID="menucabec1" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastro&amp;nbsp;de&amp;nbsp;Clientes" Font-Bold="True" CssClass="titulo2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" style="height:1%;">
                    <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="80%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" AllowPaging="True" OnPageIndexChanging="Grade_PageIndexChanging" OnRowCommand="Grade_RowCommand" Font-Bold="False">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DisplayIndex%>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DisplayIndex %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                            <HeaderStyle CssClass="padItemGrade" />
                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social" SortExpression="Nome">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="padItemGrade" Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NomeFantasia" HeaderText="Nome Fantasia" SortExpression="NomeFantasia">
                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DataCadastro" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Cadastro" SortExpression="DataCadastro" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Inativo" HeaderText="Inativo" SortExpression="Inativo" >
                            <HeaderStyle HorizontalAlign="Center" />
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
            <tr>
                <td valign="center" style="height:1%;">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblFiltro" CssClass="LetrasLabel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Procurar:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                    <asp:ListItem Value="NomeFantasia">NomeFantasia</asp:ListItem>
                                    <asp:ListItem Value="Nome"></asp:ListItem>
                                    <asp:ListItem Value="Codigo">Código</asp:ListItem>
                                    <asp:ListItem>Ativos</asp:ListItem>
                                    <asp:ListItem>Inativos</asp:ListItem>
                                    <asp:ListItem Value="CNPJ_CPF">CNPJ/CPF</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFiltro" runat="server" Width="400px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnDocumentacaoAplicavel" runat="server" Text="Documentação Aplicável" OnClick="btnDocumentacaoAplicavel_Click" Width="162px" />
                            </td>
                        </tr>
                    </table>
            </tr>
            <tr>
                <td>
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
                            <asp:MenuItem Text="Endereço Padrão" Value="1" />
                            <asp:MenuItem Text="Endereço Faturamento" Value="2" />
                            <asp:MenuItem Text="Endereço Coleta" Value="3" />
                            <asp:MenuItem Text="Informações" Value="4"></asp:MenuItem>
                            <asp:MenuItem Text="Sistema IMA" Value="5"></asp:MenuItem>
                        </Items>    
                        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                        <StaticMenuItemStyle CssClass="tab" HorizontalPadding="5px" VerticalPadding="2px"></StaticMenuItemStyle>
                        <StaticSelectedStyle CssClass="selectedTab" BackColor="#507CD1"></StaticSelectedStyle>
                    </asp:Menu>  
                    <div class="tabContents">
                        <asp:Label ID="lblTituloCodigoCliente" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="90px">&nbsp;&nbsp;&nbsp;Código&nbsp;Cliente:</asp:Label>
                        <asp:Label ID="lblCodigoCliente" runat="server" CssClass="LetrasLabel" BorderWidth="1px" BorderColor="DimGray" Width="50px" Height="20px" Font-Names="Arial" Font-Size="14px">&nbsp;</asp:Label>
                        <asp:MultiView
                        id="MultiView1"
                        ActiveViewIndex="0"
                        Runat="server">
                        <asp:View ID="ViewDados" runat="server">
                            <table class="auto-style16">
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblNovaSenha0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="100px">&nbsp;Data Cadastro</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <div style="width:380px;">
                                            <uc6:DATA ID="datDataCadastro" runat="server" />
                                            <asp:CheckBox ID="chkNaoAceitaDTRComPesoDiferente" runat="server" CssClass="LetrasLabel" Font-Bold="False" onclick="TiraCheckTerceiro();" style="margin-top: 0px" TabIndex="3" Text="Não aceita DTR peso diferente" Width="170px" />
                                            <asp:ImageButton ID="imbInativo" runat="server" ImageUrl="~/Images/selecionar.png" OnClick="imbInativo_Click1" />
                                            &nbsp;<asp:Label ID="lblInativo" runat="server" CssClass="LetrasLabel" Text="Inativo" Font-Size="9pt" Height="20px"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="auto-style12">
                                        <asp:Label ID="lblTipoCadastro" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Tipo Cadastro</asp:Label>
                                    </td>
                                    <td class="auto-style12">&nbsp;</td>
                                    <td class="auto-style13">
                                        <asp:DropDownList ID="ddlTipoCadastro" runat="server" TabIndex="3" Width="120px">
                                            <asp:ListItem>Mensal</asp:ListItem>
                                            <asp:ListItem>Eventual</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:234px">
                                        <asp:Label ID="Label9" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Código&amp;nbsp;no&amp;nbsp; aterro"></asp:Label>
                                        &nbsp;<uc4:INTEIRO7 ID="intCodigoAterro" runat="server" IndiceTab="3" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblNome" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Nome/Razão Social</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtNome" runat="server" Font-Bold="true" TabIndex="1" Width="360px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label52" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Código Func_coml:"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <uc3:INTEIRO ID="intCodigoFuncionarioComercial" runat="server" IndiceTab="3" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNomeFuncionario" runat="server" Text="nome funcionário"></asp:Label>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="5">
                                        <asp:Label ID="Label58" runat="server" CssClass="LetrasLabel" Text="Histórico&amp;nbsp;de&amp;nbsp;alteração"></asp:Label>
                                        <asp:GridView ID="GradeHistorico" runat="server" AutoGenerateColumns="False" Font-Size="10px" Font-Names="Verdana" OnRowDataBound="GradeHistorico_RowDataBound" OnRowCommand="GradeHistorico_RowCommand" >
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
                                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo">
                                                <ItemStyle HorizontalAlign="Right" />
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
                                    <td class="auto-style10">
                                        <asp:Label ID="lblNomeFantasia" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Nome Fantasia</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtNomeFantasia" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblNovaSenha2" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="67px" Font-Bold="False" >Nova Senha</asp:Label>
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtNovaSenha" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="3" Width="125px" ></asp:TextBox>
                                    </td>
                                    <td class="auto-style19">&nbsp;</td>                                    
                                </tr>
                                <tr>
                                    <td class="auto-style11">
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
                                    <td class="auto-style11">
                                        <asp:Label ID="lblNovaSenha3" CssClass="LetrasLabel" runat="server" Width="67px" Font-Bold="False" >Data Senha</asp:Label>
                                    </td>
                                    <td class="auto-style11">&nbsp;</td>
                                    <td>
                                        <uc6:DATA ID="datDataNovaSenha" runat="server" IndiceTab="3" />
                                    </td>
                                    <td class="auto-style18">&nbsp;</td>
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
                                    <td class="auto-style20">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Classificação</asp:Label>
                                    </td>
                                    <td colspan="5">
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
                                                    <asp:Label ID="Label8" CssClass="LetrasLabel" runat="server" Width="90px" Font-Bold="False">&nbsp;Código Exp p/NF</asp:Label>
                                                </td>
                                                <td>
                                                    <uc4:INTEIRO7 ID="intCodigoExpNF" runat="server" IndiceTab="2" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">
                                            <asp:CheckBox ID="chkEmiteDDR" runat="server" Text="Emitir DDR" TabIndex="1" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                        </td>
                                    <td class="auto-style9">
                                            <asp:CheckBox ID="chkEmitirCDF" runat="server" Text="Emitir CDF" TabIndex="1" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel"/>
                                        </td>
                                    <td class="auto-style9"></td>
                                    <td class="auto-style9">&nbsp;</td>
                                    <td colspan="2">
                                        <div style="width:400px">
                                            <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="3" OnClientClick="InabilitarBotaoSalvar(this.id);" />
                                            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" TabIndex="3" />
                                            <asp:Button ID="Copiar" runat="server" Font-Size="10pt" OnClick="Copiar_Click" OnClientClick="InabilitarBotaoSalvar(this.id);" TabIndex="18" Text="Copiar" />
                                            <asp:Label ID="lblMensagemCopiar" runat="server" CssClass="LetrasLabel" Font-Bold="True" Height="20px"></asp:Label>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="LetrasTD">
                                        <asp:Label ID="Label55" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="130px">&nbsp;Aliquota Imposto Federal:</asp:Label>
                                    </td>
                                    <td class="LetrasTD">
                                        <asp:DropDownList ID="ddlAliquotasFederais" runat="server" CssClass="LetrasLabel" Height="16px" Width="400px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LetrasTD">&nbsp;</td>
                                    <td class="LetrasTD">
                                        <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                        <asp:HiddenField ID="hifCodigo" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>        
                        <asp:View ID="ViewEnderecoPadrao" runat="server">
                            <table style="width:400px">
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblEndereco0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="50px">&nbsp;Endereço</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <div style="width:300px;">
                                            <asp:TextBox ID="txtEndereco0" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="auto-style12">
                                        &nbsp;</td>
                                    <td class="auto-style13">
                                        <asp:Label ID="lblNumero0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Nº"></asp:Label>
                                    </td>
                                    <td>
                                        <uc4:INTEIRO7 ID="intNumero0" runat="server" IndiceTab="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblComplemento0" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtComplemento0" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style13">
                                        <asp:Label ID="lblFone10" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()" Width="67px">Fone 1</asp:Label>
                                    </td>
                                    <td class="auto-style13">
                                        <uc9:INTEIRO2 ID="intDDD10" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtFone10" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="200px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblBairro0" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtBairro0" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblFone20" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Fone 2</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <uc9:INTEIRO2 ID="intDDD20" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtFone20" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="200px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                        <asp:Label CssClass="LetrasLabel" ID="lblCEP0" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                    </td>
                                    <td class="auto-style11">
                                        <div style="width:300px;vertical-align:central;">
                                            <asp:TextBox ID="txtCEP0" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>

                                            <asp:Label ID="lblUF0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                            &nbsp;<asp:TextBox ID="txtUF0" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>

                                            <asp:Label ID="lblCodigoIBGE0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                            &nbsp;<asp:TextBox ID="txtCodigoIBGE0" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>

                                        </div>
                                    </td>

                                    <td class="auto-style11">
                                        <asp:Label ID="lblCelular0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Celular</asp:Label>
                                    </td>
                                    <td>
                                        <uc9:INTEIRO2 ID="intDDDC0" runat="server" IndiceTab="2" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCelular0" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="200px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
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
                                                <td>
                                                    <img id="imagem" alt="x" src="../Images/procura.png" style="cursor: pointer; " onclick="AbrePesquisaMunicipios(1);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:Label ID="lblFax0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Fax</asp:Label>
                                        </td>
                                    <td class="auto-style6">
                                        <uc9:INTEIRO2 ID="intDDDF0" runat="server" IndiceTab="2" />
                                        </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtFax0" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="200px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblemail0" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtemail0" runat="server" onblur="Validar_email(this.id);" onfocus="LimpaErro()" TabIndex="1" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">
                                            <asp:Label ID="lblContato0" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                    </td>
                                    <td class="auto-style9">
                                            <asp:TextBox ID="txtContato0" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="180px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style9">
                                        </td>
                                    <td class="auto-style9">
                                        &nbsp;</td>
                                    <td class="auto-style9">
                                        <asp:Button ID="btnOk0" runat="server" CommandName="Salvar" Font-Size="10pt" OnClick="btnOk0_Click" TabIndex="2" Text="Ok" />
                                        <asp:Button ID="btnAlterar0" runat="server" OnClick="btnAlterar0_Click" TabIndex="2" Text="Alterar" />
                                    </td>
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
                                    <td class="LetrasTD">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td>
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
                        </asp:View>        
                        <asp:View ID="ViewEnderecoFaturamento" runat="server">
                            <table style="width:400px">
                                <tr>
                                    <td class="auto-style10">
                                        <table style="width: 180px; padding:0; word-spacing: 0;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Endereço</asp:Label>
                                                </td>
                                                <td>
                                                    <input id="butIgualPadrao" type="button" value="Igual Padrão" class="LetrasLabel" onclick="IgualPadrao();" style="width: 96px;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style10">
                                        <div style="width:300px;">
                                            <asp:TextBox ID="txtEndereco1" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="auto-style12">
                                        &nbsp;</td>
                                    <td class="auto-style13">
                                        <asp:Label ID="Label5" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Nº"></asp:Label>
                                    </td>
                                    <td>
                                        <uc4:INTEIRO7 ID="intNumero1" runat="server" IndiceTab="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label6" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtComplemento1" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style13">
                                        <asp:Label ID="Label11" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()" Width="67px">Fone 1</asp:Label>
                                    </td>
                                    <td class="auto-style13">
                                        <uc9:INTEIRO2 ID="intDDD11" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtFone11" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style13"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label12" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtBairro1" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label13" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Fone 2</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <uc9:INTEIRO2 ID="intDDD21" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtFone21" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                                </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                        <asp:Label CssClass="LetrasLabel" ID="Label14" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                    </td>
                                    <td class="auto-style11">
                                        <div style="width:300px;vertical-align:central;">
                                            <asp:TextBox ID="txtCEP1" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>

                                            <asp:Label ID="Label15" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                            &nbsp;<asp:TextBox ID="txtUF1" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>

                                            <asp:Label ID="Label16" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                            &nbsp;<asp:TextBox ID="txtCodigoIBGE1" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>

                                        </div>
                                    </td>

                                    <td class="auto-style11">
                                        <asp:Label ID="Label17" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Celular</asp:Label>
                                    </td>
                                    <td>
                                        <uc9:INTEIRO2 ID="intDDDC1" runat="server" IndiceTab="2" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCelular1" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="auto-style14">
                                        <asp:Label ID="Label18" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Cidade</asp:Label>
                                    </td>
                                    <td class="auto-style6">
                                        <table style="width:400px;border-spacing:0;padding:0;word-spacing: 0;">
                                            <tr>
                                                <td>
                                                    <uc4:INTEIRO7 ID="intCodigoClidade1" runat="server" IndiceTab="1" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="lbtNomeCidade1" runat="server" Enabled="False" TabIndex="1" Width="250px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <img id="Img1" alt="x" src="../Images/procura.png" style="cursor: pointer;" onclick="AbrePesquisaMunicipios(2);"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:Label ID="Label19" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Fax</asp:Label>
                                        </td>
                                    <td class="auto-style6">
                                        <uc9:INTEIRO2 ID="intDDDF1" runat="server" IndiceTab="2" />
                                        </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtFax1" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                        </td>
                                    <td class="auto-style6">
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtemail1" runat="server" onfocus="LimpaErro()" onblur="Validar_email(this.id);" TabIndex="1" Width="400px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label37" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Tipo Cobrança:</asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlTipoCobranca" runat="server" Width="250px" CssClass="LetrasLabel">
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
                                    <td class="auto-style9">
                                            <asp:TextBox ID="txtInstrucoesFaturamento" runat="server" onfocus="LimpaErro()" TabIndex="1" Width="400px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style9">
                                        &nbsp;</td>
                                    <td class="auto-style9">
                                        <asp:Label ID="Label53" runat="server" CssClass="LetrasLabel" Font-Bold="False">&nbsp;CNPJ Faturamento</asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtCNPJ_CPF_Faturamento" runat="server" onblur="OnColocaPontosIfemBarra(this.id);" onfocus="return OnRetiraPontosIfemBarra(this.id);" onkeypress="return SoNumeros(event);" TabIndex="1" TextMode="SingleLine" Width="120px" MaxLength="14"></asp:TextBox>
                                        <asp:TextBox ID="txtAlfaCNPJ" runat="server" MaxLength="1" Width="12px" onkeyup="ParaMaiusculas(this.id)" onkeypress="return SoLetras(event, this.id);" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">
                                        <asp:Label ID="Label21" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="100px">&nbsp;Contato</asp:Label>
                                    </td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtContato1" runat="server" onfocus="LimpaErro()" onkeyup="ParaMaiusculas(this.id)" TabIndex="1" Width="180px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style9"></td>
                                    <td class="auto-style9">&nbsp;</td>
                                    <td class="auto-style9">
                                        <div style="width:160px">
                                            <asp:Button ID="btnOk1" runat="server" CommandName="Salvar" Font-Size="10pt" OnClick="btnOk1_Click" TabIndex="18" Text="Ok" />
                                            <asp:Button ID="btnAlterar1" runat="server" OnClick="btnAlterar1_Click" Text="Alterar" />
                                        </div>
                                    </td>
                                    <td class="auto-style9"></td>
                                </tr>
                                <tr>
                                    <td class="LetrasTD">&nbsp;</td>
                                    <td class="LetrasTD">
                                        <asp:Label ID="lblMensagem1" runat="server" CssClass="LetrasLabel"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:View>        
                        <asp:View ID="ViewEnderecoColeta" runat="server">
                            <table style="width:400px">
                                <tr>
                                    <td class="auto-style10">
                                        <table style="width: 180px; padding:0; word-spacing: 0;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label23" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Endereço</asp:Label>
                                                </td>
                                                <td>
                                                    <input id="butIgualPadrao2" type="button" class="LetrasLabel" value="Igual Faturamento" onclick="IgualFaturamento();" style="width: 96px;"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style10">
                                        <div style="width:300px;">
                                            <asp:TextBox ID="txtEndereco2" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="auto-style12">
                                        &nbsp;</td>
                                    <td class="auto-style13">
                                        <asp:Label ID="Label24" runat="server" CssClass="LetrasLabel" Font-Bold="False" Text="Nº"></asp:Label>
                                    </td>
                                    <td>
                                        <uc4:INTEIRO7 ID="intNumero2" runat="server" IndiceTab="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label25" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Complemento</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtComplemento2" runat="server" TabIndex="1" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style13">
                                        <asp:Label ID="Label26" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()" Width="67px">Fone 1</asp:Label>
                                    </td>
                                    <td class="auto-style13">
                                        <uc9:INTEIRO2 ID="intDDD12" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtFone12" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label27" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Bairro</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtBairro2" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="300px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label28" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Fone 2</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <uc9:INTEIRO2 ID="intDDD22" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtFone22" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                        <asp:Label CssClass="LetrasLabel" ID="Label29" runat="server" Font-Bold="False">&nbsp;CEP</asp:Label>
                                    </td>
                                    <td class="auto-style11">
                                        <div style="width:300px;vertical-align:central;">
                                            <asp:TextBox ID="txtCEP2" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="90px" ></asp:TextBox>
                                            <asp:Label ID="Label30" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;UF</asp:Label>
                                            &nbsp;<asp:TextBox ID="txtUF2" runat="server" TabIndex="1" Width="20px" onkeyup="ParaMaiusculas(this.id)" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="Label31" runat="server" CssClass="LetrasLabel" Font-Bold="False" Height="20px">&nbsp;Cd.IBGE</asp:Label>
                                            &nbsp;<asp:TextBox ID="txtCodigoIBGE2" runat="server" onfocus="LimpaErro()" TabIndex="1" TextMode="SingleLine" Width="90px" Enabled="False"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="auto-style11">
                                        <asp:Label ID="Label32" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Celular</asp:Label>
                                    </td>
                                    <td>
                                        <uc9:INTEIRO2 ID="intDDDC2" runat="server" IndiceTab="2" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCelular2" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style14">
                                        <asp:Label ID="Label33" runat="server" Width="110px" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Cidade</asp:Label>
                                    </td>
                                    <td>
                                        <table style="width:400px;border-spacing:0;padding:0;word-spacing: 0;">
                                            <tr>
                                                <td>
                                                    <uc4:INTEIRO7 ID="intCodigoClidade2" runat="server" IndiceTab="1" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="lbtNomeCidade2" runat="server" Enabled="False" TabIndex="1" Width="250px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <img id="Img2" alt="x" src="../Images/procura.png" style="cursor: pointer;" onclick="AbrePesquisaMunicipios(3);"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style6">
                                        <asp:Label ID="Label34" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="67px">Fax</asp:Label>
                                        </td>
                                    <td class="auto-style6">
                                        <uc9:INTEIRO2 ID="intDDDF2" runat="server" IndiceTab="2" />
                                    </td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="txtFax2" runat="server" onfocus="LimpaErro()" TabIndex="2" TextMode="SingleLine" Width="123px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label35" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;e-mail</asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtemail2" runat="server" onfocus="LimpaErro()" onblur="Validar_email(this.id);" TabIndex="1" Width="400px"></asp:TextBox>
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
                                    <td class="auto-style9">
                                        <div style="width:160px">
                                            <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="btnOk2" OnClick="btnOk2_Click" TabIndex="2" />
                                            <asp:Button ID="btnAlterar2" runat="server" Text="Alterar" OnClick="btnAlterar2_Click" TabIndex="2" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LetrasTD">&nbsp;</td>
                                    <td class="LetrasTD">
                                        <asp:Label ID="lblMensagem2" CssClass="LetrasLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ViewInformacoes" runat="server">
                            <table style="width:400px">
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label39" runat="server" CssClass="LetrasLabel" Font-Bold="False" onfocus="LimpaErro()" Width="67px">Km média</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <div style="width:300px;">
                                            <uc4:INTEIRO7 ID="intKmMedia" runat="server" IndiceTab="1" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label22" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Names="Arial" Font-Size="10px" Width="100px">&nbsp;Ponto de Referência</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtPontoDeReferencia" runat="server" Height="50px" TabIndex="1" TextMode="MultiLine" Width="400px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="Label40" onfocus="LimpaErro()" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Observação</asp:Label>
                                    </td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtObservacao" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="400px" Height="100px" TextMode="MultiLine" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                        &nbsp;</td>
                                    <td class="auto-style11">
                                        <asp:CheckBox ID="chkIncluirAtualizarDados" runat="server" CssClass="LetrasLabel" Text="Incluir/Atualizar dados" Width="190px" TabIndex="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LetrasTD">&nbsp;</td>
                                    <td class="LetrasTD">
                                        <asp:Label ID="lblMensagem4" CssClass="LetrasLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                        &nbsp;</td>
                                    <td>
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                    </td>
                                    <td style="text-align:center;">
                                        <asp:Button ID="btnOkInformacoes" runat="server" CommandName="Salvar" Font-Size="10pt" OnClick="Salvar_Click" TabIndex="2" Text="Ok" />
                                    </td>
                                    <td>
                                        <div style="width:160px">
                                            <asp:Button ID="btnAlterar3" runat="server" OnClick="btnAlterar3_Click" TabIndex="2" Text="Alterar" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:View> 
                        <asp:View ID="ViewSistemaFatima" runat="server">
                            <table style="width:400px">
                                <tr>
                                    <td class="auto-style10">
                                        <table>
                                            <tr>
                                                <td colspan="4" style="text-align: center;" class="auto-style15">
                                                    <asp:Label ID="lblTituloFatma" runat="server" CssClass="LetrasLabel" Font-Bold="False" Font-Size="9pt" Width="702px" BackColor="#FFCCFF">&nbsp;Sistema MTR-e IMA</asp:Label>
                                                </td>
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
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtFoneFatma" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="150px" onkeyup="ParaMaiusculas(this.id)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label51" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="50px">&nbsp;e-mail</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtemailFatma" runat="server" onfocus="LimpaErro()" TabIndex="2" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label57" runat="server" CssClass="LetrasLabel" Font-Bold="False" Width="106px">&nbsp;Código Unidade IMA:</asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <uc1:INTEIRO7 ID="intCodigoUnidadeIMA" runat="server" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    &nbsp;<td>
                                                    <asp:CheckBox ID="chkClienteExigeMTReParaRCD" runat="server" CssClass="LetrasLabel" TabIndex="1" Text="Cliente Exige emissão da MTR-e para RCD" Width="230px" />
                                                </td>
                                                <td>
                                                    <div style="width:149px;text-align: right;">
                                                        <asp:Button ID="btnSalvarFatima" runat="server" CommandName="Salvar" Font-Size="10pt" OnClick="Salvar_Click" TabIndex="2" Text="Ok" />
                                                        <asp:Button ID="btnAlterar4" runat="server" OnClick="btnAlterar4_Click" TabIndex="2" Text="Alterar" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMensagem5" runat="server" CssClass="LetrasLabel"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                        <asp:HiddenField ID="HiddenField5" runat="server" />
                                    </td>
                                    <td style="width:1%">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="dadosForm">
                    <asp:HiddenField ID="hidEndereco0" runat="server" />
                    <asp:HiddenField ID="hidNumero0" runat="server" />
                    <asp:HiddenField ID="hidComplemento0" runat="server" />
                    <asp:HiddenField ID="hidDDD1_0" runat="server" />
                    <asp:HiddenField ID="hidFone1_0" runat="server" />
                    <asp:HiddenField ID="hidDDD2_0" runat="server" />
                    <asp:HiddenField ID="hidFone2_0" runat="server" />
                    <asp:HiddenField ID="hidDDD3_0" runat="server" />
                    <asp:HiddenField ID="hidFone3_0" runat="server" />
                    <asp:HiddenField ID="hidDDD4_0" runat="server" />
                    <asp:HiddenField ID="hidFone4_0" runat="server" />
                    <asp:HiddenField ID="hidBairro0" runat="server" />
                    <asp:HiddenField ID="hidCEP0" runat="server" />
                    <asp:HiddenField ID="hidUF0" runat="server" />
                    <asp:HiddenField ID="hidCodigoIBGE0" runat="server" />
                    <asp:HiddenField ID="hidCodigoCidade0" runat="server" />
                    <asp:HiddenField ID="hidEmail0" runat="server" />
                    <asp:HiddenField ID="hidContato0" runat="server" />

                    <asp:HiddenField ID="hidEndereco1" runat="server" />
                    <asp:HiddenField ID="hidNumero1" runat="server" />
                    <asp:HiddenField ID="hidComplemento1" runat="server" />
                    <asp:HiddenField ID="hidDDD1_1" runat="server" />
                    <asp:HiddenField ID="hidFone1_1" runat="server" />
                    <asp:HiddenField ID="hidDDD2_1" runat="server" />
                    <asp:HiddenField ID="hidFone2_1" runat="server" />
                    <asp:HiddenField ID="hidDDD3_1" runat="server" />
                    <asp:HiddenField ID="hidFone3_1" runat="server" />
                    <asp:HiddenField ID="hidDDD4_1" runat="server" />
                    <asp:HiddenField ID="hidFone4_1" runat="server" />
                    <asp:HiddenField ID="hidBairro1" runat="server" />
                    <asp:HiddenField ID="hidCEP1" runat="server" />
                    <asp:HiddenField ID="hidUF1" runat="server" />
                    <asp:HiddenField ID="hidCodigoIBGE1" runat="server" />
                    <asp:HiddenField ID="hidCodigoCidade1" runat="server" />
                    <asp:HiddenField ID="hidEmail1" runat="server" />
                    <asp:HiddenField ID="hidContato1" runat="server" />
                    <asp:Button ID="btnProcurar" runat="server" BackColor="White" BorderWidth="0" ForeColor="White" Text="P" Visible="true" Width="1px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
