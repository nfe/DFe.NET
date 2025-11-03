/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emiss�o de Nota Fiscal Eletr�nica - NFe e Nota Fiscal de  */
/* Consumidor Eletr�nica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Voc� pode obter a �ltima vers�o desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca � software livre; voc� pode redistribu�-la e/ou modific�-la */
/* sob os termos da Licen�a P�blica Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a vers�o 2.1 da Licen�a, ou (a seu crit�rio) */
/* qualquer vers�o posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca � distribu�da na expectativa de que seja �til, por�m, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia impl�cita de COMERCIABILIDADE OU      */
/* ADEQUA��O A UMA FINALIDADE ESPEC�FICA. Consulte a Licen�a P�blica Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICEN�A.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Voc� deve ter recebido uma c�pia da Licen�a P�blica Geral Menor do GNU junto*/
/* com esta biblioteca; se n�o, escreva para a Free Software Foundation, Inc.,  */
/* no endere�o 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Voc� tamb�m pode obter uma copia da licen�a em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco jos� da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Servicos.Download
{
    /// <summary>
    /// JR01 - Estrutura com as NF-e encontradas 
    /// </summary>
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot("retDownloadNFe", Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class retDownloadNFe : IRetornoServico
    {
        public retDownloadNFe()
        {
            retNFe = new List<retNFe>();
        }
        
        /// <summary>
        ///     JR02 - Vers�o do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        /// JR03 - Identifica��o do Ambiente: 1=Produ��o/2=Homologa��o
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        /// JR04 - Vers�o do Aplicativo que processou a consulta. JR05
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        /// JR05 - C�digo do status da resposta
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        /// JR06 - Descri��o literal do status da resposta
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        /// JR07 - Data e Hora da mensagem de resposta
        /// </summary>
        public DateTime dhResp { get; set; }

        /// <summary>
        /// JR08 - Conjunto de informa��es da NF-e
        /// </summary>
        [XmlElement("retNFe")]
        public List<retNFe> retNFe { get; set; }
    }

}