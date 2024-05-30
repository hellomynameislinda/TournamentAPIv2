using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPIv2.Core.Entities;

namespace TournamentAPIv2.Core.Dto
{
    public class TournamentDTO
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddMonths(3);
            }
        }

    }
}
