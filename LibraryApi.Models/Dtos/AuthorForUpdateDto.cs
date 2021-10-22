using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Dtos
{
    public class AuthorForUpdateDto
    {
        [Required(ErrorMessage = "FirstName Required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Required!")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
