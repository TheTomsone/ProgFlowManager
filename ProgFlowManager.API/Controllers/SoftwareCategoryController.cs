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
    public class SoftwareCategoryController : ControllerBase
    {
        private readonly ISoftwareCategoryService _softwareCategoryService;
        public SoftwareCategoryController(ISoftwareCategoryService softwareCategoryService)
        {
            _softwareCategoryService = softwareCategoryService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SoftwareCategoryForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try { if (_softwareCategoryService.Create(form.ToModel<SoftwareCategory, SoftwareCategoryForm>()))
                    return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_softwareCategoryService.GetAll());
        }
        [HttpGet("bySoftware/{id}")]
        public IActionResult GetBySoftware(int id)
        {
            if (_softwareCategoryService.GetAllById<Software>(id) is null) return NotFound();

            return Ok(_softwareCategoryService.GetAllById<Software>(id));
        }
        [HttpGet("byCategory/{id}")]
        public IActionResult GetByCategory(int id)
        {
            if (_softwareCategoryService.GetAllById<Category>(id) is null) return NotFound();

            return Ok(_softwareCategoryService.GetAllById<Category>(id));
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] SoftwareCategoryForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try { if (_softwareCategoryService.DeleteRelation(form.ToModel<SoftwareCategory, SoftwareCategoryForm>()))
                    return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return NotFound();
        }
        [HttpDelete("bySoftware/{id}")]
        public IActionResult DeleteBySoftware(int id)
        {
            try { if (_softwareCategoryService.Delete(id)) return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return NotFound();
        }
    }
}
