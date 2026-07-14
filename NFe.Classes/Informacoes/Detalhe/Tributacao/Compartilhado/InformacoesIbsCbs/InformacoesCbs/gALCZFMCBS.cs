/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/********************************************************************************/

using DFe.Classes;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs.InformacoesCbs
{
    /// <summary>
    ///     UB66a - Grupo de operações em áreas incentivadas (ALC/ZFM) com alíquota zero da CBS.
    ///     Conforme arts. 451 e 466 da LC 214/2025 (NT 2025.002-RTC), quando fornecedor e
    ///     destinatário estiverem nessas áreas.
    /// </summary>
    public class gALCZFMCBS
    {
        private decimal _pAliqEfetRegCbs;
        private decimal _vTribRegCbs;

        /// <summary>
        ///     UB66c - Alíquota efetiva do regime da CBS (em percentual)
        /// </summary>
        [XmlElement(Order = 1)]
        public decimal pAliqEfetRegCBS
        {
            get => _pAliqEfetRegCbs.Arredondar(4);
            set => _pAliqEfetRegCbs = value.Arredondar(4);
        }

        /// <summary>
        ///     UB66e - Valor do tributo do regime da CBS
        /// </summary>
        [XmlElement(Order = 2)]
        public decimal vTribRegCBS
        {
            get => _vTribRegCbs.Arredondar(2);
            set => _vTribRegCbs = value.Arredondar(2);
        }
    }
}
