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

using System;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes
{
    public class retirada
    {
        private const string ErroCpfCnpjPreenchidos = "Somente preencher um dos campos: CNPJ ou CPF, para um objeto do tipo retirada!";
        private string cnpj;
        private string cpf;

        /// <summary>
        ///     F02 - CNPJ
        /// </summary>
        public string CNPJ
        {
            get { return cnpj; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(cpf))
                    cnpj = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        ///     F02a - CPF
        /// </summary>
        public string CPF
        {
            get { return cpf; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(cnpj))
                    cpf = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        /// Razão Social ou nome do recebedor
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        ///     F03 - Logradouro
        /// </summary>
        public string xLgr { get; set; }

        /// <summary>
        ///     F04 - Número
        /// </summary>
        public string nro { get; set; }

        /// <summary>
        ///     F05 - Complemento
        /// </summary>
        public string xCpl { get; set; }

        /// <summary>
        ///     F06 - Bairro
        /// </summary>
        public string xBairro { get; set; }

        /// <summary>
        ///     F07 - Código do município
        ///     <para>Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.</para>
        /// </summary>
        public long cMun { get; set; }

        /// <summary>
        ///     F08 - Nome do município, informar EXTERIOR para operações com o exterior.
        /// </summary>
        public string xMun { get; set; }

        /// <summary>
        ///     F09 - Sigla da UF, informar EX para operações com o exterior.
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        ///     E13 - Código do CEP
        ///     <para>Informar os zeros não significativos. (NT 2011/004)</para>
        /// </summary>
        [XmlIgnore]
        public long? CEP { get; set; }

        [XmlElement(ElementName = "CEP")]
        public string ProxyCEP
        {
            get { return CEP != null ? CEP.Value.ToString("D8") : null; }
            set { CEP = long.Parse(value); }
        }

        /// <summary>
        ///     E14 - Código do País
        ///     <para>1058 - Brasil</para>
        /// </summary>
        public int? cPais { get; set; }

        /// <summary>
        ///     E15 - Nome do País
        ///     <para>Brasil ou BRASIL</para>
        /// </summary>
        public string xPais { get; set; }

        public bool cPaisSpecified { get { return cPais.HasValue; } }

        /// <summary>
        ///     E16 - Telefone
        ///     <para>
        ///         Preencher com o Código DDD + número do telefone. Nas operações com exterior é permitido informar o código do
        ///         país + código da localidade + número do telefone (v.2.0)
        ///     </para>
        /// </summary>
        public long? fone { get; set; }

        /// <summary>
        ///     E19 - Informar o e-mail do destinatário. O campo pode ser utilizado para informar o e-mail de recepção da NF-e
        ///     indicada pelo destinatário
        /// </summary>
        public string email { get; set; }

        /// <summary>
        ///     E17 - Inscrição Estadual
        ///     <para>Campo de informação obrigatória nos casos de emissão própria (procEmi = 0, 2 ou 3).</para>
        ///     <para>
        ///         A IE deve ser informada apenas com algarismos para destinatários contribuintes do ICMS, sem caracteres de
        ///         formatação (ponto, barra, hífen, etc.);
        ///     </para>
        ///     <para>
        ///         O literal “ISENTO” deve ser informado apenas para contribuintes do ICMS que são isentos de inscrição no
        ///         cadastro de contribuintes do ICMS e estejam emitindo NF-e avulsa;
        ///     </para>
        /// </summary>
        public string IE { get; set; }

        public bool ShouldSerializecPais()
        {
            return cPais.HasValue;
        }

        public bool ShouldSerializexPais()
        {
            return !string.IsNullOrEmpty(xPais);
        }

        public bool ShouldSerializefone()
        {
            return fone.HasValue;
        }

        public bool ShouldSerializexCpl()
        {
            return !string.IsNullOrEmpty(xCpl);
        }

        public bool ShouldSerializexNome()
        {
            return !string.IsNullOrEmpty(xNome);
        }

        public bool ShouldSerializeCEP()
        {
            return CEP.HasValue;
        }
    }
}