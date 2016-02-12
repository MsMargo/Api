using MedicalApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.ViewModel
{
    public class PatientFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? Pesel { get; set; }
      }
}