using Application;
using Application.Active;
using Application.Active.Dtos;
using Application.Active.Requests;
using Domain.Active.Enums;
using Domain.Active.Ports;
using Domain.Entities;
using Moq;

namespace ApplicationTests
{
    public class ActiveManagerTests
    {
        ActiveManager activeManager;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task HappyPath()
        {
            var activeDto = new ActiveDto
            {
                Name = "Test",
                ActiveType = ActiveTypes.Bonds,
                Code = "ABCD3"
            };

            int expectedId = 333;

            var request = new CreateActiveRequest()
            {
                Data = activeDto,
            };

            var fakeRepo = new Mock<IActiveRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Active>())).Returns(Task.FromResult(expectedId));

            activeManager = new ActiveManager(fakeRepo.Object);

            var res = await activeManager.CreateActive(request);

            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
            
            Assert.AreEqual(res.Data.Name, activeDto.Name);
            Assert.AreEqual(res.Data.ActiveType, activeDto.ActiveType);
            Assert.AreEqual(res.Data.Code, activeDto.Code);
        }

        [TestCase(5, "", "ABCD3")]
        [TestCase(5, null, "ABCD3")]
        public async Task Should_Return_MissingRequiredInformation(ActiveTypes ActiveType, string Name, string Code)
        {
            var activeDto = new ActiveDto
            {
                ActiveType = ActiveType,
                Name = Name,
                Code = Code,
            };

            var request = new CreateActiveRequest()
            {
                Data = activeDto,
            };

            var fakeRepo = new Mock<IActiveRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Active>())).Returns(Task.FromResult(333));

            activeManager = new ActiveManager(fakeRepo.Object);

            var res = await activeManager.CreateActive(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.ACTIVE_MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");
        }

        [TestCase(15, "NameTest", "ABCD3")]//ActiveType Invalid
        public async Task Should_Return_InvalidActiveTypeException(ActiveTypes ActiveType, string Name, string Code)
        {
            var activeDto = new ActiveDto
            {
                ActiveType = ActiveType,
                Name = Name,
                Code = Code,
            };

            var request = new CreateActiveRequest()
            {
                Data = activeDto,
            };

            var fakeRepo = new Mock<IActiveRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Active>())).Returns(Task.FromResult(333));

            activeManager = new ActiveManager(fakeRepo.Object);

            var res = await activeManager.CreateActive(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.ACTIVE_INVALID_TYPE);
            Assert.AreEqual(res.Message, "The given active type is not valid");
        }

        [TestCase(5, "NameTest", "")]
        public async Task Should_Return_InvalidCodeException(ActiveTypes ActiveType, string Name, string Code)
        {
            var activeDto = new ActiveDto
            {
                ActiveType = ActiveType,
                Name = Name,
                Code = Code,
            };

            var request = new CreateActiveRequest()
            {
                Data = activeDto,
            };

            var fakeRepo = new Mock<IActiveRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Active>())).Returns(Task.FromResult(333));

            activeManager = new ActiveManager(fakeRepo.Object);

            var res = await activeManager.CreateActive(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.ACTIVE_INVALID_CODE);
            Assert.AreEqual(res.Message, "The given code is not valid");
        }
    }
}