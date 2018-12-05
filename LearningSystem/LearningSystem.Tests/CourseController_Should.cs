namespace LearningSystem.Tests
{
    using System.Linq;
    using LearningSystem.Services.Admin.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Web.Areas.Admin.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Utilities.Constants;
    using Web.Areas.Admin.Models;

    [TestClass]
    public class CourseControllerTest
    {
        private Mock<ICoursesService> courseServiceMock;

        [TestInitialize]
        public void TestInitialize() 
        {
            this.courseServiceMock = new Mock<ICoursesService>();
        } 

        [TestMethod]
        public void CorsesController_ShoudBeInAdminArea()
        {
            //Arrange & Act
            var area = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AreaAttribute) as AreaAttribute;

            // Assert:
            Assert.IsNotNull(area);
            Assert.AreEqual(AdminConstants.AdminArea, area.RouteValue);
        }

        [TestMethod]
        public void CorsesController_ShoudAuthorizeAdmins()
        {
            // Arrange & Act
            var authorization = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AuthorizeAttribute) as AuthorizeAttribute;

            //3. Assert:
            Assert.IsNotNull(authorization);
            Assert.AreEqual(AdminConstants.AdminRole, authorization.Roles);
        }

        [TestMethod]
        public void Index_WithValidModel_ShouldCallService()
        {
            // Arrange
            var isServiceCalled = false;

            this.courseServiceMock
                .Setup(x => x.GetCoursesAsync<AllCoursesViewModel>())
                .Callback(() => isServiceCalled = true);

            var controller = new CoursesController(this.courseServiceMock.Object, null, null);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsTrue(isServiceCalled);
        }

        

        [TestMethod]
        public void Create_WithValidCourse_ShoudCallService()
        {
            // Arrange
            var model = new CreateCourseBindingModel();
            var serviceCalled = false;

            this.courseServiceMock
                .Setup(repo => repo.AddCourseAsync(model))
                .Callback(() => serviceCalled = true);

            var controller = new CoursesController(this.courseServiceMock.Object, null, null);

            // Act
            var result = controller.Create(model);

            // Assert
            Assert.IsTrue(serviceCalled);
        }
    }
}
