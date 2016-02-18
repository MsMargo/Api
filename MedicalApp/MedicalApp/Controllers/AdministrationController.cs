using System.Web.Mvc;
using MedicalApp.Models.ViewModel;
using MedicalApp.Models;
using MedicalApp.Services;
using System;
namespace MedicalApp.Controllers
{
    public class AdministrationController : Controller
    {
        //private readonly IService _service;
        //public AdministrationController(IService service)
        //{
        //    _service = service;
        //}
        public ActionResult Reception()
        {
            Service serv = new Service();
            ViewBag.DoctorSpecializations = serv.GetMedicalSpecialization();
            return View();
        }
        [HttpPost]
        public ActionResult Reception(ReceptionViewModel visit)
        {
            if (ModelState.IsValid)
            {
                Service serv = new Service();
                serv.AddVisite(visit);
                ViewBag.DoctorName = serv.GetDoctorBySpecializationId(visit.MedicalSpecializationId);
                ViewBag.DoctorSpecialization = serv.GetMedicalSpecialization(visit.MedicalSpecializationId);
                ViewBag.PatientName = serv.GetPatienName(visit.PatientId);
            }
            return Redirect("Administration/Reception");
        }
        [HttpGet]
        public ActionResult SearchVisit()
        {
            Service serv = new Service();
            ViewBag.DoctorNames = serv.GetDoctors();

            var visits = new VisitFilter();
            return PartialView(visits);
        }
        [HttpGet]
        public ActionResult GetVisitsByFilter(DataTablesRequest dataTablesRequest, VisitFilter visitFilter)
        {
            Service serv = new Service();
            int totalRecordsNumber;
            var visitList = serv.GetVisitsByFilter(dataTablesRequest, visitFilter, out totalRecordsNumber);

            return new DataTablesResult(dataTablesRequest.sEcho, totalRecordsNumber, totalRecordsNumber, visitList);
        }

        [HttpGet]
        public ActionResult SearchPatient()
        {
            var model = new PatientFilter();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult GetPatientListByFilter(DataTablesRequest dataTablesRequest, PatientFilter patient)
        {
            Service serv = new Service();
            int totalRecordsNumber;
            var model = serv.FindPatiets(dataTablesRequest, patient, out totalRecordsNumber);

            return new DataTablesResult(dataTablesRequest.sEcho, totalRecordsNumber, totalRecordsNumber, model);
        }

        public ActionResult GetPatientById(int id)
        {
            Service serv = new Service();
            var model = serv.GetPatientById(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewPatient()
        {
            Service serv = new Service();
            ViewBag.City = serv.GetCity();
            PatientViewModel patient = new PatientViewModel();
            return PartialView("NewPatient", patient);
        }
        [HttpPost]
        public ActionResult NewPatient(PatientViewModel patient)
        {
            Service serv = new Service();
            if (ModelState.IsValid)
            {
                serv.CreatePatient(patient);
                return Json(new { success = true}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.City = serv.GetCity();
                return Json(new { success = false, textError = "During save patient error" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDoctorBySpecializationId(int specId)
        {
            Service serv = new Service();
            var doctors = serv.GetDoctorsBySpecializationId(specId);
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAvailbleTimeForDoctor(DateTime visiteDate, int doctorId)
        {
            Service serv = new Service();
            var avaliableTime = serv.GetAvailbleTimeForDoctor(visiteDate, doctorId);
            return Json(avaliableTime, JsonRequestBehavior.AllowGet);
        }
    }
}