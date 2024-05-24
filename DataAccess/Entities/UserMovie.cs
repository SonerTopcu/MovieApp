#nullable disable

using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class UserMovie : RecordBase
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
