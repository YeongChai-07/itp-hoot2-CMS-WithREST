using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration;


namespace HootHoot_CMS.DAL
{
    public class HootHootDbContext:DbContext
    {
        public HootHootDbContext():base("HootHootAzureDB")
        {


        }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.Stations> Stations { get; set; }

        public System.Data.Entity.DbSet<HootHoot_CMS.Models.Questions> Questions { get; set; }
    }
}