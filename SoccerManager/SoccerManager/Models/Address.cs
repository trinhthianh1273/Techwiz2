﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SoccerManager.Models
{
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Orders>();
        }

        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string ReceiverName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}