using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Dtos
{
    public class ViewBookDto
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime YearPublished { get; set; }
    }
}
