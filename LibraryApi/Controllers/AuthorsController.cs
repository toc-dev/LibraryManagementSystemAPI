using AutoMapper;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;
        private readonly IBookService bookService;

        public AuthorsController(IAuthorService authorService, IMapper mapper, IBookService bookService)
        {
            this.authorService = authorService;
            this.mapper = mapper;
            this.bookService = bookService;
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
            var author = await authorService.GetAuthorByIdAsync(id);

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

        [HttpPost("{id}/book")]
        public async Task<IActionResult> AddAuthorBook([FromBody]BookForCreationDto bookForCreationDto)
        {
            if (bookForCreationDto == null) return BadRequest();
            var book = mapper.Map<Book>(bookForCreationDto);

            var bookAdded = await bookService.CreateBookAsync(book);
            var bookToReturn = mapper.Map<ViewBookDto>(bookAdded);

            return CreatedAtRoute("GetAuthor", new { id = bookToReturn.AuthorId }, bookToReturn);

        }
    }
}
