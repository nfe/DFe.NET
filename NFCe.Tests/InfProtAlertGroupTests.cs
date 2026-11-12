using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Protocolo;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace NFCe.Tests
{
    /// <summary>
    ///     Grupo PR13 do protocolo (NT 2018.005, ampliado pela NT 2026.002 §2.3).
    ///     <para>
    ///         O leiaute descreve o PR13 como grupo de 0 a 5 ocorrências, sem elemento agrupador,
    ///         com PR14 <c>cMsg</c> em 0-1 e PR15 <c>xMsg</c> em 1-1. Antes desta mudança
    ///         <c>infProt</c> mapeava os dois em propriedades escalares, e as ocorrências repetidas
    ///         colapsavam: só a última sobrevivia à desserialização.
    ///     </para>
    /// </summary>
    [TestClass]
    public class InfProtAlertGroupTests
    {
        private const string Namespace = "http://www.portalfiscal.inf.br/nfe";

        private static string ProtocoloXml(string ocorrencias)
        {
            return
                "<protNFe xmlns=\"" + Namespace + "\" versao=\"4.00\"><infProt>" +
                "<tpAmb>1</tpAmb><verAplic>SVRS</verAplic>" +
                "<chNFe>53260446759666000129557280000000511348419115</chNFe>" +
                "<dhRecbto>2026-08-08T10:00:00-03:00</dhRecbto>" +
                "<nProt>153260000000001</nProt><digVal>abc</digVal>" +
                "<cStat>120</cStat><xMotivo>Autorizado o uso da NF-e, com alerta</xMotivo>" +
                ocorrencias +
                "</infProt></protNFe>";
        }

        private static protNFe Desserializar(string xml)
        {
            var serializer = new XmlSerializer(typeof(protNFe), Namespace);
            using (var reader = new StringReader(xml))
            {
                return (protNFe)serializer.Deserialize(reader);
            }
        }

        [TestMethod]
        public void Multiplas_ocorrencias_sao_preservadas_na_ordem_do_documento()
        {
            var protocolo = Desserializar(ProtocoloXml(
                "<cMsg>172</cMsg><xMsg>destinatario bloqueado</xMsg>" +
                "<cMsg>999</cMsg><xMsg>segundo alerta</xMsg>" +
                "<cMsg>111</cMsg><xMsg>terceiro alerta</xMsg>"));

            var alertas = protocolo.infProt.Alertas;

            Assert.AreEqual(3, alertas.Count);
            CollectionAssert.AreEqual(
                new[] { 172, 999, 111 },
                alertas.Select(a => a.Codigo.Value).ToArray());
            Assert.AreEqual("destinatario bloqueado", alertas[0].Mensagem);
            Assert.AreEqual("terceiro alerta", alertas[2].Mensagem);
        }

        [TestMethod]
        public void Ocorrencia_sem_cMsg_e_preservada_com_codigo_nulo()
        {
            // cMsg é 0-1 no leiaute: a SEFAZ pode retornar a mensagem sem código.
            var protocolo = Desserializar(ProtocoloXml(
                "<cMsg>172</cMsg><xMsg>com codigo</xMsg>" +
                "<xMsg>sem codigo</xMsg>" +
                "<cMsg>999</cMsg><xMsg>com codigo de novo</xMsg>"));

            var alertas = protocolo.infProt.Alertas;

            Assert.AreEqual(3, alertas.Count);
            Assert.AreEqual(172, alertas[0].Codigo);
            Assert.IsNull(alertas[1].Codigo);
            Assert.AreEqual("sem codigo", alertas[1].Mensagem);
            Assert.AreEqual(999, alertas[2].Codigo);
        }

        [TestMethod]
        public void Protocolo_sem_o_grupo_produz_lista_vazia()
        {
            var protocolo = Desserializar(ProtocoloXml(string.Empty));

            Assert.AreEqual(0, protocolo.infProt.Alertas.Count);
            Assert.IsNull(protocolo.infProt.cMsg);
            Assert.IsNull(protocolo.infProt.xMsg);
        }

        [TestMethod]
        public void cMsg_sem_xMsg_seguinte_nao_forma_ocorrencia()
        {
            // xMsg é o campo obrigatório do grupo — sem ele não há ocorrência a reportar.
            var protocolo = Desserializar(ProtocoloXml("<cMsg>172</cMsg>"));

            Assert.AreEqual(0, protocolo.infProt.Alertas.Count);
        }

        [TestMethod]
        public void Campos_escalares_seguem_expondo_a_primeira_ocorrencia()
        {
            // Compatibilidade com quem já consumia infProt.cMsg / infProt.xMsg
            // (ex.: NFe.Danfe.Nativo/NFCe/DanfeNativoNfce.cs).
            var protocolo = Desserializar(ProtocoloXml(
                "<cMsg>172</cMsg><xMsg>primeira</xMsg>" +
                "<cMsg>999</cMsg><xMsg>segunda</xMsg>"));

            Assert.AreEqual(172, protocolo.infProt.cMsg);
            Assert.AreEqual("primeira", protocolo.infProt.xMsg);
        }

        [TestMethod]
        public void Round_trip_preserva_a_quantidade_de_ocorrencias()
        {
            var xml = ProtocoloXml(
                "<cMsg>172</cMsg><xMsg>com codigo</xMsg>" +
                "<xMsg>sem codigo</xMsg>" +
                "<cMsg>999</cMsg><xMsg>outro</xMsg>");

            var protocolo = Desserializar(xml);

            var serializer = new XmlSerializer(typeof(protNFe), Namespace);
            string reserializado;
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, protocolo);
                reserializado = writer.ToString();
            }

            Assert.AreEqual(3, Regex.Matches(reserializado, "<xMsg>").Count);
            Assert.AreEqual(2, Regex.Matches(reserializado, "<cMsg>").Count);
        }
    }
}
