using MediatR;
using Microsoft.Extensions.Logging;
using ServiceLayer.Command;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using ServiceLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Handlers
{
    public class UpdateCountryCommandHandler(LoggerFactory logger, IWriteCountryRepo repository) : IRequestHandler<UpdateCountryCommand, IResponse<CountryDto>>
    {
        private ILogger<UpdateCountryCommandHandler> _logger = logger.CreateLogger<UpdateCountryCommandHandler>();
        private IWriteCountryRepo _repository = repository;

        public async Task<IResponse<CountryDto>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Updating country {request.CountryId}");
                var result = await _repository.UpdateCountryAsync(request, cancellationToken);
                await _repository.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message}");
                return new Response<CountryDto>(
                    message: "Updating country has errors.",
                    errors: ex.Message,
                    status: false,
                    result: null
                );
            }
        }
    }
}
