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
        /// Дата создания заявки платежа. Для БД
        /// </summary>
        public DateTime DateTimePayment { get; set; }

        /// <summary>
        /// Размер платежа основного долга. Руб 
        /// </summary>
        public double SizePaymentBody { get; set; }

        /// <summary>
        /// Размер платежа в процентах.
        /// </summary>
        public double SizePaymentPercentage { get; set; }

        /// <summary>
        /// Остаток основного долга. В руб
        /// </summary>
        public double PrincipalBalance { get; set; }

        /// <summary>
        /// Переплата по кредиту. Только переплата. Без основного тела долга
        /// </summary>
        public double OverpaymentBalanceCredit { get; set; }

        /// <summary>
        /// Переплата по кредиту. Основной долг + переплата
        /// </summary>
        public double TotalBalanceCredit { get; set; }

        public int IdCredit { get; set; } // ссылка на связанную модель заявки кредита
        public CreditBindingModel CreditBindingModel { get; set; }

    }
}
