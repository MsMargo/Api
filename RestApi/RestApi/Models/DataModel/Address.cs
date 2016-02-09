using System.Runtime.Serialization;

namespace RestApi.Models.DataModel
{
    [DataContract]
    public class Address
    {
        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "suite")]
        public string Suite { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "zipcode")]
        public string Zipcode { get; set; }

        [DataMember(Name = "geo")]
        public Geo Geo { get; set; }
    }
}