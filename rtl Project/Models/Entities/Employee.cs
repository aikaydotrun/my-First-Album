﻿namespace RtlEmployeeApi.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public  string? Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
