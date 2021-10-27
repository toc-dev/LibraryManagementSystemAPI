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
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;
            _bookRepo = unitOfWork.GetRepository<Book>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<ViewBookDto>> GetBooksAsync()
        {
            var books = await _bookRepo.GetAllAsync();

            return _mapper.Map<IEnumerable<ViewBookDto>>(books);
        }

        public async Task<ViewBookDto> GetBookByIdAsync(Guid id)
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

        public async Task<ViewActivityDto> RequestBook(Guid userId, Guid bookId)
        {
            var isValidBookId = await _bookRepo.AnyAsync(b => b.Id == bookId);
            var book = _bookRepo.GetById(bookId);

            if (!isValidBookId)
                return null;
            if (book is null)
                return null;

            var activity = new Activity
            {
                BookId = bookId,
                UserId = userId,
                Book = new Book
                {
                    Title = book.Title,
                    Author = book.Author
                }
            };

            IActivityService activityService = _serviceFactory.GetService<IActivityService>();
            return await activityService.CreateActivity(activity);
        }

        public async Task<Book> GetBookByIdForUpdateAsync(Guid id, bool trackChanges)
        {
            return await _bookRepo.GetByIdAsync(id);
        }
    }
}
