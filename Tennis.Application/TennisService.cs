using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tennis.Application;
using Tennis.Domain;

namespace Tennis.API
{
    public class TennisService : ITennisService
    {
        private readonly ITennisRepository _tennisRepository;

        public TennisService(ITennisRepository tennisRepository)
        {
            _tennisRepository = tennisRepository;
        }

        public List<Player> GetAllPlayers()
        {
            return _tennisRepository.GetAllPlayers();
        }

        public Player GetPlayerById(int playerId)
        {
            return _tennisRepository.GetPlayerById(playerId);
        }

        public Stats GetStats()
        {
            List<Player> players = _tennisRepository.GetAllPlayers();

            return new Stats()
            {
                CountryCodeWithMaxWinRatio = this.GetCountryWithMaxWinRatio(players),
                IMC = this.GetIMC(players),
                MedianHeight = this.GetMedianHeight(players),
            };
        }

        private string GetCountryWithMaxWinRatio(List<Player> players)
        {
            return players
                .GroupBy(p => p.Country.Code)
                .Select(cc => new
                {
                    CountryCode = cc.Key,
                    WinRatio = (double)cc.Sum(p => p.Data.Last.Count(x => x == 1)) / cc.Sum(p => p.Data.Last.Count)
                })
                .OrderByDescending(g => g.WinRatio)
                .FirstOrDefault()
                .CountryCode;
        }

        private double GetIMC(List<Player> players)
        {
            if (players == null)
            {
                return 0;
            }

            double imc = 0;

            foreach (var player in players)
            {
                double weight = player.Data.Weight / 1000.0; // Convert weight to kg
                double height = player.Data.Height / 100.0; // Convert height to meters

                imc += weight / (height * height);
            }

            return imc / players.Count;
        }

        private double GetMedianHeight(List<Player> players)
        {
            var heights = players.Select(p => p.Data.Height);

            var sortedHeights = heights.OrderBy(h => h);

            int middleIndex = sortedHeights.Count() / 2;

            return sortedHeights.Count() % 2 == 0
                ? (sortedHeights.ElementAt(middleIndex) + sortedHeights.ElementAt(middleIndex - 1)) / 2.0
                : sortedHeights.ElementAt(middleIndex);
        }
    }
}
