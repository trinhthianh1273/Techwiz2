﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Feedback
{
    public int FeedbackID { get; set; }

    public int CustomerID { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public virtual Customer Customer { get; set; }
}