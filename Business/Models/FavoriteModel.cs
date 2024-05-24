#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class FavoriteModel
    {
        public int MovieId { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Movie Name")]
        public string MovieName { get; set; }

        public double Revenue { get; set; }

        [DisplayName("Revenue")]
        public string RevenueOutput { get; set; }
    }
}