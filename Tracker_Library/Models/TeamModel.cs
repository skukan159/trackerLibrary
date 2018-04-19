using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker_Library.Models
{
    public class TeamModel
    {
        public int TeamId { get; set; }
        
        public string TeamName { get; set; }

        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
    }
}
