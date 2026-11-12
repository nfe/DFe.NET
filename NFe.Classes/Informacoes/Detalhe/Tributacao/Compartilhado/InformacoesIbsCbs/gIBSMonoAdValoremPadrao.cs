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
    ///     UB91 - Tributação Monofásica Padrão do IBS Ad Valorem (tag gMonoPadrao) - PL_010f (TMonofasia)
    /// </summary>
    public class gIBSMonoAdValoremPadrao
    {
        private decimal _vBCMono;
        private decimal _pAliqMonoUF;
        private decimal _vIBSMonoUF;
        private decimal _pAliqMonoMun;
        private decimal _vIBSMonoMun;
        private decimal _vIBSMono;

        /// <summary>
        ///     UB91a - Valor tributado na monofasia
        /// </summary>
        public decimal vBCMono
        {
            get => _vBCMono.Arredondar(2);
            set => _vBCMono = value.Arredondar(2);
        }

        /// <summary>
        ///     UB91b - Alíquota ad valorem do IBS Estadual
        /// </summary>
        public decimal pAliqMonoUF
        {
            get => _pAliqMonoUF.Arredondar(4);
            set => _pAliqMonoUF = value.Arredondar(4);
        }

        /// <summary>
        ///     UB91c - Valor do IBS monofásico Estadual
        /// </summary>
        public decimal vIBSMonoUF
        {
            get => _vIBSMonoUF.Arredondar(2);
            set => _vIBSMonoUF = value.Arredondar(2);
        }

        /// <summary>
        ///     UB91d - Alíquota ad valorem do IBS Municipal
        /// </summary>
        public decimal pAliqMonoMun
        {
            get => _pAliqMonoMun.Arredondar(4);
            set => _pAliqMonoMun = value.Arredondar(4);
        }

        /// <summary>
        ///     UB91e - Valor do IBS monofásico Municipal
        /// </summary>
        public decimal vIBSMonoMun
        {
            get => _vIBSMonoMun.Arredondar(2);
            set => _vIBSMonoMun = value.Arredondar(2);
        }

        /// <summary>
        ///     UB91f - Valor do IBS monofásico
        /// </summary>
        public decimal vIBSMono
        {
            get => _vIBSMono.Arredondar(2);
            set => _vIBSMono = value.Arredondar(2);
        }
    }
}
