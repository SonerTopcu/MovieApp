#nullable disable

using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TitleType : RecordBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
