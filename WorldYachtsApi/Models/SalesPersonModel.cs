using Microsoft.AspNetCore.Identity;

namespace WorldYachtsApi.Models
{
    public class SalesPersonModel:UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
      
        public string Role => "Sales Person";
    }
}
