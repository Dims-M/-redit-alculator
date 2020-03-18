using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using СreditСalculator.AppData;
using СreditСalculator.Models;

namespace СreditСalculator.Controllers
{
    public class HomeController : Controller
    {
        CreditContext db;

       static CreditBindingModel creditBindingModelTemp = new CreditBindingModel();
       static List<string> listDataCredit;

        

        /// <summary>
        /// Логирование
        /// </summary>
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger, CreditContext creditContext)
        {
            _logger = logger;
            db = creditContext; //Подключение к БД
        }

        /// <summary>
        /// По умолчанию
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            
            return View();
        }


        /// <summary>
        /// Завяка на кредит. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreditGet()
        {
            //Отправляем форму для регистрации. Или другую форму для ввода данных пользователю
            return View();
        }

        /// <summary>
        /// Форма вода данных для разчетов кредита
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreditGet(CreditBindingModel creditG )
        {
           // проверяем на валидность моделию,
            if (ModelState.IsValid)
            {
                int summaCredita = creditG.SummaCredit;
                int timeCredit = creditG.TermCredit;
                int stavkaCrediy = creditG.LendingTate;

               TestRaschet(summaCredita, stavkaCrediy, timeCredit); // Тестовый расчет и запись в бд

               creditBindingModelTemp = creditG;

                listDataCredit = new List<string>(){ $"Ваша заявка расмотренна!{Environment.NewLine}Сумма кредита:{summaCredita}{Environment.NewLine}Нужный срок кредитования:{timeCredit}{Environment.NewLine}Утвержденная ставка{stavkaCrediy}"};

                //Добавляев в БД для статистики
                db.AddRange(
                   new CreditBindingModel
                   {
                        
                       SummaCredit = summaCredita,
                       TermCredit = timeCredit,
                       LendingTate = stavkaCrediy
                   }
                   
                   );
                db.SaveChanges(); // сохр. в бд

                return Redirect("~/Home/CalculateCredit");

               // return View("Success");
            }

            else
            {
                return View("creditG"); // возвращаем клиенту на исправление
            }
        }

        /// <summary>
        /// Получение списка запросов кретитов
        /// </summary>
        /// <returns></returns>
        public IActionResult GetHistoryRequestCredit()
        {
            
            return View(db.ResultCredits.ToList()); // Вывод списка запросо получения кредитов из БД
        }

        public IActionResult CalculateCredit()
        {
            // return Redirect("~/Home/CalculateCredit");
            return View(listDataCredit);
        }


        internal string TestRaschet(int sumCreditSum, int sumProcent, int sumPeriod)
        {
            if (sumCreditSum == 0)
            {
                // MessageBox.Show("Укажите сумму кредита.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "jib,rf";
            }

            else
            {
                // dgvGrafik.Rows.Clear(); // Очищаем таблицу
                double SumCredit = Convert.ToDouble(sumCreditSum); // Сумма кредита
                double InterestRateYear = Convert.ToDouble(sumProcent); // Процентная ставка, ГОДОВАЯ
                double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
                double InterestRateDay = InterestRateMonth / 30 ; // Процентная ставка, МЕСЯЧНАЯ
                int CreditPeriod = Convert.ToInt32(sumPeriod); // Срок кредита, переводим в месяцы, если указан в годах

                //if (sumPeriodCombo.SelectedIndex == 0) // Должен БЫТЬ Выподающий список
                CreditPeriod *= 12; //преобразование в количество месяцев

                //if (sumAnnuitet.Checked == true) // Аннуитетный платеж
                //{
                double Payment = SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod))); // Ежемесячный платеж
                double ItogCreditSum = Payment * CreditPeriod; // Итоговая сумма кредита
                double PereplataPoCredity = ItogCreditSum - SumCredit; ////переплата по кредиту

                double ObsiaSummaPereplaty = SumCredit+ PereplataPoCredity;
                //Добавляев в БД для статистики
                db.AddRange(
                   new ResultCredit
                   {
                       SizePaymentBody = Convert.ToInt32(Payment),
                       PrincipalBalance = 0, // остаток основного долга в руб 
                       SizePaymentPercentage = 0,//остаток основного долга в процентах
                       OverpaymentBalanceCredit = Convert.ToInt32(PereplataPoCredity), //переплата по кредиту
                       TotalBalanceCredit = Convert.ToInt32(PereplataPoCredity + SumCredit),
                       DateTimePayment = DateTime.Now, // дата создания расчета
                   }

                   ) ;  
                db.SaveChanges(); // сохр. в бд

                // PaymentScheduleAnnuitet(SumCredit, InterestRateYear, InterestRateMonth, CreditPeriod);
                //}
                //else if (sumDiffer.Checked == true) // Дифференцированный платеж
                //{
                //    PaymentScheduleDiffer(SumCredit, InterestRateMonth, CreditPeriod);
                //}
                //butSaveAsCSV.Enabled = true;

                return "";
            }
        }
        //Атрибут кэширования
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
