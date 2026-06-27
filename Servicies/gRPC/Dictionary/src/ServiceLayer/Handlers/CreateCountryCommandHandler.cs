using MediatR;
using Microsoft.Extensions.Logging;
using ServiceLayer.Command;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using ServiceLayer.Repository.Interfaces;

namespace ServiceLayer.Handlers
{
    public class CreateCountryCommandHandler(LoggerFactory logger, IWriteCountryRepo repository) : IRequestHandler<CreateCountryCommand, IResponse<CountryDto>>
    {
        private ILogger<CreateCountryCommandHandler> _logger = logger.CreateLogger<CreateCountryCommandHandler>();
        private IWriteCountryRepo _repository = repository;

        public async Task<IResponse<CountryDto>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Creating new country");
                var result = await _repository.CreateCountryAsync(request, cancellationToken);
                await _repository.CommitAsync(cancellationToken);

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
