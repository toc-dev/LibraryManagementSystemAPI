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
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Author> _authorRepo;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authorRepo = unitOfWork.GetRepository<Author>();
            _mapper = mapper;
        }
        public async Task<Author> CreateAuthorAsync(AuthorForCreationDto author)
        {
            var authorToAdd = _mapper.Map<Author>(author);

            var authorAdded = await _authorRepo.AddAsync(authorToAdd);
   
            return authorAdded;
        }

        public async void DeleteAuthor(Guid id)
        {
            var author = await _authorRepo.GetByIdAsync(id);

            author.IsDeleted = true;

            _authorRepo.Update(author);

            _unitOfWork.SaveChanges();
        }

        public async Task<ViewAuthorDto> GetAuthorByIdAsync(Guid authorId, bool trackChanges)
        {
            var author = await _authorRepo.GetByIdAsync(authorId);

            var authorToReturn = _mapper.Map<ViewAuthorDto>(author);

            return authorToReturn;
        }

        public async Task<IEnumerable<ViewAuthorDto>> GetAuthorsAsync()
        {
            var authors = await _authorRepo.GetAllAsync();

            var authorsToReturn = _mapper.Map<IEnumerable<ViewAuthorDto>>(authors);

            return authorsToReturn;
        }

        public async void UpdateAuthor(Guid id, AuthorForUpdateDto author)
        {
            var authorToReturn = await GetAuthorByIdAsync(id, trackChanges: true);

            _mapper.Map(author, authorToReturn);

            _unitOfWork.SaveChanges();
        }
    }
}
