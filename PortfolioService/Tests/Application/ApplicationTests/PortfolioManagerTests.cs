using Application;
using Application.Portfolio;
using Application.Portfolio.Dtos;
using Application.Portfolio.Requests;
using Application.User.Dtos;
using Domain.Entities;
using Domain.Portfolio.Ports;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
    public class PortfolioManagerTests
    {
        PortfolioManager portfolioManager;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task HappyPath()
        {
            var userRepository = new Mock<IUserRepository>();

            var userDto = new UserDto
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "password"
            };

            var userId = 1;
            var user = new User
            {
                Id = userId,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };

            userRepository.Setup(x => x.Get(userId)).ReturnsAsync(user);

            var portfolioDto = new PortfolioDto
            {
                UserId = userId,
                Name = "Test Name",
                Description = "Test Description",
            };

            int expectedId = 444;

            var request = new CreatePortfolioRequest()
            {
                Data = portfolioDto,
            };

            var fakeRepo = new Mock<IPortfolioRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Portfolio>())).Returns(Task.FromResult(expectedId));

            portfolioManager = new PortfolioManager(fakeRepo.Object, userRepository.Object);//??

            var res = await portfolioManager.CreatePortfolio(request);

            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);

            Assert.AreEqual(res.Data.UserId, portfolioDto.UserId);
            Assert.AreEqual(res.Data.Name, portfolioDto.Name);
            Assert.AreEqual(res.Data.Description, portfolioDto.Description);
        }

        [TestCase("", "")]
        [TestCase(null, null)]
        public async Task Should_Return_MissingRequiredInformation(string Name, string Description)
        {
            var userRepository = new Mock<IUserRepository>();

            var userDto = new UserDto
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "password"
            };

            var userId = 1;
            var user = new User
            {
                Id = userId,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };

            userRepository.Setup(x => x.Get(userId)).ReturnsAsync(user);

            var portfolioDto = new PortfolioDto
            {
                UserId = userId,
                Name = Name,
                Description = Description
            };

            var request = new CreatePortfolioRequest()
            {
                Data = portfolioDto,
            };

            var fakeRepo = new Mock<IPortfolioRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Portfolio>())).Returns(Task.FromResult(444));

            portfolioManager = new PortfolioManager(fakeRepo.Object, userRepository.Object);

            var res = await portfolioManager.CreatePortfolio(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.PORTFOLIO_MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");
        }

        [Test]
        public async Task Should_Return_UserNotFound()
        {
            var userRepository = new Mock<IUserRepository>();

            var portfolioDto = new PortfolioDto
            {
                UserId = 999, // Assuming this is an invalid UserId
                Name = "Test Name",
                Description = "Test Description"
            };

            userRepository.Setup(x => x.Get(portfolioDto.UserId)).ReturnsAsync((User)null);

            var request = new CreatePortfolioRequest()
            {
                Data = portfolioDto,
            };

            var fakeRepo = new Mock<IPortfolioRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Portfolio>())).Returns(Task.FromResult(444));

            portfolioManager = new PortfolioManager(fakeRepo.Object, userRepository.Object);

            var res = await portfolioManager.CreatePortfolio(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.PORTFOLIO_MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "The user id provided was not found");
        }
    }
}