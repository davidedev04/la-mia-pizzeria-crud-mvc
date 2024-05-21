using Microsoft.EntityFrameworkCore;

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

        public static Pizza GetPizza(int id, bool includeReferences = true)
        {
            using PizzaContext db = new PizzaContext();
            if (includeReferences)
                return db.Pizze.Where(x => x.Id == id).Include(p => p.Category).FirstOrDefault();
            return db.Pizze.FirstOrDefault(p => p.Id == id);
        }

        public static void InsertPizza(Pizza pizza)
        {
            using PizzaContext db = new PizzaContext();
            db.Pizze.Add(pizza);
            db.SaveChanges();
        }

        public static bool UpdatePizza(int id, Pizza pizza)
        {
            try
            {
                // Non posso riusare GetPizza()
                // perché il DbContext deve continuare a vivere
                // affinché possa accorgersi di quali modifiche deve salvare
                using PizzaContext db = new PizzaContext();
                var pizzaDaModificare = db.Pizze.FirstOrDefault(p => p.Id == id);
                if (pizzaDaModificare == null)
                    return false;
                pizzaDaModificare.Name = pizza.Name;
                pizzaDaModificare.Description = pizza.Description;
                pizzaDaModificare.Price = pizza.Price;
                pizzaDaModificare.CategoryId = pizza.CategoryId;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
