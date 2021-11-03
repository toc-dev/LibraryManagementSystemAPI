using LibraryApi.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Entities
{
    public class Role : IdentityRole, ITracking
    {
        public Role()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
