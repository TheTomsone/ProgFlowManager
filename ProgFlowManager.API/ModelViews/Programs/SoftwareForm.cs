using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews.Programs
{
    public class SoftwareForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Resume { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime Started { get; set; } = DateTime.Now;
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Languages { get; set; }
    }
}
