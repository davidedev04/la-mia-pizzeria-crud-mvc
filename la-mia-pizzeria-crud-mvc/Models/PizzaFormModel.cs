using la_mia_pizzeria_crud_mvc.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? ingredients { get; set; }
        public List<string>? SelectedIngredients { get; set; }

        public PizzaFormModel() { }

        public PizzaFormModel(Pizza pizza, List<Category>? categories)
        {
            Pizza = pizza;
            Categories = categories;
        }

        public void CreateIngredients()
        {
            ingredients = new List<SelectListItem>();
            SelectedIngredients = new List<string>();
            var ingredientsFromDB = PizzaManager.GetAllIngredients();
            foreach (var ingrediente in ingredientsFromDB) 
            {
                bool isSelected = Pizza.Ingredients?.Any(i => i.Id == ingrediente.Id) == true;
                ingredients.Add(new SelectListItem() // lista degli elementi selezionabili
                {
                    Text = ingrediente.IngName, // Testo visualizzato
                    Value = ingrediente.Id.ToString(), // SelectListItem vuole una generica stringa, non un int
                    Selected = isSelected // es. tag1, tag5, tag9
                });
                if (isSelected)
                    SelectedIngredients.Add(ingrediente.Id.ToString()); // lista degli elementi selezionati
            }
        }
    }
}
