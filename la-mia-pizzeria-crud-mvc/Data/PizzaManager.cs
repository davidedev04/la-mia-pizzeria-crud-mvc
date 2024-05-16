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
                PizzaManager.InsertPizza(new Pizza("Margherita", "Pomodoro, Mozzarella", 5.5M, "../assets/pizza-margherita.jpg"));
                PizzaManager.InsertPizza(new Pizza("Diavola", "Pomodoro, Mozzarella, Salame Piccante", 7M, "../assets/pizza-margherita.jpg"));
                PizzaManager.InsertPizza(new Pizza("Marinara", "Mozzarella", 6.5M, "../assets/pizza-margherita.jpg"));
            }
        }
    }
}
