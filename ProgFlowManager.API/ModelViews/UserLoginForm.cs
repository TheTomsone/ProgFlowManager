using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProgFlowManager.API.ModelViews
{
    public class UserLoginForm
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
