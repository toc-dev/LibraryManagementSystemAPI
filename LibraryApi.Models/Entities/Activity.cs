using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
