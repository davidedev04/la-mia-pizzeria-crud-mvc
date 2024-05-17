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

        [HttpGet]
        public IActionResult Update(int id)
        {

            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizzaToEdit = context.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(pizzaToEdit);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", pizza);
            }

            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizzaToEdit = context.Pizze.Where(pizza =>  pizza.Id == id).FirstOrDefault();

                if(pizzaToEdit != null)
                {
                    pizzaToEdit.Name = pizza.Name;
                    pizzaToEdit.Description = pizza.Description;
                    pizzaToEdit.Price = pizza.Price;

                    context.SaveChanges();

                    return RedirectToAction("Index");
                } else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizzaToDelete = context.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    context.Pizze.Remove(pizzaToDelete);

                    context.SaveChanges();

                    return RedirectToAction("Index");
                } else
                {
                    return NotFound();
                }
            }
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
