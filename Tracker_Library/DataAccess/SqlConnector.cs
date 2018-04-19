using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker_Library.Models;

 
namespace Tracker_Library.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "Tournaments";

        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@Email", model.EmailAdress);
                p.Add("@Cellphone", model.CellphoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                //Send data to the database
                connection.Execute("dbo.spPerson_Insert", p, commandType: CommandType.StoredProcedure);
                //Store the id of the added person to the model
                model.PersonId = p.Get<int>("@id");

                return model;
            }
        }

        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model"> The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmmount", model.PrizeAmmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType:DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.PrizeId = p.Get<int>("@id");

                return model;
            }

        }

        public TeamModel CreateTeam(TeamModel model)
        {
            //Connect to the database
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                //Put into variable p value of the team name...we are getting them from the team model
                var p = new DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                //Add the team to the database
                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);
                //Get the team id from the database
                model.TeamId = p.Get<int>("@id");

                //Go through every person added to the list
                foreach (PersonModel tm in model.TeamMembers)
                {
                    //Make p again
                    p = new DynamicParameters();
                    //Add the Id of the team to the p
                    p.Add("@TeamId", model.TeamId);
                    //Add the Id of the person from the list to the team
                    p.Add("@PersonId", tm.PersonId); //PROBLEM, Value = 0 --> Fixed by changing the PersonModel class
                                                     //and by setting enforce foreign key constraints to no in TeamMember table

                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);
                }

                return model;
            }
        }

        public void CreateTournament(TournamentModel model)
        {

            //Connect to the database
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {

                SaveTournament(model, connection);

                SaveTournamentPrizes(model, connection);

                SaveTournamentEntries(model, connection);

                SaveTournamentRounds(connection, model);
            }

        }

        private void SaveTournament(TournamentModel model, IDbConnection connection)
        {
            //Put into variable p value of the tournament model variables
            var p = new DynamicParameters();
            p.Add("@TournamentName", model.TournamentName);
            p.Add("@EntryFee", model.EntryFee);
            p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Add the team to the database
            connection.Execute("dbo.spTournaments_Insert", p, commandType: CommandType.StoredProcedure);
            //Get the team id from the database
            model.TournamentId = p.Get<int>("@id");
        }

        private void SaveTournamentPrizes(TournamentModel model, IDbConnection connection)
        {
            foreach (PrizeModel pz in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.TournamentId);
                p.Add("@PrizeId",pz.PrizeId);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                //Add the team to the database
                connection.Execute("dbo.spTournamentPrizes_Insert", p, commandType: CommandType.StoredProcedure);

            }
        }

        private void SaveTournamentEntries(TournamentModel model, IDbConnection connection)
        {
            foreach (TeamModel tm in model.EnteredTeams)
            {
               var p = new DynamicParameters();
                p.Add("@TournamentId", model.TournamentId);
                p.Add("@TeamId", tm.TeamId);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournamentEntries_Insert", p, commandType: CommandType.StoredProcedure);

            }
        }

        private void SaveTournamentRounds(IDbConnection connection, TournamentModel model)
        {
            //List<List<MatchupModels>> Rounds
            //List<MatchupEntryModel> Entries

            //Loop through the rounds
            //Loop through the matchups
            //Save a matchup
            //Loop through the entries and save them


            //Loop through the rounds
            foreach (List<MatchupModel> round in model.Rounds)
            {
                //Loop through the matchups
                foreach (MatchupModel matchup in round)
                {

                    //Save a matchup
                    var p = new DynamicParameters();

                    p.Add("@TournamentId", model.TournamentId);
                    p.Add("@MatchupRound", matchup.MatchupRound);

                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);
                    matchup.MatchupId = p.Get<int>("@id");

                    //Loop through the entries
                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {

                        //save them
                        p = new DynamicParameters();
                        p.Add("@MatchupId", matchup.MatchupId);
                        if (entry.ParentMatchup == null)
                        {
                            p.Add("@ParentMatchupId", null);
                        }
                        else
                        {
                            p.Add("@ParentMatchupId", entry.ParentMatchup.MatchupId);
                        }

                        if (entry.TeamCompeting == null)
                        {
                            p.Add("@TeamCompetingId", null);
                        }
                        else
                        {
                            p.Add("@TeamCompetingId", entry.TeamCompeting.TeamId);
                        }
                        p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("spMatchupEntries_Insert", p, commandType: CommandType.StoredProcedure);
                       // entry.EntryId = p.Get<int>("@id");
                    }
                }
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }

        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                output = connection.Query<TeamModel>("dbo.spTeams_GetAll").ToList();

                foreach (TeamModel team in output)
                {
                    var p = new DynamicParameters();
                    //Add the Id of the team to the p
                    p.Add("@TeamId", team.TeamId);

                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            return output;
        }


        public List<TournamentModel> GetTournament_All()
        {
            List<TournamentModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                output = connection.Query<TournamentModel>("dbo.spTournaments_GetAll").ToList();
                var p = new DynamicParameters();

                //TODO - something is wrong here!
                foreach (TournamentModel t in output)
                {
                    //Populate prizes
                    p = new DynamicParameters();
                    p.Add("@TournamentId", t.TournamentId);
                    t.Prizes = connection.Query<PrizeModel>("dbo.spPrizes_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

                    //Populate the teams
                    p = new DynamicParameters();
                    p.Add("@TournamentId", t.TournamentId);
                    t.EnteredTeams = connection.Query<TeamModel>("spTeams_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

                    //Populate members
                    foreach (TeamModel team in t.EnteredTeams) //Team members count = 0
                    {
                         p = new DynamicParameters();
                        //Add the Id of the team to the p
                        p.Add("@TeamId", team.TeamId);

                        team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList(); //ID = 2029, Members = 0 ???
                    }

                     p = new DynamicParameters();
                    
                     p.Add("@TournamentId", t.TournamentId);
                    //Populate rounds
                    List<MatchupModel> matchups = connection.Query<MatchupModel>("dbo.spMatchups_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

                    foreach (MatchupModel m in matchups)
                    {
                        p = new DynamicParameters();
                        p.Add("@MatchupId", m.MatchupId); //look here
                        //Populate rounds
                        m.Entries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchup", p, commandType: CommandType.StoredProcedure).ToList(); //Why is the entry id 0?? Something smells....

                        //Populate each entry (2 models)
                        //Populate each matchup (1 model)
                        List<TeamModel> allTeams = GetTeam_All(); 

                        if(m.WinnerId > 0)
                        {
                            m.Winner = allTeams.Where(x => x.TeamId == m.WinnerId).First();
                        }

                        foreach (var me in m.Entries)
                        {
                            if(me.TeamCompetingId > 0)
                            {
                                me.TeamCompeting = allTeams.Where(x => x.TeamId == me.TeamCompetingId).First();
                            }

                            if(me.ParentMatchupId > 0)
                            {
                                me.ParentMatchup = matchups.Where(x => x.MatchupId == me.ParentMatchupId).First();
                            }
                        }
                    }
                    //List<List<MatchupModel>>
                    List<MatchupModel> currentRow = new List<MatchupModel>();
                    int currentRound = 1;

                    foreach (MatchupModel m in matchups)
                    {
                        if(m.MatchupRound > currentRound)
                        {
                            t.Rounds.Add(currentRow);
                            currentRow = new List<MatchupModel>();
                            currentRound++;
                        }

                        currentRow.Add(m);

                    }

                    t.Rounds.Add(currentRow);
                }

            }

            return output;
        }


        public void UpdateMatchup(MatchupModel model)
        {
            //Store the matchup
            //Store the matchup entries
            //dbo.spMatchups_Update


            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@Id", model.MatchupId);
                p.Add("@WinnerId", model.Winner.TeamId);
               

                connection.Execute("dbo.spMatchups_Update", p, commandType: CommandType.StoredProcedure);

                foreach(MatchupEntryModel me in model.Entries) // ERROR: Entries have id of 0
                {
                    p = new DynamicParameters();
                    p.Add("@Id", me.MatchupEntryId);  //This is always 0
                    p.Add("@TeamCompetingId", me.TeamCompeting.TeamId); //This is correct id
                    p.Add("@Score", me.Score); //Correct value


                    connection.Execute("dbo.spMatchupEntries_Update", p, commandType: CommandType.StoredProcedure);
                }

            }
        }


        
    }
}
