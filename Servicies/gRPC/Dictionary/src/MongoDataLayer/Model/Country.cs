using MongoDB.Bson;

namespace MongoDataLayerService.Model
{
    public class Country
    {
        public ObjectId _id { get; set; }
        public int CountryId { get; set; }
        public string IsoCode { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Alfa2Code { get; set; }
        public string Alfa3Code { get; set; }
    }
}
