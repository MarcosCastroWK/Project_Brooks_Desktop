<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="SILC.Web.forms.menu" %>
<style>
     .body
     {
        background-color: white;
        font: 12px Arial;
        margin: 0px;
        padding: 20px;
    }
    .titulo
    {
        background-color:ButtonFace;
        font-size: 14px; 
        margin: 10px;
        padding: 4px;
    }
    .form
    {
        position: absolute;
        top: 5%;
        left: 1%;
        margin-top: auto;
        margin-left: auto;
        color: black;
        background-color: ButtonFace;
        font-weight: 900;
        border: ridge 8px ActiveBorder;
        height: 600px;
    }
</style>
<script>
    function confirmacaoSaida(pId)
    {
        var r = confirm("Confirma!");
        if (r == true) {
        }
    }
</script>
<asp:Menu ID="Menu1" runat="server" Width="10%" Orientation="Horizontal" Font-Bold="False" BackColor="#E3EAEB" Font-Names="Arial" Font-Size="11px" ForeColor="Black" StaticSubMenuIndent="18px" RenderingMode="Table" ScrollDownText="" OnMenuItemClick="Menu1_MenuItemClick">
    <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="5px" />
    <DynamicMenuStyle BackColor="#E3EAEB" />
    <DynamicSelectedStyle BackColor="#1C5E55" />
    <Items>
        <asp:MenuItem Text="Cadastro" Value="Cadastro" Selectable="false">
            <asp:MenuItem Text="Caminhões" Value="Caminhoes"></asp:MenuItem>
            <asp:MenuItem Text="Clientes" Value="Clientes">
                <asp:MenuItem Text="Incluir" Value="ClientesIncluir"></asp:MenuItem>
                <asp:MenuItem Text="Alterar/Consultar" Value="Clientes"></asp:MenuItem>
                <asp:MenuItem Text="Bloqueio Financeiro" Value="BloqueioFinanceiro"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Containeres" Value="Containeres"></asp:MenuItem>
            <asp:MenuItem Text="Contratos" Value="Contratos">
                <asp:MenuItem Text="Incluir" Value="Cadastrar_Contrato"></asp:MenuItem>
                <asp:MenuItem Text="Alterar/Consultar" Value="AlterarConsultar_Contrato"></asp:MenuItem>
                <asp:MenuItem Text="Reajustar/Repactuar/Histórico" Value="ReajustarRepactuar"></asp:MenuItem>
                <asp:MenuItem Text="Rescisões" Value="Rescisoes"></asp:MenuItem>
                <asp:MenuItem Text="Alterar/Consultar-antigo" Value="Alterar_Contrato"></asp:MenuItem>
                <asp:MenuItem Text="ContratoIncluirResiduos-teste" Value="contratosresiduosteste" NavigateUrl="~/forms/Contratos_Residuos.aspx?CodigoGerado=838"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Destino Final" Value="DestinoFinal"></asp:MenuItem>
            <asp:MenuItem Text="IBAMA" Value="IBAMA"></asp:MenuItem>
            <asp:MenuItem Text="Motorista/Colaborador" Value="Motoristas"></asp:MenuItem>
            <asp:MenuItem Text="Municípios" Value="Municípios"></asp:MenuItem>
            <asp:MenuItem Text="Resíduos" Value="Residuos"></asp:MenuItem>
            <asp:MenuItem Text="Usuários" Value="Usuarios" NavigateUrl="Usuarios.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Tipo Acondicionamento" Value="Acondicionamento" NavigateUrl="~/forms/TipoAcondicionamento.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Programação Serviços" Value="ProgramacaoServicos" Selectable="False">
            <asp:MenuItem Text="Consulta Programação Diária" Value="ProgramacaoDiaria" NavigateUrl="programacao.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Programação Diária" Value="Programacao"></asp:MenuItem>
            <asp:MenuItem Text="Inserção a partir do comercial" Value="ProgramacaoInsercaoComercial"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Lançamentos" Value="Lancamentos" Selectable="False">
            <asp:MenuItem Text="Locações" Value="Locacoes" Selectable="False">                    
                <asp:MenuItem Text="Locações" Value="Locacao"></asp:MenuItem>
                <asp:MenuItem Text="Locações a partir da programação" Value="LocacaoProgramacao"></asp:MenuItem>
                <asp:MenuItem Text="Tickets pendentes" Value="TicketsPendentes"></asp:MenuItem>
                <asp:MenuItem Text="MTRs pendentes" Value="MTRsPendentes"></asp:MenuItem>
                <asp:MenuItem Text="Distribuição / cálculo de quantidades descarregas" Value="Distribuicao"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Consulta Notas Fiscais" Value="ConsultaNotasFiscais" NavigateUrl="~/forms/NotasFiscais.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Notas Fiscais" Value="NotasFiscais"></asp:MenuItem>
            <asp:MenuItem Text="Distribuição Blocos MTR" Value="Distribuição Blocos MTR"></asp:MenuItem>
            <asp:MenuItem Text="MTR Cancelada/Transbordo" Value="MTR Cancelada/Transbordo"></asp:MenuItem>
            <asp:MenuItem Text="DDR" Value="DDR" NavigateUrl="~/forms/DDR.aspx?Codigo=000000&BROOKS=BRO000935"></asp:MenuItem>
            <asp:MenuItem Text="CDF" Value="CDF" NavigateUrl="~/forms/CDF.aspx?Codigo=000000&BROOKS=BRO000935"></asp:MenuItem>
            <asp:MenuItem Text="RGR" Value="RGR" NavigateUrl="~/forms/RGR.aspx?BROOKS=BRO000935"></asp:MenuItem>
            <asp:MenuItem Text="RGR2" Value="RGR2" NavigateUrl="~/forms/RGR2.aspx?BROOKS=BRO000935"></asp:MenuItem>
            <asp:MenuItem Text="Upload RGR/DDR/CDF" Value="RGR" NavigateUrl="~/forms/uploadRGRCDFDDR.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Recibo Avulso" Value="Recibo Avulso"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="DTR" Value="DTR" Selectable="False">
            <asp:MenuItem Text="Armazenados web" Value="Armazenadosweb"></asp:MenuItem>
            <asp:MenuItem Text="Enviados" Value="Enviados"></asp:MenuItem>
            <asp:MenuItem Text="Armazenados" Value="DTR"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Importações" Value="Importacoes" Selectable="False">
            <asp:MenuItem Text="Relatório da MTR-e -IMA" Value="RelatorioMTReIMA"></asp:MenuItem>
            <asp:MenuItem Text="Sistema IMA - Atualizar Senhas" Value="SistemaIMA_AtualizarSenha"></asp:MenuItem>
            <asp:MenuItem Text="Importar dados Radar - munícipios" Value="ImportarMunicipiosRadar"></asp:MenuItem>
            <asp:MenuItem Text="Relatório da MTRe IMA para Conferência diária" Value="RelatorioMTReFatimaConferenciaDiaria"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Atualização de dados" Value="AtualizacaoDados"></asp:MenuItem>
        <asp:MenuItem Text="Documentação Aplicável" Value="DocumentacaoAplicavel"></asp:MenuItem>
        <asp:MenuItem Text="Controle de Aterro Sanitário" Value="ControleAterroSanitario" NavigateUrl="~/forms/ControleAterroSanitario.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Relatórios" Value="Relatorios">
            <asp:MenuItem Text="Relatório de Containeres Locadas ou Disponíveis" Value="Relatório de Containeres Locadas ou Disponíveis" NavigateUrl="~/Relatorios/RelatorioContaineres.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Conferência Diária" Value="Relatório de Conferência Diária" NavigateUrl="~/Relatorios/RelatorioConferenciaDiaria.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Conferência por Período" Value="Relatório de Conferência por Período" NavigateUrl="~/Relatorios/RelatorioConferenciaDiariaPorPeriodo.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Conferência Cliente" Value="Relatório de Conferência Cliente" NavigateUrl="~/Relatorios/RelatorioConferenciaDiariaPorCliente.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório para faturamento com Resíduos" Value="Relatório para faturamento com Resíduos" NavigateUrl="~/Relatorios/RelatorioParaFaturamentoComResiduos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório para faturamento - Cliente" Value="Relatório para faturamento - Cliente" NavigateUrl="~/Relatorios/RelatorioParaFaturamentoParaCliente.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Logs" Value="Relatório de Logs" NavigateUrl="~/Relatorios/RelatorioDeLogs.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Indicadores" Value="RelatorioIndicadores"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Recicláveis" Value="Relatório de Recicláveis" NavigateUrl="~/Relatorios/RelatorioReciclaveis.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório Gerencial" Value="Relatório Gerencial" NavigateUrl="~/Relatorios/RelatorioGerencial.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório por Destino Final (Recicláveis)" Value="Relatório por Destino Final (Recicláveis)" NavigateUrl="~/Relatorios/RelatorioReciclaveisPorDestinoFinal.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Notas Fiscais e Impostos" Value="Relatório de Notas Fiscais e Impostos" NavigateUrl="~/Relatorios/RelatorioNotasFiscaisImpostos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório movimentação de aterro com nº da MTRe" Value="Relatório movimentação de aterro com nº da MTRe" NavigateUrl="~/Relatorios/RelatorioMovimentacaoDestinoFinal.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório movimentação de resíduos para destino final" Value="Relatório movimentação de resíduos para destino final" NavigateUrl="~/Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinal.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório movimentação de resíduos para destino final com Valores" Value="Relatório movimentação de resíduos para destino final com Valores" NavigateUrl="~/Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinalComValores.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório movimentação de resíduos para destino final Divergentes" Value="Relatório movimentação de resíduos para destino final Divergentes" NavigateUrl="~/Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinalDivergentes.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Clientes Bloqueados" Value="Relatório de Clientes Bloqueados" NavigateUrl="~/Relatorios/RelatorioDeClientesBloqueados.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Clientes a Reajustar" Value="Relatório de Clientes a Reajustar" NavigateUrl="~/Relatorios/RelatorioDeClientesAReajustar.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório Total de Resíduos" Value="Relatório Total de Resíduos" NavigateUrl="~/Relatorios/RelatorioTotalResiduos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Vendas por Representante por período" Value="Relatório_de_Vendas_por_Representante_por_periodo" NavigateUrl="~/Relatorios/RelatorioVendasPorRepresentantePorPeriodo.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Resíduos" Value="Relatório_de_Residuos" NavigateUrl="~/Relatorios/RelatorioResiduos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de consultas SQL" Value="Relatorio_de_Consultas" NavigateUrl="~/Relatorios/Relatorios_Querys.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Relatório de Movimentação por destino final com Resíduos em DTR" Value="RelatorioMovimentacaoPorDestinoFinalComResiduosEmDTR" NavigateUrl="~/Relatorios/RelatorioMovimentacaoPorDestinoFinalComResiduosEmDTR.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Configurações" Value="Configuracoes">
            <asp:MenuItem Text="Gerais" Value="Gerais" NavigateUrl="~/forms/ConfigSis.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Alterar Senha IPM" Value="Alterar Senha IPM" NavigateUrl="~/forms/SenhaIPM.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Parâmetros" Value="Parametros">
            <asp:MenuItem Text="Cadastro" Value="Cadastro" NavigateUrl="~/forms/Parametros.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Códigos dos Servicos da Prefeitura" Value="Códigos dos Servicos da Prefeitura" NavigateUrl="~/forms/CodigosServicosPrefeitura.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Descrição Serviços para NF" Value="Descrição Serviços para NF" NavigateUrl="~/forms/DescricaoServicosNF.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Liberar Programação de Serviços" Value="Liberar Programação de Serviços" NavigateUrl="~/forms/LiberarProgramacaoServicos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="e-Mail padrão para Envio" Value="e-Mail padrão para Envio" NavigateUrl="~/forms/EmailsPadraoEnvio.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Retenção Impostos NF" Value="Retenção Impostos NF" NavigateUrl="~/forms/RetencaoImpostosNF.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Documentos gerais (LAO/Álvara/ISO9002)" Value="Documentos Página BROOKS" NavigateUrl="~/forms/DocumentosPagina.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Escolher Banco de Dados" Value="EscolherDB" NavigateUrl="~/forms/EscolherDB.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Permissão Itens Menu Principal" Value="ItensMenu" NavigateUrl="~/forms/ItensMenuPermissoes.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Serviços" Value="Serviços">
            <asp:MenuItem Text="Desenvolvedor" Value="Desenvolvedor" NavigateUrl="~/forms/ServicosDesenvolvedor.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Balanca" Value="Balanca" Target="_blank" NavigateUrl="~/forms/Balanca.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="../default.aspx" Text="Sair" Value="Sair"></asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#666666" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="5px" ItemSpacing="5px"  />
    <StaticSelectedStyle BackColor="#1C5E55" />
</asp:Menu>
<asp:HiddenField ID="hifCodigo" runat="server" />
<asp:HiddenField ID="hifNome" runat="server" />
<asp:HiddenField ID="hifCodigoEmpresa" runat="server" />