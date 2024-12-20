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

using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

//using System.Web.Services;
//using System.Web.Services.Description;
//using System.Web.Services.Protocols;

namespace NFe.Wsdl.AdmCsc
{
    //[WebServiceBinding(Name = "CscNFCeBinding", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")]

    // [ServiceContract(Name = "CscNFCeBinding", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")]
    public class NfceCsc : INfeServico
    {
        public NfceCsc(string url, X509Certificate certificado, int timeOut)
        {
            //SoapVersion = SoapProtocolVersion.Soap12;
            //Url = url;
            //Timeout = timeOut;
            //ClientCertificates.Add(certificado);
        }

        //[MessageHeader(MustUnderstand = true)]
        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        //  [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        //[SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe/admCscNFCe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        //[WebMethod(MessageName = "admCscNFCe")]

        //[XmlSerializerFormat(Use = OperationFormatUse.Literal, Style = OperationFormatStyle.Rpc)]
        //  [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe/admCscNFCe")]
        [return: XmlElementAttribute("cscNFCeResult", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")]
        public XmlNode Execute(
            [XmlElementAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")] XmlNode nfeDadosMsg)
        {
            //var results = Invoke("admCscNFCe", new object[] { nfeDadosMsg });
            //return ((XmlNode)(results[0]));
            return null;
        }

        public Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            return null;
        }
    }
}