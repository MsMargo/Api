
namespace RestApi.Models.ViewModel
{
    public class UserView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public AddressView Address { get; set; }

        public string Phone { get; set; }

        public string ExtensionNumber { get; set; }

        public string Website { get; set; }

        public CompanyView Company { get; set; }
    }
}