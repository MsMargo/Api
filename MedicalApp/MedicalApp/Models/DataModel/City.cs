using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.DataModel
{
    [Table("tblCities")]
    public class City
    {
        public int Id { get; set; }
        public string CityName   { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}