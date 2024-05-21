namespace la_mia_pizzeria_crud_mvc.Data
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public PizzaFormModel() { }

        public PizzaFormModel(Pizza pizza, List<Category>? categories)
        {
            Pizza = pizza;
            Categories = categories;
        }
    }
}
