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
    public class VersionController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IVersionService _versionService;
        private readonly IContentService _contentService;
        private readonly IStageService _stageService;

        private readonly IEnumerable<VersionNbDTO> _versions;
        private readonly IEnumerable<VersionNbDTO> _versionsFull;

        public VersionController(IDataService dataService, IVersionService versionService, IContentService contentService, IStageService stageService)
        {
            _dataService = dataService;
            _versionService = versionService;
            _contentService = contentService;
            _stageService = stageService;

            _versions = _versionService.GetAll().ConvertTo<VersionNbDTO, VersionNb>()
                                       .MergeWith(_dataService.GetAll(), version => version.Id, data => data.Id)
                                       .MergeOne<VersionNbDTO, StageDTO, Stage, VersionNb>(_versionService.GetById, version => version.StageId, _stageService.GetById);

            _versionsFull = _versions.ConvertTo<VersionNbFullDTO, VersionNbDTO>()
                                     .MergeManyToOne(version => version.Contents, id => _contentService.GetAllById<VersionNb>(id)
                                                                                                       .ConvertTo<ContentDTO, Content>()
                                                                                                       .MergeWith(_dataService.GetAll(), content => content.Id, data => data.Id)
                                                                                                       .MergeOne<ContentDTO, StageDTO, Stage, Content>(_contentService.GetById, content => content.StageId, _stageService.GetById)
                                                                                                       .ToList());

            //_versionsFull = _versions.ConvertTo<VersionNbFullDTO, VersionNbDTO>()
            //                         .MergeManyToOne(version => version.Contents, id => _contentService.GetAllById<VersionNb>(id).ToList());
        }

        [HttpPost]
        public IActionResult Create(int id, [FromBody] VersionForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (_dataService.Create(form.ConvertTo<Data, VersionForm>()))
                {
                    VersionNb version = form.ConvertTo<VersionNb, VersionForm>(_dataService.GetLastId());

                    version.SoftwareId = id;

                    if (_versionService.Create(version))
                        return Ok();
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try { return Ok(_versions); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (_versions.First(v => v.Id == id) is null) return NotFound();

            try { return Ok(_versions.First(v => v.Id == id)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("bySoftware/{id}")]
        public IActionResult GetBySoftware(int id)
        {
            if (_versions.First(v => v.SoftwareId == id) is null) return NotFound();

            try { return Ok(_versions.First(v => v.SoftwareId == id)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("details")]
        public IActionResult GetAllDetails()
        {
            try { return Ok(_versionsFull); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("details/{id}")]
        public IActionResult GetDetailsById(int id)
        {
            if (_versionsFull.First(v => v.Id == id) is null) return NotFound();

            try { return Ok(_versionsFull.First(v => v.Id == id)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("details/bySoftware/{id}")]
        public IActionResult GetDetailsBySoftware(int id)
        {
            if (_versionsFull.First(v => v.SoftwareId == id) is null) return NotFound();

            try { return Ok(_versionsFull.First(v => v.SoftwareId == id)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
