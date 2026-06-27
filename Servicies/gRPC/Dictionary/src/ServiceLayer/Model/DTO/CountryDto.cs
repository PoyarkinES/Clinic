using System.Runtime.Serialization;

namespace ServiceLayer.Model.DTO
{
    public class CountryDto()
    {
        [DataMember]
        public int CountryId { get; private set; }
        [DataMember]
        public string IsoCode { get; private set; }
        [DataMember]
        public string FullName { get; private set; }
        [DataMember]
        public string ShortName { get; private set; }
        [DataMember]
        public string Alfa2Code { get; private set; }
        [DataMember]
        public string Alfa3Code { get; private set; }

        public static explicit operator CountryDto(ReadDataLayer.Model.Country country)
        {
            if (country != null)
                return new CountryDto
                {
                    CountryId = country.CountryId,
                    IsoCode = country.IsoCode,
                    FullName = country.FullName,
                    ShortName = country.ShortName,
                    Alfa3Code = country.Alfa3Code,
                    Alfa2Code = country.Alfa2Code
                };

            return new CountryDto();
        }

        public static implicit operator ReadDataLayer.Model.Country(CountryDto country)
        {
            if (country != null)
                return new ReadDataLayer.Model.Country
                {
                    CountryId = country.CountryId,
                    IsoCode = country.IsoCode,
                    FullName = country.FullName,
                    ShortName = country.ShortName,
                    Alfa3Code = country.Alfa3Code,
                    Alfa2Code = country.Alfa2Code
                };
            return new ReadDataLayer.Model.Country();
        }

        public static explicit operator CountryDto(WriteDataLayer.Model.Country country)
        {
            if (country != null)
                return new CountryDto
                {
                    CountryId = country.CountryId,
                    IsoCode = country.IsoCode,
                    FullName = country.FullName,
                    ShortName = country.ShortName,
                    Alfa3Code = country.Alfa3Code,
                    Alfa2Code = country.Alfa2Code
                };

            return new CountryDto();
        }

        public static implicit operator WriteDataLayer.Model.Country(CountryDto country)
        {
            if (country != null)
                return new WriteDataLayer.Model.Country
                {
                    CountryId = country.CountryId,
                    IsoCode = country.IsoCode,
                    FullName = country.FullName,
                    ShortName = country.ShortName,
                    Alfa3Code = country.Alfa3Code,
                    Alfa2Code = country.Alfa2Code
                };
            return new WriteDataLayer.Model.Country();
        }
    }
}
