using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Service;
using Service.Extensions;
using Service.Services;
using ServiceTest.Server.Helpers;

namespace ServiceTest.Server
{
    public class CountryServerUnitTests
    {
        [Test]
        public async Task CreateCountryServerUnitTest()
        {
            var mockCounter = new Mock<IWriteCountrier>();
            mockCounter.Setup(m => m.CreateCountryAsync(It.IsAny<CreateCountryRequest>(), CancellationToken.None)).ReturnsAsync((CreateCountryRequest r, CancellationToken cancelationtolen) =>
            {
                return new CreateCountryResponse()
                {
                    Country = new CountryModel()
                    {
                        CountryId = 1,
                        Isocode = r.Isocode,
                        Alfa2Code = r.Alfa2Code,
                        Alfa3Code = r.Alfa3Code,
                        Fullname = r.Fullname,
                        Shortname = r.Shortname,
                        Tags = r.Tags
                    },
                    Message = $"Country created successfully with ID: {1}",
                    Success = true
                };
            });

            var mockService = new WriteCountryService(mockCounter.Object);

            CreateCountryRequest request = new()
            {
                Isocode = string.Empty,
                Alfa2Code = string.Empty,
                Alfa3Code = string.Empty,
                Fullname = string.Empty,
                Shortname = string.Empty,
                Tags = string.Empty
            };

            CreateCountryResponse response = await mockService.CreateCountryAsync(request, new TestServerCallContext([], default));

            if(!response.Success)
                ClassicAssert.AreEqual($"Check complite with errors.", response.Message);
            else
                ClassicAssert.AreEqual($"Country created successfully with ID: {1}", response.Message);
        }

        [Test]
        public async Task UpdateCountryServerUnitTest()
        {
            var mockCounter = new Mock<IWriteCountrier>();
            mockCounter.Setup(m => m.UpdateCountryAsync(It.IsAny<UpdateCountryRequest>(), CancellationToken.None)).ReturnsAsync((UpdateCountryRequest r, CancellationToken cancelationtolen) => 
            {
                return new UpdateCountryResponse()
                {
                    Country = new CountryModel()
                    {
                        CountryId = r.CountryId,
                        Isocode = r.Isocode,
                        Alfa2Code = r.Alfa2Code,
                        Alfa3Code = r.Alfa3Code,
                        Fullname = r.Fullname,
                        Shortname = r.Shortname,
                        Tags = r.Tags
                    },
                    Message = $"Country updated successfully with ID: {r.CountryId}",
                    Success = true
                };
            });
            var mockService = new WriteCountryService(mockCounter.Object);

            UpdateCountryRequest request = new()
            {
                CountryId = 1,
                Isocode = string.Empty,
                Alfa2Code = string.Empty,
                Alfa3Code = string.Empty,
                Fullname = string.Empty,
                Shortname = string.Empty,
                Tags = string.Empty
            };

            UpdateCountryResponse response = await mockService.UpdateCountryAsync(request, new TestServerCallContext([], default));

            if (!response.Success)
                ClassicAssert.AreEqual($"Check complite with errors.", response.Message);
            else
                ClassicAssert.AreEqual($"Country updated successfully with ID: {response.Country.CountryId}", response.Message);
        }

        [Test]
        public async Task DeleteCountryServerUnitTest()
        {
            var mockCounter = new Mock<IWriteCountrier>();
            mockCounter.Setup(m => m.DeleteCountryAsync(It.IsAny<DeleteCountryRequest>(), CancellationToken.None)).ReturnsAsync((DeleteCountryRequest r, CancellationToken cancelationtolen) =>
            {
                return new DeleteCountryResponse()
                {
                    Message = $"Country {r.CountryId} deleted successfully.",
                    Success = true
                };
            });
            var mockService = new WriteCountryService(mockCounter.Object);

            DeleteCountryRequest request = new()
            {
                CountryId = 1
            };

            DeleteCountryResponse response = await mockService.DeleteCountryAsync(request, new TestServerCallContext([], default));

            if (!response.Success)
                ClassicAssert.AreEqual($"Check complite with errors.", response.Message);
            else
                ClassicAssert.AreEqual($"Country {request.CountryId} deleted successfully.", response.Message);
        }

        [Test]
        public async Task GetCountryServerUnitTest()
        {
            var mockCounter = new Mock<IReadCountrier>();
            mockCounter.Setup(m => m.GetCountryAsync(It.IsAny<GetCountryRequest>(), CancellationToken.None)).ReturnsAsync((GetCountryRequest r, CancellationToken cancelationtolen) =>
            {
                return new GetCountryResponse()
                {
                    Country = new CountryModel()
                    {
                        CountryId = r.CountryId
                    },
                    Message = string.Empty,
                    Success = true
                };
            });
            var mockService = new ReadCountryService(mockCounter.Object);

            GetCountryRequest request = new()
            {
                CountryId = 1
            };

            GetCountryResponse response = await mockService.GetCountryAsync(request, new TestServerCallContext([], default));

            if (!response.Success)
                ClassicAssert.AreEqual($"Check complite with errors.", response.Message);
            else
                ClassicAssert.AreEqual(string.Empty, response.Message);
        }

        [Test]
        public async Task GetAllCountryServerUnitTest()
        {
            var mockCounter = new Mock<IReadCountrier>();
            mockCounter.Setup(m => m.GetAllCountriesAsync(It.IsAny<GetCountriesRequest>(), CancellationToken.None)).ReturnsAsync((GetCountriesRequest r, CancellationToken cancelationtolen) =>
            {
                return new GetCountriesResponse()
                {
                    Country = { new List<CountryModel>().ToList() },
                    TotalCount = 0,
                    Message = string.Empty,
                    Success = true
                };
            });
            var mockService = new ReadCountryService(mockCounter.Object);

            GetCountriesRequest request = new()
            {
                Page = 1,
                PageSize = 1
            };

            GetCountriesResponse response = await mockService.GetCountriesAsync(request, new TestServerCallContext([], default));

            if (!response.Success)
                ClassicAssert.AreEqual($"Check complite with errors.", response.Message);
            else
                ClassicAssert.AreEqual(string.Empty, response.Message);
        }
    }
}
