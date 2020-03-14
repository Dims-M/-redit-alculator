using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace СreditСalculator.Models
{
    /// <summary>
    /// Реквизиты кредита
    /// </summary>
    public class CreditModel
    {
        /// <summary>
        /// Сумма кредита
        /// </summary>
        public double SummaCredit { get; set; }
        /// <summary>
        /// Срок кредитования
        /// </summary>
        public int TermCredit { get; set; }
        /// <summary>
        /// Ставка кредитования 
        /// </summary>
        public int LendingTate { get; set; }



    }
}
