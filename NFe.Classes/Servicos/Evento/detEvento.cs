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
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Utils;
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoBensServicos;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoCombustivel;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoPresumido;
using NFe.Classes.Servicos.Evento.Informacoes.Imobilizacao;
using NFe.Classes.Servicos.Evento.Informacoes.ItemConsumo;
using NFe.Classes.Servicos.Evento.Informacoes.ItemNaoFornecido;
using NFe.Classes.Servicos.Evento.Informacoes.Perecimento;


namespace NFe.Classes.Servicos.Evento
{
    public class detEvento
    {
        /// <summary>
        ///     HP18 - Versão do Pedido de Cancelamento, da carta de correção ou EPEC, deve ser informado com a mesma informação da
        ///     tag verEvento (HP16)
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     HP19 - "Cancelamento", "Carta de Correção", "Carta de Correcao" ou "EPEC"
        /// </summary>
        public string descEvento { get; set; }

        #region Cancelamento

        private string _nprot;

        /// <summary>
        ///     HP20 - Informar o número do Protocolo de Autorização da NF-e a ser Cancelada.
        /// </summary>
        public string nProt
        {
            get { return _nprot; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                descEvento = "Cancelamento";
                LimpaDadosCartaCorrecao();
                LimpaDadosEpec();
                _nprot = value;
            }
        }

        private string _xjust;

        /// <summary>
        ///     HP21 - Informar a justificativa do cancelamento
        /// </summary>
        public string xJust
        {
            get { return _xjust; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                descEvento = "Cancelamento";
                LimpaDadosCartaCorrecao();
                LimpaDadosEpec();
                _xjust = value;
            }
        }

        #endregion

        #region Carta de Correção

        private string _xcorrecao;

        /// <summary>
        ///     HP20 - Correção a ser considerada, texto livre. A correção mais recente substitui as anteriores.
        /// </summary>
        public string xCorrecao
        {
            get { return _xcorrecao; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                LimpaDadosCancelamento();
                LimpaDadosEpec();
                _xcorrecao = value;
            }
        }

        private string _xconduso;

        /// <summary>
        ///     HP20a - Condições de uso da Carta de Correção
        /// </summary>
        public string xCondUso
        {
            get { return _xconduso; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                _xconduso = value;
            }
        }

        #endregion

        #region EPEC

        private Estado? _cOrgaoAutor;

        /// <summary>
        ///     P20 - Código do Órgão do Autor do Evento.
        ///     Nota: Informar o código da UF do Emitente para este evento.
        /// </summary>
        public Estado? cOrgaoAutor
        {
            get { return _cOrgaoAutor; }
            set
            {
                if (value == null) return;
                descEvento = "EPEC";
                LimpaDadosCancelamento();
                LimpaDadosCartaCorrecao();
                _cOrgaoAutor = value;
            }
        }

        /// <summary>
        ///     P21 - Informar "1=Empresa Emitente" para este evento.
        ///     Nota: 1=Empresa Emitente; 2=Empresa Destinatária;
        ///     3=Empresa; 5=Fisco; 6=RFB; 9=Outros Órgãos.
        /// </summary>
        public TipoAutor? tpAutor { get; set; }

        /// <summary>
        ///     P22 - Versão do aplicativo do Autor do Evento.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     P23 - Data e hora
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhEmi { get; set; }

