using Nmedia.DataAccess;
using StatisticalSolutions.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.StatisticalSolutions.Domain.Models.Context
{
    public class StatisticalSolutionsContext : ContextBase
    {
        public StatisticalSolutionsContext()
            : base("DefaultConnection")
        {
            // Database.SetInitializer<AnewluvContext>(null);           
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.DisableLazyLoading = true;
            //rebuild DB if schema is differnt
            //Initializer init = new Initializer();            
            // init.InitializeDatabase(this);
            this.Configuration.ValidateOnSaveEnabled = false;

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<client> clients { get; set; }
        public DbSet<student> students { get; set; }
        public DbSet<seminar> seminars { get; set; }
        public DbSet<registration> registrations { get; set; }
        public DbSet<message> messages { get; set; }

    }
}
