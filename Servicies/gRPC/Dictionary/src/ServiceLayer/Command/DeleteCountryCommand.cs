using MediatR;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Command
{
    public class DeleteCountryCommand(Guid commandId, int countryId) : IRequest<IResponse<Unit?>>
    {
        public int CountryId { get; private set; } = countryId;
    }
}
