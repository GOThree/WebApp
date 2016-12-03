using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using WebApp.API.Controllers;
using WebApp.API.Services;
using Microsoft.Extensions.Logging;
using WebApp.API.Models.AccountViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.Db;

namespace WebApp.API.Tests.ControllerTests
{
    public class AccountControllerTests
    {
        private readonly UserManager<ApplicationUser> userManagerMock = new FakeUserManager();
        private readonly Mock<IAccountService> accountServiceMock = new Mock<IAccountService>();
        private readonly Mock<IEmailSender> emailSenderMock = new Mock<IEmailSender>();
        private readonly Mock<ILoggerFactory> loggerFactoryMock = new Mock<ILoggerFactory>();

        [Fact]
        public async Task Register_Should_Return_Ok()
        {
            //Assert
            AccountController controller = new AccountController(userManagerMock, accountServiceMock.Object, emailSenderMock.Object, loggerFactoryMock.Object);

            //Act
            var result = await controller.Register(new RegisterViewModel());
            var okResult = result as OkResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
