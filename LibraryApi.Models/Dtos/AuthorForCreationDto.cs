using LibraryApi.Models.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Dtos
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "UserId Required!")]
        public string UserId { get; set; }
    }
}
