﻿/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS51 : ICMSBasico
    {
        private decimal? _pRedBc;
        private decimal? _vBc;
        private decimal? _pIcms;
        private decimal? _vIcmsOp;
        private decimal? _pDif;
        private decimal? _vIcmsDif;
        private decimal? _vIcms;
        private decimal? _vBcfcp;
        private decimal? _pFcp;
        private decimal? _vFcp;

        private decimal? _pFcpDif;
        private decimal? _vFcpDif;
        private decimal? _vFcpEfet;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12- Situação Tributária
        /// </summary>
        public Csticms CST { get; set; }

        /// <summary>
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        public DeterminacaoBaseIcms? modBC { get; set; }

        /// <summary>
        ///     N14 - Percentual de redução da BC
        /// </summary>
        public decimal? pRedBC
        {
            get { return _pRedBc.Arredondar(4); }
            set { _pRedBc = value.Arredondar(4); }
        }

        /// <summary>
        ///     N14a - digo de Benefício Fiscal na UF aplicado ao item quando houver RBC.
        /// </summary>
        public string cBenefRBC { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS
        /// </summary>
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     N16 - Alíquota do imposto
        /// </summary>
        public decimal? pICMS
        {
            get { return _pIcms.Arredondar(4); }
            set { _pIcms = value.Arredondar(4); }
        }

        /// <summary>
        ///     N16a - Valor do ICMS da Operação
        /// </summary>
        public decimal? vICMSOp
        {
            get { return _vIcmsOp.Arredondar(2); }
            set { _vIcmsOp = value.Arredondar(2); }
        }

        /// <summary>
        ///     N16b - Percentual do diferimento
        /// </summary>
        public decimal? pDif
        {
            get { return _pDif.Arredondar(2); }
            set { _pDif = value.Arredondar(2); }
        }

        /// <summary>
        ///     N16c - Valor do ICMS diferido
        /// </summary>
        public decimal? vICMSDif
        {
            get { return _vIcmsDif.Arredondar(2); }
            set { _vIcmsDif = value.Arredondar(2); }
        }

        /// <summary>
        ///     N17 - Valor do ICMS
        /// </summary>
        public decimal? vICMS
        {
            get { return _vIcms.Arredondar(2); }
            set { _vIcms = value.Arredondar(2); }
        }

        /// <summary>
        /// N17a - Valor da Base de Cálculo do FCP
        /// Versão 4.00
        /// </summary>
        public decimal? vBCFCP
        {
            get { return _vBcfcp.Arredondar(2); }
            set { _vBcfcp = value.Arredondar(2); }
        }

        public bool vBCFCPSpecified
        {
            get { return vBCFCP.HasValue; }
        }

        /// <summary>
        /// N17b - Percentual do Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        public decimal? pFCP
        {
            get { return _pFcp.Arredondar(4); }
            set { _pFcp = value.Arredondar(4); }
        }

        public bool pFCPSpecified
        {
            get { return pFCP.HasValue; }
        }

        /// <summary>
        /// N17c - Valor do Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        public decimal? vFCP
        {
            get { return _vFcp.Arredondar(2); }
            set { _vFcp = value.Arredondar(2); }
        }

        public bool vFCPSpecified
        {
            get { return vFCP.HasValue; }
        }


        //NT2020.005_V1_21

        /// <summary>
        /// N17d - Percentual do diferimento do ICMS relativo ao Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        public decimal? pFCPDif
        {
            get { return _pFcpDif.Arredondar(4); }
            set { _pFcpDif = value.Arredondar(4); }
        }

        public bool pFCPDifSpecified
        {
            get { return pFCP.HasValue; }
        }

        /// <summary>
        /// N17e - Valor do ICMS relativo ao Fundo de Combate à Pobreza (FCP) diferido
        /// Versão 4.00
        /// </summary>
        public decimal? vFCPDif
        {
            get { return _vFcpDif.Arredondar(2); }
            set { _vFcpDif = value.Arredondar(2); }
        }

        public bool vFCPDifSpecified
        {
            get { return vFCP.HasValue; }
        }

        /// <summary>
        /// N17f - Valor efetivo do ICMS relativo ao Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        public decimal? vFCPEfet
        {
            get { return _vFcpEfet.Arredondar(2); }
            set { _vFcpEfet = value.Arredondar(2); }
        }

        public bool vFCPEfetSpecified
        {
            get { return _vFcpEfet.HasValue; }
        }

        public bool ShouldSerializemodBC()
        {
            return modBC.HasValue;
        }

        public bool ShouldSerializepRedBC()
        {
            return pRedBC.HasValue;
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepICMS()
        {
            return pICMS.HasValue;
        }

        public bool ShouldSerializevICMSOp()
        {
            return vICMSOp.HasValue;
        }

        public bool ShouldSerializepDif()
        {
            return pDif.HasValue;
        }

        public bool ShouldSerializevICMSDif()
        {
            return vICMSDif.HasValue;
        }

        public bool ShouldSerializevICMS()
        {
            return vICMS.HasValue;
        }
    }
}