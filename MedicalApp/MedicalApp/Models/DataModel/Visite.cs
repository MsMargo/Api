using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.DataModel
{
    [Table("tblVisites")]
    public class Visite
    {
        public int Id { get; set; }
        public DateTime VisiteDate { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}