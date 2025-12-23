<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DDR.aspx.cs" Inherits="SILC.Web.forms.DDR" %>
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
        .auto-style1 {
            width: 50%;
            height: 30px;
        }
        .auto-style3 {
            width: 803px;
        }
        .auto-style4 {
            width: 1190px;
        }
        .auto-style5 {
            width: 10px;
        }
    </style>
    </head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script src="../Scripts/funcaoGeral.js"></script>
<script lang="javascript" type="text/javascript">
    function Imprimir()
    {
        try
        {
            document.getElementById("btnMontaDDR").style.visibility = "hidden";
            document.getElementById("btnImprimir").style.visibility = "hidden";
            document.getElementById("butVoltar").style.visibility = "hidden";
            //document.getElementById("chkTotalResiduo").style.visibility = "hidden";
            //document.getElementById("lblTotalResiduo").style.visibility = "hidden";
            window.print();
        }
        finally
        {
            document.getElementById("btnMontaDDR").style.visibility = "visible";
            document.getElementById("btnImprimir").style.visibility = "visible";
            document.getElementById("butVoltar").style.visibility = "visible";
            //document.getElementById("chkTotalResiduo").style.visibility = "visible";
            //document.getElementById("lblTotalResiduo").style.visibility = "visible";
        }
    }
    function FazTotalPorResiduo()
    {
        document.getElementById("<%=btnMontaDDR.ClientID%>").click();
        //window.open('PesquisaMotoristas.aspx', '_blank', 'modalDialog');
    }
    function _linkMTRe(pX) {
        window.open('http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=manifesto&manifesto=' + pX + '&condicao=N');
    }
    function _linkCDFe(pX) {
        window.open('http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=certificado_destinacao_final&condicao=cdf&manifesto=' + pX);
    }

</script>
<body>
     <form id="form1" runat="server">
        <div id="Operacoes">
            <table class="auto-style4">
                <tr>
                    <td id="dadosCab" style="vertical-align:middle; height: 100px; align-content:center; width:1190px;word-spacing:0;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" />
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="lblTitulo" runat="server" Text="DDR - DECLARAÇÃO DE DESTINAÇÃO DE RESÍDUOS" Font-Bold="True" CssClass="tituloFundoBranco" Font-Size="12pt"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:1290px;height:50px;border:1px solid black; font-family:Arial; font-size:9pt;">

                                    &nbsp;BROOKS AMBIENTAL LTDA, CNPJ:03.938.048/0001-33, situada a Av. Ivo Lucchi, 729-Área Industrial, Palhoça-SC, portadora da(s) Licença(s) Ambiental de Operação:
                                    <asp:Label ID="lblLAOs" runat="server" Text="LAOs"></asp:Label>
