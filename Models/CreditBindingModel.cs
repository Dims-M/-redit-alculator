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
        /// Id для Базы данны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сумма кредита
        /// </summary>
        [Required(ErrorMessage = "Сумма кредита не должна быть пустой.")] //Поле обязателько к заполнению.
       // [StringLength(7, ErrorMessage ="Сумма не должна ьуть больше  1 мл. руб.")]
        [UIHint("SummaCredit")]
        [Display(Name= "Сумма кредита")]
        public int SummaCredit { get; set; }

        /// <summary>
        /// Срок кредитования
        /// </summary>
        [Required(ErrorMessage = "Укажите нужный срок кредита")]
      //  [StringLength(2)]
        [UIHint("TermCredit")]
        [Display(Name = "Срок кредитования")]
      
        public int TermCredit { get; set; }
        /// <summary>
        /// Ставка кредитования 
        /// </summary>
        [UIHint("LendingTate")]
        [Required(ErrorMessage = "Укажите Ставку кредитования")]
     //   [StringLength(2)]
        [Display(Name = "Ставка кредитования ")]
        public int LendingTate { get; set; }

        /// <summary>
        /// Принятие соглашеения на обработку персональных данных
        /// </summary>
        [Required]
        [UIHint("AdoptionAgreement")]
        public bool AdoptionAgreement { get; set; }


    }
}
