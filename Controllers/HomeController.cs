using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using СreditСalculator.Models;

namespace СreditСalculator.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Логирование
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        //
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// По умолчанию
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            var headers = Request.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First());

            return View();
        }


        //тестовой контроллер

        public IActionResult Calc()
        {
            string userName = User.Identity.Name;
            var headers = Request.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First());

           // int resul = x + y;
            return View();
        }


        public IActionResult Register()
        {
           //Отправляем форму для регистрации. Или другую форму для ввода данных пользователю
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationBindingModel model )
        {
            
            return View("Success");
        }


        //Атрибут кэширования
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
