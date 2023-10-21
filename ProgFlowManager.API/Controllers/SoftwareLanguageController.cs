using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgFlowManager.API.ModelViews;
using ProgFlowManager.BLL.Tools;
using ProgFlowManager.DAL.Interfaces.Programs;
using ProgFlowManager.DAL.Models.Programs;

namespace ProgFlowManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareLanguageController : ControllerBase
    {
        private readonly ISoftwareLanguageService _softwareLanguageService;

        public SoftwareLanguageController(ISoftwareLanguageService softwareLanguageService)
        {
            _softwareLanguageService = softwareLanguageService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] IEnumerable<SoftwareLanguageForm> forms)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try { foreach (SoftwareLanguageForm form in forms)
                    if (_softwareLanguageService.Create(form.ToModel<SoftwareLanguage, SoftwareLanguageForm>()))
                        return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_softwareLanguageService.GetAll());
        }
        [HttpGet("bySoftware/{id}")]
        public IActionResult GetBySoftware(int id)
        {
            if (_softwareLanguageService.GetAllById<Software>(id) is null) return NotFound();

            return Ok(_softwareLanguageService.GetAllById<Software>(id));
        }
        [HttpGet("byLanguage/{id}")]
        public IActionResult GetByLanguage(int id)
        {
            if (_softwareLanguageService.GetAllById<Language>(id) is null) return NotFound();

            return Ok(_softwareLanguageService.GetAllById<Language>(id));
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] IEnumerable<SoftwareLanguageForm> forms)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try { foreach (SoftwareLanguageForm form in forms)
                    if (_softwareLanguageService.DeleteRelation(form.ToModel<SoftwareLanguage, SoftwareLanguageForm>()))
                        return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return NotFound();
        }
        [HttpDelete("bySoftware/{id}")]
        public IActionResult DeleteBySoftware(int id)
        {
            try { if (_softwareLanguageService.Delete(id)) return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return NotFound();
        }
    }
}
