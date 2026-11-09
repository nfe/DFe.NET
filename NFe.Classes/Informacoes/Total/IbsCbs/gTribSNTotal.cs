using DFe.Utils;

namespace NFe.Classes.Informacoes.Total.IbsCbs
{
    /// <summary>
    ///     W59h - Grupo de totais da composição do valor do IBS e da CBS para o Simples Nacional (NT 2026.005 v1.00).
    ///     Obrigatório quando algum item tem gTribSN (RV W59h-10/20); os totais são a soma dos itens (W59i-10/W59j-10).
    /// </summary>
    public class gTribSNTotal
    {
        private decimal _vIBSSN;
        private decimal _vCBSSN;

        /// <summary>
        ///     W59i - Valor total do IBS para os optantes do Simples Nacional
        /// </summary>
        public decimal vIBSSN
        {
            get => _vIBSSN.Arredondar(2);
            set => _vIBSSN = value.Arredondar(2);
        }

        /// <summary>
        ///     W59j - Valor total da CBS para os optantes do Simples Nacional
        /// </summary>
        public decimal vCBSSN
        {
            get => _vCBSSN.Arredondar(2);
            set => _vCBSSN = value.Arredondar(2);
        }
    }
}
