using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProgFlowManager.API.ModelViews.Users
{
    public class UserLoginForm
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