        /// <summary>
        /// Proxy para dhEmi no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhEmi")]
        public string ProxyDhEmi
        {
            get { return dhEmi.ParaDataHoraStringUtc(); }
            set { dhEmi = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     P24 - 0=Entrada; 1=Saída;
        /// </summary>
        public TipoNFe? tpNF { get; set; }

        /// <summary>
        ///     P25 - IE do Emitente
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        ///     P26
        /// </summary>
        public detEventoDest dest { get; set; }

        public bool ShouldSerializecOrgaoAutor()
        {
            return cOrgaoAutor.HasValue;
        }

        public bool ShouldSerializetpAutor()
        {
            return tpAutor.HasValue;
        }

        public bool ShouldSerializetpNF()
        {
            return tpNF.HasValue;
        }

        private void LimpaDadosCancelamento()
        {
            nProt = "";
            xJust = "";
        }

        private void LimpaDadosCartaCorrecao()
        {
            xCorrecao = "";
            xCondUso = "";
        }

        private void LimpaDadosEpec()
        {
            cOrgaoAutor = null;
            tpAutor = null;
            verAplic = null;
            dhEmi = null;
            tpNF = null;
            IE = null;
            dest = null;
            //vNF = null;
            //vICMS = null;
            //vST = null;
        }

        #endregion
        
                #region Eventos para a apuração do IBS e da CBS

        #region Informação de efetivo pagamento integral para liberar crédito presumido do adquirente 

        /// <summary>
        ///     P23 - Indicador de efetiva quitação do pagamento integral da operação referente a NFe referenciada
        /// </summary>
        public IndicadorDeQuitacaoDoPagamento? indQuitacao { get; set; }
        
        public bool ShouldSerializeindQuitacao()
        {
            return indQuitacao.HasValue;
        }

        #endregion

        #region Solicitação de Apropriação de crédito presumido

        /// <summary>
        ///     P23 - Informações de crédito presumido por item
        /// </summary>
        [XmlElement("gCredPres")]
        public List<gCredPres> gCredPres { get; set; }
        
        public bool ShouldSerializegCredPres()
        {
            return gCredPres != null;
        }

        #endregion

        #region Destinação de item para consumo pessoal

        /// <summary>
        ///     P23 - Informações por item da NF-e de Aquisição
        ///         <para>Evento: Destinação de item para consumo pessoal</para>
        ///     P23 - Informações por item da NF-e de importação
        ///         <para>Evento: Importação em ALC/ZFM não convertida em isenção</para>
        ///     Nota: a quantidade de ocorrências não pode ser maior que a quantidade de itens da NF-e de aquisição
        /// </summary>
        [XmlElement("gConsumo")]
        public List<gConsumo> gConsumo { get; set; }
        
        public bool ShouldSerializegConsumo() => gConsumo != null;

        #endregion

        #region Aceite de débito na apuração por emissão de nota de crédito | Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão | Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão

        /// <summary>
        ///     Informação utilizada nos eventos "Aceite de débito na apuração por emissão de nota de crédito" e "Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão"
        ///     Para evento "Aceite de débito na apuração por emissão de nota de crédito": P23 - Indicador de concordância com o valor da nota de crédito que lançaram IBS e CBS na apuração assistida
        ///     Para evento "Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão": P23 - Indicador de aceitação do valor de transferência para a empresa que emitiu a nota referenciada
        ///     Para evento "Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão": P23 - Indicador de aceitação do valor de transferência para a empresa que emitiu a nota referenciada
        /// </summary>
        public IndicadorAceitacao? indAceitacao { get; set; }

        public bool ShouldSerializeindAceitacao()
        {
            return indAceitacao != null;
        }

        #endregion

        #region Imobilização de Item

        /// <summary>
        ///     P23 - Informações de itens integrados ao ativo imobilizado
        /// </summary>
        [XmlElement("gImobilizacao")]
        public List<gImobilizacao> gImobilizacao { get; set; }

        public bool ShouldSerializegImobilizacao()
        {
            return gImobilizacao != null;
        }
        
        #endregion

        #region Solicitação de Apropriação de Crédito de Combustível

        /// <summary>
        ///     P23 - Informações de consumo de combustíveis
        /// </summary>
        [XmlElement("gConsumoComb")]
        public List<gConsumoComb> gConsumoComb { get; set; }
        
        public bool ShouldSerializegConsumoComb()
        {
            return gConsumoComb != null;
        }

        #endregion

        #region Solicitação de Apropriação de Crédito para bens e serviços que dependem de atividade do adquirente

        /// <summary>
        ///     P23 - Informações de crédito
        /// </summary>
        [XmlElement("gCredito")]
        public List<gCredito> gCredito { get; set; }

        public bool ShouldSerializegCredito()
        {
            return gCredito != null;
        }
        
        #endregion

        #region Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão | Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão

        /// <summary>
        ///     Para ambos os eventos "Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão"
        ///     e "Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão" o campo representa:
        ///     Indicador de aceitação do valor de transferência para a empresa que emitiu a nota referenciada
        /// </summary>
        public IndicadorDeferimento? indDeferimento { get; set; }

        public bool ShouldSerializeindDeferimento()
        {
            return indDeferimento != null;
        }
        
        /// <summary>
        ///     P24 - Motivo deferimento
        /// </summary>
        public MotivoDeferimento? cMotivo { get; set; }
        
        public bool ShouldSerializecMotivo()
        {
            return cMotivo != null;
        }
        
        /// <summary>
        ///     P24 - Descrição deferimento
        /// </summary>
        public string xMotivo { get; set; }
        
        #endregion
        
        #region Cancelamento Evento

        /// <summary>
        ///     P23 - Código do evento autorizado a ser cancelado
        /// </summary>
        public string tpEventoAut {get; set;}
        
        #endregion

        #region Perecimento, perda, roubo ou furto durante o transporte contratado pelo adquirente

        /// <summary>
        ///     P23 - Informações por item da Nota de Aquisição 
        ///         <para>(Evento: perecimento, perda, roubo ou furto durante o transporte contratado pelo adquirente).</para>
        ///     P23 - Informações por item da Nota de Fornecimento 
        ///         <para>(Evento: perecimento, perda, roubo ou furto durante o transporte contratado pelo fornecedor).</para>
        /// </summary>
        [XmlElement("gPerecimento")]
        public List<gPerecimento> gPerecimento { get; set; }

        public bool ShouldSerializegPerecimento() => gPerecimento != null;

        #endregion

        #region Fornecimento não realizado com pagamento antecipado

        /// <summary>
        ///     P23 - Informações por item da Nota de Pagamento antecipado
        /// </summary>
        [XmlElement("gItemNaoFornecido")]
        public List<gItemNaoFornecido> gItemNaoFornecido { get; set; }

        public bool ShouldSerializegItemNaoFornecido() => gItemNaoFornecido != null;

        #endregion

        #endregion
    }
}