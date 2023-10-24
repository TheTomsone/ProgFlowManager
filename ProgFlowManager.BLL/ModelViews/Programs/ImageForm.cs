using System.ComponentModel.DataAnnotations;

namespace ProgFlowManager.API.ModelViews.Programs
{
    public class ImageForm
    {
        public byte[]? ImageData { get; set; }
        public string ImageDataString { get; set; }
        public string? ImageMime { get; set; }
    }
}
