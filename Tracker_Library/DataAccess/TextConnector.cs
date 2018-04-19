using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker_Library.Models;
using Tracker_Library.DataAccess.TextHelpers;

namespace Tracker_Library.DataAccess
{
    public class TextConnector : IDataConnection
    {
        

        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";
        private const string MatchupFile = "MatchupModels.csv";
        private const string MatchupEntryFile = "MatchupEntryModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            //Load the text file
            //Convert the text to a List<PrizeModel>
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
            //Find the ID
            int currentId = 1;
            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.PersonId).First().PersonId + 1;
            }

            model.PersonId = currentId;
            //Add a new reacord with the new ID
            people.Add(model);

            //Convert the prizes  to list<string>
            //Save the list<string> to the text file
            people.SaveToPersonFile(PeopleFile);

            return model;
        }

    
   

        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Load the text file
            //Convert the text to a List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            //Find the ID
            int currentId = 1;
                if(prizes.Count>0)
            {
                currentId = prizes.OrderByDescending(x => x.PrizeId).First().PrizeId + 1;
            }

            model.PrizeId = currentId;
            //Add a new reacord with the new ID
            prizes.Add(model);

            //Convert the prizes  to list<string>
            //Save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }


        public TeamModel CreateTeam(TeamModel model)
        {
            //Load the text file
            //Convert the text to a List<TeamModel>
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            int currentId = 1;
            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.TeamId).First().TeamId + 1;
            }

            model.TeamId = currentId;

            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);

            return model;
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentFile.
                FullFilePath().
                LoadFile().
                ConvertToTournamentModels(TeamFile,PeopleFile,PrizesFile);


            int currentId = 1;
            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.TournamentId).First().TournamentId + 1;
            }

            model.TournamentId = currentId;

            model.SaveRoundsToFile(MatchupFile,MatchupEntryFile);

            tournaments.Add(model);

            tournaments.SaveToTournamentFile(TournamentFile);

        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }

        public List<TournamentModel> GetTournament_All()
        {
            return TournamentFile.
            FullFilePath().
            LoadFile().
            ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);
        }

        public void UpdateMatchup(MatchupModel model)
        {
            model.UpdateMatchupToFile();
        }
    }
}

