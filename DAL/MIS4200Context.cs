using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS4200_Team7.Models; // This is needed to access the models
using System.Data.Entity;

namespace MIS4200_Team7.DAL
{
    
        public class MIS4200Context : DbContext // inherits from DbContext
        {
            public MIS4200Context() : base("name=DefaultConnection")
            {
            // this method is a 'constructor' and is called when a new context is created
            // the base attribute says which connection string to use
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MIS4200Context, MIS4200_Team7.Migrations.MISContext.Configuration>("DefaultConnection"));
            }
            // Include each object here. The value inside <> is the name of the class,
            // the value outside should generally be the plural of the class name
            // and is the name used to reference the entity in code
            public DbSet<userProfile> userProfiles { get; set; }
            public DbSet<position> positions { get; set; }
            public DbSet<value> values { get; set; }
            public DbSet<recognition> recognitions { get; set; }
            public DbSet<Leaderboard> leaderboards { get; set; }




        /*public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }*/


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        
    }
    
}