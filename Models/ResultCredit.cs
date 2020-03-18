using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace СreditСalculator.Models
{
    /// <summary>
    /// Платежи клиента
    /// </summary>
    public class ResultCredit
    {

        public int Id { get; set; }

        /// <summary>
        /// Номер платежа
        /// </summary>
        public int NumberPayment { get; set; }

        /// <summary>
        /// дата платежа
        /// </summary>
        public DateTime DateTimePayment { get; set; }

        /// <summary>
        /// Размер  тела платежа
        /// </summary>
        public int SizePaymentBody { get; set; }

        /// <summary>
        /// Размер платежа в процентах
        /// </summary>
        public int SizePaymentPercentage { get; set; }

        /// <summary>
        /// Остаток основного долга
        /// </summary>
        public int PrincipalBalance { get; set; }

        /// <summary>
        /// Переплата по кредиту
        /// </summary>
        public int OverpaymentBalanceCredit { get; set; }

        public int IdCredit { get; set; } // ссылка на связанную модель заявки кредита
        public CreditBindingModel CreditBindingModel { get; set; }

    }
}
