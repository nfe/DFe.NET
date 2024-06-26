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
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS15 : ICMSBasico
    {
        private decimal _adRemICMS;
        private decimal _vICMSMono;
        private decimal _adRemICMSReten;
        private decimal _vICMSMonoReten;
        private decimal _qBCMono;
        private decimal _qBCMonoReten;
        private decimal? _pRedAdRem;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [XmlElement(Order = 1)]
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12- Situação Tributária
        /// </summary>
        [XmlElement(Order = 2)]
        public Csticms CST { get; set; }

        /// <summary>
        ///     N37a - Quantidade tributada 
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal qBCMono
        {
            get { return _qBCMono; }
            set { _qBCMono = value.Arredondar(4); }
        }

        /// <summary>
        ///     N38 - Valor da BC do ICMS
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal adRemICMS
        {
            get { return _adRemICMS; }
            set { _adRemICMS = value.Arredondar(4); }
        }

        /// <summary>
        ///     N39 - Alíquota do imposto
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal vICMSMono
        {
            get { return _vICMSMono.Arredondar(2); }
            set { _vICMSMono = value.Arredondar(2); }
        }

        /// <summary>
        ///     N39a - Quantidade tributada sujeita a retenção 
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal qBCMonoReten
        {
            get { return _qBCMonoReten; }
            set { _qBCMonoReten = value.Arredondar(4); }
        }

        /// <summary>
        ///     N40 - Alíquota ad rem do imposto com retenção
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal adRemICMSReten
        {
            get { return _adRemICMSReten; }
            set { _adRemICMSReten = value.Arredondar(4); }
        }

        /// <summary>
        ///     N41 - Valor do ICMS com retenção 
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal vICMSMonoReten
        {
            get { return _vICMSMonoReten.Arredondar(2); }
            set { _vICMSMonoReten = value.Arredondar(2); }
        }

        /// <summary>
        ///     N47 - Percentual de redução do valor da alíquota adrem do ICMS
        /// </summary>
        [XmlElement(Order = 9)]
        public decimal? pRedAdRem
        {
            get { return _pRedAdRem.Arredondar(2); }
            set { _pRedAdRem = value.Arredondar(2); }
        }

        /// <summary>
        ///     N48 - Motivo da redução do adrem
        /// </summary>
        [XmlElement(Order = 10)]
        public MotivoReducaoAdRem? motRedAdRem { get; set; }

        public bool ShouldSerializepRedAdRem()
        {
            return pRedAdRem.HasValue;
        }

        public bool ShouldSerializemotRedAdRem()
        {
            return motRedAdRem.HasValue;
        }
    }
}
