using WriteDataLayer.Model;
using ServiceLayer.Command;
using ServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using ServiceLayer.Model.DTO;

namespace ServiceLayer.Repository.Interfaces
{
    public interface IReadCountryRepo : IDisposable
    {
        /// <summary>
        /// Get country by countryId
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<IResponse<CountryDto>> GetCountryAsync(int countryId, CancellationToken cancellationToken);

        /// <summary>
        /// Get country by countryId
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<IResponse<List<CountryDto>>> GetAllCountryAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
    }
}
