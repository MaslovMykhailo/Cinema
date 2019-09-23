using Cinema.BusinessLogic.Interfaces;
using Cinema.Persisted.Entities;
using Cinema.Persisted.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.BusinessLogic.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await _unitOfWork.TicketRepository.AddAsync(ticket);
            await _unitOfWork.CommitAsync();

            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _unitOfWork.TicketRepository.GetAsync();
        }

        public async Task<Ticket> GetAsync(Guid id)
        {
            return await _unitOfWork.TicketRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            await _unitOfWork.TicketRepository.RemoveAsync(id);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<Ticket> UpdateAsync(Guid id, Ticket ticket)
        {
            var updatedTicket = await _unitOfWork.TicketRepository.UpdateAsync(id, ticket);
            await _unitOfWork.CommitAsync();

            return updatedTicket;
        }
    }
}
