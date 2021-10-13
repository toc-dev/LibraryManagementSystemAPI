using System;
using System.Collections.Generic;
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
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
