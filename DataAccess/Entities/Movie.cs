#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Movie : RecordBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime? PublishDate { get; set; }
        public decimal? Revenue { get; set; }
        public int? DirectorId { get; set; }
        public Director Director { get; set; }
        public List<UserMovie> UserMovies { get; set; }


    }
}
