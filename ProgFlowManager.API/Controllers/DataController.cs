using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgFlowManager.API.ModelViews.Programs;
using ProgFlowManager.BLL.Tools;
using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Models;
using System.Text;
using System.Text.Encodings.Web;

namespace ProgFlowManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost("{id}/image")]
        public IActionResult UploadImage(int id, [FromBody] ImageForm images)
        {
            if (_dataService.GetById(id) is null) return NotFound();


            images.ImageData = Encoding.UTF8.GetBytes(images.ImageDataString);
            images.ImageMime = "image/png";


            try { if (_dataService.Update(images.ConvertTo<Data, ImageForm>(id))) return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpGet("{id}/image")]
        public IActionResult GetImage(int id)
        {
            if (_dataService.GetById(id) is null) return NotFound("No data found");
            if (_dataService.GetById(id).ImageData is null) return NotFound("No image found in this data");
            if (_dataService.GetById(id).ImageMime is null) return NotFound("Error corrupted file extension");

            try { return File(_dataService.GetById(id).ImageData, _dataService.GetById(id).ImageMime); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
