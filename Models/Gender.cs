using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteca.Models
{
    public class Gender
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "El id es obligatorio")]
        public int ID { get; set; }

        [Display(Name = "Descripcion")]
        public string description { get; set; }
    }
}
