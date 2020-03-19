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
       static List<ResultCredit> FullresultCredits = new List<ResultCredit>();



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

                //расчет сум, процентов.
                FullresultCredits = TestRaschet(summaCredita, stavkaCrediy, timeCredit); // Тестовый расчет и запись в бд

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
           
            return View(FullresultCredits);
        }

        /// <summary>
        /// Расчет процентов, переплат
        /// </summary>
        /// <param name="sumCreditSum"></param>
        /// <param name="sumProcent"></param>
        /// <param name="sumPeriod"></param>
        /// <returns></returns>
        internal List<ResultCredit> TestRaschet(int sumCreditSum, int sumProcent, int sumPeriod)
        {
            List<ResultCredit> resultCredits = new List<ResultCredit>();
           
            if (sumCreditSum == 0)
            {
                return null;
            }

            else
            {
               
                double SumCredit = Convert.ToDouble(sumCreditSum); // Сумма кредита
                double InterestRateYear = Convert.ToDouble(sumProcent); // Процентная ставка, ГОДОВАЯ

               // double InterestRateMonth = Convert.ToDouble(sumProcent); // Процентная ставка, МЕСЯЧНАЯ
                double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
                double InterestRateDay = InterestRateMonth / 30 ; // Процентная ставка, дневная
                int CreditPeriod = Convert.ToInt32(sumPeriod); // Срок кредита, переводим в месяцы, если указан в годах
                
                // Аннуитетный платеж
                double Payment = SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod))); // Ежемесячный платеж
                double ItogCreditSum = Payment * CreditPeriod; // Итоговая сумма кредита
                double PereplataPoCredity = ItogCreditSum - SumCredit; ////переплата по кредиту

                double ObsiaSummaPereplaty = SumCredit+ PereplataPoCredity;
                
                //РАСЧЕТ 
                double SumCreditOperation = SumCredit;
                double ItogCreditSumOperation = ItogCreditSum;
                double ItogPlus;
                double itogOverpayment = 0; // итоговая переплата
                double procent = 0; // платеж по процент в руб
                double pereplataOchovTela = 0;

                string temp = "";
                DateTime dataTime = new DateTime();

                for (int i = 0; i < CreditPeriod; ++i)
                {
                    
                    procent = SumCreditOperation * (InterestRateYear / 100) / 12; //   платеж по процент в руб переплаты уменьшается при каждом цикле
                    SumCreditOperation -= Payment - procent;
                   
                    temp += i + 1+"\n"; //номер месяца
                    temp += Payment.ToString("N2")+"\n"; //Ежемесячный платеж
                    temp += (Payment - procent).ToString("N2")+"\n"; //Платеж за основной долг
                    pereplataOchovTela = (Payment - procent); // платеж по основнмому долгу. в руб
                    temp += procent.ToString("N2")+"\n"; //Платеж процента
                    temp += SumCreditOperation.ToString("N2")+"\n"; //Основной остаток
                    ItogCreditSumOperation -= Payment;
                    ItogPlus = SumCreditOperation;
                    temp += ItogPlus + "\n";
                    itogOverpayment = (ItogCreditSum - SumCredit + ItogPlus);


                    resultCredits.Add(
                        new ResultCredit
                        {

                            NumberPayment = i,
                            //DateTimePayment = dataTime,  //для расчетов следущего платежа
                            SummaCredita = sumCreditSum, // Нужная клиенту самма кредита 
                            StavkaCredit = InterestRateYear,
                            SizePaymentBody = Math.Round(pereplataOchovTela, 2),
                            PrincipalBalance = Math.Round(SumCreditOperation, 2), // размер платежа основного долга в руб 
                            SizePaymentPercentage = Math.Round(procent, 2),//остаток основного долга в процентах
                            OverpaymentBalanceCredit = Math.Round(PereplataPoCredity, 2), //переплата по кредиту
                            TotalBalanceCredit = Math.Round(PereplataPoCredity + SumCredit, 2),
                        }

                    ) ;
                }
                
                //Добавляев в БД для статистики
                db.AddRange(
                   new ResultCredit
                   {
                       // SizePaymentBody = Convert.ToInt32(Payment),
                       SizePaymentBody = Math.Round(Payment, 2),
                       PrincipalBalance = Math.Round(pereplataOchovTela, 2), // размер платежа основного долга в руб 
                       SizePaymentPercentage = Math.Round(procent,2),//остаток основного долга в процентах
                       OverpaymentBalanceCredit = Math.Round(PereplataPoCredity, 2), //переплата по кредиту
                        // TotalBalanceCredit = Convert.ToInt32(PereplataPoCredity + SumCredit),
                       TotalBalanceCredit = Math.Round(PereplataPoCredity + SumCredit, 2),
                       DateTimePayment = DateTime.Now, // дата создания расчета
                      
                   }

                   ); ;
                db.SaveChanges(); // сохр. в бд

                return resultCredits;
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
