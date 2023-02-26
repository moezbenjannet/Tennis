using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Tennis.API;
using Tennis.API.Controllers;
using Tennis.Domain;
using Tennis.Infrastructure;
using Xunit;
using System;

namespace Tennis.Tests
{
    public class TennisControllerTests : BaseTennisTests
    {
        [Fact]
        public void GetAllPlayers_WhenPlayerExist_ReturnOk()
        {
            // Arrange
            _mockTennisService.Setup(s => s.GetAllPlayers()).Returns(new List<Player>() 
            {
                new Player()
                {
                    Id = 90
                }
            });

            // Act
            var result = _tennisController.GetAllPlayers();

            // Assert
            Assert.IsType<ActionResult<List<Player>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var players = Assert.IsType<List<Player>>(okResult.Value);

            Assert.Single(players);
            Assert.Equal(90, players.Single().Id);
        }

        [Fact]
        public void GetAllPlayers_WhenPlayerDoesNotExist_ReturnNoContent()
        {
            // Arrange
            _mockTennisService.Setup(s => s.GetAllPlayers()).Returns(new List<Player>() { });

            // Act
            var result = _tennisController.GetAllPlayers();

            // Assert
            Assert.IsType<ActionResult<List<Player>>>(result);
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public void GetAllPlayers_WhenExceptionExist_ReturnBadRequest()
        {
            // Arrange
            string exceptionMessage = "An error occurred.";
            _mockTennisService.Setup(s => s.GetAllPlayers()).Throws(new Exception(exceptionMessage));

            // Act
            var result = _tennisController.GetAllPlayers();

            // Assert
            Assert.IsType<ActionResult<List<Player>>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
        }

        [Fact]
        public void GetPlayerById_WhenPlayerExist_ReturnOk()
        {
            // Arrange
            int playerId = 90;
            _mockTennisService.Setup(s => s.GetPlayerById(playerId)).Returns(new Player()
            {
                Id = playerId
            });

            // Act
            var result = _tennisController.GetPlayerById(playerId);

            // Assert
            Assert.IsType<ActionResult<Player>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var player = Assert.IsType<Player>(okResult.Value);

            Assert.NotNull(player);
            Assert.Equal(playerId, player.Id);
        }

        [Fact]
        public void GetPlayerById_WhenPlayerDoesNotExist_ReturnNoContent()
        {
            // Arrange
            int playerId = 90;
            Player player = null;
            _mockTennisService.Setup(s => s.GetPlayerById(playerId)).Returns(player);

            // Act
            var result = _tennisController.GetPlayerById(playerId);

            // Assert
            Assert.IsType<ActionResult<Player>>(result);
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public void GetPlayerById_WhenExceptionExist_ReturnBadRequest()
        {
            // Arrange
            string exceptionMessage = "An error occurred.";
            int playerId = 90;
            _mockTennisService.Setup(s => s.GetPlayerById(playerId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _tennisController.GetPlayerById(playerId);

            // Assert
            Assert.IsType<ActionResult<Player>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
        }

        [Fact]
        public void GetStats_WhenStatsExist_ReturnOk()
        {
            // Arrange
            Stats stats = new Stats()
            {
                CountryCodeWithMaxWinRatio = "FR",
                IMC = 25.25,
                MedianHeight = 180,
            };
            _mockTennisService.Setup(s => s.GetStats()).Returns(stats);

            // Act
            var result = _tennisController.GetStats();

            // Assert
            Assert.IsType<ActionResult<Stats>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var statsResult = Assert.IsType<Stats>(okResult.Value);

            Assert.NotNull(statsResult);
            Assert.Equal(stats, statsResult);
        }

        [Fact]
        public void GetStats_WhenStatsExist_ReturnNoContent()
        {
            // Arrange
            Stats stats = null;
            _mockTennisService.Setup(s => s.GetStats()).Returns(stats);

            // Act
            var result = _tennisController.GetStats();

            // Assert
            Assert.IsType<ActionResult<Stats>>(result);
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public void GetStats_WhenExceptionExist_ReturnBadRequest()
        {
            // Arrange
            string exceptionMessage = "An error occurred.";
            _mockTennisService.Setup(s => s.GetStats()).Throws(new Exception(exceptionMessage));

            // Act
            var result = _tennisController.GetStats();

            // Assert
            Assert.IsType<ActionResult<Stats>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
        }
    }
}
