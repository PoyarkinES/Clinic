using WriteDataLayer.Model;
using ServiceLayer.Model.DTO;

namespace Service.Extensions
{
    /// <summary>
    /// Extensions methods 
    /// </summary>
    public static class ExtensionsMethod
    {
        /// <summary>
        /// Map countrydto to countrymodel
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static CountryModel MapToCountryModel(this CountryDto? item)
        {
            if (item == null || item == default) return new CountryModel();

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
