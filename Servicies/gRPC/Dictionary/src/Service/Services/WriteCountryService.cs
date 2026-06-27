using Grpc.Core;

namespace Service.Services
{
    /// <summary>
    /// Dictionary grpc service for modify
    /// </summary>
    /// <param name="writeCountrier"></param>
    public class WriteCountryService(IWriteCountrier writeCountrier) : CountryProto.CountryProtoBase
    {
        private readonly IWriteCountrier _writeCountrier = writeCountrier;

        /// <summary>
        /// Create new country
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<CreateCountryResponse> CreateCountryAsync(CreateCountryRequest request, ServerCallContext context)
        {

            return await _writeCountrier.CreateCountryAsync(request, context.CancellationToken);
        }

        /// <summary>
        /// Update country by countryId
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<UpdateCountryResponse> UpdateCountryAsync(UpdateCountryRequest request, ServerCallContext context)
        {
            return await _writeCountrier.UpdateCountryAsync(request, context.CancellationToken);
        }

        /// <summary>
        /// Delete country by countryId
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<DeleteCountryResponse> DeleteCountryAsync(DeleteCountryRequest request, ServerCallContext context)
        {
            return await _writeCountrier.DeleteCountryAsync(request, context.CancellationToken);
        }
    }
}
