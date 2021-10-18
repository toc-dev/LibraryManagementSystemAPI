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
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Book> _bookRepo;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookRepo = unitOfWork.GetRepository<Book>();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookRepo.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _bookRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(string category)
        {
            return await Task.FromResult(_bookRepo.GetByCondition(b => b.Categories.Contains(category)));
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            return await _bookRepo.AddAsync(book);
        }

        public void DeleteBook(Book book)
        {
            _bookRepo.Delete(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepo.Update(book);
        }
    }
}
