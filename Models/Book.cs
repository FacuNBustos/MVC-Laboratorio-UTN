using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Biblioteca.Models
{
    public class Book
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "El id es obligatorio")]
        public int ID { get; set; }

        [Display(Name = "Titulo")]
        public string title { get; set; }

        [Display(Name = "Resumen")]
        public string resume { get; set; }

        [Display(Name = "Fecha de publicacion")]
        public DateTime publicationDate { get; set; }

        public string photo { get; set; }

        public int authorID { get; set; }

        [Display(Name = "Autor")]
        public Author author { get; set; }

        public int genderID { get; set; }

        [Display(Name = "Genero")]
        public Gender gender { get; set; }
    }
}
