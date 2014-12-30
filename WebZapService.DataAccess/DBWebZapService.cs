using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebZapService.DataAccess.DataModel;

namespace WebZapService.DataAccess
{
    public class DBWebZapService: DbContext
    {
        public DBWebZapService()
            : base("DBWebZapService")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBZapService, ZapService.DataAccess.Migrations.Configuration>("DBZapService"));
        }

        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }        
    }
}
