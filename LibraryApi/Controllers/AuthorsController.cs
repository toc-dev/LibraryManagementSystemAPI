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
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await authorService.GetAuthorsAsync();

            return Ok(authors);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetSingleAuthor(Guid id)
        {
            var author = await authorService.GetAuthorByIdAsync(id, trackChanges: false);

            return Ok(author);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AddSingleAuthor([FromBody]AuthorForCreationDto authorForCreationDto)
        {
            if (authorForCreationDto == null) return BadRequest();

            var authorToReturn = await authorService.CreateAuthorAsync(authorForCreationDto);

            return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id },  authorToReturn);
        }

        [HttpPost("{id}/books")]
        [Authorize(Policy = "RequireAuthorOrAdminRole")]
        public async Task<IActionResult> AddAuthorBook(Guid id, [FromBody]BookForCreationDto bookForCreationDto)
        {
            if (bookForCreationDto == null) return BadRequest();

            var author = authorService.GetAuthorByIdAsync(id, trackChanges: false);
            if (author == null) return BadRequest("AuthorId is invalid or null!");

            var book = await bookService.CreateBookAsync(bookForCreationDto);

            return CreatedAtRoute("GetAuthor", new { id = book.AuthorId }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAuthorOrAdminRole")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody]AuthorForUpdateDto authorForUpdateDto)
        {
            if (authorForUpdateDto == null) return BadRequest("authorDto is null!");

            var author = await authorService.GetAuthorByIdAsync(id, trackChanges: true);
            if (author == null) return BadRequest("Author dosen't exist!");

            authorService.UpdateAuthor(id, authorForUpdateDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult DeleteAuthor(Guid id)
        {
            authorService.DeleteAuthor(id);

            return NoContent();
        }

        [HttpPut("{authorId}/books/{id}")]
        [Authorize(Policy = "RequireAuthorOrAdminRole")]
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
        [Authorize(Policy = "RequireAuthorOrAdminRole")]
        public async Task<IActionResult> GetAuthorBooks(Guid id)
        {
            var books = await bookService.GetBooksByAuthorIdAsync(id);
            
            if (books == null) return BadRequest("AuthorId is invalid!");

            return Ok(books);
        }
    }
}
