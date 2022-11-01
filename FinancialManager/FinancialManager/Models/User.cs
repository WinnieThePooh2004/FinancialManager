﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
