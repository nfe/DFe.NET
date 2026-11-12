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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DFe.Classes.Assinatura;
using DFe.Classes.Flags;
using DFe.Utils;

namespace NFe.Classes.Protocolo
{
    public class infProt
    {
        /// <summary>
        ///     PR04 - Identificador da TAG a ser assinada, somente precisa ser informado se a UF assinar a resposta.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     PR05 - Identificação do Ambiente
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     PR06 - Versão do Aplicativo que processou a consulta.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     PR07 - Chave de Acesso da NF-e
        /// </summary>
        public string chNFe { get; set; }

        /// <summary>
        ///     PR08 - Data e hora de recebimento
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxyDhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     PR09 - Número do Protocolo da NF-e
        /// </summary>
        public string nProt { get; set; }

        /// <summary>
        ///     PR10 - Digest Value da NF-e processada Utilizado para conferir a integridade da NFe original.
        /// </summary>
        public string digVal { get; set; }

        /// <summary>
        ///     PR11 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     PR12 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     PR13 - Grupo de mensagens da SEFAZ para o emissor (0-5 ocorrências).
        ///     <para>
        ///         O leiaute descreve o PR13 como "Sequência XML": um grupo SEM elemento agrupador,
        ///         cujos campos aparecem em sequência direta dentro de infProt — PR14 <c>cMsg</c>
        ///         (ocorrência 0-1, opcional) e PR15 <c>xMsg</c> (ocorrência 1-1, obrigatório).
        ///     </para>
        ///     <para>
        ///         Modelado como coleção de escolha com <see cref="XmlChoiceIdentifierAttribute" />
        ///         porque é o único arranjo do XmlSerializer que preserva a ORDEM entre dois nomes
        ///         de elemento distintos — e é a ordem que permite reconstruir cada ocorrência.
        ///         Mapear <c>cMsg</c> e <c>xMsg</c> em propriedades escalares, como antes, fazia as
        ///         ocorrências repetidas colapsarem: só a última sobrevivia.
        ///     </para>
        ///     Ver <see cref="Alertas" /> para consumir os pares já reconstruídos.
        /// </summary>
        [XmlElement("cMsg", typeof(string))]
        [XmlElement("xMsg", typeof(string))]
        [XmlChoiceIdentifier(nameof(MensagensTipos))]
        public string[] Mensagens { get; set; }

        /// <summary>
        ///     Discriminador que diz, para cada posição de <see cref="Mensagens" />, se o valor veio
        ///     de um <c>cMsg</c> ou de um <c>xMsg</c>. Preenchido pelo XmlSerializer; não serializa.
        /// </summary>
        [XmlIgnore]
        public TipoMensagemProtocolo[] MensagensTipos { get; set; }

        /// <summary>
        ///     Ocorrências do PR13 já reconstruídas em pares código/mensagem, na ordem do documento.
        /// </summary>
        /// <remarks>
        ///     O agrupamento é ancorado no <c>xMsg</c>, que é o campo obrigatório do grupo: cada
        ///     <c>xMsg</c> encerra uma ocorrência, e o <c>cMsg</c> imediatamente anterior — quando
        ///     existe — é o código dela. Um <c>cMsg</c> sem <c>xMsg</c> seguinte é ignorado, por não
        ///     formar ocorrência válida segundo o leiaute.
        /// </remarks>
        [XmlIgnore]
        public IReadOnlyList<MensagemProtocolo> Alertas
        {
            get
            {
                var resultado = new List<MensagemProtocolo>();

                if (Mensagens == null || MensagensTipos == null)
                    return resultado;

                int? codigoPendente = null;

                for (var i = 0; i < Mensagens.Length && i < MensagensTipos.Length; i++)
                {
                    if (MensagensTipos[i] == TipoMensagemProtocolo.cMsg)
                    {
                        codigoPendente = int.TryParse(Mensagens[i], out var codigo) ? codigo : (int?)null;
                        continue;
                    }

                    resultado.Add(new MensagemProtocolo(codigoPendente, Mensagens[i]));
                    codigoPendente = null;
                }

                return resultado;
            }
        }

        /// <summary>
        ///     PR14 - Código da primeira mensagem, quando há. Mantido por compatibilidade com quem
        ///     consumia o campo escalar; para todas as ocorrências use <see cref="Alertas" />.
        /// </summary>
        [XmlIgnore]
        public int? cMsg
        {
            get { return Alertas.Select(a => a.Codigo).FirstOrDefault(c => c.HasValue); }
        }

        /// <summary>
        ///     PR15 - Texto da primeira mensagem, quando há. Mantido por compatibilidade com quem
        ///     consumia o campo escalar; para todas as ocorrências use <see cref="Alertas" />.
        /// </summary>
        [XmlIgnore]
        public string xMsg
        {
            get { return Alertas.Count > 0 ? Alertas[0].Mensagem : null; }
        }

        /// <summary>
        ///     PR90 - Assinatura XML do grupo identificado pelo atributo “Id”
        ///     A decisão de assinar a mensagem fica a critério da UF interessada.
        /// </summary>
        public Signature Signature { get; set; }
    }

    /// <summary>
    ///     Discriminador dos campos do grupo PR13.
    /// </summary>
    [XmlType(IncludeInSchema = false)]
    public enum TipoMensagemProtocolo
    {
        cMsg,
        xMsg
    }

    /// <summary>
    ///     Uma ocorrência do grupo PR13: PR14 <c>cMsg</c> (opcional) e PR15 <c>xMsg</c>.
    /// </summary>
    public class MensagemProtocolo
    {
        public MensagemProtocolo(int? codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }

        /// <summary>
        ///     PR14 - Código da mensagem. Nulo quando a ocorrência veio sem <c>cMsg</c>.
        /// </summary>
        public int? Codigo { get; private set; }

        /// <summary>
        ///     PR15 - Mensagem da SEFAZ para o emissor.
        /// </summary>
        public string Mensagem { get; private set; }
    }
}