﻿using System.Data.Entity;

namespace HootHoot_CMS.DAL
{
    public class HootHootDbContext:DbContext
    {
        public HootHootDbContext():base("HootHootAzureDB")
        {

        }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.Stations> Stations { get; set; }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.Questions> Questions { get; set; }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.FunFacts> FunFacts { get; set; }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.OptionType> OptionTypes { get; set; }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.QuestionType> QuestionTypes { get; set; }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.StationType> StationType { get; set; }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.Accounts> AccountsModels { get; set; }
    }
}