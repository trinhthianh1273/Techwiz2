﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Status
{
    public int StatusID { get; set; }

    public string StatusName { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}