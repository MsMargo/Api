using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.ViewModel
{
    public class VisitViewModel
    {
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string VisitDate { get; set; }
        public string DoctorSpecialization { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}