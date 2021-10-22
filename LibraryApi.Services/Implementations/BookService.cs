using AutoMapper;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
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
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookRepo = unitOfWork.GetRepository<Book>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<ViewBookDto>> GetBooksAsync()
        {
            var books = await _bookRepo.GetAllAsync();

            return _mapper.Map<IEnumerable<ViewBookDto>>(books);
        }

        public async Task<ViewBookDto> GetBookByIdAsync(Guid id, bool trackChanges)
        {
            var book = await _bookRepo.GetByIdAsync(id);

            if (book is null) return null;

            return _mapper.Map<ViewBookDto>(book);
        }

        public async Task<IEnumerable<ViewBookDto>> GetBooksByCategoryAsync(string category)
        {
            var books = await Task.FromResult(_bookRepo.GetByCondition(b => b.Category.Contains(category)));

            if (books is null) return null;

            return _mapper.Map<IEnumerable<ViewBookDto>>(books);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            return await _bookRepo.AddAsync(book);
        }

        public async Task<IEnumerable<ViewBookDto>> GetBooksByAuthorIdAsync(Guid id)
        {
            var books = await Task.FromResult(_bookRepo.GetByCondition(b => b.AuthorId == id));

            if (books is null) return null;

            return _mapper.Map<IEnumerable<ViewBookDto>>(books);
        }

        public void DeleteBook(Book book)
        {
            book.IsDeleted = true;
            _bookRepo.Update(book);
            _unitOfWork.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _bookRepo.Update(book);
            _unitOfWork.SaveChanges();
        }
    }
}
