﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SoccerManager.Models;

public partial class Customer
{
    public int CustomerID { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Fullname { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Token { get; set; }

    public virtual ICollection<Address> Address { get; set; } = new List<Address>();

    public virtual ICollection<Cart> Cart { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> Feedback { get; set; } = new List<Feedback>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}