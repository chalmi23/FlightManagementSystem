﻿using System.ComponentModel.DataAnnotations;

namespace FlightManagementSystem.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
