using MediatR;
using Microsoft.Extensions.Logging;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using ServiceLayer.Queries;
using ServiceLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Handlers
{
    public class GetCountryQueryHandler(LoggerFactory logger, IReadCountryRepo repository) : IRequestHandler<GetCountryQuery, IResponse<CountryDto>>
    {
        private ILogger<GetCountryQueryHandler> _logger = logger.CreateLogger<GetCountryQueryHandler>();
        private IReadCountryRepo _repository = repository;

        public async Task<IResponse<CountryDto>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Creating new country");
                var result = await _repository.GetCountryAsync(request.Id, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message}");
                return new Response<CountryDto>(
                    message: "Creating country has errors.",
                    errors: ex.Message,
                    status: false,
                    result: null
                );
            }
        }
    }
}
