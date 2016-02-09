
namespace RestApi.Models.ViewModel
{
    public class AddressView
    {
        public string Street { get; set; }

        public string Suite { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }

        public GeoView Geo { get; set; }
    }
}