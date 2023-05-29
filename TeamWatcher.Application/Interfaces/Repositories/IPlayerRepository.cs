using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWatcher.Domain.Entities;

namespace TeamWatcher.Application.Interfaces.Repositories
{
    public interface IPlayerRepository:IRepository<Player>
    {
        Task<List<Player>> GetAllAsync(int teamId);
    }
}
