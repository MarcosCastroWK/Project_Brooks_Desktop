<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DTR.aspx.cs" Inherits="SILC.Web.forms.DTR" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>
<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        ._visibility {
            visibility: hidden;
            font-size: 0px;
            width: 0px;
        }
        .BotaoSelecao {
            background-color: white;
            text-align: center;
        }
        .styeDivBotoes {
            width: 493px;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function AbrePorResiduo(pId) {
        document.getElementById('Salvar').disabled = true;
        document.getElementById('btnMTRImprimir').disabled = true;
        document.getElementById('inpImprimir').disabled = true;
        document.getElementById('btnEnviar').disabled = true;
        pId = pId.replace('lbtResiduo', 'lblCodigoResiduo');
        var slink = 'DTR.aspx?CodigoResiduo=' + document.getElementById(pId).value;
        window.open(slink);
    }   
    function AbrirMovimentacaoDTR(pId) {
        document.getElementById('Salvar').disabled = true;
        document.getElementById('btnMTRImprimir').disabled = true;
        document.getElementById('inpImprimir').disabled = true;
        document.getElementById('btnEnviar').disabled = true;
        pId = pId.replace('lblLocalDTR', 'lblCodigoResiduo');
        var slink = 'MovimentacaoDTR.aspx?CodigoResiduo=' + document.getElementById(pId).value;
        pId = pId.replace('lblCodigoResiduo', 'lblNumeroLancamento');
        slink = slink + '&NumeroLancamento=' + document.getElementById(pId).value;
        pId = pId.replace('lblNumeroLancamento', 'lblCodigoCliente');
        slink = slink + '&CodigoCliente=' + document.getElementById(pId).value;
        pId = pId.replace('lblCodigoCliente', 'lblLocalDTR');
        slink = slink + '&Local=' + document.getElementById(pId).value;
        document.getElementById(pId).disabled = true;
        document.getElementById(pId).value = 'alterando';
        document.getElementById(pId).style.cursor = 'none';
        var _width = (screen.width / 2) - 250;
        var _height = (screen.height / 2) - 400;
        //window.open(slink, 'ModalPopUp', 'height=680, width=500, left=' + _width + ', top=' + _height + ', modal=yes, toolbar=no, scrollbars=no, location=no, statusbar=no, menubar=no, resizable=0');
        window.location = slink;
    }
    function Imprimir() {
        try {
            document.getElementById('tableLegenda').style.visibility = "hidden"; 
            document.getElementById('tdmenu').style.visibility = "hidden";
            document.getElementById('tableBotoes').style.visibility = "hidden";
            document.getElementById('contentTopRightDiv').style = "height: 100%";
            window.print();
        }
        finally {
            document.getElementById('tableLegenda').style.visibility = "visible";
            document.getElementById('tdmenu').style.visibility = "visible";
            document.getElementById('tableBotoes').style.visibility = "visible";
        }
    }
    function Novo() {
        var r = confirm("Confirma?");
        if (r == true) {
            window.location = 'DTR.aspx';
        }
    }
    function RetornaPrimeiraDataSelecionada(pId)
    {
        var _iCount = 0;
        pId = pId.replace('Grade_ctl', '');
        pId = pId.replace('_lblSeq', '');
        _iCount = parseFloat(pId) + 1;
        var selecionado = '';
        var i;
        var _exp0 = '';
        for (i = 2; i < _iCount; i++)
        {
            _exp0 = 'Grade_ctl0';
            if (i > 9) {
                _exp0 = 'Grade_ctl';
            }
            if (document.getElementById(_exp0 + i + '_lblSeq').style.backgroundColor == "dodgerblue")
            {
                selecionado = document.getElementById(_exp0 + i + '_datDataSaida_txtData').value;
                break;
            }
        }
        return selecionado;
    }
    function RetornaPrimeiroDestinoFinalSelecionado(pId)
    {
        var _iCount = 0;
        pId = pId.replace('Grade_ctl', '');
        pId = pId.replace('_lblSeq', '');
        _iCount = parseFloat(pId) + 1;
        var selecionado = '';
        var i;
        var _exp0 = '';
        for (i = 2; i < _iCount; i++)
        {
            _exp0 = 'Grade_ctl0';
            if (i > 9) {
                _exp0 = 'Grade_ctl';
            }
            if (document.getElementById(_exp0 + i + '_lblSeq').style.backgroundColor == "dodgerblue")
            {
                selecionado = document.getElementById(_exp0 + i + '_ddlDestinoFinal').value;
                break;
            }
        }
        return selecionado;
    }
    function RetornaPrimeiraLinhaSelecionada(pId)
    {
        var _iCount = 0;
        pId = pId.replace('Grade_ctl', '');
        pId = pId.replace('_lblSeq', '');
        _iCount = parseFloat(pId) + 1;
        var i;
        var _exp0 = '';
        for (i = 2; i < _iCount; i++)
        {
            _exp0 = 'Grade_ctl0';
            if (i > 9) {
                _exp0 = 'Grade_ctl';
            }
            if (document.getElementById(_exp0 + i + '_lblSeq').style.backgroundColor == "dodgerblue")
            {
                return i;
            }
        }
        return 0;
    }
    function Selecionar(pId)
    {
        var _dataselecionada = RetornaPrimeiraDataSelecionada(pId);
        var _destinoselecionado = RetornaPrimeiroDestinoFinalSelecionado(pId);
        var _data = new Date();
        var _dia  = _data.getDate() - 1;       // 01/01/2021
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

        if (_dataselecionada != '')
            _str_data = _dataselecionada;

        var i = 0;
        var _pLnSel = 0;
        var _pLnSel = RetornaPrimeiraLinhaSelecionada(pId);
        var _iCount = 0;
        if (document.getElementById(pId).style.backgroundColor == "dodgerblue")
        {
            document.getElementById(pId).style.backgroundColor = "white";
            document.getElementById(pId).style.color = "black";
            document.getElementById(pId.replace('_lblSeq', '_datDataSaida_txtData')).value = '';
            document.getElementById(pId.replace('_lblSeq', '_ddlDestinoFinal')).value = '';
            return;
        }
        else
        {
            if (_pLnSel > 1)
            {
                px = pId.replace('Grade_ctl', '');
                px = px.replace('_lblSeq', '');
                var _iAte = parseFloat(px) - 1;
                var r = confirm("Selecionar até " + _iAte + "?");
                if (!r) {
                    document.getElementById(pId).style.backgroundColor = "dodgerblue";
                    document.getElementById(pId).style.color = "white";
                    document.getElementById(pId.replace('_lblSeq', '_datDataSaida_txtData')).value = _str_data;
                    document.getElementById(pId.replace('_lblSeq', '_ddlDestinoFinal')).value = _destinoselecionado;
                    return;
                }
            }
            document.getElementById(pId).style.backgroundColor = "dodgerblue";
            document.getElementById(pId).style.color = "white";
            document.getElementById(pId.replace('_lblSeq', '_datDataSaida_txtData')).value = _str_data;
            document.getElementById(pId.replace('_lblSeq', '_ddlDestinoFinal')).value = _destinoselecionado;
        }
        pId = pId.replace('Grade_ctl', '');
        pId = pId.replace('_lblSeq', '');
        _iCount = parseFloat(pId) + 1;
        var _exp0 = 'Grade_ctl0';
        if (_iCount > _pLnSel)
        {
            for (i = _pLnSel; i < _iCount; i++)
            {
                _exp1 = 'Grade_ctl0';
                if (i > 9) {
                    _exp1 = 'Grade_ctl';
                }
                document.getElementById(_exp1 + i + '_lblSeq').style.backgroundColor = "dodgerblue";
                document.getElementById(_exp1 + i + '_lblSeq').style.color = "white";
                if (_str_data != '')
                    document.getElementById(_exp1 + i + '_datDataSaida_txtData').value = _str_data;
                if (_destinoselecionado != '')
                    document.getElementById(_exp1 + i + '_ddlDestinoFinal').value = _destinoselecionado;
            }
        }
        else if (_iCount == _pLnSel && _iCount > 0)
        {
            var _newln = 0;
            var _newln = _pLnSel;
            var _exp2 = 'Grade_ctl0';
            _exp2 = 'Grade_ctl0';
            if (_newln > 9) {
                _exp2 = 'Grade_ctl';
            }
            if (_newln == 10)
            {
                if (document.getElementById('Grade_ctl10_lblSeq').style.backgroundColor == "dodgerblue")
                {
                    document.getElementById('Grade_ctl09_lblSeq').style.backgroundColor = "white";
                    document.getElementById('Grade_ctl09_lblSeq').style.color = "black";
                    document.getElementById('Grade_ctl09_datDataSaida_txtData').value = '';
                    document.getElementById('Grade_ctl09_ddlDestinoFinal').value = '';
                }
            }
            else if (document.getElementById(_exp2 + _newln + '_lblSeq').style.backgroundColor == "dodgerblue")
            {
                document.getElementById(_exp2 + (_newln - 1) + '_lblSeq').style.backgroundColor = "white";
                document.getElementById(_exp2 + (_newln - 1) + '_lblSeq').style.color = "black";
                document.getElementById(_exp2 + (_newln - 1) + '_datDataSaida_txtData').value = '';
                document.getElementById(_exp2 + (_newln - 1) + '_ddlDestinoFinal').value = '';
            }
        }
    }

    function SelecionarDesTodos()
    {
        var i = 0;
        var _exp3 = 'Grade_ctl0';
        var iLns = document.getElementById('hifLinhasGrade').value;

        for (i = 2; i < iLns + 2; i++)
        {
            _exp3 = 'Grade_ctl0';
            if (i > 9) {
                _exp3 = 'Grade_ctl';
            }
            if (document.getElementById(_exp3 + i + '_lblSeq').style.backgroundColor == "dodgerblue")
            {
                document.getElementById(_exp3 + i + '_lblSeq').style.backgroundColor = "white";
                document.getElementById(_exp3 + i + '_lblSeq').style.color = "black";
                document.getElementById(_exp3 + i + '_datDataSaida_txtData').value = '';
                document.getElementById(_exp3 + i + '_ddlDestinoFinal').value = '';
                document.getElementById(_exp3 + i + '_txtImprimido').value = '';
            }
        }
    }
    function TiraImprimido(pId)
    {
        if (document.getElementById(pId).value == "I")
        {
            document.getElementById(pId).value = "";
            return;
        }
    }
    function ProibiDigitacaoCampoImprimido(evt, pId)
    {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode == 73 &&
            document.getElementById(pId).value != 'I' &&
            document.getElementById(pId.replace('txtImprimido', 'datDataSaida_txtData')).value != '' &&
            document.getElementById(pId.replace('txtImprimido', 'ddlDestinoFinal')).value != '')
        {
            return true;
        }
        else
        {
            return false;
        }
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td>
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>
                <tr>
                    <td id="tdmenu" class="Menu">
                        <uc2:menu ID="menu1" runat="server" Visible="false" />                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tableLegenda">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitulo" runat="server" Text="DTR  - Depósito Temporário de Resíduos" Font-Bold="True" CssClass="titulo2"></asp:Label>        
                                </td>
                                <td class="LetrasLabel">Na coluna Nº, um click seleciona</td>
                            </tr>
                            <tr>
                                <td style="width:1230px">&nbsp;<asp:Label ID="lblLote" runat="server" Text="Lote:" CssClass="LetrasLabel"></asp:Label></td>
                                <td><input id="btnTiraSelecionado" type="button" class="LetrasLabel" style="width: 146px;" value="Tirar todos selecionados" onclick="SelecionarDesTodos();"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:1400px;height:510px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="1360px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:BoundField DataField="DataColeta" HeaderText="Data Coleta" SortExpression="DataColeta" DataFormatString=" {0:dd/MM/yyyy}">
                                    <HeaderStyle CssClass="padItemGrade" Width="60"/>
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeCliente" HeaderText="Nome ou Razão Social" SortExpression="NomeCliente">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle CssClass="padItemGrade" Width="280px"/>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Resíduo" SortExpression="Residuo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtResiduo" runat="server" BorderWidth="0" Width="500px" style="cursor:pointer;" Text='<%# Bind("Residuo") %>' onclick="AbrePorResiduo(this.id);"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" CssClass="padItemGrade" Width="500px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" DataFormatString=" {0:n2}">
                                    <ItemStyle CssClass="padItemGrade" Width="62px" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="UN" SortExpression="Unidade" DataField="Unidade">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="DTR" SortExpression="DTR">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblLocalDTR" runat="server" Text='<%# Bind("LocalDTR") %>' BackColor="White" BorderStyle="None" Style="width: 50px; text-align: center; cursor:pointer;" CssClass="LetrasLabel" Width="30px" Height="12px" onclick="AbrirMovimentacaoDTR(this.id);"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data Saída" SortExpression="DataSaida">
                                        <ItemTemplate>
                                            <uc6:DATA ID="datDataSaida" runat="server" Style="width: 56px; text-align: center;" BackColor="#ffffcc" BorderStyle="None" CssClass="LetrasLabel" Data='<%# Bind("DataSaida", "{0:yyyy-MM-dd}") %>'/>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Destino final" SortExpression="DestinoFinal">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hifDestinoFinal" runat="server" Value='<%# Bind("DestinoFinal") %>'></asp:HiddenField>
                                            <asp:DropDownList ID="ddlDestinoFinal" runat="server" Width="304px" style="border: 0px; border-spacing: 2px;" CssClass="LetrasLabel" BackColor="#ffffcc">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Lote" HeaderText="">
                                        <HeaderStyle CssClass="_visibility"/>
                                        <ItemStyle CssClass="_visibility"/>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nº">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeq" runat="server" Text="lblSeq" CssClass="BotaoSelecao" Width="30px" Height="12px" onclick="Selecionar(this.id);"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="I">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtImprimido" runat="server" Width="16px" BorderWidth="0" Font-Names="Tahoma" Height="12px" CssClass="BotaoSelecao" onkeypress="return ProibiDigitacaoCampoImprimido(event, this.id);" ToolTip="Click aqui para tirar o I para reimprimir" Text='<%# Bind("Imprimido")%>' onclick="TiraImprimido(this.id);"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="14px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblCodigoResiduo" runat="server" BorderWidth="0" Font-Names="Tahoma" Font-Size="9px" Height="9px" CssClass="BotaoSelecao" Width="30px" Text='<%# Bind("CodigoResiduo")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="_visibility"/>
                                        <ItemStyle CssClass="_visibility"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblNumeroLancamento" runat="server" BorderWidth="0" Font-Names="Tahoma" Width="30px" Text='<%# Bind("NumeroLancamento")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="_visibility"/>
                                        <ItemStyle CssClass="_visibility"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblCodigoCliente" runat="server" BorderWidth="0" Font-Names="Tahoma" Width="30px" Text='<%# Bind("CodigoCliente")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="_visibility"/>
                                        <ItemStyle CssClass="_visibility"/>
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
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;">
                        <asp:Label ID="lblTotal" runat="server" CssClass="LetrasLabel" Font-Bold="True" Text="Total quantidade: 0,00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table id="tableBotoes">
                            <tr>
                                <td>
                                    <div style="width:270px;">
                                        <asp:Label ID="lblDataInicial" CssClass="LetrasLabel" runat="server" Height="20px">Data inicial:</asp:Label>
                                        <uc6:DATA ID="datDataInicial" runat="server" />
                                        <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Ok" Height="22px" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="styeDivBotoes">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Salvar" ID="Salvar" OnClick="Salvar_Click" TabIndex="1" />
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Imprimir MTR" ID="btnMTRImprimir" OnClick="btnMTRImprimir_Click" TabIndex="1" />
                                        <input id="inpImprimir" type="button" value="Imprimir" onclick="Imprimir();" />
                                        <asp:Button ID="btnEnviar" runat="server" Font-Size="10pt" Text="Enviar" OnClick="btnEnviar_Click" />
                                        <input id="btnCancelar" type="button" value="Voltar a tela inicial" onclick="Novo();" />
                                    </div>
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hifNumeroLancamento" runat="server" />
                                    <asp:HiddenField ID="hifNumeroMTR" runat="server" />
                                </td>
                                <td style="width:100%">
                                    <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px"/>
                                    <asp:HiddenField ID="hifCodigoResiduo" runat="server" />
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                    <asp:HiddenField ID="hifLinhasGrade" runat="server" />
                                    <asp:DropDownList ID="pddlDestinoFinal" runat="server" Visible="false">
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
