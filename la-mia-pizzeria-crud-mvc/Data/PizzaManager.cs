using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using la_mia_pizzeria_crud_mvc.Models;

namespace la_mia_pizzeria_crud_mvc.Data
{
    public enum ResultType
        {
            OK,
            Exception,
            NotFound
        }
    public static class PizzaManager
    {
        

        public static int CountAllPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizze.Count();
        }

        public static List<Pizza> GetAllPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizze.ToList();
        }
        public static List<Category> GetAllCategories()
        {
            using PizzaContext db = new PizzaContext();
            return db.Category.ToList();
        }

        public static List<Ingredients> GetAllIngredients()
        {
            using PizzaContext db = new PizzaContext();
            return db.ingredients.ToList();
        }
        public static Ingredients GetIngredientById(int id)
        {
            using PizzaContext db = new PizzaContext();
            return db.ingredients.FirstOrDefault(t => t.Id == id);
        }

        public static Pizza GetPizza(int id, bool includeReferences = true)
        {
            using PizzaContext db = new PizzaContext();
            if (includeReferences)
                return db.Pizze.Where(x => x.Id == id).Include(p => p.Category).FirstOrDefault();
            return db.Pizze.FirstOrDefault(p => p.Id == id);
        }

        public static void InsertPizza(Pizza pizza, List<string> SelectedIngredients = null)
        {
            using PizzaContext db = new PizzaContext();
            if (SelectedIngredients != null)
            {
                pizza.Ingredients = new List<Ingredients>();
                // Trasformiamo gli ID scelti in tag da aggiungere tra i riferimenti in Post
                foreach (var ingredientsId in SelectedIngredients)
                {
                    int id = int.Parse(ingredientsId);
                    var ing = db.ingredients.FirstOrDefault(t => t.Id == id); // PostManager.GetTagById(id); NON usiamo GetTagById() perché usa un db context diverso e ciò causerebbe errore in fase di salvataggio - usiamo lo stesso context all'interno della stessa operazione
                    pizza.Ingredients.Add(ing);
                }
            }
            db.Pizze.Add(pizza);
            db.SaveChanges();
        }

        public static bool UpdatePizza(int id, Action<Pizza> edit)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizze.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
                return false;

            edit(pizza);

            db.SaveChanges();

            return true;
        }

        public static bool UpdatePizza(int id, string name, string description, int? categoryId, List<string> ingredients)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizze.Where(x => x.Id == id).Include(x => x.Ingredients).FirstOrDefault();

            if (pizza == null)
                return false;

            pizza.Name = name;
            pizza.Description = description;
            pizza.CategoryId = categoryId;

            pizza.Ingredients.Clear(); // Prima svuoto così da salvare solo le informazioni che l'utente ha scelto, NON le aggiungiamo ai vecchi dati
            if (ingredients != null)
            {
                foreach (var tag in ingredients)
                {
                    int ingredientId = int.Parse(tag);
                    var ingredientFromDb = db.ingredients.FirstOrDefault(x => x.Id == ingredientId);
                    pizza.Ingredients.Add(ingredientFromDb);
                }
            }

            db.SaveChanges();

            return true;
        }

        public static void Seed()
        {
            if (PizzaManager.CountAllPizzas() == 0)
            {
                PizzaManager.InsertPizza(new Pizza("Margherita", "Pomodoro, Mozzarella", 5.5M, "https://www.finedininglovers.it/sites/g/files/xknfdk1106/files/styles/recipes_1200_800/public/fdl_content_import_it/margherita-50kalo.jpg.webp?itok=QlO8_AHv"));
                PizzaManager.InsertPizza(new Pizza("Diavola", "Pomodoro, Mozzarella, Salame Piccante", 7M, "https://c7.alamy.com/compit/rfkrf3/pizza-diavola-salamy-rfkrf3.jpg"));
                PizzaManager.InsertPizza(new Pizza("Marinara", "Mozzarella", 6.5M, "https://img.delicious.com.au/R29uytco/w759-h506-cfill/del/2019/03/marinara-pizza-102752-2.jpg"));
            }
        }
    }
}
