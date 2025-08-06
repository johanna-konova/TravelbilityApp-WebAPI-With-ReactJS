using MockQueryable;
using Moq;

using System.Reflection;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

namespace TravelbilityApp.Services.Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IRepository> mockedRepository;
        private PropertyService propertyService;

        [SetUp]
        public void Setup()
        {
            mockedRepository = new Mock<IRepository>();
            propertyService = new PropertyService(mockedRepository.Object, Mock.Of<IFacilityService>());
        }

        [Test]
        [TestCase(true, PropertyStatus.Published, PropertyStatus.Saved, ExpectedResult = true)]
        [TestCase(true, PropertyStatus.Saved, PropertyStatus.Published, ExpectedResult = false)]
        [TestCase(true, PropertyStatus.Deleted, PropertyStatus.Published, ExpectedResult = false)]
        [TestCase(false, PropertyStatus.Published, PropertyStatus.Saved, ExpectedResult = false)]
        public async Task<bool> HasPropertyWithGivenIdAsync_ShouldReturnExpectedResult(
            bool isPropertyIdCorrect,
            PropertyStatus propertyStatus,
            PropertyStatus filterStatus)
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var testPropertyId = isPropertyIdCorrect ? propertyId : Guid.NewGuid();

            var mockedProperties = new List<Property>
            {
                new Property { Id = propertyId, Status = propertyStatus }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            return await propertyService.HasPropertyWithGivenIdAsync(testPropertyId, filterStatus);
        }

        [Test]
        [TestCase(true, true, PropertyStatus.Published, ExpectedResult = true)]
        [TestCase(true, false, PropertyStatus.Published, ExpectedResult = false)]
        [TestCase(true, true, PropertyStatus.Deleted, ExpectedResult = false)]
        [TestCase(false, true, PropertyStatus.Published, ExpectedResult = false)]
        public async Task<bool> IsUserPropertyPublisherAsync_ShouldReturnExpectedResult(
            bool isPropertyIdCorrect,
            bool isPublisherCorrect,
            PropertyStatus propertyStatus)
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var testPropertyId = isPropertyIdCorrect ? propertyId : Guid.NewGuid();
            var testUserId = isPublisherCorrect ? userId : Guid.NewGuid();

            var mockedProperties = new List<Property>
            {
                new Property { Id = propertyId, PublisherId = userId, Status = propertyStatus }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            return await propertyService.IsUserPropertyPublisherAsync(testPropertyId, testUserId);
        }

        [Test]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = false)]
        [TestCase(true, true, ExpectedResult = false)]
        public async Task<bool> HasAccessibleRoom_ShouldReturnExpectedResult(
            bool isRoomAccessible,
            bool isRoomDeleted)
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = propertyId,
                    Rooms = new List<Room>
                    {
                        new Room
                        {
                            IsDeleted = isRoomDeleted,
                            RoomType = new RoomType { IsForAccessibility = isRoomAccessible }
                        }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            return await propertyService.HasAccessibleRoom(propertyId);
        }

        [Test]
        public async Task GetAllByUserIdAsync_ShouldReturnOnlyNonDeletedUserProperties()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var otherUserId = Guid.NewGuid();
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    PublisherId = userId,
                    Status = PropertyStatus.Published,
                    Name = "Hotel A",
                    Address = "Address A",
                    StarsCount = 3,
                    Description = "Desc A",
                    Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "photo1.jpg" } }
                },
                new Property { Id = Guid.NewGuid(), PublisherId = userId, Status = PropertyStatus.Deleted, Name = "Hotel B" },
                new Property { Id = Guid.NewGuid(), PublisherId = otherUserId, Status = PropertyStatus.Published, Name = "Hotel C" }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetAllByUserIdAsync(userId, currentPage, itemsPerPage);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(1));
                Assert.That(result.Items.Any(p => p.Name == "Hotel A"), Is.True);
                Assert.That(result.Items.Any(p => p.Name == "Hotel B"), Is.False);
                Assert.That(result.Items.Any(p => p.Name == "Hotel C"), Is.False);
                Assert.That(result.TotalCount, Is.EqualTo(1));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllByUserIdAsync_ShouldReturnCorrectPageData()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currentPage = 2;
            var itemsPerPage = 2;

            var mockedProperties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), PublisherId = userId, Status = PropertyStatus.Published, Name = "Hotel 1", Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p1.jpg" } } },
                new Property { Id = Guid.NewGuid(), PublisherId = userId, Status = PropertyStatus.Published, Name = "Hotel 2", Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p2.jpg" } } },
                new Property { Id = Guid.NewGuid(), PublisherId = userId, Status = PropertyStatus.Published, Name = "Hotel 3", Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p3.jpg" } } },
                new Property { Id = Guid.NewGuid(), PublisherId = userId, Status = PropertyStatus.Published, Name = "Hotel 4", Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p4.jpg" } } },
                new Property { Id = Guid.NewGuid(), PublisherId = userId, Status = PropertyStatus.Published, Name = "Hotel 5", Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p5.jpg" } } }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetAllByUserIdAsync(userId, currentPage, itemsPerPage);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.TotalCount, Is.EqualTo(5));
                Assert.That(result.Items.Count(), Is.EqualTo(2));
                Assert.That(result.Items.First().Name, Is.EqualTo("Hotel 3"));
                Assert.That(result.Items.Last().Name, Is.EqualTo("Hotel 4"));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllByUserIdAsync_ShouldReturnEmpty_WhenUserHasNoProperties()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), PublisherId = userId, Status = PropertyStatus.Deleted },
                new Property { Id = Guid.NewGuid(), PublisherId = Guid.NewGuid(), Status = PropertyStatus.Published }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetAllByUserIdAsync(userId, currentPage, itemsPerPage);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.That(result.Items.Count(), Is.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
            Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
            Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
        }


        [Test]
        public async Task GetNewestAddedAsync_ShouldReturnLatestProperties_LimitedByCount()
        {
            // Arrange
            var mockedProperties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), Name = "Old Hotel", CreatedAt = DateTime.UtcNow.AddDays(-10), Address = "Address A", StarsCount = 3, Description = "Desc A", Status = PropertyStatus.Published, PropertyType = new PropertyType { Name = "Type A" }, Photos = new List<PropertyPhoto> { new PropertyPhoto { Url = "old.jpg" } } },
                new Property { Id = Guid.NewGuid(), Name = "New Hotel", CreatedAt = DateTime.UtcNow, Address = "Address B", StarsCount = 3, Description = "Desc B"
                , Status = PropertyStatus.Published, PropertyType = new PropertyType { Name = "Type B" }, Photos = new List<PropertyPhoto> { new PropertyPhoto { Url = "new.jpg" } } },
                new Property { Id = Guid.NewGuid(), Name = "Mid Hotel", CreatedAt = DateTime.UtcNow.AddDays(-5), Address = "Address C", StarsCount = 3, Description = "Desc C", Status = PropertyStatus.Published, PropertyType = new PropertyType { Name = "Type C" }, Photos = new List<PropertyPhoto> { new PropertyPhoto { Url = "mid.jpg" } } }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var count = 2;

            // Act
            var result = await propertyService.GetNewestAddedAsync(count);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.First().Name, Is.EqualTo("New Hotel"));
                Assert.That(result.Any(p => p.Name == "Old Hotel"), Is.False);
            });
        }

        [Test]
        public async Task GetNewestAddedAsync_ShouldReturnEmpty_WhenNoPropertiesExist()
        {
            // Arrange
            var mockedEmptyProperties = new List<Property>().BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedEmptyProperties);

            var count = 3;

            // Act
            var result = await propertyService.GetNewestAddedAsync(count);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnDetailedPropertyDto_WhenPropertyExists()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var property = new Property
            {
                Id = propertyId,
                Name = "Luxury Hotel",
                Address = "123 Street",
                StarsCount = 5,
                Description = "Great place",
                Status = PropertyStatus.Saved,
                Facilities = new List<PropertyFacility>(),
                Photos = new List<PropertyPhoto>
                {
                    new PropertyPhoto { Url = "photo1.jpg" },
                    new PropertyPhoto { Url = "photo2.jpg" }
                },
            };

            var mockedProperties = new List<Property> { property }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetByIdAsync(propertyId, PropertyStatus.Saved);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(propertyId));
                Assert.That(result.Name, Is.EqualTo("Luxury Hotel"));
                Assert.That(result.Address, Is.EqualTo("123 Street"));
                Assert.That(result.StarsCount, Is.EqualTo(5));
                Assert.That(result.Description, Is.EqualTo("Great place"));
                Assert.That(result.PhotoUrls.Count(), Is.EqualTo(2));
            });
        }

        [Test]
        [TestCase(false, true, PropertyStatus.Saved)]   // грешно ID
        //[TestCase(true, false, PropertyStatus.Saved)]   // дублирани записи
        [TestCase(true, true, PropertyStatus.Deleted)]  // статус под изискването
        public void GetByIdAsync_ShouldThrow_WhenPropertyIsInvalid(
            bool isFirstPropertyIdCorrect,
            bool isSecondPropertyIdCorrect,
            PropertyStatus propertyStatus)
        {
            // Arrange
            var firstPropertyId = Guid.NewGuid();
            var secondPropertyId = isSecondPropertyIdCorrect ? Guid.NewGuid() : firstPropertyId;

            var mockedProperties = new List<Property>
            {
                new Property() { Id = firstPropertyId, Status = propertyStatus },
                new Property() { Id = secondPropertyId, Status = propertyStatus }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var testPropertyId = isFirstPropertyIdCorrect ? firstPropertyId : Guid.NewGuid();

            // Act & Assert
            var exception = Assert.ThrowsAsync<TargetInvocationException>(
                async () => await propertyService.GetByIdAsync(testPropertyId, PropertyStatus.Saved)
            );

            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task GetByUserIdAsync_ShouldReturnProperty_WhenUserIsPublisher()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var property = new Property
            {
                Id = propertyId,
                Name = "Luxury Hotel",
                PropertyType = new PropertyType() { Name = "Hotel" },
                Address = "123 Street",
                StarsCount = 5,
                Description = "Great place",
                Status = PropertyStatus.Saved,
                PublisherId = userId,
                Facilities = new List<PropertyFacility>(),
                Photos = new List<PropertyPhoto>
                {
                    new PropertyPhoto { Url = "photo1.jpg" },
                    new PropertyPhoto { Url = "photo2.jpg" }
                },
            };

            var mockedProperties = new List<Property> { property }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetByUserIdAsync(propertyId, userId, PropertyStatus.Saved);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(propertyId));
                Assert.That(result.Name, Is.EqualTo("Luxury Hotel"));
                Assert.That(result.TypeName, Is.EqualTo("Hotel"));
                Assert.That(result.Address, Is.EqualTo("123 Street"));
                Assert.That(result.StarsCount, Is.EqualTo(5));
                Assert.That(result.Description, Is.EqualTo("Great place"));
                Assert.That(result.PhotoUrls.Count(), Is.EqualTo(2));
            });
        }

        [Test]
        [TestCase(false, true, true, PropertyStatus.Published)]  // грешно PropertyId
        //[TestCase(true, false, true, PropertyStatus.Saved)]   // дублирани записи
        [TestCase(true, true, false, PropertyStatus.Published)]  // грешен PublisherId
        [TestCase(true, true, true, PropertyStatus.Deleted)]     // статус под Saved
        public void GetByUserIdAsync_ShouldThrow_WhenConditionsAreNotMet(
            bool isFirstPropertyIdCorrect,
            bool isSecondPropertyIdCorrect,
            bool isPublisherIdCorrect,
            PropertyStatus propertyStatus)
        {
            // Arrange
            var firstPropertyId = Guid.NewGuid();
            var secondPropertyId = isSecondPropertyIdCorrect ? Guid.NewGuid() : firstPropertyId;
            var correctUserId = Guid.NewGuid();

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = firstPropertyId,
                    PublisherId = correctUserId,
                    Status = propertyStatus
                },
                new Property
                {
                    Id = secondPropertyId,
                    PublisherId = correctUserId,
                    Status = propertyStatus
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var testPropertyId = isFirstPropertyIdCorrect ? firstPropertyId : Guid.NewGuid();
            var testUserId = isPublisherIdCorrect ? correctUserId : Guid.NewGuid();

            // Act & Assert
            var exception = Assert.ThrowsAsync<TargetInvocationException>(
                async () => await propertyService.GetByUserIdAsync(testPropertyId, testUserId, PropertyStatus.Saved)
            );

            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task GetForEditByIdAsync_ShouldReturnEditDto_WhenUserIsOwner()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var property = new Property
            {
                Id = propertyId,
                PublisherId = userId,
                Status = PropertyStatus.Published,
                Name = "Editable Hotel",
                PropertyType = new PropertyType() { Id = 1, Name = "Hotel" },
                Address = "Edit Address",
                Description = "Edit Description",
                StarsCount = 4,
                Facilities = new List<PropertyFacility>
                {
                    new PropertyFacility { Facility = new Facility() { Id = 1, Name = "Higher level toilet", IsForAccessibility = true }, RoomId = null },
                    new PropertyFacility { FacilityId = 2, RoomId = new Guid() }
                },
                Photos = new List<PropertyPhoto>
                {
                    new PropertyPhoto { Url = "photo1.jpg", RoomId = null },
                    new PropertyPhoto { Url = "photo2.jpg", RoomId = new Guid() }
                }
            };

            var mockedProperties = new List<Property> { property }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetForEditByIdAsync(propertyId, userId, PropertyStatus.Saved);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(propertyId));
                Assert.That(result.PublisherId, Is.EqualTo(userId));
                Assert.That(result.Name, Is.EqualTo("Editable Hotel"));
                Assert.That(result.Address, Is.EqualTo("Edit Address"));
                Assert.That(result.Description, Is.EqualTo("Edit Description"));
                Assert.That(result.StarsCount, Is.EqualTo(4));
                Assert.That(result.Type.Id, Is.EqualTo(1));
                Assert.That(result.Facilities.Count(), Is.EqualTo(1));
                Assert.That(result.PhotoUrls.Count(), Is.EqualTo(1));
            });
        }

        [Test]
        [TestCase(false, true, true, PropertyStatus.Published)]  // грешно PropertyId
        [TestCase(true, true, false, PropertyStatus.Published)]  // грешен PublisherId
        //[TestCase(true, false, true, PropertyStatus.Saved)]   // дублирани записи
        [TestCase(true, true, true, PropertyStatus.Deleted)]     // статус под Saved
        public void GetForEditByIdAsync_ShouldThrow_WhenConditionsAreNotMet(
            bool isFirstPropertyIdCorrect,
            bool isSecondPropertyIdCorrect,
            bool isPublisherIdCorrect,
            PropertyStatus propertyStatus)
        {
            // Arrange
            var firstPropertyId = Guid.NewGuid();
            var secondPropertyId = isSecondPropertyIdCorrect ? Guid.NewGuid() : firstPropertyId;
            var firstUserId = Guid.NewGuid();
            var secondUserId = Guid.NewGuid();

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = firstPropertyId,
                    PublisherId = firstUserId,
                    Status = propertyStatus
                },
                new Property
                {
                    Id = secondPropertyId,
                    PublisherId = secondPropertyId,
                    Status = propertyStatus
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var testPropertyId = isFirstPropertyIdCorrect ? firstPropertyId : Guid.NewGuid();
            var testUserId = isPublisherIdCorrect ? firstUserId : secondUserId;

            // Act & Assert
            var exception = Assert.ThrowsAsync<TargetInvocationException>(
                async () => await propertyService.GetForEditByIdAsync(testPropertyId, testUserId, PropertyStatus.Saved)
            );

            Assert.That(exception.InnerException, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllProperties_WhenNoFiltersApplied()
        {
            // Arrange
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel A",
                    Address = "Center",
                    StarsCount = 3,
                    Status = PropertyStatus.Published,
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoA.jpg", RoomId = null }
                    },
                    Facilities = new List<PropertyFacility>()
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel B",
                    Address = "Beach",
                    StarsCount = 5,
                    Status = PropertyStatus.Published,
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoB.jpg", RoomId = null }
                    },
                    Facilities = new List<PropertyFacility>()
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                PropertyTypeIds = null,
                RoomTypeIds = null,
                PropertyFacilityIds = null,
                RoomFacilityIds = null,
                PropertyAccessibilityIds = null,
                RoomAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage,
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(2));
                Assert.That(result.Items.Any(p => p.Name == "Hotel A"), Is.True);
                Assert.That(result.Items.Any(p => p.Name == "Hotel B"), Is.True);
                Assert.That(result.TotalCount, Is.EqualTo(2));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnOnlyPropertiesMatchingPropertyTypeIds()
        {
            // Arrange
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel A",
                    PropertyTypeId = 1,
                    Status = PropertyStatus.Published,
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoA.jpg", RoomId = null }
                    },
                    Facilities = new List<PropertyFacility>()
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel B",
                    PropertyTypeId = 2,
                    Status = PropertyStatus.Published,
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoB.jpg", RoomId = null }
                    },
                    Facilities = new List<PropertyFacility>()
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                PropertyTypeIds = new List<int> { 1 },
                RoomTypeIds = null,
                PropertyFacilityIds = null,
                RoomFacilityIds = null,
                PropertyAccessibilityIds = null,
                RoomAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(1));
                Assert.That(result.Items.First().Name, Is.EqualTo("Hotel A"));
                Assert.That(result.TotalCount, Is.EqualTo(1));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnOnlyPropertiesMatchingAllRoomTypeIds()
        {
            // Arrange
            var firstRoomTypeId = 10;
            var secondRoomTypeId = 20;
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel A",
                    Status = PropertyStatus.Published,
                    Rooms = new List<Room>
                    {
                        new Room { RoomTypeId = firstRoomTypeId, IsDeleted = false },
                        new Room { RoomTypeId = secondRoomTypeId, IsDeleted = false }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoA.jpg", RoomId = null }
                    },
                    Facilities = new List<PropertyFacility>()
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel B",
                    Status = PropertyStatus.Published,
                    Rooms = new List<Room>
                    {
                        new Room { RoomTypeId = firstRoomTypeId, IsDeleted = false }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoB.jpg", RoomId = null }
                    },
                    Facilities = new List<PropertyFacility>()
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                RoomTypeIds = new List<int> { firstRoomTypeId, secondRoomTypeId },
                PropertyTypeIds = null,
                PropertyFacilityIds = null,
                RoomFacilityIds = null,
                PropertyAccessibilityIds = null,
                RoomAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(1));
                Assert.That(result.Items.First().Name, Is.EqualTo("Hotel A"));
                Assert.That(result.TotalCount, Is.EqualTo(1));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnOnlyPropertiesMatchingAllPropertyFacilityIds()
        {
            // Arrange
            var firstFacilityId = 100;
            var secondFacilityId = 200;
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel A",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility { FacilityId = firstFacilityId, RoomId = null },
                        new PropertyFacility { FacilityId = secondFacilityId, RoomId = null }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoA.jpg", RoomId = null }
                    }
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel B",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility { FacilityId = firstFacilityId, RoomId = null }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoB.jpg", RoomId = null }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                PropertyFacilityIds = new List<int> { firstFacilityId, secondFacilityId },
                PropertyTypeIds = null,
                RoomTypeIds = null,
                RoomFacilityIds = null,
                PropertyAccessibilityIds = null,
                RoomAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(1));
                Assert.That(result.Items.First().Name, Is.EqualTo("Hotel A"));
                Assert.That(result.TotalCount, Is.EqualTo(1));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnOnlyPropertiesMatchingAllPropertyAccessibilityIds()
        {
            // Arrange
            var firstFacilityId = 100;
            var secondFacilityId = 200;
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Accessible Hotel A",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility
                        {
                            FacilityId = firstFacilityId,
                            RoomId = null,
                            Facility = new Facility { Id = firstFacilityId, IsForAccessibility = true, WhereStatus = WhereStatus.OnlyInCommonArea }
                        },
                        new PropertyFacility
                        {
                            FacilityId = secondFacilityId,
                            RoomId = null,
                            Facility = new Facility { Id = secondFacilityId, IsForAccessibility = true, WhereStatus = WhereStatus.Both }
                        }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoA.jpg", RoomId = null }
                    }
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "NonAccessible Hotel B",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility
                        {
                            FacilityId = firstFacilityId,
                            RoomId = null,
                            Facility = new Facility { Id = firstFacilityId, IsForAccessibility = true, WhereStatus = WhereStatus.OnlyInCommonArea }
                        }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoB.jpg", RoomId = null }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                PropertyAccessibilityIds = new List<int> { firstFacilityId, secondFacilityId },
                PropertyTypeIds = null,
                RoomTypeIds = null,
                PropertyFacilityIds = null,
                RoomFacilityIds = null,
                RoomAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(1));
                Assert.That(result.Items.First().Name, Is.EqualTo("Accessible Hotel A"));
                Assert.That(result.TotalCount, Is.EqualTo(1));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnOnlyPropertiesMatchingAllRoomFacilityIds()
        {
            // Arrange
            var firstFacilityId = 100;
            var secondFacilityId = 200;
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel With All Room Facilities",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility
                        {
                            FacilityId = firstFacilityId,
                            RoomId = Guid.NewGuid(),
                            Room = new Room { IsDeleted = false }
                        },
                        new PropertyFacility
                        {
                            FacilityId = secondFacilityId,
                            RoomId = Guid.NewGuid(),
                            Room = new Room { IsDeleted = false }
                        }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoA.jpg", RoomId = null }
                    }
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel With Partial Facilities",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility
                        {
                            FacilityId = firstFacilityId,
                            RoomId = Guid.NewGuid(),
                            Room = new Room { IsDeleted = false }
                        }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoB.jpg", RoomId = null }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                RoomFacilityIds = new List<int> { firstFacilityId, secondFacilityId },
                PropertyTypeIds = null,
                RoomTypeIds = null,
                PropertyFacilityIds = null,
                PropertyAccessibilityIds = null,
                RoomAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(1));
                Assert.That(result.Items.First().Name, Is.EqualTo("Hotel With All Room Facilities"));
                Assert.That(result.TotalCount, Is.EqualTo(1));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnOnlyPropertiesMatchingAllRoomAccessibilityIds()
        {
            // Arrange
            var firstFacilityId = 100;
            var secondFacilityId = 200;
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Accessible Hotel With All Room Facilities",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility
                        {
                            FacilityId = firstFacilityId,
                            RoomId = Guid.NewGuid(),
                            Room = new Room { IsDeleted = false },
                            Facility = new Facility { Id = firstFacilityId, IsForAccessibility = true, WhereStatus = WhereStatus.OnlyInCommonArea }
                        },
                        new PropertyFacility
                        {
                            FacilityId = secondFacilityId,
                            RoomId = Guid.NewGuid(),
                            Room = new Room { IsDeleted = false },
                            Facility = new Facility { Id = secondFacilityId, IsForAccessibility = true, WhereStatus = WhereStatus.Both }
                        }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoA.jpg", RoomId = null }
                    }
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Partial Accessible Hotel",
                    Status = PropertyStatus.Published,
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility
                        {
                            FacilityId = firstFacilityId,
                            RoomId = Guid.NewGuid(),
                            Room = new Room { IsDeleted = false },
                            Facility = new Facility { Id = firstFacilityId, IsForAccessibility = true, WhereStatus = WhereStatus.OnlyInCommonArea }
                        }
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photoB.jpg", RoomId = null }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                RoomAccessibilityIds = new List<int> { firstFacilityId, secondFacilityId },
                PropertyTypeIds = null,
                RoomTypeIds = null,
                PropertyFacilityIds = null,
                RoomFacilityIds = null,
                PropertyAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(1));
                Assert.That(result.Items.First().Name, Is.EqualTo("Accessible Hotel With All Room Facilities"));
                Assert.That(result.TotalCount, Is.EqualTo(1));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoPropertiesExist()
        {
            // Arrange
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>().BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                PropertyTypeIds = null,
                RoomTypeIds = null,
                PropertyFacilityIds = null,
                RoomFacilityIds = null,
                PropertyAccessibilityIds = null,
                RoomAccessibilityIds = null,
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(0));
                Assert.That(result.TotalCount, Is.EqualTo(0));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        [TestCase(true, true, true, true, true, true, 1)]    // Всички филтри съвпадат → 1 резултат
        [TestCase(false, true, true, true, true, true, 0)]   // Липсва PropertyType → 0
        [TestCase(true, false, true, true, true, true, 0)]   // Липсва RoomType → 0
        [TestCase(true, true, false, true, true, true, 0)]   // Липсва PropertyFacility → 0
        [TestCase(true, true, true, false, true, true, 0)]   // Липсва PropertyAccessibility → 0
        [TestCase(true, true, true, true, false, true, 0)]   // Липсва RoomFacility → 0
        [TestCase(true, true, true, true, true, false, 0)]   // Липсва RoomAccessibility → 0
        [TestCase(false, false, false, false, false, false, 0)] // Всички филтри грешни → 0
        public async Task GetAllAsync_ShouldReturnCorrectResults_WhenMultipleFiltersApplied(
            bool hasCorrectPropertyType,
            bool hasCorrectRoomType,
            bool hasCorrectPropertyFacility,
            bool hasCorrectPropertyAcceessibility,
            bool hasCorrectRoomFacility,
            bool hasCorrectRoomAcceessibility,
            int expectedCount)
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var currentPage = 1;
            var itemsPerPage = 10;

            var propertyTypeId = hasCorrectPropertyType ? 1 : 99;
            var roomTypeId = hasCorrectRoomType ? 10 : 99;
            var propertyFacilityId = hasCorrectPropertyFacility ? 100 : 999;
            var propertyAccessibilityId = hasCorrectPropertyAcceessibility ? 200 : 899;
            var roomFacilityId = hasCorrectRoomFacility ? 300 : 799;
            var roomAccessibilityId = hasCorrectRoomAcceessibility ? 400 : 699;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Filtered Hotel",
                    PropertyTypeId = 1,
                    Status = PropertyStatus.Published,
                    Rooms = new List<Room> { new Room { Id = roomId, RoomTypeId = 10, IsDeleted = false } },
                    Facilities = new List<PropertyFacility>
                    {
                        new PropertyFacility { FacilityId = 100, RoomId = null },
                        new PropertyFacility { FacilityId = 200, RoomId = null },
                        new PropertyFacility { FacilityId = 300, RoomId = roomId, Room = new Room { Id = roomId, RoomTypeId = 10, IsDeleted = false } },
                        new PropertyFacility { FacilityId = 400, RoomId = roomId, Room = new Room { Id = roomId, RoomTypeId = 10, IsDeleted = false } },
                    },
                    Photos = new List<PropertyPhoto>
                    {
                        new PropertyPhoto { Url = "photo.jpg", RoomId = null }
                    }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                PropertyTypeIds = new List<int> { propertyTypeId },
                RoomTypeIds = new List<int> { roomTypeId },
                PropertyFacilityIds = new List<int> { propertyFacilityId },
                PropertyAccessibilityIds = new List<int> { propertyAccessibilityId },
                RoomFacilityIds = new List<int> { roomFacilityId },
                RoomAccessibilityIds = new List<int> { roomAccessibilityId },
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result.Items.Count(), Is.EqualTo(expectedCount));
            Assert.That(result.TotalCount, Is.EqualTo(expectedCount));
            Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
            Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnCorrectPageData_WhenPagingApplied()
        {
            // Arrange
            var currentPage = 2;
            var itemsPerPage = 2;

            var mockedProperties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), Name = "Hotel 1", Address = "Addr 1", StarsCount = 3, Status = PropertyStatus.Published, Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p1.jpg" } }, Facilities = new List<PropertyFacility>() },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 2", Address = "Addr 2", StarsCount = 4, Status = PropertyStatus.Published, Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p2.jpg" } }, Facilities = new List<PropertyFacility>() },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 3", Address = "Addr 3", StarsCount = 5, Status = PropertyStatus.Published, Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p3.jpg" } }, Facilities = new List<PropertyFacility>() },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 4", Address = "Addr 4", StarsCount = 3, Status = PropertyStatus.Published, Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p4.jpg" } }, Facilities = new List<PropertyFacility>() },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 5", Address = "Addr 5", StarsCount = 2, Status = PropertyStatus.Published, Photos = new List<PropertyPhoto>{ new PropertyPhoto { Url = "p5.jpg" } }, Facilities = new List<PropertyFacility>() }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            var queryDto = new PropertyQueryParamsDto
            {
                CurrentPageNumber = currentPage,
                PropertiesPerPage = itemsPerPage
            };

            // Act
            var result = await propertyService.GetAllAsync(queryDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.TotalCount, Is.EqualTo(5));                 // общо 5 имота
                Assert.That(result.Items.Count(), Is.EqualTo(2));              // втора страница с по 2 имота
                Assert.That(result.Items.First().Name, Is.EqualTo("Hotel 3")); // 3-ти и 4-ти имот
                Assert.That(result.Items.Last().Name, Is.EqualTo("Hotel 4"));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllForAdminAsync_ShouldReturnAllNonDeletedProperties()
        {
            // Arrange
            var currentPage = 1;
            var itemsPerPage = 10;

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel A",
                    Address = "Center",
                    Status = PropertyStatus.Published,
                    PropertyType = new PropertyType { Name = "Type A" },
                    Publisher = new ApplicationUser { Email = "publisherA@example.com" }
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel B",
                    Address = "Beach",
                    Status = PropertyStatus.Saved,
                    PropertyType = new PropertyType { Name = "Type B" },
                    Publisher = new ApplicationUser { Email = "publisherB@example.com" }
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel C",
                    Address = "Mountain",
                    Status = PropertyStatus.Deleted,  // Този трябва да се филтрира
                    PropertyType = new PropertyType { Name = "Type C" },
                    Publisher = new ApplicationUser { Email = "publisherC@example.com" }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetAllForAdminAsync(currentPage, itemsPerPage);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(2)); // Deleted е изключен
                Assert.That(result.Items.Any(p => p.Name == "Hotel A"), Is.True);
                Assert.That(result.Items.Any(p => p.Name == "Hotel B"), Is.True);
                Assert.That(result.Items.Any(p => p.Name == "Hotel C"), Is.False);
                Assert.That(result.TotalCount, Is.EqualTo(2));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllForAdminAsync_ShouldReturnCorrectPageData()
        {
            // Arrange
            var currentPage = 2;
            var itemsPerPage = 2;

            var mockedProperties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), Name = "Hotel 1", Status = PropertyStatus.Published, PropertyType = new PropertyType { Name = "Type 1" }, Publisher = new ApplicationUser { Email = "p1@mail.com" } },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 2", Status = PropertyStatus.Saved, PropertyType = new PropertyType { Name = "Type 2" }, Publisher = new ApplicationUser { Email = "p2@mail.com" } },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 3", Status = PropertyStatus.Pending, PropertyType = new PropertyType { Name = "Type 3" }, Publisher = new ApplicationUser { Email = "p3@mail.com" } },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 4", Status = PropertyStatus.Rejected, PropertyType = new PropertyType { Name = "Type 4" }, Publisher = new ApplicationUser { Email = "p4@mail.com" } },
                new Property { Id = Guid.NewGuid(), Name = "Hotel 5", Status = PropertyStatus.Published, PropertyType = new PropertyType { Name = "Type 5" }, Publisher = new ApplicationUser { Email = "p5@mail.com" } }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetAllForAdminAsync(currentPage, itemsPerPage);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.TotalCount, Is.EqualTo(5));                 // Всички без Deleted
                Assert.That(result.Items.Count(), Is.EqualTo(2));              // Втора страница с 2 елемента
                Assert.That(result.Items.First().Name, Is.EqualTo("Hotel 3")); // Очакваме 3-ти и 4-ти елемент
                Assert.That(result.Items.Last().Name, Is.EqualTo("Hotel 4"));
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllForAdminAsync_ShouldReturnEmpty_WhenAllPropertiesAreDeleted()
        {
            // Arrange
            var currentPage = 1;
            var itemsPerPage = 5;

            var mockedProperties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), Name = "Deleted Hotel 1", Status = PropertyStatus.Deleted, PropertyType = new PropertyType { Name = "Type X" }, Publisher = new ApplicationUser { Email = "d1@mail.com" } },
                new Property { Id = Guid.NewGuid(), Name = "Deleted Hotel 2", Status = PropertyStatus.Deleted, PropertyType = new PropertyType { Name = "Type Y" }, Publisher = new ApplicationUser { Email = "d2@mail.com" } }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetAllForAdminAsync(currentPage, itemsPerPage);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Items.Count(), Is.EqualTo(0));  // Няма резултати
                Assert.That(result.TotalCount, Is.EqualTo(0));     // Броят също е 0
                Assert.That(result.CurrentPageNumber, Is.EqualTo(currentPage));
                Assert.That(result.ItemsPerPage, Is.EqualTo(itemsPerPage));
            });
        }

        [Test]
        public async Task GetAllForAdminAsync_ShouldMapNavigationPropertiesCorrectly()
        {
            // Arrange
            var currentPage = 1;
            var itemsPerPage = 5;

            var propertyId = Guid.NewGuid();

            var mockedProperties = new List<Property>
            {
                new Property
                {
                    Id = propertyId,
                    Name = "Mapped Hotel",
                    Address = "Some Address",
                    Status = PropertyStatus.Published,
                    PropertyType = new PropertyType { Name = "Resort" },
                    Publisher = new ApplicationUser { Email = "publisher@example.com" }
                }
            }.BuildMock();

            mockedRepository
                .Setup(r => r.AllAsNoTracking<Property>())
                .Returns(mockedProperties);

            // Act
            var result = await propertyService.GetAllForAdminAsync(currentPage, itemsPerPage);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            var item = result.Items.First();
            Assert.Multiple(() =>
            {
                Assert.That(item.Id, Is.EqualTo(propertyId));
                Assert.That(item.Name, Is.EqualTo("Mapped Hotel"));
                Assert.That(item.Address, Is.EqualTo("Some Address"));
                Assert.That(item.TypeName, Is.EqualTo("Resort"));               // Навигационно свойство
                Assert.That(item.Publisher, Is.EqualTo("publisher@example.com")); // Навигационно свойство
                Assert.That(item.Status, Is.EqualTo(PropertyStatus.Published.ToString()));
            });
        }

        [Test]
        [TestCase(PropertyStatus.Saved)]
        [TestCase(PropertyStatus.Pending)]
        [TestCase(PropertyStatus.Published)]
        [TestCase(PropertyStatus.Rejected)]
        [TestCase(PropertyStatus.Deleted)]
        public async Task ChangePropertyStatus_ShouldUpdateStatus_WhenPropertyExistsAndIsNotDeleted(PropertyStatus newStatus)
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            var existingProperty = new Property
            {
                Id = propertyId,
                Status = PropertyStatus.Saved // текущият статус е Saved
            };

            mockedRepository
                .Setup(r => r.GetByIdAsync<Property>(propertyId))
                .ReturnsAsync(existingProperty);

            // Act
            await propertyService.ChangePropertyStatus(propertyId, newStatus);

            // Assert
            if (newStatus != PropertyStatus.Saved) // ако новият е различен
            {
                Assert.That(existingProperty.Status, Is.EqualTo(newStatus));
                mockedRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
            }
            else
            {
                // ако е същият, не трябва да се вика SaveChangesAsync
                mockedRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
            }
        }

        [Test]
        public async Task ChangePropertyStatus_ShouldDoNothing_WhenPropertyIsDeleted()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            
            var existingProperty = new Property
            {
                Id = propertyId,
                Status = PropertyStatus.Deleted
            };

            mockedRepository
                .Setup(r => r.GetByIdAsync<Property>(propertyId))
                .ReturnsAsync(existingProperty);

            // Act
            await propertyService.ChangePropertyStatus(propertyId, PropertyStatus.Published);

            // Assert
            Assert.That(existingProperty.Status, Is.EqualTo(PropertyStatus.Deleted)); // без промяна
            mockedRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task ChangePropertyStatus_ShouldDoNothing_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = Guid.NewGuid();

            mockedRepository
                .Setup(r => r.GetByIdAsync<Property>(propertyId))
                .ReturnsAsync((Property)null!);

            // Act
            await propertyService.ChangePropertyStatus(propertyId, PropertyStatus.Published);

            // Assert
            mockedRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
