﻿namespace TheHealthCare.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
    }
}
