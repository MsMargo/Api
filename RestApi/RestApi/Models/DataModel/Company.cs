using System.Runtime.Serialization;

namespace RestApi.Models.DataModel
{
    [DataContract]
    public class Company
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "catchPhrase")]
        public string CatchPhrase { get; set; }

        [DataMember(Name = "bs")]
        public string Bs { get; set; }
    }
}