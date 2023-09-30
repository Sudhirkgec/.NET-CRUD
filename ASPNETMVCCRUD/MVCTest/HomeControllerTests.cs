

using ASPNETMVCCRUD.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ASPNETMVCTest
{
    public class HomeControllerTests
    {
        
        [Fact]
        public void Test_Index_ReturnsNotNull()
        {

            var controller = new HomeController(new NullLogger<HomeController>());
            var result = controller.Index() ;
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Privacy_ReturnsNotNull()
        {

            var controller = new HomeController(new NullLogger<HomeController>());
            var result = controller.Privacy() ;
            Assert.NotNull(result);
        }
    }
}