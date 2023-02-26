using System;
using System.Collections.Generic;
using Tennis.Domain;

namespace Tennis.Application
{
    public interface ITennisRepository
    {
        public List<Player> GetAllPlayers();

        public Player GetPlayerById(int playerId);
    }
}
