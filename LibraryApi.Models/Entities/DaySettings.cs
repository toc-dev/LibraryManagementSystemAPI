using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Models.Entities
{
    public class DaySettings
    {
        public const string SectionName = "DaySettings";

        public int DueIn { get; set; }
    }
}
