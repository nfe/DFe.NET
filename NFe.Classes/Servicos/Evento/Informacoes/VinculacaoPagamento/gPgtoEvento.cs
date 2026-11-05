/********************************************************************************/
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
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.Evento.Informacoes.VinculacaoPagamento
{
    /// <summary>
    ///     P23 - Dados do pagamento do evento 110300 - Vinculação Pagamento (NT 2026.006 - split payment)
    ///     <para>Tipo TEventoPgto do e110300_v1.00.xsd. Difere do gPgto da nota: não tem o atributo nPag.</para>
    /// </summary>
    public class gPgtoEvento
    {
        /// <summary>
        ///     Identificador da transação financeira, de acordo com a transação de pagamento (2 a 35 caracteres)
        /// </summary>
        [XmlAttribute]
        public string idTransacao { get; set; }

        /// <summary>
        ///     Código do meio de pagamento (2 dígitos, ver IT DFe 2026.001)
        /// </summary>
        public string tpMeioPgto { get; set; }

        /// <summary>
        ///     CNPJ do recebedor do pagamento (com zeros não significativos)
        /// </summary>
        public string CNPJReceb { get; set; }

        /// <summary>
        ///     CNPJ base (8 posições) da instituição financeira ou de pagamento (com zeros não significativos)
        /// </summary>
        public string CNPJBasePSP { get; set; }
    }
}
