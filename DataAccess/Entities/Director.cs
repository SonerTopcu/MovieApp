#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Director : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }    
        public List<Movie> Movies { get; set; }
    }
}
