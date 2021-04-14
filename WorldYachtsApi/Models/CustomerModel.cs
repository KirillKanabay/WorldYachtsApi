using System;

namespace WorldYachtsApi.Models
{
    public class CustomerModel:UserModel
    {
        public string Role => "Customer";
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; }
        public string IdNumber { get; set; }
        public string IdDocumentName { get; set; }
    }
}
