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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepo;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepo = unitOfWork.GetRepository<Category>();
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _categoryRepo.AddAsync(category);
        }

        public void DeleteCategory(Category category)
        {
            category.IsDeleted = true;
            _categoryRepo.Update(category);
            _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepo.GetAllAsync();
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            return await _categoryRepo.GetByIdAsync(id);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepo.Update(category);
            _unitOfWork.SaveChanges();
        }
    }
}
