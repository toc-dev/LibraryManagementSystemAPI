/*using LibraryApi.Data.Interfaces;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        private readonly IActivityService _activityService;

        public UserService(IUnitOfWork unitOfWork, IServiceFactory serviceFactory)
        {
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;
            _bookService = serviceFactory.GetService<BookService>();
            _authorService = serviceFactory.GetService<AuthorService>();
            _categoryService = serviceFactory.GetService<CategoryService>();
            _activityService = serviceFactory.GetService<ActivityService>();
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            return await _authorService.CreateAuthorAsync(author);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _categoryService.CreateCategoryAsync(category);
        }

        public void DeleteAuthor(Author author)
        {
            _authorService.DeleteAuthor(author);
        }


        public void DeleteCategory(Category category)
        {
            _categoryService.DeleteCategory(category);
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return await _activityService.GetActivities();
        }

        public async Task<ViewBookDto> GetBookByIdAsync(Guid id)
        {
            return await _bookService.GetBookByIdAsync(id);
        }

        public async Task<IEnumerable<ViewBookDto>> GetBooksAsync()
        {
            return await _bookService.GetBooksAsync();
        }

        public async Task<IEnumerable<ViewBookDto>> GetBooksByCategoryAsync(string category)
        {
            return await _bookService.GetBooksByCategoryAsync(category);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryService.GetCategoriesAsync();
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            return await _categoryService.GetCategoryAsync(id);
        }

        public async Task<IEnumerable<Activity>> GetUserActivitiesAsync(Guid userId)
        {
            return await _activityService.GetUserActivities(userId);
        }

        public void UpdateAuthorAsync(Author author)
        {
            _authorService.UpdateAuthor(author);
        }

        public void UpdateCategory(Category category)
        {
            _categoryService.UpdateCategory(category);
        }
    }
}
*/