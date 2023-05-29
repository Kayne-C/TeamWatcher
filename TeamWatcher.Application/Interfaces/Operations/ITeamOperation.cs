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
    public interface ITeamOperation
    {
        Task<ServiceResponse<Team>> CreateAsync(TeamDTO entity);
        Task<ServiceResponseBase> DeleteAsync(int id);
        Task<ServiceResponse<List<TeamListViewModel>>> GetAllAsync();
        Task<ServiceResponse<Team>> GetSingleAsync(int id);
        Task<ServiceResponse<Team>> UpdateAsync(TeamDTO entity);
    }
}
