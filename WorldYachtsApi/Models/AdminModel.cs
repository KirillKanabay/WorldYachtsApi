﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Services.Models
{
    public class AdminModel:UserModel
    {
        public string Role => "Admin";
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}