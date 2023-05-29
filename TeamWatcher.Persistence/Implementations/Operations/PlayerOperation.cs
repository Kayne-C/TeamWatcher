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
    public class PlayerOperation : IPlayerOperation
    {
        IPlayerRepository _playerRepository;
        public PlayerOperation(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<ServiceResponse<Player>> CreateAsync(PlayerDTO playerDTO)
        {

            var dbEntity = new Player();
            dbEntity.Name=playerDTO.Name;
            dbEntity.Surname=playerDTO.Surname;
            dbEntity.Age=playerDTO.Age;
            dbEntity.TeamId=playerDTO.TeamId;
            dbEntity.IsDeleted = false;
            dbEntity.CreatedDate=DateTime.Now;
            ServiceResponse<Player> response = new ServiceResponse<Player>();
            var result = await _playerRepository.CreateAsync(dbEntity);
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }

        public async Task<ServiceResponseBase> DeleteAsync(int id)
        {
            ServiceResponseBase response = new ServiceResponseBase();
            await _playerRepository.DeleteAsync(id);
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }

        public async Task<ServiceResponse<List<PlayerListViewModel>>> GetAllAsync(int teamId)
        {
            List<PlayerListViewModel> playerListViewModels = new List<PlayerListViewModel>();

            ServiceResponse<List<PlayerListViewModel>> response = new ServiceResponse<List<PlayerListViewModel>>();
            var result = await _playerRepository.GetAllAsync(teamId);
            foreach (var item in result)
            {
                playerListViewModels.Add(new PlayerListViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Age = item.Age,
                    TeamId = item.TeamId
                });
            }
            response.Message = "";
            response.Data= playerListViewModels;
            return response;
        }

        public async Task<ServiceResponse<Player>> GetSingleAsync(int id)
        {
            ServiceResponse<Player> response = new ServiceResponse<Player>();
            var result = await _playerRepository.GetSingleAsync(id);
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }

        public async Task<ServiceResponse<Player>> UpdateAsync(PlayerDTO playerDTO)
        {
            var dbEntity = new Player();
            dbEntity.Id = playerDTO.Id;
            dbEntity.Name = playerDTO.Name;
            dbEntity.Surname = playerDTO.Surname;
            dbEntity.Age = playerDTO.Age;
            dbEntity.IsDeleted = false;
            ServiceResponse<Player> response = new ServiceResponse<Player>();
            var result = await _playerRepository.UpdateAsync(dbEntity);
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "";
            return response;
        }
    }
}
