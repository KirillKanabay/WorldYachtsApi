using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Services.Models
{
    public class SalesPersonModel:UserModel
    {
        public string Role => "Sales Person";
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}
