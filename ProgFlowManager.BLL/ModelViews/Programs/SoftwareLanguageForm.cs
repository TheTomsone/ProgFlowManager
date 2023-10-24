using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews.Programs
{
    public class SoftwareLanguageForm
    {
        [Required]
        public int SoftwareId { get; set; }
        [Required]
        public int LanguageId { get; set; }
    }
}
