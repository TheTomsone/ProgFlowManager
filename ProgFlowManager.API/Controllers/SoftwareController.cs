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
        private readonly ISoftwareService _softwareService;
        private readonly ISoftwareCategoryService _softwareCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly ISoftwareLanguageService _softwareLanguageService;
        private readonly ILanguageService _languageService;
        private readonly IEnumerable<SoftwareDTO> _softwares;

        public SoftwareController(ISoftwareService programService, ISoftwareCategoryService programCategoryService, ICategoryService categoryService, IDataService dataService, ISoftwareLanguageService softwareLanguageService, ILanguageService languageService)
        {
            _softwareService = programService;
            _softwareCategoryService = programCategoryService;
            _categoryService = categoryService;
            _dataService = dataService;
            _softwareLanguageService = softwareLanguageService;
            _languageService = languageService;

            _softwares = _softwareService.GetAll().ToDTO<SoftwareDTO, Software>()
                                        .MergeWith(_dataService.GetAll().ToDTO<SoftwareDTO, Data>())
                                        .MergeData<SoftwareDTO, CategoryDTO, SoftwareCategory, Category>(
                                                    software => software.Categories,
                                                    _softwareCategoryService.GetAllById<Software>,
                                                    relation => relation.CategoryId,
                                                    _categoryService.GetById)
                                        .MergeData<SoftwareDTO, LanguageDTO, SoftwareLanguage, Language>(
                                                    software => software.Languages,
                                                    _softwareLanguageService.GetAllById<Software>,
                                                    relation => relation.LanguageId,
                                                    _languageService.GetById);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SoftwareForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (_dataService.Create(form.ToModel<Data, SoftwareForm>()) &&
                        _softwareService.Create(form.ToModel<Software, SoftwareForm>(_dataService.GetLastId())))
                    return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_softwares);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (_softwares.First(s => s.Id == id) is null) return NotFound();

            return Ok(_softwares.First(s => s.Id == id));
        }

        [HttpPatch]
        public IActionResult Update(int id, [FromBody] SoftwareForm form)
        {
            if (!ModelState.IsValid) return NotFound(ModelState);

            try
            {
                if (_dataService.Update(form.ToModel<Data, SoftwareForm>(id)) &&
                        _softwareService.Update(form.ToModel<Software, SoftwareForm>(id)))
                    return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try { if (_softwareService.Delete(id) && _dataService.Delete(id)) return Ok(); }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return NotFound();
        }

        //private IEnumerable<SoftwareDTO> GetSoftwareDTOs()
        //{
        //    IEnumerable<SoftwareDTO> softwareDTOs = _softwareService.Models.ToDTO<SoftwareDTO, Software>()
        //                                                            .MergeWith(_dataService.Models.ToDTO<SoftwareDTO, Data>());

        //    foreach (SoftwareDTO software in softwareDTOs)
        //    {
        //        Console.WriteLine(software.Name + " - ");
        //        foreach (LanguageDTO lang in software.Languages) Console.Write(lang.Label + ",");
        //        Console.WriteLine();

        //        IEnumerable<SoftwareCategory> categoryList = _softwareCategoryService.GetAllById<Software>(software.Id);
        //        IEnumerable<SoftwareLanguage> languageList = _softwareLanguageService.GetAllById<Language>(software.Id);
        //        List<CategoryDTO> categories = new();
        //        List<LanguageDTO> languages = new();
        //        foreach (SoftwareLanguage softLang in languageList) Console.WriteLine(softLang.SoftwareId + " - " + softLang.LanguageId);



        //        foreach (SoftwareCategory programCat in categoryList)
        //            categories.Add(_categoryService.GetById(programCat.CategoryId).ToDTO<CategoryDTO, Category>());
        //        foreach (SoftwareLanguage programLang in languageList)
        //        {
        //            Console.WriteLine("Before conversion : " + _languageService.GetById(programLang.LanguageId).Label);
        //            languages.Add(_languageService.GetById(programLang.LanguageId).ToDTO<LanguageDTO, Language>());
        //            Console.WriteLine("After conversion : " + _languageService.GetById(programLang.LanguageId).ToDTO<LanguageDTO, Language>().Label);
        //        }

        //        software.Categories = categories;
        //        software.Languages = languages;
        //    }

        //    return softwareDTOs;
        //}
    }
}
