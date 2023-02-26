using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tennis.API;
using Tennis.Domain;

namespace Tennis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TennisController : ControllerBase
    {
        private readonly ITennisService _tennisService;

        public TennisController(ITennisService tennisService)
        {
            _tennisService = tennisService;
        }

        // /api/Tennis/GetAllPlayers
        [HttpGet("GetAllPlayers")]
        public ActionResult<List<Player>> GetAllPlayers()
        {
            try
            {
                var players = _tennisService.GetAllPlayers();
                return players != null && players.Any() ? this.Ok(players) : this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        // /api/Tennis/GetPlayerById?playerId
        [HttpGet("GetPlayerById")]
        public ActionResult<Player> GetPlayerById(int playerId)
        {
            try
            {
                var player = _tennisService.GetPlayerById(playerId);
                return player != null ? this.Ok(player) : this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        // /api/Tennis/GetStats
        [HttpGet("GetStats")]
        public ActionResult<Stats> GetStats()
        {
            try
            {
                var stats = _tennisService.GetStats();
                return stats != null ? this.Ok(stats) : this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
