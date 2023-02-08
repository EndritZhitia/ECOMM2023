using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ECOMM2023.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}