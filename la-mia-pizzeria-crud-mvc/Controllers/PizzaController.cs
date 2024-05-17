using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud_mvc.Data;
using la_mia_pizzeria_crud_mvc.Models;
using System.Diagnostics;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(PizzaManager.GetAllPizzas());
        }

        [HttpGet]
        public IActionResult GetPizza(int id)
        {
            var pizza = PizzaManager.GetPizza(id);
            if (pizza != null)
                return View(pizza);
            else
                return View("errore");
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Pizza pizzaC)
        {
            if (!ModelState.IsValid)
            {
                return View(pizzaC);
            }

            using (var db = new PizzaContext())
            {
                db.Add(pizzaC);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
