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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try { return Ok(_categoryService.GetAll().ConvertTo<CategoryDTO, Category>()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (_categoryService.GetById(id) is null) return NotFound();

            try { return Ok(_categoryService.GetById(id).ConvertTo<CategoryDTO, Category>()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
