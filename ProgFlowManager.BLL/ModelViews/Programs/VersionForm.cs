using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews.Programs
{
    public class VersionForm
    {
        [Required]
        public string Name { get; set; }
        public string Resume { get; set; }
        [Required]
        public int Major { get; set; }
        [Required]
        public int Minor { get; set; }
        [Required]
        public int Patch { get; set; }
        public DateTime? Goal { get; set; }
        public DateTime? Release { get; set; }
        [Required]
        public int StageId {  get; set; }
        [Required]
        public int SoftwareId { get; set; }
    }
}
