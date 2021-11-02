using AutoMapper;
using LibraryApi.Data.Interfaces;
using LibraryApi.Models.Dtos;
using LibraryApi.Models.Entities;
using LibraryApi.Models.Enumerators;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger,
            IServiceFactory serviceFactory, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _authorRepo = unitOfWork.GetRepository<Author>();
            _serviceFactory = serviceFactory;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Author> CreateAuthorAsync(AuthorForCreationDto author)
        {
            var userService = _serviceFactory.GetService<IUserService>();

            var user = await userService.FindUserAsync(author.UserId);

            if (user is null) return null;

            var authorToAdd = new Author
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            var authorAdded = await _authorRepo.AddAsync(authorToAdd);
            await _userManager.AddToRoleAsync(user, AppRole.Author.ToString());

            return authorAdded;
        }

        public async void DeleteAuthor(Guid id)
        {
            var author = await _authorRepo.GetByIdAsync(id);

            if (author == null)
                _logger.LogError("Author is null or invalid");

            author.IsDeleted = true;

            _authorRepo.Update(author);

            _unitOfWork.SaveChanges();
        }

        public async Task<ViewAuthorDto> GetAuthorByIdAsync(Guid authorId)
        {
            var author = await _authorRepo.GetByIdAsync(authorId);

            if (author == null)
                _logger.LogError("Author is null or invalid");

            var authorToReturn = _mapper.Map<ViewAuthorDto>(author);

            return authorToReturn;
        }

        public async Task<Author> GetAuthorForUpdateAsync(Guid id, bool trackChanges)
        {
            return await _authorRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ViewAuthorDto>> GetAuthorsAsync()
        {
            var authors = await _authorRepo.GetAllAsync();

            if (authors == null)
                _logger.LogError("Authors are null or invalid");

            var authorsToReturn = _mapper.Map<IEnumerable<ViewAuthorDto>>(authors);

            return authorsToReturn;
        }

        public async void UpdateAuthor(Guid id, AuthorForUpdateDto author)
        {
            var authorToReturn = await GetAuthorForUpdateAsync(id, trackChanges: true);

            if (authorToReturn == null)
                _logger.LogError("Author is null or invalid");

            _mapper.Map(author, authorToReturn);

            _unitOfWork.SaveChanges();
        }
    }
}
