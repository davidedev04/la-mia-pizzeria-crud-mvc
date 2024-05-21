using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud_mvc.Data;
using la_mia_pizzeria_crud_mvc.Models;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;

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
            PizzaFormModel model = new PizzaFormModel();
            model.Pizza = new Pizza();
            model.Categories = PizzaManager.GetAllCategories();
            model.CreateIngredients();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel pizzaC)
        {
            if (ModelState.IsValid == false)
            {
                // Ritorno la form di prima con i dati della pizza
                // precompilati dall'utente
                pizzaC.Categories = PizzaManager.GetAllCategories();
                pizzaC.CreateIngredients();
                return View("Create", pizzaC);
            }

            PizzaManager.InsertPizza(pizzaC.Pizza, pizzaC.SelectedIngredients);
            // Richiamiamo la action Index affinché vengano mostrate tutte le pizze
            // inclusa quella nuova
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            var pizzaToEdit = PizzaManager.GetPizza(id);

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                PizzaFormModel model = new PizzaFormModel(pizzaToEdit, PizzaManager.GetAllCategories());
                model.CreateIngredients();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel model)
        {
            if (ModelState.IsValid == false)
            {
                // Ritorno la form di prima con i dati della pizza
                // precompilati dall'utente
                model.Categories = PizzaManager.GetAllCategories();
                model.CreateIngredients();
                return View("Update", model);
            }

            if (PizzaManager.UpdatePizza(id, model.Pizza.Name, model.Pizza.Description, model.Pizza.CategoryId, model.SelectedIngredients))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }



            bool result = PizzaManager.UpdatePizza(id, pizzaToEdit =>
            {
                pizzaToEdit.Name = model.Pizza.Name;
                pizzaToEdit.Description = model.Pizza.Description;
                pizzaToEdit.CategoryId = model.Pizza.CategoryId;
                pizzaToEdit.Ingredients.Clear();
            });
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
                }
                else
                {
                    return NotFound();
                }
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
