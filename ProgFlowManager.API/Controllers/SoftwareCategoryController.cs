﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgFlowManager.API.ModelViews.Programs;
using ProgFlowManager.BLL.Tools;
using ProgFlowManager.DAL;
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

            try { if (_softwareCategoryService.Create(form.ConvertTo<SoftwareCategory, SoftwareCategoryForm>()))
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
            if (_softwareCategoryService.GetAllById(id, "Software") is null) return NotFound();

            return Ok(_softwareCategoryService.GetAllById(id, "Software"));
        }
        [HttpGet("byCategory/{id}")]
        public IActionResult GetByCategory(int id)
        {
            if (_softwareCategoryService.GetAllById(id, "Category") is null) return NotFound();

            return Ok(_softwareCategoryService.GetAllById(id, "Category"));
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] SoftwareCategoryForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try { if (_softwareCategoryService.DeleteRelation(form.ConvertTo<SoftwareCategory, SoftwareCategoryForm>()))
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
