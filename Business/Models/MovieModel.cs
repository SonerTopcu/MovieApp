#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class MovieModel : RecordBase
    {
        #region Entity Properties
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Movie Name")]
        public string Name { get; set; }

        [DisplayName("Publish Date")]
        public DateTime? PublishDate { get; set; }

        [DisplayName("Revenue")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be positive!")]
        public decimal? Revenue { get; set; }

        [DisplayName("Director")]
        public int? DirectorId { get; set; }
        #endregion

        #region Extra Properties
        [DisplayName("Publish Date")]
        public string PublishDateOutput { get; set; }

        [DisplayName("Revenue")]
        public string RevenueOutput { get; set; }

        [DisplayName("Director")]
        public string DirectorOutput { get; set; }
     
        [DisplayName("Users")]
        public List<int> UsersInput { get; set; }

        public List<UserModel> Users { get; set; }
        #endregion
    }
}
