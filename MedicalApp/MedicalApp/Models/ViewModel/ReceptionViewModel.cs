using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.ViewModel
{
    public class ReceptionViewModel
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int MedicalSpecializationId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public string VisiteDate { get; set; }
        [Required]
        public string VisiteTime { get; set; }
        [Required]
        public string Description { get; set; }

    }
}