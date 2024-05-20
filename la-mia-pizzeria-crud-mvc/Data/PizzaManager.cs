namespace la_mia_pizzeria_crud_mvc.Data
{
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

        public static Pizza GetPizza(int id)
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizze.FirstOrDefault(p => p.Id == id);
        }

        public static void InsertPizza(Pizza pizza)
        {
            using PizzaContext db = new PizzaContext();
            db.Pizze.Add(pizza);
            db.SaveChanges();
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
