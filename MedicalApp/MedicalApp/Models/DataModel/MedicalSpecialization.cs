using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalApp.Models.DataModel
{
    [Table("tblMedicalSpecializations")]
    public class MedicalSpecialization
    {
        public int Id { get; set; }
        public string Specialization { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}