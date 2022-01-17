using ServiceDAL;
using ServiceDAL.Interfaces;
using Xunit;
using Xunit.Extensions.Ordering;

namespace ServiceDALTests.StructuralTests
{
    [Order(0)]
    public class ServiceStructuralTests
    {
        internal static IService ServiceDAL { get; set; }

        [Fact]
        public void ImplementService_NoException()
        {
            // Arrange

            // Act
            var exception = Record.Exception(() => ServiceDAL = new Service());

            // Assert
            Assert.Null(exception);
        }
    }
}
