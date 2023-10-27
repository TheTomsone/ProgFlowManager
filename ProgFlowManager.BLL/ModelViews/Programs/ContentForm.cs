using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews.Programs
{
    public class ContentForm
    {
        public string Name { get; set; }
        public string Resume { get; set; }
        public int StageId { get; set; }
        public int VersionNbId { get; set; }
    }
}
