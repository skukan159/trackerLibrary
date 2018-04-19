﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracker_Library.DataAccess;

namespace Tracker_Library
{
    public static class GlobalConfig
    {

        public const string PrizesFile = "PrizeModels.csv";
        public const string PeopleFile = "PersonModels.csv";
        public const string TeamFile = "TeamModels.csv";
        public const string TournamentFile = "TournamentModels.csv";
        public const string MatchupFile = "MatchupModels.csv";
        public const string MatchupEntryFile = "MatchupEntryModels.csv";

        /// <summary>
        /// Before it was a list of conenctios. Now we decided we will have only 1 at a time
        /// so we do not have id's in files conflicting with id's in the database
        /// </summary>
        public static IDataConnection Connection { get; private set; }

        //Connect either to database or textfile
        public static void InitializeConnections(DatabaseType db)
        {
            
            switch (db)
            {
                case DatabaseType.Sql:
                    SqlConnector sql = new SqlConnector();
                    Connection = (sql);
                    break;
                case DatabaseType.TextFile:
                    TextConnector text = new TextConnector();
                    Connection = text;
                    break;
                default:
                    break;
            }

         
        }

        public static string ConnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;

        }

    }
}
