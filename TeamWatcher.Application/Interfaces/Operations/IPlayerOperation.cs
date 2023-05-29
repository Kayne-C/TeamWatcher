using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWatcher.Application.DTO;
using TeamWatcher.Application.ViewModels;
using TeamWatcher.Application.Wrappers;
using TeamWatcher.Domain.Entities;

namespace TeamWatcher.Application.Interfaces.Operations
{
    public interface IPlayerOperation
    {
        Task<ServiceResponse<Player>> CreateAsync(PlayerDTO entity);
        Task<ServiceResponseBase> DeleteAsync(int id);
        Task<ServiceResponse<List<PlayerListViewModel>>> GetAllAsync(int teamId);
        Task<ServiceResponse<Player>> GetSingleAsync(int id);
        Task<ServiceResponse<Player>> UpdateAsync(PlayerDTO entity);
    }
}
