using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using СreditСalculator.Models;

namespace СreditСalculator.AppData
{
    /// <summary>
    ///Контекст данных. Связь и работа с БД 
    /// </summary>
    public class CreditContext : DbContext
    {
        /// <summary>
        /// Таблицы Кредит в БД
        /// </summary>
        public DbSet<CreditBindingModel> Credits { get; set; }
        /// <summary>
        /// Таблицы Результат кредита в БД
        /// </summary>
        public DbSet<ResultCredit> ResultCredits { get; set; }

        //При отсуствии бд. Создаст новую
        public CreditContext(DbContextOptions<CreditContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
