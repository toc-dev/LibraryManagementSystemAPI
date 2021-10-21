using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _bookService.GetBooksAsync());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            return Ok(await _bookService.GetBookByIdAsync(id));
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetBooksByCategory(string category)
        {
            return Ok(await _bookService.GetBooksByCategoryAsync(category));
        }
    }
}
