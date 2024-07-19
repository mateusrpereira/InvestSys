using Application;
using Application.Active.Dtos;
using Application.Portfolio.Dtos;
using Application.Transaction;
using Application.Transaction.Dtos;
using Application.Transaction.Requests;
using Domain.Active.Enums;
using Domain.Active.Ports;
using Domain.Entities;
using Domain.Portfolio.Ports;
using Domain.Transaction.Enums;
using Domain.Transaction.Ports;
using Moq;

namespace ApplicationTests
{
    public class TransactionManagerTests
    {
        TransactionManager transactionManager;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task HappyPath()
        {
            var portfolioRepository = new Mock<IPortfolioRepository>();
            var activeRepository = new Mock<IActiveRepository>();

            //Mock de Portfolio
            var portfolioDto = new PortfolioDto
            {
                UserId = 1,
                Name = "Portfolio Name Test",
                Description = "Porfolio Description Test"
            };
            var portfolioId = 1;
            var portfolio = new Portfolio
            {
                Id = portfolioId,
                User = new User { Id = 1, Name = "Fulano", Email = "fulano@gmail.com" },
                Name = portfolioDto.Name,
                Description = portfolioDto.Description,
            };
            portfolioRepository.Setup(x => x.Get(portfolioId)).ReturnsAsync(portfolio);

            //Mock de Active
            var activeDto = new ActiveDto
            {
                Name = "Active Name Test",
                Code = 1,
                ActiveType = ActiveTypes.Commodities
            };
            var activeId = 1;
            var active = new Active
            {
                Id = activeId,
                Name = activeDto.Name,
                ActiveType = activeDto.ActiveType,
                Code = activeDto.Code
            };
            activeRepository.Setup(x => x.Get(activeId)).ReturnsAsync(active);

            var transactionDto = new TransactionDto
            {
                PortfolioId = portfolioId,
                ActiveId = activeId,
                TransactionType = TransactionTypes.Withdrawal,
                Quantity = 10,
                Price = 100.0
            };

            int expectedId = 555;

            var request = new CreateTransactionRequest()
            {
                Data = transactionDto,
            };

            var fakeRepo = new Mock<ITransactionRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Transaction>())).Returns(Task.FromResult(expectedId));

            transactionManager = new TransactionManager(
                fakeRepo.Object, 
                portfolioRepository.Object, 
                activeRepository.Object);

