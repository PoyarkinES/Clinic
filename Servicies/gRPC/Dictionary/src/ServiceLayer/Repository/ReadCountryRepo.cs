using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDataLayerService;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using ServiceLayer.Repository.Interfaces;

namespace ServiceLayer.Repository
{
    public class ReadCountryRepo(ILoggerFactory loggerFactory, ReadDbContext dbContext) : IReadCountryRepo
    {

        private readonly ILogger<ReadCountryRepo> _logger = loggerFactory.CreateLogger<ReadCountryRepo>();
        private readonly ReadDbContext _dbContext = dbContext ?? throw new ArgumentNullException();

        public async Task<IResponse<CountryDto>> GetCountryAsync(int countryId, CancellationToken cancellationToken)
        {
            try
            {
                if (countryId != default)
                {
                    return new Response<CountryDto>(message: string.Empty, errors: $"Invalid country ID value {countryId}.", status: false, result: null);
                }

                var item = await _dbContext.countries.FindAsync(countryId).ConfigureAwait(false);
                if (item == null)
                {
                    return new Response<CountryDto>(
                        message: "Country recived successfully",
                        errors: string.Empty,
                        status: true,
                        result: (CountryDto)item);
                }

                return new Response<CountryDto>(
                    message: $"Coutry not found by id {countryId}",
                    errors: string.Empty,
                    status: false,
                    result: null
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}.");
                return new Response<CountryDto>(
                    message: $"Error: {ex.Message}.",
                    errors: ex.Message,
                    status: false,
                    result: null
                );
            }
        }

        public async Task<IResponse<List<CountryDto>>> GetAllCountryAsync(CancellationToken cancellationToken)
        {
            return new Response<List<CountryDto>>(
                message: "Countries recived successfully",
                errors: string.Empty,
                status: true,
                result: await _dbContext.countries.AsNoTracking().Cast<CountryDto>().ToListAsync().ConfigureAwait(false));
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Save changes complite.");
        }

        public void Dispose() => _dbContext.Dispose();
    }
}
