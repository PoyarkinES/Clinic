using MediatR;
using Service.Extensions;
using ServiceLayer.Command;

namespace Service.Services
{
    /// <summary>
    /// Interface of modify country service performer 
    /// </summary>
    public interface IWriteCountrier
    {
        /// <summary>
        /// Create new country
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<CreateCountryResponse> CreateCountryAsync(CreateCountryRequest request, CancellationToken cancellation);

        /// <summary>
        /// Update country by Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<UpdateCountryResponse> UpdateCountryAsync(UpdateCountryRequest request, CancellationToken cancellation);

        /// <summary>
        /// Delete country by Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<DeleteCountryResponse> DeleteCountryAsync(DeleteCountryRequest request, CancellationToken cancellation);
    }

    /// <summary>
    /// Performer of country service method
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="mediator"></param>
    /// <param name="servicelayer"></param>
    public class WriteCountrier(ILoggerFactory loggerFactory, IMediator mediator) : IWriteCountrier
    {
        private readonly ILogger<WriteCountrier> _logger = loggerFactory.CreateLogger<WriteCountrier>();
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Create new country
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<CreateCountryResponse> CreateCountryAsync(CreateCountryRequest request, CancellationToken cancellation)
        {
            try
            {
                var command = new CreateCountryCommand(Guid.NewGuid(), request.Isocode, request.Fullname,
                    request.Shortname, request.Alfa2Code, request.Alfa3Code);
                var response = await _mediator.Send(command, cancellation);

                if (response.Status == true)
                {
                    _logger.LogInformation($"Country created successfully with ID: {response.Result.CountryId}");
                }

                return new CreateCountryResponse()
                {
                    Country = ExtensionsMethod.MapToCountryModel(response.Result),
                    Message = response.Status ? response.Message : response.Errors,
                    Success = response.Status
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error create new country with ID: {request.Fullname}");

                return new CreateCountryResponse
                {
                    Success = false,
                    Message = $"Error deleting product: {ex.Message}."
                };
            }
        }

        /// <inheritdoc/>
        public async Task<DeleteCountryResponse> DeleteCountryAsync(DeleteCountryRequest request, CancellationToken cancellation)
        {
            try
            {
                var command = new DeleteCountryCommand(Guid.NewGuid(), request.CountryId);
                var response = await _mediator.Send(command, cancellation);

                if (response.Status == true)
                {
                    _logger.LogInformation($"Country deleted successfully with ID: {request.CountryId}");
                }

                return new DeleteCountryResponse
                {
                    Message = response.Status ? response.Message : response.Errors,
                    Success = response.Status
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID: {request.CountryId}");

                return new DeleteCountryResponse
                {
                    Success = false,
                    Message = $"Error deleting product: {ex.Message}."
                };
            }
        }

        /// <inheritdoc/>
        public async Task<UpdateCountryResponse> UpdateCountryAsync(UpdateCountryRequest request, CancellationToken cancellation)
        {
            try
            {
                var command = new UpdateCountryCommand(Guid.NewGuid(), request.CountryId, request.Isocode, 
                    request.Fullname, request.Shortname, request.Alfa2Code, request.Alfa3Code);

                var response = await _mediator.Send(command, cancellation);

                if (response.Status == true)
                {
                    _logger.LogInformation($"Country updated successfully with ID: {response.Result.CountryId}");
                }

                return new UpdateCountryResponse
                {
                    Country = ExtensionsMethod.MapToCountryModel(response.Result),
                    Message = response.Status ? response.Message : response.Errors,
                    Success = response.Status
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID: {ProductId}", request.CountryId);

                return new UpdateCountryResponse
                {
                    Success = false,
                    Message = $"Error updating product: {ex.Message}"
                };
            }
        }
    }
}
