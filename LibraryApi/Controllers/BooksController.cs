using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using LibraryApi.Extensions;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAnyRole")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            return Ok(await _bookService.GetBookByIdAsync(id, trackChanges: false));
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetBooksByCategory(string category)
        {
            return Ok(await _bookService.GetBooksByCategoryAsync(category));
        }

        [HttpGet("{id}/request")]
        [Authorize(Policy = "RequireUserOrAuthorRole")]
        public IActionResult RequestBook(Guid id)
        {
            var userId = HttpContext.User.GetLoggedInUserId();

            return Ok(new { UserId = userId });
        }
    }
}
