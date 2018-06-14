using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using src.Controllers;
using src.Models.AccountViewModels;
using src.Services;
using tests.Builders;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace tests.Controllers
{
    public class AccountController_Tests
    {
        [Fact]
        public async void Test()
        {
            var fakeUserManager = new FakeUserManagerBuilder()
                .Build();
            var fakeSignInManager = new FakeSignInManagerBuilder()
                .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success))
                .Build();
            var fakeEmailSender = new Mock<IEmailSender>();
            var fakeLogger = new Mock<ILogger<AccountController>>();
            
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(true)
                .Verifiable();

            var controller = new AccountController(
                fakeUserManager.Object, 
                fakeSignInManager.Object, 
                fakeEmailSender.Object, 
                fakeLogger.Object);
            
            controller.Url = mockUrlHelper.Object;

            var result = await controller.Login(new LoginViewModel(), "testPath");

            Assert.IsType<RedirectResult>(result);
        }
    }
}