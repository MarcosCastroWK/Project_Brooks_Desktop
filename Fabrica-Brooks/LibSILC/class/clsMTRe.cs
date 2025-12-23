namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.IO;
    using Newtonsoft.Json;
    using System.Text;

    public partial class clsMTRe
    {
    }
    public partial class clsMTReLogin
    {
        public long manifestoCodigo { get; set; }
        public string cnpGerador { get; set; }
        public string cnpTransportador { get; set; }
        public string cnpDestinador { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public clsMTReLogin()
        {
            cnpTransportador = "03938048000133";
            login = "03938048000133";
            senha = "b10757";
        }
    }
    public partial class clsPostMTReConsulta
    {
        public long manifestoCodigo { get; set; }
        public long situacaoManifestoCodigo { get; set; }
        public long retornoCodigo { get; set; }
        public string situacaoManifestoDescricao { get; set; }
        public string cnpGerador { get; set; }
        public string cnpTransportador { get; set; }
        public string cnpDestinador { get; set; }
        public string retorno { get; set; }

        public clsPostMTReConsulta()
        {
        }
    }

    public partial class LoteManifestoJSON
    {
        public static long serialVersionUID = 1L;
        public string login { get; set; }
        public string senha { get; set; }
        public string cnp { get; set; }
        public int codUnidade { get; set; }
        public List<ManifestoJSONDto> manifestoJSONDtos { get; set; }
    }

    public partial class ManifestoJSONDto
    {
        public static long serialVersionUID = 1L;

        public long manifestoCodigo { get; set; }
        public int retornoCodigo { get; set; }
        public string cnpGerador { get; set; }
        public int codUnidadeGerador { get; set; }
        public string cnpTransportador { get; set; }
        public int codUnidadeTransportador { get; set; }
        public string cnpDestinador { get; set; }
        public string codUnidadeDestinador { get; set; }
        public string cnpArmazenador { get; set; }
        public int codUnidadeArmazenador { get; set; }
        public int situacaoManifestoCodigo { get; set; }
        public string manifData { get; set; }
        public string manifDataExpedicao { get; set; }
        public string manifObservacao { get; set; }
        public string manifGeradorNomeResponsavel { get; set; }
        public string manifGeradorCargoResponsavel { get; set; }
        public string manifTransportadorNomeMotorista { get; set; }
        public string manifTransportadorPlacaVeiculo { get; set; }
        public string manifTransportadorDataExpedicao { get; set; }
        public string retorno { get; set; }
        public List<ItemManifestoJSONDto> itemManifestoJSONs = new List<ItemManifestoJSONDto>();
        public ManifestoJSONDto()
        {

        }
    }

    public partial class ItemManifestoJSONDto
    {
        public static long serialVersionUID = 1L;
        public int codigoSequencial { get; set; }
        public string justificativa { get; set; }
        public string codigoInterno { get; set; }
        public double quantidade { get; set; }
        public string residuo { get; set; }
        public int codigoAcondicionamento { get; set; }
        public int codigoClasse { get; set; }
        public int codigoTecnologia { get; set; }
        public int codigoTipoEstado { get; set; }
        public int codigoUnidade { get; set; }
        public string manifestoItemObservacao { get; set; }
        public string manifestoItemCodInterno { get; set; }
        public string manifestoItemCodInternoDestinador { get; set; }
        public string tipoDensidadeValor { get; set; }
        public string tipoDensidadeUnidade { get; set; }

        public string NumeroONU { get; set; }
        public string ClasseDeRisco { get; set; }
        public string NomeEmbarque { get; set; }
        public string GrupoEmbalagem { get; set; }

        public ItemManifestoJSONDto()
        {
        }
    }
    public partial class clsJsonCriar
    {
        public clsJsonCriar()
        {
        }
        public string CriarJsonGeraLote(ManifestoJSONDto pManifestoDTO, string pLogin, string pSenha, string pSequencialProgramacao)
        {
            string _ret = "";
            _ret = _ret + "{\"login\":\"" + LibSILC.geral.RetiraCharsCNPJCPF(pLogin) + "\", \n";
            _ret = _ret + " \"senha\":\"" + pSenha  + "\", \n";
            _ret = _ret + " \"cnp\":\"" + pManifestoDTO.cnpGerador + "\", \n";
            //_ret = _ret + " \"cnp\":\"03938048000133\", \n";
            if (pManifestoDTO.codUnidadeGerador > 0)
                _ret = _ret + " \"codUnidade\":" + pManifestoDTO.codUnidadeGerador.ToString() + ", \n";
            else
                _ret = _ret + " \"codUnidade\":null, \n";
            _ret = _ret + " \"manifestoJSONDtos\":";
            _ret = _ret + " [";
            if (pManifestoDTO.cnpTransportador != "" && pManifestoDTO.cnpTransportador != null)
                _ret = _ret + "  {\"cnpTransportador\":\"" + pManifestoDTO.cnpTransportador + "\", \n";
            else
                _ret = _ret + "  {\"cnpTransportador\":null, \n";

            _ret = _ret + "   \"codUnidadeDestinador\":" + pManifestoDTO.codUnidadeDestinador + ", \n";
            _ret = _ret + "   \"cnpDestinador\":\"" + pManifestoDTO.cnpDestinador + "\", \n";
            
            if (pManifestoDTO.cnpArmazenador != "" && pManifestoDTO.cnpArmazenador != null)
                _ret = _ret + "   \"cnpArmazenador\": \"" + pManifestoDTO.cnpArmazenador + "\", \n";
            else
                _ret = _ret + "   \"cnpArmazenador\":null, \n";

            if (pManifestoDTO.codUnidadeArmazenador > 0)
                _ret = _ret + "   \"codUnidadeArmazenador\": " + pManifestoDTO.codUnidadeArmazenador + ", \n";
            else
                _ret = _ret + "   \"codUnidadeArmazenador\":null, \n";
            _ret = _ret + "   \"seuCodigoReferencia\":\"" + pSequencialProgramacao + "\", \n";
            if (pManifestoDTO.manifObservacao != "")
                _ret = _ret + "   \"manifObservacao\": \"" + pManifestoDTO.manifObservacao + "\", \n";
            else
                _ret = _ret + "   \"manifObservacao\":null, \n";
            _ret = _ret + "   \"manifGeradorNomeResponsavel\":\"" + pManifestoDTO.manifGeradorNomeResponsavel + "\", \n";
            _ret = _ret + "   \"manifGeradorCargoResponsavel\":\"" + pManifestoDTO.manifGeradorCargoResponsavel + "\", \n";
            _ret = _ret + "   \"manifTransportadorNomeMotorista\":\"" + pManifestoDTO.manifTransportadorNomeMotorista + "\", \n";
            _ret = _ret + "   \"manifTransportadorPlacaVeiculo\":\"" + pManifestoDTO.manifTransportadorPlacaVeiculo + "\", \n";
            _ret = _ret + "   \"manifTransportadorDataExpedicao\":\"" + pManifestoDTO.manifTransportadorDataExpedicao + "\", \n";
            _ret = _ret + "   \"itemManifestoJSONs\": ";
            _ret = _ret + "    [";
            int iitem = 0;
            foreach (ItemManifestoJSONDto _item in pManifestoDTO.itemManifestoJSONs)
            {
                _ret = _ret + "    {\"codigoSequencial\":1, \n";
                _ret = _ret + "     \"justificativa\":null, \n";
                _ret = _ret + "     \"codigoInterno\":\"" + _item.codigoInterno + "\", \n";
                _ret = _ret + "     \"quantidade\":" + _item.quantidade.ToString("N2").Replace(",", ".") + ", \n";
                _ret = _ret + "     \"residuo\":\"" + _item.residuo.Replace(" ", "") + "\", \n";
                _ret = _ret + "     \"codigoAcondicionamento\":" + _item.codigoAcondicionamento + ", \n";
                _ret = _ret + "     \"codigoClasse\":" + _item.codigoClasse + ", \n";
                _ret = _ret + "     \"codigoTecnologia\":" + _item.codigoTecnologia + ", \n";
                _ret = _ret + "     \"codigoTipoEstado\":" + _item.codigoTipoEstado + ", \n";
                _ret = _ret + "     \"codigoUnidade\":" + _item.codigoUnidade + ", \n";
                //_ret = _ret + "     \"manifestoItemObservacao\":\"" + _item.manifestoItemObservacao + "\", \n";
                _ret = _ret + "     \"manifestoItemObservacao\":null, \n";
                _ret = _ret + "     \"manifestoItemCodInterno\":\"" + _item.manifestoItemCodInterno + "\", \n";
                _ret = _ret + "     \"manifestoItemCodInternoDestinador\":\"" + _item.manifestoItemCodInternoDestinador + "\", \n";
                _ret = _ret + "     \"tipoDensidadeValor\":" + _item.tipoDensidadeValor + ", \n";
                if (_item.codigoClasse != 1)
                    _ret = _ret + "     \"tipoDensidadeUnidade\":" + _item.tipoDensidadeUnidade + " \n";
                else
                {
                    _ret = _ret + "     \"tipoDensidadeUnidade\":" + _item.tipoDensidadeUnidade + ", \n";
                    _ret = _ret + "     \"numeroONU\":\"" + _item.NumeroONU + "\", \n";
                    _ret = _ret + "     \"classeDeRisco\":\"" + _item.ClasseDeRisco + "\", \n";
                    _ret = _ret + "     \"nomeEmbarque\":\"" + _item.NomeEmbarque + "\", \n";
                    _ret = _ret + "     \"grupoEmbalagem\":\"" + _item.GrupoEmbalagem + "\" \n";
                }
                iitem++;
                if (iitem == 1 && pManifestoDTO.itemManifestoJSONs.Count > 1)
                    _ret = _ret + "    }, \n"; // a diferença é só a vírgula
                else
                    _ret = _ret + "    } \n";
            }
            _ret = _ret + "   ] \n";
            _ret = _ret + "  } \n";
            _ret = _ret + " ] \n";
            _ret = _ret + "} \n";
            return _ret;
        }
    }
}
