using AutoMapper;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IBookService bookService;
        private readonly IMapper mapper;

        public AuthorsController(IAuthorService authorService, IBookService bookService, IMapper mapper)
        {
            this.authorService = authorService;
            this.bookService = bookService;
            this.mapper = mapper;
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
            var author = await authorService.GetAuthorByIdAsync(id);

            return Ok(author);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AddSingleAuthor([FromBody]AuthorForCreationDto authorForCreationDto)
        {
            if (authorForCreationDto == null) return BadRequest();

            var authorToReturn = await authorService.CreateAuthorAsync(authorForCreationDto);

            if (authorToReturn is null) return BadRequest("Invalid User Id!");

            return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id },  authorToReturn);
        }

        [HttpPost("{id}/books")]
        [Authorize(Policy = "RequireAuthorOrAdminRole")]
        public async Task<IActionResult> AddAuthorBook(Guid id, [FromBody]BookForCreationDto bookForCreationDto)
        {
            if (bookForCreationDto == null) return BadRequest();

            var author = authorService.GetAuthorByIdAsync(id);
            if (author == null) return BadRequest("AuthorId is invalid or null!");

            var book = await bookService.CreateBookAsync(bookForCreationDto);

            return CreatedAtRoute("GetAuthor", new { id = book.AuthorId }, book);
        }


       /* [HttpPut("{id}")]
        [Authorize(Policy = "RequireAuthorOrAdminRole")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody]AuthorForUpdateDto authorForUpdateDto)
        {
            if (authorForUpdateDto == null) return BadRequest("authorDto is null!");

            var author = await authorService.GetAuthorByIdAsync(id);
            if (author == null) return BadRequest("Author dosen't exist!");

            authorService.UpdateAuthor(id, authorForUpdateDto);

            return NoContent();
        }*/

        [HttpPatch("{id}")]
        [Authorize(Policy = "RequireAuthorOrAdminRole")]
        public async Task<IActionResult> UpdateAuthorL(Guid id, [FromBody]JsonPatchDocument<AuthorForUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Author details is null");

            var author = await authorService.GetAuthorForUpdateAsync(id, trackChanges: true);
            if (author == null) return BadRequest("Author is null or invalid!");

            var authorToUpdate = mapper.Map<AuthorForUpdateDto>(author);

            patchDoc.ApplyTo(authorToUpdate);
            
            mapper.Map(authorToUpdate, author);

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
            if (bookForUpdate == null) return BadRequest("Book is null");

            var author = await authorService.GetAuthorByIdAsync(authorId);
            if (author == null) return BadRequest("AuthorId is invalid!");

            bookService.UpdateBook(id, bookForUpdate);

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
