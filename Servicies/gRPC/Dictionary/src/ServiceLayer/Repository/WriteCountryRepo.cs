using MediatR;
using Microsoft.Extensions.Logging;
using ServiceLayer.Command;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using ServiceLayer.Repository.Interfaces;
using WriteDataLayer;
using WriteDataLayer.Model;

namespace ServiceLayer.Repository
{
    public class WriteCountryRepo(ILoggerFactory loggerFactory, WriteDbContext dbContext) : IWriteCountryRepo
    {
        private readonly ILogger<WriteCountryRepo> _logger = loggerFactory.CreateLogger<WriteCountryRepo>();
        private readonly WriteDbContext _dbContext = dbContext ?? throw new ArgumentNullException();

        public async Task<IResponse<CountryDto>> CreateCountryAsync(CreateCountryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var additem = new Country
                {
                    CountryId = _dbContext.Countries.Select(s => s.CountryId).Max() + 1,
                    IsoCode = command.IsoCode,
                    ShortName = command.ShortName,
                    FullName = command.FullName,
                    Alfa2Code = command.Alfa2Code,
                    Alfa3Code = command.Alfa3Code
                };
                _logger.LogInformation($"CountryService: add new Country item: {additem}");
                await _dbContext.Countries.AddAsync(additem).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                return new Response<CountryDto>(
                    message: "Country created successfully.",
                    errors: string.Empty,
                    status: true,
                    result: (CountryDto)additem
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"CountryService: {ex.Message}");
                return new Response<CountryDto>(
                    message: "Creating country has errors.",
                    errors: ex.Message,
                    status: false,
                    result: null
                );
            }
        }

        public async Task<IResponse<Unit?>> DeleteCountryAsync(DeleteCountryCommand command, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Countries.FindAsync(command.CountryId, cancellationToken);
            if (item == null)
            {
                _logger.LogError($"CountryLayer: Check complite with errors: Country not found.");
                return new Response<Unit?>(
                    message: "Check complite with errors.",
                    errors: "Country not found.",
                    status: false,
                    result: null
                );
            }

            _logger.LogInformation($"Check complite without errors.");
            _logger.LogInformation($"Delete Country item: {item}");
            _dbContext.Countries.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new Response<Unit?>(
                message: "Country deleted successfully.",
                errors: string.Empty,
                status: true,
                result: null
            );

        }

        public async Task<IResponse<CountryDto>> UpdateCountryAsync(UpdateCountryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Checking values...");
                var existingProduct = await _dbContext.Countries.FindAsync(command.CountryId, cancellationToken).ConfigureAwait(false);
                if (existingProduct == null)
                {
                    _logger.LogError($"CountryLayer: Check complite with errors: Country not found.");
                    return new Response<CountryDto>(
                        message: "Check complite with errors.",
                        errors: "Country not found.",
                        status: false,
                        result: null
                    );
                }

                existingProduct.IsoCode = command.IsoCode;
                existingProduct.FullName = command.FullName;
                existingProduct.ShortName = command.ShortName;
                existingProduct.Alfa2Code = command.Alfa2Code;
                existingProduct.Alfa3Code = command.Alfa3Code;

                _logger.LogInformation($"Check complite without errors.");
                _logger.LogInformation($"Update Country item: {existingProduct}");
                _dbContext.Countries.Update(existingProduct);

                return new Response<CountryDto>(
                    message: "Country updated successfully.",
                    errors: string.Empty,
                    status: true,
                    result: (CountryDto)existingProduct
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"CountryLayer: {ex.Message}");
                return new Response<CountryDto>(
                    message: "Update country has errors.",
                    errors: ex.Message,
                    status: false,
                    result: null
                );
            }
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Save changes complite.");
        }

        public void Dispose() => _dbContext.Dispose();
    }
}
