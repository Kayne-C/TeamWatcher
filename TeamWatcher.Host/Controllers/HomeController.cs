using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamWatcher.Application.DTO;
using TeamWatcher.Application.Interfaces.Operations;
using TeamWatcher.Host.Models;

namespace TeamWatcher.Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITeamOperation _teamOperation;
        private readonly IPlayerOperation _playerOperation;

        public HomeController(ILogger<HomeController> logger,ITeamOperation teamOperation,IPlayerOperation playerOperation)
        {
            _playerOperation = playerOperation;
            _teamOperation = teamOperation;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            var teamList = await _teamOperation.GetAllAsync();
            return View(teamList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Overview(int teamId)
        {
            var playerList = await _playerOperation.GetAllAsync(teamId);
            var team = await _teamOperation.GetSingleAsync(teamId);
            ViewBag.TeamName = team.Data.Name;
            ViewBag.TeamId = team.Data.Id;
            return View(playerList.Data);
        }

        [HttpPost]
        public async Task<JsonResult> AddTeam(TeamDTO entity)
        {

            var team = await _teamOperation.CreateAsync(entity);

            return Json(team);
        }

        [HttpGet]
        public IActionResult EditTeam(int Id)
        {
           ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditTeam(TeamDTO team)
        {
            var updatedTeam = await _teamOperation.UpdateAsync(team);
            return RedirectToAction("Teams", "Home");    
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTeam(int id)
        {

            var result = await _teamOperation.DeleteAsync(id);
            return Json(result);
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}