using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgFlowManager.DAL.Models;
using ProgFlowManager.BLL.Models.Programs;
using ProgFlowManager.BLL.Tools;
using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Interfaces.Programs;
using ProgFlowManager.DAL.Models.Programs;
using ProgFlowManager.DAL.Services.Programs;
using System.Reflection;
using ProgFlowManager.API.ModelViews;

namespace ProgFlowManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ISoftwareService _programService;
        private readonly ISoftwareCategoryService _programCategoryService;
        private readonly ICategoryService _categoryService;
        public SoftwareController(ISoftwareService programService, ISoftwareCategoryService programCategoryService, ICategoryService categoryService, IDataService dataService)
        {
            _programService = programService;
            _programCategoryService = programCategoryService;
            _categoryService = categoryService;
            _dataService = dataService;

        }

        [HttpPost]
        public IActionResult Create([FromBody] SoftwareForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try { if (_dataService.Create(form.ToModel<Data, SoftwareForm>()) &&
                        _programService.Create(form.ToModel<Software, SoftwareForm>(_dataService.GetLastId())))
                            return Ok(); } 
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(GetSoftwareDTOs());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (GetSoftwareDTOs().First(s => s.Id == id) is null) return NotFound();

            return Ok(GetSoftwareDTOs().First(s => s.Id == id));
        }

        [HttpPatch]
        public IActionResult Update(int id, [FromBody] SoftwareForm form)
        {
            if (!ModelState.IsValid) return NotFound(ModelState);

            try { if (_dataService.Update(form.ToModel<Data, SoftwareForm>(id)) &&
                        _programService.Update(form.ToModel<Software, SoftwareForm>(id)))
                            return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try { if (_programService.Delete(id) && _dataService.Delete(id)) return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return NotFound();
        }

        private IEnumerable<SoftwareDTO> GetSoftwareDTOs()
        {
            IEnumerable<SoftwareDTO> softwareDTOs = _programService.Models.ToDTO<SoftwareDTO, Software>()
                                                                    .MergeWith(_dataService.Models.ToDTO<SoftwareDTO, Data>());

            foreach (SoftwareDTO software in softwareDTOs)
            {
                IEnumerable<SoftwareCategory> dalList = _programCategoryService.GetAllById<Software>(software.Id);
                List<CategoryDTO> categories = new();

                foreach (SoftwareCategory programCat in dalList)
                    categories.Add(_categoryService.GetById(programCat.CategoryId).ToDTO<CategoryDTO, Category>());

                software.Categories = categories;
            }

            return softwareDTOs;
        }
    }
}
