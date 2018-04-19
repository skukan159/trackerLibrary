using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker_Library.Models;
//*Load the text file
//*Convert the text to a List<PrizeModel>
//Find the ID
//Add a new reacord with the new ID
//Convert the prizes  to list<string>
//Save the list<string> to the text file
namespace Tracker_Library.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) //PrizeModels.csv -> fileName
        {   //C:\data\TournamentTracker\PrizeModels.csv
            return $"{ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }
    
        public static List<string> LoadFile(this string file)
        {
            if(!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            //initialize empty output so we can read from file
            List<PrizeModel> output = new List<PrizeModel>();

   
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.PrizeId = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }
            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach(PrizeModel p in models)
            {
                lines.Add($"{p.PrizeId},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmmount},{p.PrizePercentage}");

            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            //initialize empty output so we can read from file
            List<PersonModel> output = new List<PersonModel>();


            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.PersonId = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAdress = cols[3];
                p.CellphoneNumber = cols[4];
                output.Add(p);
            }
            return output;
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            //id,team name, list of ID's seperated by the |
            //3,filip's team, 1|3|5

            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel();
                t.TeamId = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    //Take the list of all the people in the text file.Search through it
                    //Filter where the id of the person in the list equals the id of here..
                    //In theory it should give us only 1 person because each id is unique.
                    //But c# is still giving us a list so we are asking for the first(only) item
                    //If the list is 0 the app should explode
                      t.TeamMembers.Add(people.Where(x=>x.PersonId == int.Parse(id)).First());

                }
                output.Add(t);
            }
            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines, string teamFileName,string peopleFileName, string prizeFileName)
        {   //id= 0, TournamentName = 1, entryFee = 2, enteredTeams = 3, Prizes=4,Rounds = 5
            //ID,TournamentName,EntryFee, (id|id|id - Entered Teams), (id|id|id - Prizes), (Rounds- id^id^id|id^id^id|id^id^id)

            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = teamFileName.FullFilePath().LoadFile().ConvertToTeamModels(peopleFileName);
            List<PrizeModel> prizes = prizeFileName.FullFilePath().LoadFile().ConvertToPrizeModels();
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels(); //AHA! THERE YOU ARE YOU LITTLE BASTARD!

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TournamentModel tm = new TournamentModel();
                tm.TournamentId = int.Parse(cols[0]);
                tm.TournamentName = cols[1];
                tm.EntryFee = decimal.Parse(cols[2]);

                string[] teamIds = cols[3].Split('|');

                foreach (string id in teamIds)
                {
                    //Load the list of teams
                    //Find the team based upon the id
                    //
                    //  t.TeamMembers.Add(people.Where(x => x.PersonId == int.Parse(id)).First());
                    tm.EnteredTeams.Add(teams.Where(x => x.TeamId == int.Parse(id)).First());

                }
                if(cols[4].Length >0)
                {

                string[] prizeIds = cols[4].Split('|');
                
                foreach (string id in prizeIds)
                {
                    tm.Prizes.Add(prizes.Where(x => x.PrizeId == int.Parse(id)).First());
                }
                }
                // Capture rounds information
                //(Rounds- id^id^id|id^id^id|id^id^id)
                string[] rounds = cols[5].Split('|');

                foreach(string round in rounds)
                {
                    //id^id^id
                    string[] matchup_text = round.Split('^');
                    List<MatchupModel> matchup = new List<MatchupModel>();

                    foreach (string matchupModelTextId in matchup_text)
                    {
                        //fill in id^id^id
                        matchup.Add(matchups.Where(x => x.MatchupId == int.Parse(matchupModelTextId)).First()); //Something is wrong here!!
                    }
                    // push id^id^id to Rounds
                    tm.Rounds.Add(matchup);
                }
                output.Add(tm);
            }
            return output;
        }

        public static void SaveToPersonFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.PersonId},{p.FirstName},{p.LastName},{p.EmailAdress},{p.CellphoneNumber}");

            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            
            foreach(TeamModel t in models)
            {
                lines.Add($"{ t.TeamId },{ t.TeamName},{ ConvertPeopleListToString(t.TeamMembers)}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTournamentFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach(TournamentModel t in models)
            {
                lines.Add($@"{t.TournamentId},{t.TournamentName},{t.EntryFee},{ConvertTeamListToString(t.EnteredTeams)},{ConvertPrizeListToString(t.Prizes)},{ConvertRoundListToString(t.Rounds)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);

        }

        public static void SaveRoundsToFile(this TournamentModel model, string matchupFile, string matchupEntryFile)
        {

            
            //Loop through each matchup
            //Get the id for the new matchup
            //Save the record
            //Loop through each entry
            //Get the id and save it

            //Loop through each round
            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    //Load all of the matchups from file
                    //Get the top id and add 1
                    //Store the id
                    //Save the matchup record
                    matchup.SaveMatchupToFile(matchupFile, matchupEntryFile);
                }
            }
        }

        public static List<MatchupEntryModel> ConvertToMatchupEntryModel(this List<string> lines)
        {
            ////initialize empty output so we can read from file
            //List<PersonModel> output = new List<PersonModel>();

            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupEntryModel m = new MatchupEntryModel();
                m.MatchupEntryId = int.Parse(cols[0]);
                if(cols[1].Length == 0)
                {
                    m.TeamCompeting = null;
                }
                else
                {
                    m.TeamCompeting = LookUpTeamById(int.Parse(cols[1]));
                }
                m.Score = double.Parse(cols[2]);
                int parentId = 0;
                if(int.TryParse(cols[3], out parentId))
                {
                    m.ParentMatchup = LookUpMatchupById(parentId);
                }
                else
                {
                    m.ParentMatchup = null;
                }

                output.Add(m);
            }
            return output;
        }

        private static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string input)
        {
            string[] ids = input.Split('|');
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            List<string> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile();
            List<string> matchingEntries = new List<string>();



            foreach (string id in ids)
            {
                foreach (string entry in entries)
                {
                    string[] cols = entry.Split(',');

                    if(cols[0] == "id")
                    {
                        matchingEntries.Add(entry);
                    }
                }
            }

            output = matchingEntries.ConvertToMatchupEntryModel();

            return output;
        }

        private static TeamModel LookUpTeamById(int id)
        {
            List<string> teams = GlobalConfig.TeamFile.FullFilePath().LoadFile();

            //Return the first team where the id matches the passed in ID
            //If not. blow up
            foreach (string team in teams)
            {
                string[] cols = team.Split(',');
                if(cols[0] == id.ToString())
                {
                    List<string> matchingTeams = new List<string>();
                    matchingTeams.Add(team);
                    return matchingTeams.ConvertToTeamModels(GlobalConfig.PeopleFile).First();
                }
            }

            return null;
        }

        private static MatchupModel LookUpMatchupById(int id)
        {
            List<string> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile();
           
            foreach(string matchup in matchups)
            {
                string[] cols = matchup.Split(',');
                if(cols[0] == id.ToString())
                {
                    List<string> matchingMatchups = new List<string>();
                    matchingMatchups.Add(matchup);
                    return matchingMatchups.ConvertToMatchupModels().First();
                }
            }
            return null;
        }

        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
         {
            //initialize empty output so we can read from file
            List<MatchupModel> output = new List<MatchupModel>();

            //id = 0
            //Entries = 1(pipe delimited by id)
            //winner = 2
            //MatchupRound = 3
   
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupModel m = new MatchupModel();
                m.MatchupId = int.Parse(cols[0]);       //
                m.Entries = ConvertStringToMatchupEntryModels(cols[1]); //The entries are in the file! What is wrong with you?
                if(cols[2].Length == 0)
                {
                    m.Winner = null;
                }
                else
                {
                    m.Winner = LookUpTeamById(int.Parse(cols[2]));
                }
                
                m.MatchupRound = int.Parse(cols[3]);

                output.Add(m);
            }
            return output;
        }

        public static void SaveMatchupToFile(this MatchupModel matchup, string matchupFile, string matchupEntryFile)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            int currentId = 1;
            if (matchups.Count > 0)
            {
                currentId = matchups.OrderByDescending(x => x.MatchupId).First().MatchupId + 1;
            }
            matchup.MatchupId = currentId;

            matchups.Add(matchup);


            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.SaveEntryToFile(matchupEntryFile);
            }

            //Save to matchup File
             List<string> lines = new List<string>();

            foreach (MatchupModel m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner =m.Winner.TeamId.ToString();
                }
                //id = 0, entries = 1,winner = 2, round = 3
                lines.Add($"{ m.MatchupId },{ ConvertMatchupEntryListToString(m.Entries) },{ winner },{ m.MatchupRound }");
            }
            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);
        }

        public static void UpdateMatchupToFile(this MatchupModel matchup)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            MatchupModel oldMatchup = new MatchupModel();

            foreach (MatchupModel m in matchups)
            {
                if(m.MatchupId == matchup.MatchupId)
                {
                    oldMatchup = m;
                }
            }

            matchups.Remove(oldMatchup);

            matchups.Add(matchup);


            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.UpdateEntryToFile();
            }

            //Save to matchup File
            List<string> lines = new List<string>();

            foreach (MatchupModel m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner = m.Winner.TeamId.ToString();
                }
                //id = 0, entries = 1,winner = 2, round = 3
                lines.Add($"{ m.MatchupId },{ ConvertMatchupEntryListToString(m.Entries) },{ winner },{ m.MatchupRound }");
            }
            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);
        }

        public static void UpdateEntryToFile(this MatchupEntryModel entry)

        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModel();

            MatchupEntryModel oldEntry = new MatchupEntryModel();

            foreach (MatchupEntryModel e in entries)
            {
                if(e.MatchupEntryId == entry.MatchupEntryId)
                {
                    oldEntry = e;
                }
            }

            entries.Remove(oldEntry);

            entries.Add(entry);

            //Save to file
            List<string> lines = new List<string>();

            foreach (MatchupEntryModel e in entries)
            {
                string parent = "";
                if (e.ParentMatchup != null)
                {
                    parent = e.ParentMatchup.MatchupId.ToString();
                }
                string teamCompeting = "";
                if (e.TeamCompeting != null)
                {
                    teamCompeting = e.TeamCompeting.TeamId.ToString();
                }
                lines.Add($"{ e.MatchupEntryId },{ teamCompeting },{e.Score},{parent}");
            }
            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);

        }

        public static void SaveEntryToFile(this MatchupEntryModel entry, string matchupEntryFile)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModel();

            int currentId = 1;
            if(entries.Count > 0)
            {
                currentId = entries.OrderByDescending(x => x.MatchupEntryId).First().MatchupEntryId + 1;
            }
            entry.MatchupEntryId = currentId;
            entries.Add(entry);

            //Save to file
            List<string> lines = new List<string>();

            foreach(MatchupEntryModel e in entries)
            {
                string parent = "";
                if(e.ParentMatchup != null)
                {
                    parent = e.ParentMatchup.MatchupId.ToString();
                }
                string teamCompeting = "";
                if(e.TeamCompeting != null)
                {
                    teamCompeting = e.TeamCompeting.TeamId.ToString();
                }
                lines.Add($"{ e.MatchupEntryId },{ teamCompeting },{e.Score},{parent}");
            }
            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(),lines);

        }

        private static string ConvertRoundListToString(List<List<MatchupModel>> rounds)
        {
            string output = "";
            if (rounds.Count == 0)
            {
                return "";
            }
            //2|5
            foreach(List<MatchupModel> r in rounds)
            {//Get rid of the last |
                output += $"{ ConvertMatchupListToString(r) }|";
            }
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            string output = "";
            if (matchups.Count == 0)
            {
                return "";
            }
            //2|5
            foreach (MatchupModel m in matchups)
            {  //Get rid of the last |
                output += $"{ m.MatchupId }^";
            }
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertTeamListToString(List<TeamModel> teams)
        {
            string output = "";
            if (teams.Count == 0)
            {
                return "";
            }
            //2|5
            foreach (TeamModel t in teams)
            {
                output += $"{ t.TeamId }|";
            }
            //Get rid of the last |
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertPrizeListToString(List<PrizeModel> prizes)
        {
            string output = "";
            if (prizes.Count == 0)
            {
                return "";
            }
            //2|5
            foreach (PrizeModel p in prizes)
            {
                output += $"{ p.PrizeId }|";
            }
            //Get rid of the last |
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertPeopleListToString(List <PersonModel> people)
        {
            string output = "";
            if(people.Count == 0)
            {
                return "";
            }
            //2|5
            foreach (PersonModel p in people)
            {
                output += $"{ p.PersonId }|";
            }
            //Get rid of the last |
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertMatchupEntryListToString(List<MatchupEntryModel> entries)
        {
            string output = "";
            if (entries.Count == 0)
            {
                return "";
            }
            //2|5
            foreach (MatchupEntryModel e in entries)
            {
                output += $"{ e.MatchupEntryId }|";
            }
            //Get rid of the last |
            output = output.Substring(0, output.Length - 1);

            return output;
        }



    }
}
