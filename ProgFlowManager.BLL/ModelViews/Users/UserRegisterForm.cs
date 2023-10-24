using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProgFlowManager.API.ModelViews.Users
{
    public class UserRegisterForm
    {
        public required string Name { get; set; }
        public string? Resume { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
