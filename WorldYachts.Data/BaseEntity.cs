﻿using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
