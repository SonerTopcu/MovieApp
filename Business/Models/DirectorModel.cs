#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class DirectorModel : RecordBase
    {
        #region Entity Properties
        [DisplayName("Director Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
        #endregion

        #region Extra Properties
        public string Movies { get; set; }
        #endregion
    }
}