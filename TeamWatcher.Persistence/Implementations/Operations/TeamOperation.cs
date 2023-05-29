using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWatcher.Application.DTO;
using TeamWatcher.Application.Interfaces.Operations;
using TeamWatcher.Application.Interfaces.Repositories;
using TeamWatcher.Application.ViewModels;
using TeamWatcher.Application.Wrappers;
using TeamWatcher.Domain.Entities;

namespace TeamWatcher.Persistence.Implementations.Operations
{
    public class TeamOperation : ITeamOperation
    {
        readonly ITeamRepository _teamRepository;
        public TeamOperation(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public async Task<ServiceResponse<Team>> CreateAsync(TeamDTO teamDTO)
        {
            ServiceResponse<Team> response = new ServiceResponse<Team>();
            var team = new Team();
            team.Name = teamDTO.Name;
            team.LogoUrl = teamDTO.LogoUrl;
            var result = await _teamRepository.CreateAsync(team);
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }

        public async Task<ServiceResponseBase> DeleteAsync(int id)
        {
            ServiceResponseBase response = new ServiceResponseBase();
            await _teamRepository.DeleteAsync(id);
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }

        public async Task<ServiceResponse<List<TeamListViewModel>>> GetAllAsync()
        {
            List<TeamListViewModel> teamListViewModels = new List<TeamListViewModel>();
            ServiceResponse<List<TeamListViewModel>> response = new ServiceResponse<List<TeamListViewModel>>();
            var result = await _teamRepository.GetAllAsync();
            foreach (var item in result)
            {

                teamListViewModels.Add(new TeamListViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    LogoUrl = item.LogoUrl
                }); 
            }
            response.Message = "";
            response.Data = teamListViewModels;
            return response;
        }



        public async Task<ServiceResponse<Team>> GetSingleAsync(int id)
        {
            ServiceResponse<Team> response = new ServiceResponse<Team>();
            var result = await _teamRepository.GetSingleAsync(id);
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }

        public async Task<ServiceResponse<Team>> UpdateAsync(TeamDTO teamDTO)
        {
            ServiceResponse<Team> response = new ServiceResponse<Team>();
            var team = new Team();
            team.Name = teamDTO.Name;
            team.Id = teamDTO.Id;
            team.LogoUrl = teamDTO.LogoUrl;
            var result = await _teamRepository.UpdateAsync(team);
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }
    }
}
