#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Movie : RecordBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsRecommended { get; set; }
        public decimal IMDbRating { get; set; }
        public decimal Metascore { get; set; }
        public int TitleTypeId { get; set; }
        public TitleType TitleTypes { get; set; }
    }
}
