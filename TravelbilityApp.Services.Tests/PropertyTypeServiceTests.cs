using MockQueryable;
using Moq;

using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Services.Tests
{
    [TestFixture]
    public class PropertyTypeServiceTests
    {
        private Mock<IRepository> mockedRepository;
        private PropertyTypeService propertyTypeService;
        private IQueryable<PropertyType> mockedPropertyTypes;

        [SetUp]
        public void Setup()
        {
            mockedRepository = new Mock<IRepository>();
            propertyTypeService = new PropertyTypeService(mockedRepository.Object);

            mockedPropertyTypes = new List<PropertyType>
            {
                new PropertyType { Id = 1, Name = "Hotel" },
                new PropertyType { Id = 2, Name = "Hostel" }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<PropertyType>())
                .Returns(mockedPropertyTypes);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllPropertyTypes()
        {
            // Act
            var result = await propertyTypeService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Any(pt => pt.Name == "Hotel"), Is.True);
                Assert.That(result.Any(pt => pt.Name == "Hostel"), Is.True);
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoPropertyTypesExist()
        {
            // Arrange
            var mockedEmptyPropertyTypes = new List<PropertyType>().BuildMock();
            mockedRepository
                .Setup(r => r.AllAsNoTracking<PropertyType>())
                .Returns(mockedEmptyPropertyTypes);

            // Act
            var result = await propertyTypeService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public async Task<bool> HasPropertyTypeWithGivenIdAsync_ShouldReturnExpectedResult(int? id)
        {
            // Act
            return await propertyTypeService.HasPropertyTypeWithGivenIdAsync(id);
        }

    }
}
