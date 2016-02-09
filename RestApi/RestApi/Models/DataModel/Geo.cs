using System.Runtime.Serialization;

namespace RestApi.Models.DataModel
{
    [DataContract]
    public class Geo
    {
        [DataMember(Name = "lat")]
        public string Lat { get; set; }

        [DataMember(Name = "lng")]
        public string Lng { get; set; }
    }
}