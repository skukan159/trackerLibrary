using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;


namespace Tracker_Library.Models
{
    public class MatchupEntryModel
    {   /// <summary>
        /// Unique identifier for the matchup entry
        /// </summary>
        public int MatchupEntryId { get; set; }
        /// <summary>
        /// Unique identifier for the competing team. Because database has the integer column for the competing team
        /// </summary>
        public int TeamCompetingId { get; set; }
        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular team
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        ///     Unique parent matchup identifier for the databaes
        /// </summary>
        public int ParentMatchupId { get; set; }
        /// <summary>
        /// Represents the matchup that this team 
        /// came from as the winner
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }


    }

}
