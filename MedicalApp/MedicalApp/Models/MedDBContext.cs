using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MedicalApp.Models.DataModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

namespace MedicalApp.Models
{
    public class MedDBContext:DbContext
    {
        public DbSet<City> Cites { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalSpecialization> MedicalSpecializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visite> Visites { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}