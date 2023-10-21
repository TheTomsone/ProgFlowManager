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
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_languageService.GetAll().ToDTO<LanguageDTO, Language>());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (_languageService.GetById(id) is null) return NotFound();

            return Ok(_languageService.GetById(id).ToDTO<LanguageDTO, Language>());
        }
    }
}
