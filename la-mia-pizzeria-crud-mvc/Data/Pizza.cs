using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Data
{
    public class Pizza
    {
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "Il campo é richiesto")]
        [StringLength(50, ErrorMessage = "Il campo deve contenere massimo 50 caratteri")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il campo é richiesto")]
        [StringLength(300, ErrorMessage = "Il campo deve contenere masssimo 50 caratteri")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Il campo é richiesto")]
        [Range(0.01, 7000, ErrorMessage = "Il valore deve essere maggiore di 0")]
        public decimal Price { get; set; }
        public string? Image { get; set; }

        public Pizza() { }

        public Pizza(string name, string description, decimal price, string image)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Image = image;
        }
    }
}
