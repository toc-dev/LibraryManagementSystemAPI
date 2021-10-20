using LibraryApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Book>> GetBooksAsync();

        Task<Book> GetBookByIdAsync(Guid id);

        Task<IEnumerable<Book>> GetBooksByCategoryAsync(string category);

        Task<Book> CreateBookAsync(Book book);

        void UpdateBook(Book book);

        void DeleteBook(Book book);

        Task<Author> CreateAuthorAsync(Author author);

        void UpdateAuthorAsync(Author author);

        void DeleteAuthor(Author author);

        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category> GetCategoryAsync(Guid id);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);

        Task<IEnumerable<Activity>> GetActivitiesAsync();

        Task<IEnumerable<Activity>> GetUserActivitiesAsync(Guid userId);

    }
}
