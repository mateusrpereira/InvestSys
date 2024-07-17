using Application;
using Application.User;
using Application.User.Dtos;
using Application.User.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
    public class UserManagerTests
    {
        UserManager userManager;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task HappyPath()
        {
            var userDto = new UserDto
            {
                Name = "Fulano",
                Email = "fulano@gmail.com",
                Password = "password"
            };

            int expectedId = 222;

            var request = new CreateUserRequest()
            {
                Data = userDto,
            };

            var fakeRepo = new Mock<IUserRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.FromResult(expectedId));

            userManager = new UserManager(fakeRepo.Object);

            var res = await userManager.CreateUser(request);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
            Assert.AreEqual(res.Data.Name, userDto.Name);
        }

        [TestCase("", "abcd@email.com", "abcd1234")]
        [TestCase(null, "abcd@email.com", "abcd1234")]
        [TestCase("Fulano", "", "abcd1234")]
        [TestCase("Fulano", null, "abcd1234")]
        [TestCase("Fulano", "abcd@email.com", "")]
        [TestCase("Fulano", "abcd@email.com", null)]
        [TestCase("", "", "")]
        [TestCase(null, null, null)]
        public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(string name, string email, string password)
        {
            var userDto = new UserDto
            {
                Name = name,
                Email = email,
                Password = password
            };

            var request = new CreateUserRequest()
            {
                Data = userDto,
            };

            var fakeRepo = new Mock<IUserRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.FromResult(222));

            userManager = new UserManager(fakeRepo.Object);

            var res = await userManager.CreateUser(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");
        }

        [TestCase("b@b.com")]
        [TestCase("emailsemarrobaesemponto")]
        public async Task Should_Return_InvalidEmailExceptions_WhenEmailAreInvalid(string email)
        {
            var userDto = new UserDto
            {
                Name = "Fulano",
                Email = email,
                Password = "abcd1234"
            };

            var request = new CreateUserRequest()
            {
                Data = userDto,
            };

            var fakeRepo = new Mock<IUserRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.FromResult(222));

            userManager = new UserManager(fakeRepo.Object);

            var res = await userManager.CreateUser(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.INVALID_EMAIL);
            Assert.AreEqual(res.Message, "The given email is not valid");
        }
    }
}