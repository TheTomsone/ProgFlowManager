using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews
{
    public class SoftwareLanguageForm
    {
        [Required]
        public int SoftwareId { get; set; }
        [Required]
        public int LanguageId { get; set; }
    }
}
