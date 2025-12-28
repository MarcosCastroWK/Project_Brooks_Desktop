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
            <asp:MenuItem Text="CaminhÃµes" Value="Caminhoes"></asp:MenuItem>
            <asp:MenuItem Text="Clientes" Value="Clientes">
                <asp:MenuItem Text="Incluir" Value="ClientesIncluir"></asp:MenuItem>
                <asp:MenuItem Text="Alterar/Consultar" Value="Clientes"></asp:MenuItem>
                <asp:MenuItem Text="Bloqueio Financeiro" Value="BloqueioFinanceiro"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Containeres" Value="Containeres"></asp:MenuItem>
            <asp:MenuItem Text="Contratos" Value="Contratos">
                <asp:MenuItem Text="Incluir" Value="Cadastrar_Contrato"></asp:MenuItem>
                <asp:MenuItem Text="Alterar/Consultar" Value="AlterarConsultar_Contrato"></asp:MenuItem>
                <asp:MenuItem Text="Reajustar/Repactuar/HistÃ³rico" Value="ReajustarRepactuar"></asp:MenuItem>
                <asp:MenuItem Text="RescisÃµes" Value="Rescisoes"></asp:MenuItem>
                <asp:MenuItem Text="Alterar/Consultar-antigo" Value="Alterar_Contrato"></asp:MenuItem>
                <asp:MenuItem Text="ContratoIncluirResiduos-teste" Value="contratosresiduosteste" NavigateUrl="~/forms/Contratos_Residuos.aspx?CodigoGerado=838"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Destino Final" Value="DestinoFinal"></asp:MenuItem>
            <asp:MenuItem Text="IBAMA" Value="IBAMA"></asp:MenuItem>
            <asp:MenuItem Text="Motorista/Colaborador" Value="Motoristas"></asp:MenuItem>
            <asp:MenuItem Text="MunicÃ­pios" Value="MunicÃ­pios"></asp:MenuItem>
            <asp:MenuItem Text="ResÃ­duos" Value="Residuos"></asp:MenuItem>
            <asp:MenuItem Text="UsuÃ¡rios" Value="Usuarios" NavigateUrl="Usuarios.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Tipo Acondicionamento" Value="Acondicionamento" NavigateUrl="~/forms/TipoAcondicionamento.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="ProgramaÃ§Ã£o ServiÃ§os" Value="ProgramacaoServicos" Selectable="False">
            <asp:MenuItem Text="Consulta ProgramaÃ§Ã£o DiÃ¡ria" Value="ProgramacaoDiaria" NavigateUrl="programacao.aspx"></asp:MenuItem>
            <asp:MenuItem Text="ProgramaÃ§Ã£o DiÃ¡ria" Value="Programacao"></asp:MenuItem>
            <asp:MenuItem Text="InserÃ§Ã£o a partir do comercial" Value="ProgramacaoInsercaoComercial"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="LanÃ§amentos" Value="Lancamentos" Selectable="False">
            <asp:MenuItem Text="LocaÃ§Ãµes" Value="Locacoes" Selectable="False">                    
                <asp:MenuItem Text="LocaÃ§Ãµes" Value="Locacao"></asp:MenuItem>
                <asp:MenuItem Text="LocaÃ§Ãµes a partir da programaÃ§Ã£o" Value="LocacaoProgramacao"></asp:MenuItem>
                <asp:MenuItem Text="Tickets pendentes" Value="TicketsPendentes"></asp:MenuItem>
                <asp:MenuItem Text="MTRs pendentes" Value="MTRsPendentes"></asp:MenuItem>
                <asp:MenuItem Text="DistribuiÃ§Ã£o / cÃ¡lculo de quantidades descarregas" Value="Distribuicao"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Consulta Notas Fiscais" Value="ConsultaNotasFiscais" NavigateUrl="~/forms/NotasFiscais.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Notas Fiscais" Value="NotasFiscais"></asp:MenuItem>
            <asp:MenuItem Text="DistribuiÃ§Ã£o Blocos MTR" Value="DistribuiÃ§Ã£o Blocos MTR"></asp:MenuItem>
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
        <asp:MenuItem Text="ImportaÃ§Ãµes" Value="Importacoes" Selectable="False">
            <asp:MenuItem Text="RelatÃ³rio da MTR-e -IMA" Value="RelatorioMTReIMA"></asp:MenuItem>
            <asp:MenuItem Text="Sistema IMA - Atualizar Senhas" Value="SistemaIMA_AtualizarSenha"></asp:MenuItem>
            <asp:MenuItem Text="Importar dados Radar - munÃ­cipios" Value="ImportarMunicipiosRadar"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio da MTRe IMA para ConferÃªncia diÃ¡ria" Value="RelatorioMTReFatimaConferenciaDiaria"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="AtualizaÃ§Ã£o de dados" Value="AtualizacaoDados"></asp:MenuItem>
        <asp:MenuItem Text="DocumentaÃ§Ã£o AplicÃ¡vel" Value="DocumentacaoAplicavel"></asp:MenuItem>
        <asp:MenuItem Text="Controle de Aterro SanitÃ¡rio" Value="ControleAterroSanitario" NavigateUrl="~/forms/ControleAterroSanitario.aspx"></asp:MenuItem>
        <asp:MenuItem Text="RelatÃ³rios" Value="Relatorios">
            <asp:MenuItem Text="RelatÃ³rio de Containeres Locadas ou DisponÃ­veis" Value="RelatÃ³rio de Containeres Locadas ou DisponÃ­veis" NavigateUrl="~/Relatorios/RelatorioContaineres.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de ConferÃªncia DiÃ¡ria" Value="RelatÃ³rio de ConferÃªncia DiÃ¡ria" NavigateUrl="~/Relatorios/RelatorioConferenciaDiaria.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de ConferÃªncia por PerÃ­odo" Value="RelatÃ³rio de ConferÃªncia por PerÃ­odo" NavigateUrl="~/Relatorios/RelatorioConferenciaDiariaPorPeriodo.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de ConferÃªncia Cliente" Value="RelatÃ³rio de ConferÃªncia Cliente" NavigateUrl="~/Relatorios/RelatorioConferenciaDiariaPorCliente.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio para faturamento com ResÃ­duos" Value="RelatÃ³rio para faturamento com ResÃ­duos" NavigateUrl="~/Relatorios/RelatorioParaFaturamentoComResiduos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio para faturamento - Cliente" Value="RelatÃ³rio para faturamento - Cliente" NavigateUrl="~/Relatorios/RelatorioParaFaturamentoParaCliente.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de Logs" Value="RelatÃ³rio de Logs" NavigateUrl="~/Relatorios/RelatorioDeLogs.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de Indicadores" Value="RelatorioIndicadores"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de ReciclÃ¡veis" Value="RelatÃ³rio de ReciclÃ¡veis" NavigateUrl="~/Relatorios/RelatorioReciclaveis.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio Gerencial" Value="RelatÃ³rio Gerencial" NavigateUrl="~/Relatorios/RelatorioGerencial.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio por Destino Final (ReciclÃ¡veis)" Value="RelatÃ³rio por Destino Final (ReciclÃ¡veis)" NavigateUrl="~/Relatorios/RelatorioReciclaveisPorDestinoFinal.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de Notas Fiscais e Impostos" Value="RelatÃ³rio de Notas Fiscais e Impostos" NavigateUrl="~/Relatorios/RelatorioNotasFiscaisImpostos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio movimentaÃ§Ã£o de aterro com nÂº da MTRe" Value="RelatÃ³rio movimentaÃ§Ã£o de aterro com nÂº da MTRe" NavigateUrl="~/Relatorios/RelatorioMovimentacaoDestinoFinal.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio movimentaÃ§Ã£o de resÃ­duos para destino final" Value="RelatÃ³rio movimentaÃ§Ã£o de resÃ­duos para destino final" NavigateUrl="~/Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinal.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio movimentaÃ§Ã£o de resÃ­duos para destino final com Valores" Value="RelatÃ³rio movimentaÃ§Ã£o de resÃ­duos para destino final com Valores" NavigateUrl="~/Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinalComValores.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio movimentaÃ§Ã£o de resÃ­duos para destino final Divergentes" Value="RelatÃ³rio movimentaÃ§Ã£o de resÃ­duos para destino final Divergentes" NavigateUrl="~/Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinalDivergentes.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de Clientes Bloqueados" Value="RelatÃ³rio de Clientes Bloqueados" NavigateUrl="~/Relatorios/RelatorioDeClientesBloqueados.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de Clientes a Reajustar" Value="RelatÃ³rio de Clientes a Reajustar" NavigateUrl="~/Relatorios/RelatorioDeClientesAReajustar.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio Total de ResÃ­duos" Value="RelatÃ³rio Total de ResÃ­duos" NavigateUrl="~/Relatorios/RelatorioTotalResiduos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de Vendas por Representante por perÃ­odo" Value="RelatÃ³rio_de_Vendas_por_Representante_por_periodo" NavigateUrl="~/Relatorios/RelatorioVendasPorRepresentantePorPeriodo.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de ResÃ­duos" Value="RelatÃ³rio_de_Residuos" NavigateUrl="~/Relatorios/RelatorioResiduos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de consultas SQL" Value="Relatorio_de_Consultas" NavigateUrl="~/Relatorios/Relatorios_Querys.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RelatÃ³rio de MovimentaÃ§Ã£o por destino final com ResÃ­duos em DTR" Value="RelatorioMovimentacaoPorDestinoFinalComResiduosEmDTR" NavigateUrl="~/Relatorios/RelatorioMovimentacaoPorDestinoFinalComResiduosEmDTR.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="ConfiguraÃ§Ãµes" Value="Configuracoes">
            <asp:MenuItem Text="Gerais" Value="Gerais" NavigateUrl="~/forms/ConfigSis.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Alterar Senha IPM" Value="Alterar Senha IPM" NavigateUrl="~/forms/SenhaIPM.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="ParÃ¢metros" Value="Parametros">
            <asp:MenuItem Text="Cadastro" Value="Cadastro" NavigateUrl="~/forms/Parametros.aspx"></asp:MenuItem>
            <asp:MenuItem Text="CÃ³digos dos Servicos da Prefeitura" Value="CÃ³digos dos Servicos da Prefeitura" NavigateUrl="~/forms/CodigosServicosPrefeitura.aspx"></asp:MenuItem>
            <asp:MenuItem Text="DescriÃ§Ã£o ServiÃ§os para NF" Value="DescriÃ§Ã£o ServiÃ§os para NF" NavigateUrl="~/forms/DescricaoServicosNF.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Liberar ProgramaÃ§Ã£o de ServiÃ§os" Value="Liberar ProgramaÃ§Ã£o de ServiÃ§os" NavigateUrl="~/forms/LiberarProgramacaoServicos.aspx"></asp:MenuItem>
            <asp:MenuItem Text="e-Mail padrÃ£o para Envio" Value="e-Mail padrÃ£o para Envio" NavigateUrl="~/forms/EmailsPadraoEnvio.aspx"></asp:MenuItem>
            <asp:MenuItem Text="RetenÃ§Ã£o Impostos NF" Value="RetenÃ§Ã£o Impostos NF" NavigateUrl="~/forms/RetencaoImpostosNF.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Documentos gerais (LAO/Ãlvara/ISO9002)" Value="Documentos PÃ¡gina BROOKS" NavigateUrl="~/forms/DocumentosPagina.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Escolher Banco de Dados" Value="EscolherDB" NavigateUrl="~/forms/EscolherDB.aspx"></asp:MenuItem>
            <asp:MenuItem Text="PermissÃ£o Itens Menu Principal" Value="ItensMenu" NavigateUrl="~/forms/ItensMenuPermissoes.aspx"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="ServiÃ§os" Value="ServiÃ§os">
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
