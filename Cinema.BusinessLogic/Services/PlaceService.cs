using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.BusinessLogic.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Place> AddAsync(Place place)
        {
            await _unitOfWork.PlaceRepository.AddAsync(place);
            await _unitOfWork.CommitAsync();

            return place;
        }

        public async Task<IEnumerable<Place>> GetAllAsync()
        {
            return await _unitOfWork.PlaceRepository.GetAsync();
        }

        public async Task<Place> GetAsync(Guid id)
        {
            return await _unitOfWork.PlaceRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            await _unitOfWork.PlaceRepository.RemoveAsync(id);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<Place> UpdateAsync(Guid id, Place place)
        {
            var updatedPlace = await _unitOfWork.PlaceRepository.UpdateAsync(id, place);
            await _unitOfWork.CommitAsync();

            return updatedPlace;
        }
    }
}
