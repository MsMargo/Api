using System.Runtime.Serialization;

namespace RestApi.Models.DataModel
{
    [DataContract]
    public class Post
    {
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }
    }
}