using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Tennis.API;
using Tennis.API.Controllers;
using Tennis.Infrastructure;

namespace Tennis.Tests
{
    public class BaseTennisTests
    {
        protected Mock<ITennisService> _mockTennisService;
        protected TennisController _tennisController;

        public BaseTennisTests()
        {
            _mockTennisService = new Mock<ITennisService>();
            _tennisController = new TennisController(_mockTennisService.Object);
        }
    }
}
