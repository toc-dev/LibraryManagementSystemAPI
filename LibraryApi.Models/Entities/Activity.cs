using LibraryApi.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Entities
{
    public class Activity : ITracking
    {
        private IConfiguration _configuration;
        private readonly int _day;
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Activity(IConfiguration configuration)
        {
            _configuration = configuration;
            _day = Int32.Parse(_configuration.GetSection("DaySettings").GetSection("dueIn").Value);
            RequestDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(_day);
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
