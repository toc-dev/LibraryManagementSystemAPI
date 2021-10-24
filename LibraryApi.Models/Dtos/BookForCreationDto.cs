using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Dtos
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "A book should have a title!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "What's the ISBN?")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "When was this book published?")]
        public DateTime YearPublished { get; set; }
        [Required(ErrorMessage = "AuthorId is mandatory!")]
        public Guid AuthorId { get; set; }
        [Required(ErrorMessage = "Specify categories!")]
        public string Categories { get; set; }
        [Required(ErrorMessage = "Who's adding this book?")]
        public string CreatedBy { get; set; }
    }
}
