namespace WorldYachtsApi.Models
{
    public class SalesPersonModel:UserModel
    {
        public string Role => "Sales Person";
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}
