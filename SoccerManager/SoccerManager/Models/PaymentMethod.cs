﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SoccerManager.Models;

public partial class PaymentMethod
{
    public int PaymentMethodID { get; set; }

    public string PaymentMethod1 { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}