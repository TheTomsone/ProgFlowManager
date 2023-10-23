using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgFlowManager.API.ModelViews.Programs;
using ProgFlowManager.BLL.Models.Programs;
using ProgFlowManager.BLL.Tools;
using ProgFlowManager.DAL;
using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Interfaces.Programs;
using ProgFlowManager.DAL.Models;
using ProgFlowManager.DAL.Models.Programs;

namespace ProgFlowManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IContentService _contentService;
        private readonly IStageService _stageService;

        private readonly IEnumerable<ContentDTO> _contents;

        public ContentController(IDataService dataService, IContentService contentService, IStageService stageService)
        {
            _dataService = dataService;
            _contentService = contentService;
            _stageService = stageService;

            _contents = _contentService.GetAll().ConvertTo<ContentDTO, Content>()
                                       .MergeWith(_dataService.GetAll(), content => content.Id, data => data.Id)
                                       .MergeOne<ContentDTO, StageDTO, Stage, Content>(_contentService.GetById, content => content.StageId, _stageService.GetById);
        }

        [HttpPost]
        public IActionResult Create(int id, [FromBody] ContentForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (_dataService.Create(form.ConvertTo<Data, ContentForm>()))
                {
                    Content content = form.ConvertTo<Content, ContentForm>();

                    content.VersionNbId = id;

                    if (_contentService.Create(content)) return Ok();
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try { return Ok(_contents); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (_contents.First(c => c.Id == id) is null) return NotFound();

            try { return Ok(_contents.First(c => c.Id == id)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("byVersion/{id}")]
        public IActionResult GetByVersion(int id)
        {
            if (_contents.First(c => c.VersionNbId == id) is null) return NotFound();

            try { return Ok(_contents.First(c => c.VersionNbId == id)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
