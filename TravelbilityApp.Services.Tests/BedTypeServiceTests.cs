using MockQueryable;
using Moq;

using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Services.Tests
{
    [TestFixture]
    public class BedTypeServiceTests
    {
        private Mock<IRepository> mockedRepository;
        private BedTypeService bedTypeService;
        private IQueryable<BedType> mockedBedTypes;

        [SetUp]
        public void Setup()
        {
            mockedRepository = new Mock<IRepository>();
            bedTypeService = new BedTypeService(mockedRepository.Object);

            mockedBedTypes = new List<BedType>()
            {
                new BedType { Id = 1, Name = "Single" },
                new BedType { Id = 2, Name = "Double" }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<BedType>())
                .Returns(mockedBedTypes);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllBedTypes()
        {
            // Act
            var result = await bedTypeService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Any(x => x.Name == "Single"), Is.True);
                Assert.That(result.Any(x => x.Name == "Double"), Is.True);
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoBedTypesExist()
        {
            // Arrange
            var mockedEmptyBedTypes = new List<BedType>().BuildMock();
            mockedRepository.Setup(r => r.AllAsNoTracking<BedType>()).Returns(mockedEmptyBedTypes);

            // Act
            var result = await bedTypeService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = false)]
        public async Task<bool> HasBedTypeWithGivenIdAsync_ShouldReturnExpectedResult(int id)
        {
            // Act
            return await bedTypeService.HasBedTypeWithGivenIdAsync(id);
        }

        [Test]
        public async Task HasBedTypeWithGivenIdAsync_ShouldReturnFalse_WhenIdIsNull()
        {
            // Act
            var result = await bedTypeService.HasBedTypeWithGivenIdAsync(null);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}