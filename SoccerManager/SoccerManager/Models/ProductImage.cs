﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SoccerManager.Models;

public partial class ProductImage
{
    public int ProductImageID { get; set; }

    public int ProductID { get; set; }

    public string ImageURL { get; set; }

    public virtual Products Product { get; set; }
}