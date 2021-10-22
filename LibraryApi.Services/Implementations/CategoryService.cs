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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryRepo = unitOfWork.GetRepository<Category>();
            _mapper = mapper;
        }
        public async Task<ViewCategoryDto> CreateCategoryAsync(Category category)
        {
            var newCategory = await _categoryRepo.AddAsync(category);
            return _mapper.Map<ViewCategoryDto>(newCategory);
        }
                   
        public async Task DeleteCategory(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            category.UpdatedAt = DateTime.Now;
            category.UpdatedBy = "Admin";
            category.IsDeleted = true;
            _categoryRepo.Update(category);
            _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<ViewCategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewCategoryDto>>(categories);
        }

        public async Task<ViewCategoryDto> GetCategoryAsync(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return _mapper.Map<ViewCategoryDto>(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepo.Update(category);
            _unitOfWork.SaveChanges();
        }
    }
}
