using MedicalApp.Models;
using MedicalApp.Models.DataModel;
using MedicalApp.Models.Enums;
using MedicalApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MedicalApp.Services
{
    public class Service : IService
    {
        MedDBContext db = new MedDBContext();

        public IEnumerable<SelectListItem> GetMedicalSpecialization()
        {
            var medspec = db.MedicalSpecializations;
            List<SelectListItem> listItems = medspec.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Specialization, Selected = false }).ToList();
            var spec = new SelectListItem() { Text = "Please select specialization", Value = "-1", Selected = true };
            listItems.Add(spec);

            return listItems;
        }
        public string GetMedicalSpecialization(int specializationId)
        {
            var medspec = db.MedicalSpecializations.FirstOrDefault(x => x.Id == specializationId);
            return medspec.Specialization;
        }
        public IEnumerable<SelectListItem> GetDoctors()
        {
            var doctors = db.Doctors.AsEnumerable().Select(x => new SelectListItem()
            {
                //Value = x.Id.ToString(),
                Value = String.Format("{0} {1}", x.FirstName, x.LastName),
                Text = String.Format("{0} {1}", x.FirstName, x.LastName),
                Selected = false
            }).ToList();
            var doctorDefault = new SelectListItem() { Text = "Select doctor", Value = "-1", Selected = true };
            doctors.Add(doctorDefault);

            return doctors;
        }
        public IEnumerable<SelectListItem> GetDoctorsBySpecializationId(int specializationId)
        {
            var doctors = db.Doctors.Where(x => x.SpecializationId == specializationId).AsEnumerable().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = String.Format("{0} {1}", x.FirstName, x.LastName),
                Selected = false
            }).ToList();
            var doctorDefault = new SelectListItem() { Text = "Please select doctor", Value = "-1", Selected = true };
            doctors.Add(doctorDefault);

            return doctors;
        }
        public string GetDoctorBySpecializationId(int specializationId)
        {
            var doctorName = db.Doctors.FirstOrDefault(x => x.Id == specializationId);
            return doctorName.FirstName+" "+doctorName.LastName;
        }
        public IEnumerable<SelectListItem> GetPesel()
        {
            var patients = db.Patients;
            IEnumerable<SelectListItem> listItems = patients.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Pesel.ToString() });
            return listItems;
        }
        public string GetPatienName(int patientId)
        {
            var patientName = db.Patients.FirstOrDefault(x => x.Id == patientId);
            return patientName.FirstName+" "+patientName.LastName;
        }
        public IEnumerable<SelectListItem> GetPatientFirstNames()
        {
            var patientsFirstNames = db.Patients.GroupBy(f => f.FirstName).Select(r => r.FirstOrDefault()).ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var r in patientsFirstNames)
            {
                listItems.Add(new SelectListItem { Text = r.FirstName, Value = r.Id.ToString() });
            }
            return listItems;
        }
        public IEnumerable<SelectListItem> GetPatientLastNames()
        {
            var patientLastNames = db.Patients.GroupBy(f => f.LastName).Select(r => r.FirstOrDefault()).ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var r in patientLastNames)
            {
                listItems.Add(new SelectListItem { Text = r.LastName, Value = r.Id.ToString() });
            }
            return listItems;
        }
        public IEnumerable<SelectListItem> GetCity()
        {
            var cities = db.Cites;
            IEnumerable<SelectListItem> listItems = cities.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.CityName }).ToList();
            return listItems;
        }
        public IEnumerable<PatientTableView> FindPatiets(DataTablesRequest dataTablesRequest, PatientFilter patient, out int totalRecordsNumber)
        {
            IQueryable<Patient> query = db.Patients.AsQueryable<Patient>();
            if (!String.IsNullOrEmpty(patient.FirstName))
            {
                query = query.Where(x => x.FirstName.Contains(patient.FirstName));
            }
            if (!String.IsNullOrEmpty(patient.LastName))
            {
                query = query.Where(x => x.LastName.Contains(patient.LastName));
            }
            if (patient.Pesel.HasValue)
            {
                query = query.Where(x => x.Pesel == patient.Pesel);
            }

            totalRecordsNumber = query.Count();

            query = query.OrderBy(x => x.Id).Skip(dataTablesRequest.iDisplayStart).Take(dataTablesRequest.iDisplayLength);

            var patientDBList = query.ToList<Patient>();
            var patientTableList = new List<PatientTableView>();
            foreach (var pat in patientDBList)
            {
                PatientTableView p = new PatientTableView()
                {
                    City = pat.City.CityName,
                    FirstName = pat.FirstName,
                    Id = pat.Id,
                    LastName = pat.LastName,
                    Pesel = pat.Pesel.ToString()
                };
                patientTableList.Add(p);
            }

            return patientTableList;
        }
        public IEnumerable<VisitViewModel> GetVisitsByFilter(DataTablesRequest dataTablesRequest, VisitFilter visitFilter, out int totalRecordsNumber)
        {
            IQueryable<Visite> query = db.Visites.AsQueryable<Visite>();
            if (!String.IsNullOrEmpty(visitFilter.VisitDate))
            {
                DateTime date = DateTime.Parse(visitFilter.VisitDate);
                query = query.Where(x => x.VisiteDate.Year == date.Year).Where(x => x.VisiteDate.Month == date.Month).Where(x => x.VisiteDate.Day == date.Day);
                //query = query.Where(x => x.VisiteDate == visitFilter.VisitDate);

            }
            if (visitFilter.DoctorName!="-1")
            {
                query = query.Where(x => (x.Doctor.FirstName+" "+x.Doctor.LastName == visitFilter.DoctorName));
            }
            totalRecordsNumber = query.Count();

            query = query.OrderBy(x => x.DoctorId).Skip(dataTablesRequest.iDisplayStart).Take(dataTablesRequest.iDisplayLength);

            var visistTableList = query.ToList<Visite>().Select(v => new VisitViewModel()
            {
                Description = v.Description,
                City = v.Patient.City.CityName,
                DoctorName = v.Doctor.FirstName + " " + v.Doctor.LastName,
                DoctorSpecialization = v.Doctor.MedicalSpecialization.Specialization,
                PatientName = v.Patient.FirstName + " " + v.Patient.LastName,
                VisitDate = v.VisiteDate.ToString()
            });
            return visistTableList;
        }
        public void CreatePatient(PatientViewModel patient)
        {
            db.Patients.Add(new Patient { CityId = patient.CityId, FirstName = patient.FirstName, LastName = patient.LastName, Gender = patient.Gender == GenderSelector.Male ? false : true, Pesel = patient.Pesel.HasValue ? patient.Pesel.Value : 0 });
            db.SaveChanges();
        }

        public PatientViewModel GetPatientById(int id)
        {
            var patientDB = db.Patients.FirstOrDefault(x => x.Id == id);
            PatientViewModel patient = new PatientViewModel();
            patient.FirstName = patientDB.FirstName;
            patient.LastName = patientDB.LastName;
            patient.Pesel = patientDB.Pesel;
            patient.Id = patientDB.Id;

            return patient;
        }
        public void AddVisite(ReceptionViewModel visit)
        {
            string date = visit.VisiteDate + " " + visit.VisiteTime;

            db.Visites.Add(new Visite
            {
                PatientId = visit.PatientId,
                VisiteDate = DateTime.Parse(date),
                Description = visit.Description,
                DoctorId = visit.DoctorId
            });
            db.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetAvailbleTimeForDoctor(DateTime visiteDate, int doctorId)
        {
            var appointedTime = db.Visites.Where(x => x.DoctorId == doctorId).Where(x => x.VisiteDate.Year == visiteDate.Year)
                .Where(x => x.VisiteDate.Month == visiteDate.Month).Where(x => x.VisiteDate.Day == visiteDate.Day).Select(x => x.VisiteDate).ToList();

            DateTime start = new DateTime(visiteDate.Year, visiteDate.Month, visiteDate.Day, 8, 0, 0);
            DateTime end = new DateTime(visiteDate.Year, visiteDate.Month, visiteDate.Day, 20, 0, 0);

            List<DateTime> timeList = new List<DateTime>();
            for (DateTime time = start; time <= end; time = time.AddMinutes(30))
            {
                timeList.Add(time);
            }

            foreach (var time in appointedTime)
            {
                timeList.Remove(time);
            }

            var availbleTime = timeList.Select(x => new SelectListItem() { Text = x.TimeOfDay.ToString("hh\\:mm"), Value = x.TimeOfDay.ToString() });
            return availbleTime;
        }


    }
}