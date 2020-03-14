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
    }
}
