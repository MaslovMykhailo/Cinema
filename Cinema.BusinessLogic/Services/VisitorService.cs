using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.BusinessLogic.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VisitorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Visitor> AddAsync(Visitor visitor)
        {
            await _unitOfWork.VisitorRepository.AddAsync(visitor);
            await _unitOfWork.CommitAsync();

            return visitor;
        }

        public async Task<IEnumerable<Visitor>> GetAllAsync()
        {
            return await _unitOfWork.VisitorRepository.GetAsync();
        }

        public async Task<Visitor> GetAsync(Guid id)
        {
            return await _unitOfWork.VisitorRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            await _unitOfWork.VisitorRepository.RemoveAsync(id);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<Visitor> UpdateAsync(Guid id, Visitor visitor)
        {
            var updatedVisitor = await _unitOfWork.VisitorRepository.UpdateAsync(id, visitor);
            await _unitOfWork.CommitAsync();

            return updatedVisitor;
        }
    }
}
