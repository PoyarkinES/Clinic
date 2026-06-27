using Grpc.Core;

namespace Service.Services
{
    /// <summary>
    /// Dictionary grpc service for read
    /// </summary>
    /// <param name="readCountrier"></param>
    public class ReadCountryService(IReadCountrier readCountrier) : CountryProto.CountryProtoBase
    {
        private readonly IReadCountrier _readCountrier = readCountrier;

        /// <summary>
        /// Get country by CountryId
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetCountryResponse> GetCountryAsync(GetCountryRequest request, ServerCallContext context)
        {
            return await _readCountrier.GetCountryAsync(request, context.CancellationToken);
        }

        /// <summary>
        /// Get all ciuntries from dictionary
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetCountriesResponse> GetCountriesAsync(GetCountriesRequest request, ServerCallContext context)
        {
            return await _readCountrier.GetAllCountriesAsync(request, context.CancellationToken);
        }
    }
}
