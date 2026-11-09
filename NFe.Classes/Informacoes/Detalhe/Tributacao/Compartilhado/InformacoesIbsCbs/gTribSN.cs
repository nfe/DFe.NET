using DFe.Utils;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs
{
    /// <summary>
    ///     UB115a - Grupo de informações da composição do valor do IBS e da CBS para o Simples Nacional
    ///     (NT 2026.005 v1.00). Opcional (0-1), fora da choice de grupos de tributação: pode sair junto com o
    ///     gIBSCBS (ex.: CRT 2 sem regime regular). Não é informado com o CST 620 (gIBSCBSMono).
    /// </summary>
    public class gTribSN
    {
        /// <summary>
        ///     UB115b - Composição do valor do IBS para o Simples Nacional (0-1)
        /// </summary>
        public gIBSSN gIBSSN { get; set; }

        /// <summary>
        ///     UB115e - Composição do valor da CBS para o Simples Nacional (1-1)
        /// </summary>
        public gCBSSN gCBSSN { get; set; }
    }

    /// <summary>
    ///     UB115b - Composição do valor do IBS para o Simples Nacional
    /// </summary>
    public class gIBSSN
    {
        private decimal _pIBSSN;
        private decimal _vIBSSN;

        /// <summary>
        ///     UB115c - Alíquota efetiva do IBS para os optantes do Simples Nacional (3v2-4)
        /// </summary>
        public decimal pIBSSN
        {
            get => _pIBSSN.Arredondar(4);
            set => _pIBSSN = value.Arredondar(4);
        }

        /// <summary>
        ///     UB115d - Valor do IBS para os optantes do Simples Nacional (13v2)
        /// </summary>
        public decimal vIBSSN
        {
            get => _vIBSSN.Arredondar(2);
            set => _vIBSSN = value.Arredondar(2);
        }
    }

    /// <summary>
    ///     UB115e - Composição do valor da CBS para o Simples Nacional
    /// </summary>
    public class gCBSSN
    {
        private decimal _pCBSSN;
        private decimal _vCBSSN;

        /// <summary>
        ///     UB115f - Alíquota efetiva da CBS para os optantes do Simples Nacional (3v2-4)
        /// </summary>
        public decimal pCBSSN
        {
            get => _pCBSSN.Arredondar(4);
            set => _pCBSSN = value.Arredondar(4);
        }

        /// <summary>
        ///     UB115g - Valor da CBS para os optantes do Simples Nacional (13v2)
        /// </summary>
        public decimal vCBSSN
        {
            get => _vCBSSN.Arredondar(2);
            set => _vCBSSN = value.Arredondar(2);
        }
    }
}
