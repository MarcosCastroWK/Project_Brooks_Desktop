<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatorioParaFaturamentoComResiduos.aspx.cs" Inherits="RelatorioParaFaturamentoComResiduos" %>
<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>
<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />

    <script src="../Scripts/funcaoGeral.js"></script>
    <script>

        var tempo = new Number();
        // Tempo em segundos
        tempo = 3600;

        function startCountdown()
        {
	        // Se o tempo não for zerado
            if ((tempo - 1) >= 0)
            {
		        // Pega a parte inteira dos minutos
		        var min = parseInt(tempo/60);

		        // Calcula os segundos restantes
		        var seg = tempo%60;
		        // Formata o número menor que dez, ex: 08, 07, ...
		        if(min < 10){
			        min = "0"+min;
			        min = min.substr(0, 2);
		        }
		        if(seg <=9){
			        seg = "0"+seg;
		        }

		        // Cria a variável para formatar no estilo hora/cronômetro
		        horaImprimivel = min + ':' + seg;

		        //JQuery pra setar o valor
		        //$("#sessao").html(horaImprimivel);

		        // Define que a função será executada novamente em 1000ms = 1 segundo
                setTimeout('startCountdown()', 10000);

                document.getElementById('lblTempoRestante').innerText = 'Tempo limite para encerrar sessão: ' + horaImprimivel;

		        // diminui o tempo
                tempo--;

	        // Quando o contador chegar a zero faz esta ação
	        } else {
		        window.open('../forms/brooks/login.aspx', '_self');
	        }
        }

        // Chama a função ao carregar a tela
        startCountdown();

        function AbrePesquisaClientes() {
            var navegador = navigator.appName.toLowerCase();
            if (navegador.indexOf("internet") == -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                window.open('../forms/PesquisaClientes.aspx');
            }
            else if (navegador.indexOf("internet") > -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                location = '../forms/PesquisaClientes.aspx?explorer=../Relatorios/RelatorioParaFaturamentoComResiduos.aspx';
            }
        }
        function TiraOrdemCaminhao() {
            if (document.getElementById('rdbOrdemCaminhao').checked) {
                document.getElementById('rdbOrdemData').checked = false;
            }
            else if (document.getElementById('rdbOrdemCaminhao').checked == false) {
                document.getElementById('rdbOrdemData').checked = true;
            }
        }
        function TiraOrdemData() {
            if (document.getElementById('rdbOrdemData').checked) {
                document.getElementById('rdbOrdemCaminhao').checked = false;
            }
            else if (document.getElementById('rdbOrdemData').checked == false) {
                document.getElementById('rdbOrdemCaminhao').checked = true;
            }
        }
        function Imprime() {
            try {
                document.getElementById('Operacoes').style.visibility = "hidden";
                document.getElementById('Panel1').style.position = "absolute";
                document.getElementById('Panel1').style.top = 0;
                window.print();
            }
            finally {
                document.getElementById('Operacoes').style.visibility = "visible";
                document.getElementById('Panel1').style.position = "";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Operacoes">
            <uc1:cabecalho ID="cabecalho1" runat="server" />            
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório para Faturamento com Resíduos"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Período:" Width="30px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data1" runat="server" />
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="Label1" runat="server" CssClass="LetrasLabel" Text=" a " Width="20px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data2" runat="server" />
                    </td>
                    <td>
                        <uc2:CLIENTESCONTROL ID="CLIENTESCONTROL1" runat="server" onkeypress="TextoDoOk();" />
                    </td>

                    <td>
                        <img id="imagem" alt="x" src="../Images/procura.png" onclick="AbrePesquisaClientes()"/>
                    </td>
                    <td>
                        <asp:Button ID="btnProcurar" Visible="true" BackColor="White" ForeColor="White" BorderWidth="0" runat="server" Text="P" Width="1px" />            
                    </td>
                </tr>
            </table>
            <table style="padding: 0">
                <tr>
                    <td>
                        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click"  OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório para Faturamento com Resíduos');" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />    
                    </td>
                    <td>
                        <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="LetrasLabel" Text="Sem a informação do Código do Cliente será gerado relatório para todos clientes com contrato e ou clientes que têm lançamento." Width="343px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTempoRestante" runat="server" Text="mm:ss" CssClass="LetrasLabel"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <asp:Panel ID="Panel1" runat="server" Width="1336px">
        </asp:Panel>       
    </form>
</body>
</html>
