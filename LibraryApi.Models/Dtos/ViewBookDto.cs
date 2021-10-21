using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Dtos
{
    public class ViewBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }
        public string ISBN { get; set; }
        public Guid AuthorId { get; set; }
    }
}
