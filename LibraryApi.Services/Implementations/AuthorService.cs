using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Entities;
using LibraryApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Author> _authorRepo;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _authorRepo = unitOfWork.GetRepository<Author>();
        }
        public async Task<Author> CreateAuthor(Author author)
        {
            await _authorRepo.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();

            return author;
        }

        public void DeleteAuthor(Author author)
        {
            _authorRepo.Delete(author);
        }

        public async Task<Author> GetAuthorByIdAsync(Guid authorId)
        {
            return await _authorRepo.GetByIdAsync(authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _authorRepo.GetAllAsync();
        }

        public void UpdateAuthor(Author author)
        {
            _authorRepo.Update(author);
            _unitOfWork.SaveChanges();
        }
    }
}
