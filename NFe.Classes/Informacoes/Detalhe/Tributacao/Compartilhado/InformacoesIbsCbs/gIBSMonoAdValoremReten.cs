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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs
{
    /// <summary>
    ///     UB92 - Tributação Monofásica Sujeita à Retenção do IBS Ad Valorem (tag gMonoReten) - PL_010f (TMonofasia)
    /// </summary>
    public class gIBSMonoAdValoremReten
    {
        private decimal _vBCMonoReten;
        private decimal _pAliqMonoReten;
        private decimal _vIBSMonoReten;

        /// <summary>
        ///     UB92a - Valor tributado sujeito à retenção na monofasia
        /// </summary>
        public decimal vBCMonoReten
        {
            get => _vBCMonoReten.Arredondar(2);
            set => _vBCMonoReten = value.Arredondar(2);
        }

        /// <summary>
        ///     UB92b - Alíquota ad valorem do IBS sujeito à retenção
        /// </summary>
        public decimal pAliqMonoReten
        {
            get => _pAliqMonoReten.Arredondar(4);
            set => _pAliqMonoReten = value.Arredondar(4);
        }

        /// <summary>
        ///     UB92c - Valor do IBS monofásico sujeito à retenção
        /// </summary>
        public decimal vIBSMonoReten
        {
            get => _vIBSMonoReten.Arredondar(2);
            set => _vIBSMonoReten = value.Arredondar(2);
        }
    }
}
