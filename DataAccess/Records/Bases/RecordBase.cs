#nullable disable

namespace DataAccess.Records.Bases
{
    public abstract class RecordBase
    {
        public int Id { get; set; }
        public string? Guid { get; set; } = System.Guid.NewGuid().ToString();
    }
}
