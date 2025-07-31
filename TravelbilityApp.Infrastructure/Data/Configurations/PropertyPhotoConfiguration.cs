using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.Constants.SeedDataConstants;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    public class PropertyPhotoConfiguration : IEntityTypeConfiguration<PropertyPhoto>
    {
        public void Configure(EntityTypeBuilder<PropertyPhoto> builder)
        {
            builder.HasData(GeneratePropertiesPhotos());
        }

        private IEnumerable<PropertyPhoto> GeneratePropertiesPhotos()
        {
            var propertiesPhotos = new HashSet<PropertyPhoto>();

            string[] firstPropertyPhotoUrls = [
                "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-1-637327495922065801.jpg",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/117749607.jpg?k=a7377ffe413a077a665056fa5058bdf9d3ff47000e8f703d59192535b92a198e&o=",
                "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-2-637327495922065801.jpg",
                "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-4-637327495922065801.jpg",
                "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-3-637327495922065801.jpg"
                ];

            foreach (var url in firstPropertyPhotoUrls)
            {
                var propertyPhoto = new PropertyPhoto()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(FirstPropertyId),
                    Url = url,
                };

                propertiesPhotos.Add(propertyPhoto);
            }

            string[] firstPropertyFirstRoomPhotoUrls = [
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568077.jpg?k=90b8087fad5a98e35d8c991681db1b308ba69e3c92c44ec5e79bbfbbb4d10343&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568080.jpg?k=c809ee1a5895d43812c9051d967825ba872b84157dac7163a66390991160ca09&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642564429.jpg?k=adc6bcbad98434f49672fb7a52c62ade54673fb1866ceecf04c24ebf00691ace&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568129.jpg?k=6231e8c3ce340683d6a5ea3a4c31319573f6fcb70e768fc8daac4dd4c30371b2&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568137.jpg?k=82e16dadd5a3d9e3c23afcd917a8b061f71d537da515aa9bac7b920088738391&o="
            ];

            foreach (var url in firstPropertyFirstRoomPhotoUrls)
            {
                var propertyPhoto = new PropertyPhoto()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(FirstPropertyId),
                    RoomId = Guid.Parse(FirstPropertyFirstRoomId),
                    Url = url,
                };

                propertiesPhotos.Add(propertyPhoto);
            }

            string[] firstPropertySecondRoomPhotoUrls = [
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568934.jpg?k=af600b8487d4f95512b7af7c3de4a42a5dd340445bb98c5f3ade0a834af8ec61&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568942.jpg?k=e74666193c057d13d8c57edf9eaa2b0f6f0c3e6be0b31b99cf088eda27de7afc&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568957.jpg?k=9a61b99aecfb26925d9e04b1e09b6ff791da5121f027acc671ac23ceae1f5783&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642564429.jpg?k=adc6bcbad98434f49672fb7a52c62ade54673fb1866ceecf04c24ebf00691ace&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568863.jpg?k=a175cad3389263f9e0bbd2460d55aeeef4fa932cbbfaabf8c581de08f9680d8d&o="
            ];

            foreach (var url in firstPropertySecondRoomPhotoUrls)
            {
                var propertyPhoto = new PropertyPhoto()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(FirstPropertyId),
                    RoomId = Guid.Parse(FirstPropertySecondRoomId),
                    Url = url,
                };

                propertiesPhotos.Add(propertyPhoto);
            }

            string[] secondPropertyPhotoUrls = [
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105483.jpg?k=d381f915f7e9311bf64734cbb87742eec40a834264ba1c1237f7e18f7276060a&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/513203867.jpg?k=d2899a2b7c4e9951abb20c934dfefbcc7c7e60c025ee3feca3f7483913ab877c&o=&hp=1",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105543.jpg?k=adbc5414747997bd2c7d69ec85b08d84fd379719f210e610d60fa75220500abb&o=&hp=1",
                "https://cf.bstatic.com/xdata/images/hotel/max300/484105537.jpg?k=e47db70ecd10f46adc980421e2495483a18a256147949ec75fecc5fb108a87de&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max500/484806261.jpg?k=82afaa65c2e8396120999c45e40bb3ec760a91a793f9f93e85abc1e678f0a7a1&o="
            ];

            foreach (var url in secondPropertyPhotoUrls)
            {
                var propertyPhoto = new PropertyPhoto()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(SecondPropertyId),
                    Url = url,
                };

                propertiesPhotos.Add(propertyPhoto);
            }

            string[] secondPropertyFirstRoomPhotoUrls = [
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105547.jpg?k=70acbfe097e254504627d28cb087caa803f79d54c9c93036d2b9bb954a1f2b8c&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105593.jpg?k=8927a514e029b26f105f355056c4332e2e103162729faae719189f4f8c9b9079&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105598.jpg?k=a2c196492a682c671ccafff03e52eae0bcde337f883ad6a5068082063e5c6e01&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105552.jpg?k=399c0e58cb01ed3c1d8f645e51855a25f31007bed9439f43119e51ccf0971b8b&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105561.jpg?k=c2d2be660be85b2dc70294c906194dc75e042342e1a928192d4dd2adb1336c84&o="
            ];

            foreach (var url in secondPropertyFirstRoomPhotoUrls)
            {
                var propertyPhoto = new PropertyPhoto()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(SecondPropertyId),
                    RoomId = Guid.Parse(SecondPropertyFirstRoomId),
                    Url = url,
                };

                propertiesPhotos.Add(propertyPhoto);
            }

            string[] thirdPropertyPhotoUrls = [
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398767814.jpg?k=d9962306000f2dfa5aab8437d8b693ee3973b24b5d8e21b0acf6ea0895394d15&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398767820.jpg?k=6724f5c42976d9f85d6ea43dd55ac74f19f39236bda8a94b22172f5c73983e7d&o=&hp=1",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398915704.jpg?k=331223231b2f2dcbcb9e6aecbc74f8f0b2e2020b288c150b60cfc34b447fa32e&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398915707.jpg?k=888ffd15c625eead51e21c81f8095e705bba850546c580cf272213135913ee16&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398767817.jpg?k=e625a8c84d8c55e279931f86ac0bad61436de6f200203801138490f956413528&o=&hp=1"
            ];

            foreach (var url in thirdPropertyPhotoUrls)
            {
                var propertyPhoto = new PropertyPhoto()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(ThirdPropertyId),
                    Url = url,
                };
                propertiesPhotos.Add(propertyPhoto);
            }

            string[] thirdPropertyFirstRoomPhotoUrls = [
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788806.jpg?k=59ada28a447085e1328c22f48683ae09006e132f368ed5042157dd29b1c5576a&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788793.jpg?k=f4ea5a1232e4bc6911c85019239bdcd36fcaed2006bba78a97f9f598c4f05ce2&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788808.jpg?k=bdc82b35918e4f921bca93cf7979baedefba3016b51d820f51b67b76d32da45b&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788815.jpg?k=a6957bfcf1b646c76ea9a2dc946898bb9d077cf8284f1056af1916a861197f8c&o=",
                "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788797.jpg?k=e78b542c382a6c006bb6393db978c8dd1b5e9279e71c65cd6abd02b069bba9c4&o="
            ];

            foreach (var url in thirdPropertyFirstRoomPhotoUrls)
            {
                var propertyPhoto = new PropertyPhoto()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(ThirdPropertyId),
                    RoomId = Guid.Parse(ThirdPropertyFirstRoomId),
                    Url = url,
                };

                propertiesPhotos.Add(propertyPhoto);
            }

            return propertiesPhotos;
        }
    }
}
