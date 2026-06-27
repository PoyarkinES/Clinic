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
    public class DeleteCountryCommandHandler(LoggerFactory logger, IWriteCountryRepo repository) : IRequestHandler<DeleteCountryCommand, IResponse<Unit?>>
    {
        private ILogger<DeleteCountryCommandHandler> _logger = logger.CreateLogger<DeleteCountryCommandHandler>();
        private IWriteCountryRepo _repository = repository;

        public async Task<IResponse<Unit?>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Deleting country {request.CountryId}");
                var result = await _repository.DeleteCountryAsync(request, cancellationToken);
                await _repository.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message}");
                return new Response<Unit?>(
                    message: "Deleting country has errors.",
                    errors: ex.Message,
                    status: false,
                    result: null
                );
            }
        }
    }
}