            var res = await transactionManager.CreateTransaction(request);

            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);

            Assert.AreEqual(res.Data.PortfolioId, transactionDto.PortfolioId);
            Assert.AreEqual(res.Data.ActiveId, transactionDto.ActiveId);
            Assert.AreEqual(res.Data.TransactionType, transactionDto.TransactionType);
            Assert.AreEqual(res.Data.Quantity, transactionDto.Quantity);
            Assert.AreEqual(res.Data.Price, transactionDto.Price);
        }

        [TestCase(0, -1)] //Invalid quantity and invalid price
        [TestCase(1, -1)] //Valid quantity and invalid price
        [TestCase(0, 100)] //Invalid quantity and valid price
        public async Task Should_Return_MissingRequiredInformation(int qtde, double price)
        {
            var portfolioRepository = new Mock<IPortfolioRepository>();
            var activeRepository = new Mock<IActiveRepository>();

            var portfolioId = 1;
            var activeId = 1;

            var portfolio = new Portfolio
            {
                Id = portfolioId,
                User = new User { Id = 1, Name = "Fulano", Email = "fulano@gmail.com" },
                Name = "Test Portfolio",
                Description = "Test Description"
            };

            var active = new Active
            {
                Id = activeId,
                Name = "Test Active",
                ActiveType = ActiveTypes.Bonds,
                Code = 1
            };

            portfolioRepository.Setup(x => x.Get(portfolioId)).ReturnsAsync(portfolio);
            activeRepository.Setup(x => x.Get(activeId)).ReturnsAsync(active);

            var transactionDto = new TransactionDto
            {
                PortfolioId = portfolioId,
                ActiveId = activeId,
                TransactionType = TransactionTypes.Buy,
                Quantity = qtde,
                Price = price 
            };

            var request = new CreateTransactionRequest
            {
                Data = transactionDto
            };

            var fakeRepo = new Mock<ITransactionRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Transaction>())).Returns(Task.FromResult(555));

            transactionManager = new TransactionManager(
                fakeRepo.Object,
                portfolioRepository.Object,
                activeRepository.Object);

            var res = await transactionManager.CreateTransaction(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.TRANSACTION_MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");
        }

        [TestCase(99)]//Invalid transaction type
        public async Task Should_Return_InvalidTransactionTypeException(TransactionTypes transactionType)
        {
            var portfolioRepository = new Mock<IPortfolioRepository>();
            var activeRepository = new Mock<IActiveRepository>();

            var portfolioDto = new PortfolioDto
            {
                UserId = 1,
                Name = "Portfolio Name Test",
                Description = "Porfolio Description Test"
            };
            var portfolioId = 1;
            var portfolio = new Portfolio
            {
                Id = portfolioId,
                User = new User { Id = 1, Name = "Fulano", Email = "fulano@gmail.com" },
                Name = portfolioDto.Name,
                Description = portfolioDto.Description,
            };
            portfolioRepository.Setup(x => x.Get(portfolioId)).ReturnsAsync(portfolio);

            var activeDto = new ActiveDto
            {
                Name = "Active Name Test",
                Code = 1,
                ActiveType = ActiveTypes.Commodities
            };
            var activeId = 1;
            var active = new Active
            {
                Id = activeId,
                Name = activeDto.Name,
                ActiveType = activeDto.ActiveType,
                Code = activeDto.Code
            };
            activeRepository.Setup(x => x.Get(activeId)).ReturnsAsync(active);

            var transactionDto = new TransactionDto
            {
                PortfolioId = portfolioId,
                ActiveId = activeId,
                TransactionType = transactionType,
                Quantity = 10,
                Price = 100.0
            };

            var request = new CreateTransactionRequest
            {
                Data = transactionDto
            };

            var fakeRepo = new Mock<ITransactionRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Transaction>())).Returns(Task.FromResult(555));

            transactionManager = new TransactionManager(
                fakeRepo.Object,
                portfolioRepository.Object,
                activeRepository.Object);

            var res = await transactionManager.CreateTransaction(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.TRANSACTION_INVALID_TYPE);
            Assert.AreEqual(res.Message, "The given transaction type is not valid");
        }

        [Test]
        public async Task Should_Return_PortfolioNotFound()
        {
            var portfolioRepository = new Mock<IPortfolioRepository>();
            var activeRepository = new Mock<IActiveRepository>();

            var activeDto = new ActiveDto
            {
                Name = "Active Name Test",
                Code = 1,
                ActiveType = ActiveTypes.Commodities
            };
            var activeId = 1;
            var active = new Active
            {
                Id = activeId,
                Name = activeDto.Name,
                ActiveType = activeDto.ActiveType,
                Code = activeDto.Code
            };

            var transactionDto = new TransactionDto
            {
                PortfolioId = 999, // Assuming this is an invalid PortfolioId
                ActiveId = activeId,
                TransactionType = TransactionTypes.Withdrawal,
                Quantity = 10,
                Price = 100.0
            };

            portfolioRepository.Setup(x => x.Get(transactionDto.PortfolioId)).ReturnsAsync((Portfolio)null);
            activeRepository.Setup(x => x.Get(activeId)).ReturnsAsync(active);

            var request = new CreateTransactionRequest
            {
                Data = transactionDto
            };

            var fakeRepo = new Mock<ITransactionRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Transaction>())).Returns(Task.FromResult(555));

            transactionManager = new TransactionManager(
                fakeRepo.Object,
                portfolioRepository.Object,
                activeRepository.Object);

            var res = await transactionManager.CreateTransaction(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.TRANSACTION_MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "The portfolio id provided was not found");
        }

        [Test]
        public async Task Should_Return_ActiveNotFound()
        {
            var portfolioRepository = new Mock<IPortfolioRepository>();
            var activeRepository = new Mock<IActiveRepository>();

            var portfolioDto = new PortfolioDto
            {
                UserId = 1,
                Name = "Portfolio Name Test",
                Description = "Porfolio Description Test"
            };
            var portfolioId = 1;
            var portfolio = new Portfolio
            {
                Id = portfolioId,
                User = new User { Id = 1, Name = "Fulano", Email = "fulano@gmail.com" },
                Name = portfolioDto.Name,
                Description = portfolioDto.Description,
            };
            portfolioRepository.Setup(x => x.Get(portfolioId)).ReturnsAsync(portfolio);

            var transactionDto = new TransactionDto
            {
                PortfolioId = portfolioId,
                ActiveId = 999,// Assuming this is an invalid activeId
                TransactionType = TransactionTypes.Withdrawal,
                Quantity = 10,
                Price = 100.0
            };

            activeRepository.Setup(x => x.Get(transactionDto.ActiveId)).ReturnsAsync((Active)null);
            portfolioRepository.Setup(x => x.Get(portfolioId)).ReturnsAsync(portfolio);

            var request = new CreateTransactionRequest
            {
                Data = transactionDto
            };

            var fakeRepo = new Mock<ITransactionRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Transaction>())).Returns(Task.FromResult(555));

            transactionManager = new TransactionManager(
                fakeRepo.Object,
                portfolioRepository.Object,
                activeRepository.Object);

            var res = await transactionManager.CreateTransaction(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.TRANSACTION_MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "The active id provided was not found");
        }
    }
}