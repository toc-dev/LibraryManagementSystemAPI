using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime YearPublished { get; set; }
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
