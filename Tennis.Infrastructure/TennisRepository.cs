using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Tennis.Application;
using Tennis.Domain;

namespace Tennis.Infrastructure
{
    public class TennisRepository : ITennisRepository
    {
        public List<Player> GetAllPlayers()
        {
            return this.ReadData();
        }

        public Player GetPlayerById(int playerId)
        {
            List<Player> players = this.ReadData();

            return players?.FirstOrDefault(player => player.Id.Equals(playerId));
        }

        private List<Player> ReadData()
        {
            try
            {
                string jsonFilePath = this.GetDataJsonFilePath();
                string jsonString = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<List<Player>>(jsonString);
            }
            catch (Exception e)
            {
                return new List<Player>();
            }
        }

        private string GetDataJsonFilePath()
        {
            string parentPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            return Path.Combine(parentPath, "Tennis.Infrastructure", "players.json");
        }
    }
}