&nbsp;declara que coletou<br />
&nbsp;do Gerador&nbsp;identificado abaixo, no período de <asp:Label ID="lblPeriodoDesejado" runat="server" ForeColor="Maroon" Text="Selecione o periodo desejado:"></asp:Label>
                                    &nbsp;<uc6:DATA ID="txtDataInicial" runat="server" />
                                    <uc6:DATA ID="txtDataFinal" runat="server" />
                                            ,&nbsp;os Resíduos listados a seguir:<asp:CheckBox ID="chkTotalResiduo" runat="server" onclick="FazTotalPorResiduo();"/><asp:Label ID="lblTotalResiduo" runat="server" Text="Total p/Resíduo"></asp:Label>
                                        <asp:Button ID="btnMontaDDR" runat="server" OnClick="btnMontaDDR_Click" Text="Ok" OnClientClick="DesabilitaOperacoes(this.id, 'txtDataInicial', 'txtDataFinal', 'DDR - DECLARAÇÃO DE DESTINAÇÃO DE RESÍDUOS');" />
                                        <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprimir();" class="letralabel" />
                                        <asp:Button ID="butVoltar" runat="server" Text="Voltar" OnClick="butVoltar_Click" />
                                </td>
                            </tr>
                        </table>
                     </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;
                        Informações do Gerador</td>
                </tr>
                <tr>
                    <td style="border:1px solid black; font-family:Arial; font-size:9pt;" class="auto-style1">
                        Código-Razão Social:<asp:TextBox ID="txtCodigoCliente" runat="server" MaxLength="6" Width="54px" BorderWidth="0px"></asp:TextBox>
                        <asp:Label ID="lblNomeCliente" runat="server" CssClass="tituloFundoBranco" Text="Nome cliente:"></asp:Label>
                        <asp:Label ID="lblCNPJ" runat="server" CssClass="tituloFundoBranco" Text="CNPJ: 00.000.000/0001-00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width:1190PX; border:1px solid black; font-family:Arial; font-size:9pt;">
                        <asp:Label ID="lblEnderecoT" runat="server" Text="Endereço:"></asp:Label>
                        <asp:Label ID="lblEndereco" runat="server" CssClass="tituloFundoBranco" Text="Endereço"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="lblDadosLAO" runat="server" Text=" Informações do(s) Resíduo(s)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width:1190px" cellpadding="0" cellspacing="0">
                                    <asp:GridView ID="GradeMTR" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" OnRowDataBound="GradeMTR_RowDataBound" Width="1190px" GridLines="Vertical">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="DescricaoGrupo" HeaderText="Descrição grupo">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DescricaoResiduo" HeaderText="Denominação">
                                            <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Classe" HeaderText="Classe">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="35"/>
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoIBAMA" HeaderText="Código IBAMA">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="60"/>
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumeroMTRFatima" HeaderText="Nº MTR-e" >
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="80"/>
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DataColeta" HeaderText="Data Coleta" DataFormatString="{0: dd/MM/y}">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="60" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtdeColetada" HeaderText="Quantidade Gerada" DataFormatString="{0:n2}" >
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtdeDTR" DataFormatString="{0:n2}" HeaderText="Quantidade Armazenada">
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Right" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtdeDestinada" HeaderText="Quantidade Destinada" DataFormatString="{0:n2}">
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DataDestinada" HeaderText="Data Destinada" DataFormatString=" {0: dd/MM/y}">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="60" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unidade" HeaderText="Un">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="20" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Tecnologia Aplicada" DataField="TecnologiaAplicada" />
                                            <asp:BoundField DataField="Deposito" HeaderText="Destinador Final" />
                                            <asp:BoundField DataField="NumeroCDFe" HeaderText="CDF-e Nº" />
                                            <asp:BoundField DataField="NumeroLancamento" HeaderText="Nº Lançamento" Visible="False">
                                            <HeaderStyle CssClass="padItemGrade" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoDestinoFinal" HeaderText="CodigoDestinoFinal" Visible="false" />
                                            <asp:BoundField DataField="DataDescarga" DataFormatString=" {0: dd/MM/y}" HeaderText="DataDescarga" Visible="False" />
                                            <asp:BoundField DataField="QtdeCDF" HeaderText="Qtde CDF" />
                                            <asp:BoundField DataField="Situacao" HeaderText="Situacao" />
                                        </Columns>
                                        <EditRowStyle Wrap="False" />
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="DimGray" BorderWidth="1"/>
                                        <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#F7F7DE" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </td>
                                <td class="auto-style5"></td>
                            </tr>
                            <tr>
                                <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                                    &nbsp;&nbsp;<asp:Label ID="lblDestinador" runat="server" Text="Dados do Licenciamento Ambiental do Destinador "></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td style="width:1190px" cellpadding="0" cellspacing="0">
                                    <asp:GridView ID="GradeDestinador" runat="server" CellPadding="4" BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" OnRowDataBound="GradeDestinador_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="NomeDestinoFinal" HeaderText="Destinador Final" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumeroLicenca" HeaderText="Licença Ambiental Operação/Arquivo">
                                            <HeaderStyle CssClass="padItemGrade" Width="220px" HorizontalAlign="Left" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoAtividade" HeaderText="Código Atividade Principal">
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PrazoValidade" HeaderText="Prazo de Validade" >
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Observacao" HeaderText="Observação">
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="200px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" Width="400px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoAterro" HeaderText="cd">
                                            <HeaderStyle ForeColor="White" Width="1px" />
                                            <ItemStyle ForeColor="White" Width="1px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                                        <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </td>
                                <td class="auto-style5">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-family:Arial; font-size:9pt;">
                                    <br />
                                    <asp:Label ID="lblDeclaracao" runat="server" Text="Declaração"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblDeclaracao1" runat="server" Text="Este documento certifica o recebimento e o respectivo encaminhamento a destinação final dos resíduos acima relacionados, utilizando-se às tecnologias mencionadas. "></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDeclaracao2" runat="server" Text="A validade desta informação está restrita aos resíduos declarados e às suas respectivas quantidades.
"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblCidadeEmpresa" runat="server" Text="Palhoça,"></asp:Label>
&nbsp;<asp:Label ID="lblDataEmissao" runat="server" Text="Data de emissão"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/assinatura sergio.jpg" Visible="False" />
                                </td>
                            </tr>
                        </table>                       
                        <asp:HiddenField ID="hifCodigo" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
