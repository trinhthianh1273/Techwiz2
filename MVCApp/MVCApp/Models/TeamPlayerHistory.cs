//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeamPlayerHistory
    {
        public int ID { get; set; }
        public int TeamID { get; set; }
        public int PlayerID { get; set; }
        public System.DateTime JoinDate { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
    
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}