using MedicalApp.Models;
using MedicalApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Services
{
   public interface IService
    {
       IEnumerable<PatientTableView> FindPatiets(DataTablesRequest dataTablesRequest, PatientFilter patient, out int totalRecordsNumber);
    }
}
