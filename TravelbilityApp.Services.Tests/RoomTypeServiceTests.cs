using MockQueryable;
using Moq;

using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Services.Tests
{
    [TestFixture]
    public class RoomTypeServiceTests
    {
        private Mock<IRepository> mockedRepository;
        private RoomTypeService roomTypeService;
        private IQueryable<RoomType> mockedRoomTypes;

        [SetUp]
        public void Setup()
        {
            mockedRepository = new Mock<IRepository>();
            roomTypeService = new RoomTypeService(mockedRepository.Object);

            mockedRoomTypes = new List<RoomType>
            {
                new RoomType { Id = 1, Name = "Standard Room", IsForAccessibility = false },
                new RoomType { Id = 2, Name = "Accessible Room", IsForAccessibility = true }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<RoomType>())
                .Returns(mockedRoomTypes);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllRoomTypes()
        {
            // Act
            var result = await roomTypeService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Any(x => x.Name == "Standard Room" && x.IsForAccessibility == false), Is.True);
                Assert.That(result.Any(x => x.Name == "Accessible Room" && x.IsForAccessibility == true), Is.True);
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoRoomTypesExist()
        {
            // Arrange
            var mockedEmptyRoomTypes = new List<RoomType>().BuildMock();
            mockedRepository.Setup(r => r.AllAsNoTracking<RoomType>()).Returns(mockedEmptyRoomTypes);

            // Act
            var result = await roomTypeService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public async Task<bool> HasRoomTypeWithGivenIdAsync_ShouldReturnExpectedResult(int? id)
        {
            // Act
            var result = await roomTypeService.HasRoomTypeWithGivenIdAsync(id);

            // Assert
            return result;
        }

        [Test]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public async Task<bool> IsRoomAccessibleAsync_ShouldReturnExpectedResult(int? id)
        {
            // Act
            var result = await roomTypeService.IsRoomAccessibleAsync(id);

            // Assert
            return result;
        }
    }
}