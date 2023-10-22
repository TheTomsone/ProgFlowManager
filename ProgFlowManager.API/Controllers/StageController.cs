using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgFlowManager.BLL.Models.Programs;
using ProgFlowManager.BLL.Tools;
using ProgFlowManager.DAL.Interfaces.Programs;
using ProgFlowManager.DAL.Models.Programs;

namespace ProgFlowManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly IStageService _stageService;

        public StageController(IStageService stageService)
        {
            _stageService = stageService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try { return Ok(_stageService.GetAll().ToDTO<StageDTO, Stage>()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_stageService.GetById(id) is null) return NotFound();

            try { return Ok(_stageService.GetById(id).ToDTO<StageDTO, Stage>()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
