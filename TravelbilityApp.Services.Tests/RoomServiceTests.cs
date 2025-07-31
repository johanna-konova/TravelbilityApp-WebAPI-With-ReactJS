using MockQueryable;
using Moq;

using System.Reflection;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Services.Tests
{
    [TestFixture]
    public class RoomServiceTests
    {
        private Mock<IRepository> mockedRepository;
        private RoomService roomService;

        [SetUp]
        public void Setup()
        {
            mockedRepository = new Mock<IRepository>();
            roomService = new RoomService(mockedRepository.Object, Mock.Of<IFacilityService>(), Mock.Of<IPropertyService>());
        }

        [Test]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = false)]
        [TestCase(true, true, ExpectedResult = false)]
        public async Task<bool> HasRoomWithGivenIdAsync_ShouldReturnExpectedResult(
            bool isRoomIdCorrect,
            bool isRoomDeleted)
        {
            // Arrange
            var roomId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room { Id = roomId, IsDeleted = isRoomDeleted }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            var testRoomId = isRoomIdCorrect ? roomId : Guid.NewGuid();

            // Act
            var result = await roomService.HasRoomWithGivenIdAsync(testRoomId);

            // Assert
            return result;
        }

        [Test]
        public async Task GetAllByPropertyIdAsync_ShouldReturnRooms_ForGivenPropertyId()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    PricePerNight = 100,
                    IsDeleted = false,
                    RoomType = new RoomType { Name = "Deluxe", IsForAccessibility = false },
                    MainBedType = new BedType { Name = "King" },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "room1.jpg" }
                    }
                },
                new Room
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    PricePerNight = 150,
                    IsDeleted = false,
                    RoomType = new RoomType { Name = "Accessible", IsForAccessibility = true },
                    MainBedType = new BedType { Name = "Queen" },
                    Photos = new List<PropertyPhoto>() // без снимка
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllByPropertyIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));

            var firstRoom = result.First();
            Assert.Multiple(() =>
            {
                Assert.That(firstRoom.RoomTypeName, Is.EqualTo("Deluxe"));
                Assert.That(firstRoom.MainBedTypeName, Is.EqualTo("King"));
                Assert.That(firstRoom.PricePerNight, Is.EqualTo(100));
                Assert.That(firstRoom.MainPhotoUrl, Is.EqualTo("room1.jpg"));
                Assert.That(firstRoom.IsAccessibleRoom, Is.False);
            });

            var secondRoom = result.Last();
            Assert.Multiple(() =>
            {
                Assert.That(secondRoom.MainPhotoUrl, Is.EqualTo(string.Empty)); // когато няма снимка
                Assert.That(secondRoom.IsAccessibleRoom, Is.True);
            });
        }

        [Test]
        public async Task GetAllByPropertyIdAsync_ShouldReturnEmptyList_WhenNoRoomsExist()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var mockedRooms = new List<Room>().BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllByPropertyIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllByPropertyIdAsync_ShouldReturnEmptyList_WhenPropertyIdDoesNotMatch()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    IsDeleted = false,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllByPropertyIdAsync(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllByPropertyIdAsync_ShouldReturnEmptyList_WhenAllRoomsAreDeleted()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    IsDeleted = true,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllByPropertyIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllDetailedByPropertyIdAsync_ShouldReturnDetailedRooms_ForGivenPropertyId()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    PricePerNight = 120,
                    MaxGuests = 2,
                    IsDeleted = false,
                    RoomType = new RoomType { Name = "Suite", IsForAccessibility = true },
                    MainBedType = new BedType { Name = "Queen" },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photo1.jpg" },
                        new PropertyPhoto { Url = "photo2.jpg" }
                    },
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility { Facility = new Facility { Name = "Wi-Fi", IsForAccessibility = false } },
                        new PropertyFacility { Facility = new Facility { Name = "Higher level toilet", IsForAccessibility = true } }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllDetailedByPropertyIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);

            var room = result.First();
            Assert.Multiple(() =>
            {
                Assert.That(room.RoomTypeName, Is.EqualTo("Suite"));
                Assert.That(room.MainBedTypeName, Is.EqualTo("Queen"));
                Assert.That(room.PricePerNight, Is.EqualTo(120));
                Assert.That(room.MaxGuests, Is.EqualTo(2));
                Assert.That(room.IsAccessibleRoom, Is.True);
                Assert.That(room.CommonFacilityNames.Count(), Is.EqualTo(1));
                Assert.That(room.CommonFacilityNames, Does.Contain("Wi-Fi"));
                Assert.That(room.AccessibilityNames.Count(), Is.EqualTo(1));
                Assert.That(room.AccessibilityNames, Does.Contain("Higher level toilet"));
            });
        }

        [Test]
        public async Task GetAllDetailedByPropertyIdAsync_ShouldReturnEmpty_WhenNoRoomsExist()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var mockedRooms = new List<Room>().BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllDetailedByPropertyIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllDetailedByPropertyIdAsync_ShouldReturnEmpty_WhenAllRoomsAreDeleted()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    IsDeleted = true,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllDetailedByPropertyIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllDetailedByPropertyIdAsync_ShouldReturnEmpty_WhenPropertyIdDoesNotMatch()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    IsDeleted = false,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetAllDetailedByPropertyIdAsync(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetByIdAndPropertyIdAsync_ShouldReturnRoom_WhenRoomExists()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var roomId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = roomId,
                    PropertyId = propertyId,
                    PricePerNight = 120,
                    MaxGuests = 2,
                    IsDeleted = false,
                    RoomType = new RoomType { Name = "Suite", IsForAccessibility = true },
                    MainBedType = new BedType { Name = "Queen" },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photo1.jpg" },
                        new PropertyPhoto { Url = "photo2.jpg" }
                    },
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility { Facility = new Facility { Name = "Wi-Fi", IsForAccessibility = false } },
                        new PropertyFacility { Facility = new Facility { Name = "Higher level toilet", IsForAccessibility = true } }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetByIdAndPropertyIdAsync(roomId, propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.RoomTypeName, Is.EqualTo("Suite"));
                Assert.That(result.MainBedTypeName, Is.EqualTo("Queen"));
                Assert.That(result.PricePerNight, Is.EqualTo(120));
                Assert.That(result.MaxGuests, Is.EqualTo(2));
                Assert.That(result.IsAccessibleRoom, Is.True);
                Assert.That(result.CommonFacilityNames.Count(), Is.EqualTo(1));
                Assert.That(result.CommonFacilityNames, Does.Contain("Wi-Fi"));
                Assert.That(result.AccessibilityNames.Count(), Is.EqualTo(1));
                Assert.That(result.AccessibilityNames, Does.Contain("Higher level toilet"));
            });
        }

        [Test]
        public void GetByIdAndPropertyIdAsync_ShouldThrow_WhenRoomDoesNotExist()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = roomId,
                    PropertyId = propertyId,
                    IsDeleted = false,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act & Assert
            var exception = Assert.ThrowsAsync<TargetInvocationException>(
                async () => await roomService.GetByIdAndPropertyIdAsync(Guid.NewGuid(), Guid.NewGuid())
            );

            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void GetByIdAndPropertyIdAsync_ShouldThrow_WhenRoomIsDeleted()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = roomId,
                    PropertyId = propertyId,
                    IsDeleted = true,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act & Assert
            var exception = Assert.ThrowsAsync<TargetInvocationException>(
                async () => await roomService.GetByIdAndPropertyIdAsync(roomId, propertyId)
            );

            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task GetForEditByIdAndPropertyIdAsync_ShouldReturnRoomEditDto_WhenRoomExists()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = roomId,
                    PropertyId = propertyId,
                    PricePerNight = 300,
                    MaxGuests = 3,
                    SizeInSquareMeters = 25.5,
                    NumberOfUnits = 2,
                    Description = "Spacious room",
                    IsDeleted = false,
                    RoomType = new RoomType { Id = 2, Name = "Suite", IsForAccessibility = true },
                    MainBedType = new BedType { Id = 5, Name = "King" },
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility { Facility = new Facility { Id = 10, Name = "Wi-Fi", IsForAccessibility = false } },
                        new PropertyFacility { Facility = new Facility { Id = 20, Name = "Accessible Bathroom", IsForAccessibility = true } }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photo1.jpg" },
                        new PropertyPhoto { Url = "photo2.jpg" }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act
            var result = await roomService.GetForEditByIdAndPropertyIdAsync(roomId, propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(roomId));
                Assert.That(result.PricePerNight, Is.EqualTo(300));
                Assert.That(result.MaxGuests, Is.EqualTo(3));
                Assert.That(result.Size, Is.EqualTo(25.5));
                Assert.That(result.NumberOfUnits, Is.EqualTo(2));
                Assert.That(result.Description, Is.EqualTo("Spacious room"));
                Assert.That(result.RoomType.Id, Is.EqualTo(2));
                Assert.That(result.RoomType.Name, Is.EqualTo("Suite"));
                Assert.That(result.RoomType.IsForAccessibility, Is.True);
                Assert.That(result.MainBedType.Id, Is.EqualTo(5));
                Assert.That(result.MainBedType.Name, Is.EqualTo("King"));
                Assert.That(result.Facilities.Count(), Is.EqualTo(2));
                Assert.That(result.PhotoUrls.Count(), Is.EqualTo(2));
                Assert.That(result.Facilities.Any(f => f.Name == "Accessible Bathroom" && f.IsForAccessibility), Is.True);
            });
        }

        [Test]
        public void GetForEditByIdAndPropertyIdAsync_ShouldThrow_WhenRoomDoesNotExist()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = roomId,
                    PropertyId = propertyId,
                    IsDeleted = false,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act & Assert
            var exception = Assert.ThrowsAsync<TargetInvocationException>(
                async () => await roomService.GetForEditByIdAndPropertyIdAsync(Guid.NewGuid(), Guid.NewGuid())
            );

            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void GetForEditByIdAndPropertyIdAsync_ShouldThrow_WhenRoomIsDeleted()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var propertyId = Guid.NewGuid();

            var mockedRooms = new List<Room>
            {
                new Room
                {
                    Id = roomId,
                    PropertyId = propertyId,
                    IsDeleted = true,
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Room>())
                .Returns(mockedRooms);

            // Act & Assert
            var exception = Assert.ThrowsAsync<TargetInvocationException>(
                async () => await roomService.GetForEditByIdAndPropertyIdAsync(roomId, propertyId)
            );

            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
        }

    }
}
