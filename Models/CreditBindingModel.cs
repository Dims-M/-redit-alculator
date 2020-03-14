using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace СreditСalculator.Models
{
    /// <summary>
    /// Реквизиты кредита
    /// </summary>
    public class CreditBindingModel
    {
        /// <summary>
        /// Сумма кредита
        /// </summary>
        [UIHint("Сумма кредита")]
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
