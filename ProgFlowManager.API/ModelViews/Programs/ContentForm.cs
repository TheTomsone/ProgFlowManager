using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews.Programs
{
    public class ContentForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Resume { get; set; }
        [Required]
        public int StageId { get; set; }
    }
}
