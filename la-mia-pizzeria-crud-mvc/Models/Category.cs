using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }
        public string Type { get; set; }
        public List<Pizza> Pizzas { get; set; }

        public Category() { }
    }
}
