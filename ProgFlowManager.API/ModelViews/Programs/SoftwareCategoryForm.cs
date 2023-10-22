using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews.Programs
{
    public class SoftwareCategoryForm
    {
        [Required]
        public int SoftwareId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
