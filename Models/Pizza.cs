using la_mia_pizzeria_static.Validator;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int PizzaId { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Nome non può avere più di 50 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(150, ErrorMessage = "La Descrizione non può avere più di 150 caratteri")]
        [CinqueParole]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(1000, ErrorMessage = "La Url dell'immagine non può avere più di 1000 caratteri")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(0, 999, ErrorMessage = "Il Prezzo non può essere minore di 0, o superiore a 999")]
        public int Prezzo { get; set; }

        public Pizza ()
        {

        }

        public Pizza (string name, string description, int prezzo, string image)
        {
            Name = name;
            Description = description;
            Prezzo = prezzo;
            Image = image;
        }
    }
}
