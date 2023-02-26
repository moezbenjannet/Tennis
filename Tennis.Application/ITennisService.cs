using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tennis.Domain;

namespace Tennis.API
{
    public interface ITennisService
    {
        public List<Player> GetAllPlayers();

        public Player GetPlayerById(int playerId);

        public Stats GetStats();
    }
}
