using Microsoft.AspNetCore.Mvc;
using TeamWatcher.Application.DTO;
using TeamWatcher.Application.Interfaces.Operations;

namespace TeamWatcher.Host.Controllers
{
    public class PlayerController : Controller
    {
        IPlayerOperation _playerOperation;
        public PlayerController(IPlayerOperation playerOperation)
        {
            _playerOperation = playerOperation;
        }


        [HttpPost]
        public async Task<JsonResult> CreatePlayer(PlayerDTO player)
        {
            var result = await _playerOperation.CreateAsync(player);
            return Json(result);
        }

        [HttpGet]
        public IActionResult EditPlayer(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> EditPlayer(PlayerDTO player)
        {
            var result = await _playerOperation.UpdateAsync(player);
            return RedirectToAction("Teams", "Home");
        }


        [HttpPost]
        public async Task<JsonResult> DeletePlayer(int id)
        {
            var result  = await _playerOperation.DeleteAsync(id);
            return Json(result);
        }
    }
}
