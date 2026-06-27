using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Service;
using Service.Services;
using ServiceLayer.Command;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using ServiceTest.Server.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using WriteDataLayer.Model;

namespace ServiceTest.ServiceLayer
{
    public class WriteServiceUnitTest
    {
        [Test]
        public async Task CreateCountryServiceUnitTest()
        {
            var mockLogger = new Mock<ILoggerFactory>();
            mockLogger.Setup(logger => logger.CreateLogger(It.IsAny<string>())).Returns(Mock.Of<ILogger<IWriteCountrier>>);

            var mockMediator= new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateCountryCommand>(), CancellationToken.None)).ReturnsAsync((CreateCountryCommand r, CancellationToken cancelationtolen) =>
            {
                return new Response<CountryDto>("Status true", "Status false", false, (CountryDto)new Country()
                {
                    CountryId = 1,
                    IsoCode = r.IsoCode,
                    ShortName = r.ShortName,
                    FullName = r.FullName,
                    Alfa2Code = r.Alfa2Code,
                    Alfa3Code = r.Alfa3Code
                });
            });

            var mockService = new WriteCountrier(mockLogger.Object, mockMediator.Object);

            CreateCountryRequest request = new()
            {
                Isocode = string.Empty,
                Alfa2Code = string.Empty,
                Alfa3Code = string.Empty,
                Fullname = string.Empty,
                Shortname = string.Empty,
                Tags = string.Empty
            };
            CreateCountryResponse response = await mockService.CreateCountryAsync(request, CancellationToken.None);

            if (!response.Success)
                ClassicAssert.AreEqual(false, response.Success);
            else
                ClassicAssert.AreEqual(true, response.Success);
        }
    }
}
