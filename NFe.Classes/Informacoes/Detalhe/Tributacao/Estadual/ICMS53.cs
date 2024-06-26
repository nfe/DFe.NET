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
    public class ICMS53 : ICMSBasico
    {
        private decimal _adRemICMS;
        private decimal _vICMSMonoDif;
        private decimal _vICMSMonoOp;
        private decimal _pDif;
        private decimal _qBCMono;
        private decimal _vICMSMono;

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
        ///     N37a - Quantidade tributada diferida (qBCMono)
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal qBCMono
        {
            get { return _qBCMono; }
            set { _qBCMono = value.Arredondar(4); }
        }

        /// <summary>
        ///     N38 - Alíquota ad rem do imposto diferido (adRemICMS)
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal adRemICMS
        {
            get { return _adRemICMS; }
            set { _adRemICMS = value.Arredondar(4); }
        }

        /// <summary>
        ///     N41a - Valor do ICMS da operação 
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal vICMSMonoOp
        {
            get { return _vICMSMonoOp.Arredondar(2); }
            set { _vICMSMonoOp = value.Arredondar(2); }
        }

        /// <summary>
        ///     N42 - Percentual do diferimento 
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal pDif
        {
            get { return _pDif.Arredondar(2); }
            set { _pDif = value.Arredondar(4); }
        }

        /// <summary>
        ///     N43 - Valor do ICMS diferido 
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal vICMSMonoDif
        {
            get { return _vICMSMonoDif.Arredondar(2); }
            set { _vICMSMonoDif = value.Arredondar(2); }
        }

        /// <summary>
        ///     N39 - Valor do ICMS próprio devido
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal vICMSMono
        {
            get { return _vICMSMono.Arredondar(2); }
            set { _vICMSMono = value.Arredondar(2); }
        }
    }
}