using MockQueryable;
using Moq;

using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

namespace TravelbilityApp.Services.Tests
{
    [TestFixture]
    public class FacilityServiceTests
    {
        private Mock<IRepository> mockedRepository;
        private FacilityService facilityService;
        private IQueryable<Facility> mockedFacilities;

        [SetUp]
        public void Setup()
        {
            mockedRepository = new Mock<IRepository>();
            facilityService = new FacilityService(mockedRepository.Object);

            mockedFacilities = new List<Facility>
            {
                new Facility { Id = 1, Name = "Room service", IsForAccessibility = false, WhereStatus = WhereStatus.OnlyInRoom },
                new Facility { Id = 2, Name = "Shower chair", IsForAccessibility = true, WhereStatus = WhereStatus.OnlyInRoom },
                new Facility { Id = 3, Name = "Restaurant", IsForAccessibility = false, WhereStatus = WhereStatus.OnlyInCommonArea },
                new Facility { Id = 4, Name = "Accessible Parking", IsForAccessibility = true, WhereStatus = WhereStatus.OnlyInCommonArea },
                new Facility { Id = 5, Name = "Free Wi-Fi", IsForAccessibility = false, WhereStatus = WhereStatus.Both },
                new Facility { Id = 6, Name = "Higher level toilet", IsForAccessibility = true, WhereStatus = WhereStatus.Both }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Facility>())
                .Returns(mockedFacilities);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllFacilities()
        {
            // Act
            var result = await facilityService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(6));
                Assert.That(result.Any(f => f.Name == "Free Wi-Fi" && f.IsForAccessibility == false && f.WhereStatus == "Both"), Is.True);
                Assert.That(result.Any(f => f.Name == "Shower chair" && f.IsForAccessibility == true && f.WhereStatus == "OnlyInRoom"), Is.True);
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoFacilitiesExist()
        {
            // Arrange
            var mockedEmptyFacilities = new List<Facility>().BuildMock();
            mockedRepository.Setup(r => r.AllAsNoTracking<Facility>()).Returns(mockedEmptyFacilities);

            // Act
            var result = await facilityService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllInAsync_ShouldReturnFacilitiesMatchingWhereStatus_OrBoth()
        {
            // Act
            var result = await facilityService.GetAllInAsync(WhereStatus.OnlyInRoom);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(4));
                Assert.That(result.Any(f => f.Name == "Room service"), Is.True);
                Assert.That(result.Any(f => f.Name == "Shower chair"), Is.True);
                Assert.That(result.Any(f => f.Name == "Restaurant"), Is.False);
            });
        }

        [Test]
        public async Task GetAllInAsync_ShouldReturnEmpty_WhenNoFacilitiesMatchStatus()
        {
            // Arrange
            var mockedFacilities = new List<Facility>
            {
                new Facility { Id = 1, Name = "Room service", IsForAccessibility = false, WhereStatus = WhereStatus.OnlyInCommonArea }
            }.BuildMock();

            mockedRepository.Setup(r => r.AllAsNoTracking<Facility>()).Returns(mockedFacilities);

            // Act
            var result = await facilityService.GetAllInAsync(WhereStatus.OnlyInRoom);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetValidSelectedAsync_ShouldReturnFacilitiesMatchingIdsAndStatus()
        {
            // Arrange
            var rowSelectedIds = new List<int?> { 1, 2, 4, null };

            // Act
            var result = await facilityService.GetValidSelectedAsync(rowSelectedIds, WhereStatus.OnlyInRoom);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Any(f => f.Id == 1), Is.True);
                Assert.That(result.Any(f => f.Id == 2), Is.True);
                Assert.That(result.Any(f => f.Id == 3), Is.False);
            });
        }

        [Test]
        public async Task GetValidSelectedAsync_ShouldReturnEmpty_WhenNoValidIdsProvided()
        {
            var rowSelectedIds = new List<int?> { null, 99 };

            // Act
            var result = await facilityService.GetValidSelectedAsync(rowSelectedIds, WhereStatus.OnlyInCommonArea);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetValidSelectedIdsAsync_ShouldReturnValidIdsMatchingStatus()
        {
            // Arrange
            var rawSelectedIds = new List<int?> { 1, 2, 4, null };

            // Act
            var result = await facilityService.GetValidSelectedIdsAsync(rawSelectedIds, WhereStatus.OnlyInRoom);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Contains(1), Is.True);
                Assert.That(result.Contains(2), Is.True);
                Assert.That(result.Contains(3), Is.False);
            });
        }

        [Test]
        public async Task GetValidSelectedIdsAsync_ShouldReturnEmpty_WhenNoValidIdsProvided()
        {
            var rawSelectedIds = new List<int?> { null, 99 };

            // Act
            var result = await facilityService.GetValidSelectedIdsAsync(rawSelectedIds, WhereStatus.OnlyInCommonArea);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAccessibilityAsync_ShouldReturnOnlyAccessibleFacilities_OnlyInCommonArea_OrBoth()
        {
            // Act
            var result = await facilityService.GetAccessibilityAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Any(f => f.Name == "Shower chair"), Is.False);
                Assert.That(result.Any(f => f.Name == "Higher level toilet"), Is.True);
                Assert.That(result.Any(f => f.Name == "Heating"), Is.False);
                Assert.That(result.Any(f => f.Name == "Room service"), Is.False);
            });
        }

        [Test]
        public async Task GetAccessibilityAsync_ShouldReturnEmpty_WhenNoAccessibleFacilitiesExist_OnlyInCommonArea_OrBoth()
        {
            // Arrange
            var mockedFacilities = new List<Facility>
            {
                new Facility { Id = 1, Name = "Room service", IsForAccessibility = false, WhereStatus = WhereStatus.OnlyInRoom },
                new Facility { Id = 2, Name = "Shower chair", IsForAccessibility = true, WhereStatus = WhereStatus.OnlyInRoom },
                new Facility { Id = 3, Name = "Restaurant", IsForAccessibility = false, WhereStatus = WhereStatus.OnlyInCommonArea },
                new Facility { Id = 5, Name = "Free Wi-Fi", IsForAccessibility = false, WhereStatus = WhereStatus.Both },
            }.BuildMock();

            mockedRepository.Setup(r => r.AllAsNoTracking<Facility>()).Returns(mockedFacilities);

            // Act
            var result = await facilityService.GetAccessibilityAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

    }
}
