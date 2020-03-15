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
        [UIHint("SummaCredit")]
        [Display(Name= "Сумма кредита")]
        public double SummaCredit { get; set; }
      
        /// <summary>
        /// Срок кредитования
        /// </summary>
        [UIHint("TermCredit")]
        [Display(Name = "Срок кредитования")]
        public int TermCredit { get; set; }
        /// <summary>
        /// Ставка кредитования 
        /// </summary>
        [UIHint("LendingTate")]
        [Display(Name = "Ставка кредитования ")]
        public int LendingTate { get; set; }

        /// <summary>
        /// Принятие соглашеения на обработку персональных данных
        /// </summary>
        [UIHint("AdoptionAgreement")]
        public bool AdoptionAgreement { get; set; }

    }
}
