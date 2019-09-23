using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cinema.BusinessLogic.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Session> AddAsync(Session session)
        {
            await _unitOfWork.SessionRepository.AddAsync(session);
            await _unitOfWork.CommitAsync();

            return session;
        }

        public async Task<List<Session>> Find(Expression<Func<Session, bool>> expression)
        {
            return await _unitOfWork.SessionRepository.Find(expression);
        }

        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await _unitOfWork.SessionRepository.GetAsync();
        }

        public async Task<Session> GetAsync(Guid id)
        {
            return await _unitOfWork.SessionRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            await _unitOfWork.SessionRepository.RemoveAsync(id);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<Session> UpdateAsync(Guid id, Session session)
        {
            var updatedPlace = await _unitOfWork.SessionRepository.UpdateAsync(id, session);
            await _unitOfWork.CommitAsync();

            return updatedPlace;
        }
    }
}
