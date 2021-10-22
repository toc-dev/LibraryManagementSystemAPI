using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBookAsync(Book book);
        Task<ViewBookDto> GetBookByIdAsync(Guid id);
        Task<IEnumerable<ViewBookDto>> GetBooksAsync();
        Task<IEnumerable<ViewBookDto>> GetBooksByCategoryAsync(string category);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}
