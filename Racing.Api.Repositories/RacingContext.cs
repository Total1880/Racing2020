﻿using Microsoft.EntityFrameworkCore;
using Racing.Model;

namespace Racing.Api.Repositories
{
    public class RacingContext : DbContext
    {
        public DbSet<Nation> NationList { get; set; }
        public DbSet<FirstNames> FirstNamesList { get; set; }
        public DbSet<LastNames> LastNamesList { get; set; }
        public DbSet<Race> RaceList { get; set; }
        public DbSet<Setting> SettingList { get; set; }
        public DbSet<Team> TeamList { get; set; }
        public DbSet<RacePart> RacePartList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Racing2020;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
