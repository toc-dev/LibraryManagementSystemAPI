using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> CreateAuthorAsync(AuthorForCreationDto author);
        Task<ViewAuthorDto> GetAuthorByIdAsync(Guid authorId);
        Task<Author> GetAuthorForUpdateAsync(Guid id, bool trackChanges);
        Task<IEnumerable<ViewAuthorDto>> GetAuthorsAsync();
        void UpdateAuthor(Guid id, AuthorForUpdateDto author);
        void DeleteAuthor(Guid id);
    }
}
