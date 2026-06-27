using MediatR;
using ServiceLayer.Command;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ServiceLayer.Repository.Interfaces
{
    public interface IWriteCountryRepo : IDisposable
    {
        /// <summary>
        /// Create new country
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<IResponse<CountryDto>> CreateCountryAsync(CreateCountryCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Update country by countryId
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<IResponse<CountryDto>> UpdateCountryAsync(UpdateCountryCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Delete country by countryId
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<IResponse<Unit?>> DeleteCountryAsync(DeleteCountryCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
