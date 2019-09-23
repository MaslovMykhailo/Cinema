using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cinema.BusinessLogic.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilmService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Film> AddAsync(Film film)
        {
            await _unitOfWork.FilmRepository.AddAsync(film);
            await _unitOfWork.CommitAsync();

            return film;
        }

        public async Task<List<Film>> Find(Expression<Func<Film, bool>> expression)
        {
            return await _unitOfWork.FilmRepository.Find(expression);
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _unitOfWork.FilmRepository.GetAsync();
        }

        public async Task<Film> GetAsync(Guid id)
        {
            return await _unitOfWork.FilmRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            await _unitOfWork.FilmRepository.RemoveAsync(id);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<Film> UpdateAsync(Guid id, Film film)
        {
            var updatedFilm = await _unitOfWork.FilmRepository.UpdateAsync(id, film);
            await _unitOfWork.CommitAsync();

            return updatedFilm;
        }
    }
}
