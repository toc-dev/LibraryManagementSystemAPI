using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ViewCategoryDto> CreateCategoryAsync(Category category);

        Task<IEnumerable<ViewCategoryDto>> GetCategoriesAsync();

        Task<ViewCategoryDto> GetCategoryAsync(Guid id);

        void UpdateCategory(Category category);

        Task DeleteCategory(Guid id);
    }
}
