﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SoccerManager.Models
{
    public partial class PlayerScore
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Scores { get; set; }

        public virtual Match Match { get; set; }
        public virtual Player Player { get; set; }
    }
}