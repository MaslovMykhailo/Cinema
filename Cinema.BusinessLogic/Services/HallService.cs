using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cinema.BusinessLogic.Services
{
    public class HallService : IHallService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HallService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Hall> AddAsync(Hall hall)
        {
            await _unitOfWork.HallRepository.AddAsync(hall);
            await _unitOfWork.CommitAsync();

            return hall;
        }

        public async Task<List<Hall>> Find(Expression<Func<Hall, bool>> expression)
        {
            return await _unitOfWork.HallRepository.Find(expression);
        }

        public async Task<IEnumerable<Hall>> GetAllAsync()
        {
            return await _unitOfWork.HallRepository.GetAsync();
        }

        public async Task<Hall> GetAsync(Guid id)
        {
            return await _unitOfWork.HallRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            await _unitOfWork.HallRepository.RemoveAsync(id);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<Hall> UpdateAsync(Guid id, Hall hall)
        {
            var updatedHall = await _unitOfWork.HallRepository.UpdateAsync(id, hall);
            await _unitOfWork.CommitAsync();

            return updatedHall;
        }
    }
}
