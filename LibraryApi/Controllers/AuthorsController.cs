using AutoMapper;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/authors")]
    [ApiController]
   /* [Authorize(Policy = "AuthorClaimPolicy")]
    [Authorize(Policy = "AdminClaimPolicy")]*/
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;
        private readonly IBookService bookService;
        private readonly IUnitOfWork unitOfWork;

        public AuthorsController(IAuthorService authorService, 
            IMapper mapper, 
            IBookService bookService,
            IUnitOfWork unitOfWork)
        {
            this.authorService = authorService;
            this.mapper = mapper;
            this.bookService = bookService;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await authorService.GetAuthorsAsync();

            var authorsDto = mapper.Map<IEnumerable<ViewAuthorDto>>(authors);
            return Ok(authorsDto);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<IActionResult> GetSingleAuthor(Guid id)
        {
            var author = await authorService.GetAuthorByIdAsync(id, trackChanges: false);

            var authorDto = mapper.Map<ViewAuthorDto>(author);
            return Ok(authorDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSingleAuthor([FromBody]AuthorForCreationDto authorForCreationDto)
        {
            if (authorForCreationDto == null) return BadRequest();
            var author = mapper.Map<Author>(authorForCreationDto);

            var addedAuthor = await authorService.CreateAuthorAsync(author);
            var authorToReturn = mapper.Map<ViewAuthorDto>(addedAuthor);

            return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id }, authorToReturn);
        }

        [HttpPost("{id}/books")]
        public async Task<IActionResult> AddAuthorBook(Guid id, [FromBody]BookForCreationDto bookForCreationDto)
        {
            if (bookForCreationDto == null) return BadRequest();
            var author = authorService.GetAuthorByIdAsync(id, trackChanges: false);
            if (author == null) return BadRequest("AuthorId is invalid or null!");

            var book = mapper.Map<Book>(bookForCreationDto);

            var bookAdded = await bookService.CreateBookAsync(book);
            var bookToReturn = mapper.Map<ViewBookDto>(bookAdded);

            return CreatedAtRoute("GetAuthor", new { id = bookToReturn.AuthorId }, bookToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody]AuthorForUpdateDto authorForUpdateDto)
        {
            if (authorForUpdateDto == null) return BadRequest("authorDto is null!");

            var author = await authorService.GetAuthorByIdAsync(id, trackChanges: true);
            if (author == null) return BadRequest("Author dosen't exist!");

            mapper.Map(authorForUpdateDto, author);
            unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await authorService.GetAuthorByIdAsync(id, trackChanges: false);
            authorService.DeleteAuthor(author);
            unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpPut("{authorId}/books/{id}")]
        public async Task<IActionResult> UpdateAuthorBook(Guid authorId, [FromBody]BookForUpdateDto bookForUpdate, Guid id)
        {
            var author = await authorService.GetAuthorByIdAsync(authorId, trackChanges: false);
            if (author == null) return BadRequest("AuthorId is invalid!");

            if (bookForUpdate == null) return BadRequest("Book is null");
            var book = await bookService.GetBookByIdForUpdateAsync(id, trackChanges: true);

            mapper.Map(bookForUpdate, book);
            unitOfWork.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetAuthorBooks(Guid id)
        {
            var books = await bookService.GetBooksByAuthorIdAsync(id);
            if (books == null) return BadRequest("AuthorId is invalid!");

            return Ok(books);
        }
    }
}
