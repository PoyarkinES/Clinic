using MediatR;
using Microsoft.Extensions.Logging;
using ServiceLayer.Command;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using ServiceLayer.Queries;
using ServiceLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Handlers
{
    public class GetAllCountriesQueryHandler(LoggerFactory logger, IReadCountryRepo repository) : IRequestHandler<GetAllCountriesQuery, IResponse<List<CountryDto>>>
    {
        private ILogger<GetAllCountriesQueryHandler> _logger = logger.CreateLogger<GetAllCountriesQueryHandler>();
        private IReadCountryRepo _repository = repository;

        public async Task<IResponse<List<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Creating new country");
                var result = await _repository.GetAllCountryAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message}");
                return new Response<List<CountryDto>>(
                    message: "Creating country has errors.",
                    errors: ex.Message,
                    status: false,
                    result: null
                );
            }
        }
    }
}
