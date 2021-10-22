using LibraryApi.Models.Dtos;
using LibraryApi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Models.Entities;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return Ok(await _categoryService.GetCategoryAsync(id));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetCategoriesAsync());
        }

        [HttpPost("")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryForCreationDto newCategory)
        {
            if (newCategory is null) return BadRequest();

            var category = _mapper.Map<Category>(newCategory);

            return Ok(await _categoryService.CreateCategoryAsync(category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }

    }
}
