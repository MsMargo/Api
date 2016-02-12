using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MedicalApp.Models.DataModel
{
    [Table("tblDoctors")]
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("MedicalSpecialization")]
        public int SpecializationId { get; set; }

        public virtual ICollection<Visite> Visites { get; set; }
        public virtual MedicalSpecialization MedicalSpecialization { get; set; }
    }

}