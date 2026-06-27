using MediatR;
using Service.Extensions;
using ServiceLayer.Queries;

namespace Service.Services
{
    /// <summary>
    /// Interface of read country service performer 
    /// </summary>
    public interface IReadCountrier
    {
        /// <summary>
        /// Get country by Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancelationtolen"></param>
        /// <returns></returns>
        Task<GetCountryResponse> GetCountryAsync(GetCountryRequest request, CancellationToken cancelationtolen);

        /// <summary>
        /// Get al countries
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancelationtolen"></param>
        /// <returns></returns>
        Task<GetCountriesResponse> GetAllCountriesAsync(GetCountriesRequest request, CancellationToken cancelationtolen);

    }

    /// <summary>
    /// Performer of read country service method
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="mediator"></param>
    /// <param name="servicelayer"></param>
    public class ReadCountrier(ILoggerFactory loggerFactory, IMediator mediator) : IReadCountrier
    {
        private readonly ILogger<ReadCountrier> _logger = loggerFactory.CreateLogger<ReadCountrier>();
        private readonly IMediator _mediator = mediator;


        /// <inheritdoc/>
        public async Task<GetCountriesResponse> GetAllCountriesAsync(GetCountriesRequest request, CancellationToken cancelationtolen)
        {
            _logger.LogInformation($"Getting countries...");
            var query = new GetAllCountriesQuery();
            var response = await _mediator.Send(query);

            return new GetCountriesResponse()
            {
                Country = { response.Result.Select(ExtensionsMethod.MapToCountryModel).ToList() },
                TotalCount = response.Result.Count(),
                Message = response.Status ? response.Message : response.Errors,
                Success = response.Status
            };
        }

        /// <inheritdoc/>
        public async Task<GetCountryResponse> GetCountryAsync(GetCountryRequest request, CancellationToken cancelationtolen)
        {
            _logger.LogInformation($"Getting country by id: {request.CountryId}");
            var query = new GetCountryQuery();
            var response = await _mediator.Send(query);

            return new GetCountryResponse()
            {
                Country = ExtensionsMethod.MapToCountryModel(response.Result),
                Message = response.Status ? response.Message : response.Errors,
                Success = response.Status
            };
        }
    }
}
