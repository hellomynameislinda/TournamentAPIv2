﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TournamentAPIv2.Core.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Game> Games { get; set; } = [];
    }
}
