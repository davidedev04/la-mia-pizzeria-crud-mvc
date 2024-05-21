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
            var tagsFromDB = PizzaManager.GetAllIngredients();
            foreach (var tag in tagsFromDB) // es. tag1, tag2, tag3... tag10
            {
                bool isSelected = Pizza.Ingredients?.Any(t => t.Id == tag.Id) == true;
                ingredients.Add(new SelectListItem() // lista degli elementi selezionabili
                {
                    Text = tag.IngName, // Testo visualizzato
                    Value = tag.Id.ToString(), // SelectListItem vuole una generica stringa, non un int
                    Selected = isSelected // es. tag1, tag5, tag9
                });
                if (isSelected)
                    SelectedIngredients.Add(tag.Id.ToString()); // lista degli elementi selezionati
            }
        }
    }
}
