using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteca.Models
{
    public class Author
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "El id es obligatorio")]
        public int ID { get; set; }

        [Display(Name = "Nombre")]
        public string fullName { get; set; }

        [Display(Name = "Apellido")]
        public string lastName { get; set; }

        [Display(Name = "Biografia")]
        public string biography { get; set; }

        public string photo { get; set; }
    }
}
