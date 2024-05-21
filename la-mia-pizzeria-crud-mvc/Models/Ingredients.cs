using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class Ingredients
    {
        [Key] public int Id { get; set; }
        public string IngName { get; set; }

        public List<Pizza> Pizzas { get; set; }
    }
}
