﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Api.Models;

public class Employee
{
    public int EmployeeID { get; set; }

    public string FullName { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}