using Service;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using MongoDataLayerService;
using MongoDataLayerService.Model;
using MongoDB.Bson;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Service.Services
{
    public class CountryService(ILogger<CountryService> logger, MongoDbContext dbContext) : CountriesServiceProto.CountriesServiceProtoBase
    {
        private readonly MongoDbContext _dbContext = dbContext;
        private readonly ILogger<CountryService> _logger = logger;

        public override async Task<CreateCountryResponse> CreateCountry(CreateCountryRequest request, ServerCallContext server)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(request.Fullname))
                {
                    return new CreateCountryResponse
                    {
                        Success = false,
                        Message = "Country FullName is required"
                    };
                }

                // Input validation
                if (string.IsNullOrWhiteSpace(request.Isocode))
                {
                    return new CreateCountryResponse
                    {
                        Success = false,
                        Message = "Country Isocode is required"
                    };
                }

                var addItem = new Country
                {
                    _id = ObjectId.GenerateNewId(),
                    CountryId = _dbContext.countries.Select(s => s.CountryId).Max() + 1,
                    IsoCode = request.Isocode,
                    FullName = request.Fullname,
                    ShortName = request.Shortname,
                    Alfa2Code = request.Alfa2Code,
                    Alfa3Code = request.Alfa3Code,
                };

                // Add to database
                _dbContext.countries.Add(addItem);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Country created successfully with ID: {addItem.CountryId}");

                // Return success response
                return new CreateCountryResponse
                {
                    Success = true,
                    Message = "Country created successfully",
                    Country = MapToProductModel(addItem)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");

                return new CreateCountryResponse
                {
                    Success = false,
                    Message = $"Error creating product: {ex.Message}"
                };
            }
        }

        public override async Task<GetCountryResponse> GetCountry(GetCountryRequest request, ServerCallContext server)
        {
            try
            {
                // Validate and parse the product ID
                if (request.CountryId != default)
                {
                    return new GetCountryResponse
                    {
                        Success = false,
                        Message = $"Invalid country ID value {request.CountryId}."
                    };
                }

                // Find product in database
                var country = await _dbContext.countries.FindAsync(request.CountryId);

                if (country == null)
                {
                    _logger.LogWarning($"Country not found with ID: {request.CountryId}");

                    return new GetCountryResponse
                    {
                        Success = false,
                        Message = "Country not found"
                    };
                }

                _logger.LogInformation($"Country retrieved successfully with ID: {request.CountryId}");

                // Return success response with product data
                return new GetCountryResponse
                {
                    Success = true,
                    Message = "Country retrieved successfully",
                    Country = MapToProductModel(country)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID: {request.CountryId}");

                return new GetCountryResponse
                {
                    Success = false,
                    Message = $"Error retrieving product: {ex.Message}"
                };
            }
        }

        public override async Task<GetCountriesResponse> GetCountries(GetCountriesRequest request, ServerCallContext server)
        {
            try
            {
                // Set default pagination values
                var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100); // Max 100 items per page
                var page = request.Page <= 0 ? 1 : request.Page;

                // Calculate skip amount for pagination
                var skip = (page - 1) * pageSize;

                // Get total count for pagination metadata
                var totalCount = await _dbContext.countries.CountAsync();

                // Retrieve paginated products
                var countries = await _dbContext.countries
                    .OrderBy(p => p.CountryId)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();

                // Create response
                var response = new GetCountriesResponse
                {
                    Success = true,
                    Message = countries.Any()
                        ? $"Retrieved {countries.Count} products (Page {page} of {Math.Ceiling((double)totalCount / pageSize)})"
                        : "No products found",
                    TotalCount = totalCount
                };

                // Add products to response
                response.Country.AddRange(countries.Select(MapToProductModel));

                _logger.LogInformation("Listed {ProductCount} products for page {Page}", countries.Count, page);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products list");

                return new GetCountriesResponse
                {
                    Success = false,
                    Message = $"Error retrieving products: {ex.Message}",
                    TotalCount = 0
                };
            }
        }

        public override async Task<UpdateCountryResponse> UpdateCountry(UpdateCountryRequest request, ServerCallContext context)
        {
            try
            {
                if (request.CountryId == default)
                {
                    return new UpdateCountryResponse
                    {
                        Success = false,
                        Message = $"Invalid country ID {request.CountryId} format."
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Fullname))
                {
                    return new UpdateCountryResponse
                    {
                        Success = false,
                        Message = "Country Fullname is required"
                    };
                }

                var existingProduct = await _dbContext.countries.FindAsync(request.CountryId);

                if (existingProduct == null)
                {
                    return new UpdateCountryResponse
                    {
                        Success = false,
                        Message = "Country not found"
                    };
                }

                existingProduct.IsoCode = request.Isocode;
                existingProduct.FullName = request.Fullname;
                existingProduct.ShortName = request.Shortname;
                existingProduct.Alfa2Code = request.Alfa2Code;
                existingProduct.Alfa3Code = request.Alfa3Code;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Product updated successfully with ID: {request.CountryId}");

                // Return success response with updated product
                return new UpdateCountryResponse
                {
                    Success = true,
                    Message = "Product updated successfully",
                    Country = MapToProductModel(existingProduct)
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

        public override async Task<DeleteCountryResponse> DeleteCountry(DeleteCountryRequest request, ServerCallContext context)
        {
            try
            {
                if (request.CountryId == default)
                {
                    return new DeleteCountryResponse
                    {
                        Success = false,
                        Message = $"Invalid country ID {request.CountryId} format."
                    };
                }

                var product = await _dbContext.countries.FindAsync(request.CountryId);

                if (product == null)
                {
                    return new DeleteCountryResponse
                    {
                        Success = false,
                        Message = "Country not found"
                    };
                }

                _dbContext.countries.Remove(product);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Country deleted successfully with ID: {request.CountryId}");

                // Return success response
                return new DeleteCountryResponse
                {
                    Success = true,
                    Message = "Country deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID: {request.CountryId}");

                return new DeleteCountryResponse
                {
                    Success = false,
                    Message = $"Error deleting product: {ex.Message}"
                };
            }
        }

        private static CountryModel MapToProductModel(Country item)
        {
            return new CountryModel
            {
                CountryId = item.CountryId,
                Isocode = item.IsoCode,
                Fullname = item.FullName,
                Shortname = item.ShortName,
                Alfa2Code = item.Alfa2Code,
                Alfa3Code = item.Alfa3Code,
                Tags = string.Empty
            };
        }
    }
}
