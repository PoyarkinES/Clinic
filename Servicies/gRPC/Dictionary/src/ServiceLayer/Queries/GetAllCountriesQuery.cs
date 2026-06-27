using MediatR;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Queries
{
    public class GetAllCountriesQuery : IRequest<IResponse<List<CountryDto>>>
    {
    }
}
