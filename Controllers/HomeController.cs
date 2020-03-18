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

                ViewBag.Resul2 = creditG;

                ViewBag.Resul = $"Ваша заявка расмотренна!" +
                    $" {Environment.NewLine}" +
                    $"Сумма кредита:{summaCredita}" +
                    $"{Environment.NewLine}" +
                    $"Нужный срок кредитования:{timeCredit}" +
                    $"{Environment.NewLine}" +
                    $"Утвержденная ставка{stavkaCrediy}";
 
                return View("Success");
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

        //Атрибут кэширования
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
