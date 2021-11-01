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
            return Ok(await _bookService.GetBookByIdAsync(id));
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetBooksByCategory(string category)
        {
            return Ok(await _bookService.GetBooksByCategoryAsync(category));
        }

        [HttpGet("{id}/request")]
        [Authorize(Policy = "RequireUserOrAuthorRole")]
        public async Task<IActionResult> RequestBook(Guid id)
        {
            var userId = HttpContext.User.GetLoggedInUserId();

            var request = await _bookService.RequestBook(userId, id);
            if (request is null)
                return BadRequest("You already requested for this book!");

            return Ok(request);
        }

        /* Gideon's Review
            KCM please refactor this method
         */
        /*[HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminOrAuthorRole")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book is null)
                return BadRequest("Book is null or invalid");

            var bookToDel = _mapper.Map<Book>(book);
            _bookService.DeleteBook(bookToDel);

            return NoContent();
        }*/
    }
}
