using MediatR;
using ServiceLayer.Model;
using ServiceLayer.Model.DTO;
using System.Runtime.Serialization;

namespace ServiceLayer.Command
{
    public class UpdateCountryCommand(Guid commandId, int countryId, string isoCode, string fullName, string shortName, string alfa2Code, string alfa3Code) : IRequest<IResponse<CountryDto>>
    {
        [DataMember]
        public Guid CommandId { get; private set; } = commandId;
        [DataMember]
        public int CountryId { get; private set; } = countryId;
        [DataMember]
        public string IsoCode { get; private set; } = isoCode;
        [DataMember]
        public string FullName { get; private set; } = fullName;
        [DataMember]
        public string ShortName { get; private set; } = shortName;
        [DataMember]
        public string Alfa2Code { get; private set; } = alfa2Code;
        [DataMember]
        public string Alfa3Code { get; private set; } = alfa3Code;
    }
}
