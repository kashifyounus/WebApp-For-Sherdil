using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAppForSherdilCloud.Data;
using WebAppForSherdilCloud.Models;
using WebAppForSherdilCloud.VeiwModel;

namespace WebAppForSherdilCloud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClinicContext Context;
        public HomeController(ClinicContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var doctors = Context.Doctor.Count();
            var patients = Context.Patient.Count();
            var appointments = Context.Appointment.Count();
            var dashboard = new Dashboard()
            {
                DoctorsCount = doctors,
                PatientsCount = patients,
                TotalAppointments = appointments,
            };
            return View(dashboard);
        }

        #region Patient
        public IActionResult PatientList()
        {
            var patientList = Context.Patient.ToList();
            return View(patientList);
        }
        public IActionResult AddPatient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var isEmailExist = Context.Patient.Where(x => x.Email == patient.Email).FirstOrDefault();
                if (isEmailExist != null)
                {
                    return Json(new { success = false, message = "Email already exists" });
                }
                try
                {
                    Context.Add(patient);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Added" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = ModelErrors() });
            }
        }
        public IActionResult EditPatient(int Id)
        {
            if (Id > 0)
            {
                var patient = Context.Patient.Where(x => x.Id == Id).FirstOrDefault();
                return View(patient);
            }

            return NotFound();
        }
        [HttpPost]
        public IActionResult EditPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var pat = Context.Patient.Where(x => x.Id == patient.Id).FirstOrDefault();
                pat.FirstName = patient.FirstName;
                pat.LastName = patient.LastName;
                pat.Email = patient.Email;
                pat.PhoneNo = patient.PhoneNo;
                pat.DOB = patient.DOB;
                pat.Address = patient.Address;
                try
                {
                    Context.Update(pat);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Updated" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = ModelErrors() });
            }
        }

        [HttpPost]
        public IActionResult DeletePatient(int patientId)
        {
            if (patientId > 0)
            {
                var patient = Context.Patient.Where(x => x.Id == patientId).FirstOrDefault();
                try
                {
                    Context.Remove(patient);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Deleted" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid ID" });
            }
        }
        #endregion
        #region Doctor
        public IActionResult DoctorList()
        {
            var doctorList = Context.Doctor.ToList();
            return View(doctorList);
        }
        public IActionResult AddDoctor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var isEmailExist = Context.Doctor.Where(x => x.Email == doctor.Email).FirstOrDefault();
                if (isEmailExist != null)
                {
                    return Json(new { success = false, message = "Email already exists" });
                }
                try
                {
                    Context.Add(doctor);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Added" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = ModelErrors() });
            }
        }
        public IActionResult EditDoctor(int Id)
        {
            if (Id > 0)
            {
                var doctor = Context.Doctor.Where(x => x.Id == Id).FirstOrDefault();
                return View(doctor);
            }
            
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var doc = Context.Doctor.Where(x => x.Id == doctor.Id).FirstOrDefault();
                doc.FirstName = doctor.FirstName;
                doc.LastName = doctor.LastName;
                doc.Email = doctor.Email;
                doc.Speciality = doctor.Speciality;
                doc.PhoneNo= doctor.PhoneNo;
                doc.DOB= doctor.DOB;
                doc.Address = doctor.Address;
                try
                {
                    Context.Update(doc);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Updated" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = ModelErrors() });
            }
        }

        [HttpPost]
        public IActionResult DeleteDoctor(int doctorId)
        {
            if (doctorId > 0)
            {
                var doctor = Context.Doctor.Where(x => x.Id == doctorId).FirstOrDefault();
                try
                {
                    Context.Remove(doctor);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Deleted" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
                
            }
            else
            {
                return Json(new { success = false, message = "Invalid ID" });
            }
        }
        #endregion
        #region Appointment
        public IActionResult AppointmentList()
        {
            var list = Context.Appointment.Include(x=>x.Doctor).Include(x=>x.Patient).ToList();
            return View(list);
        }
        public IActionResult AddAppointment()
        {
            var vm = new AppointmentVM
            {
                Doctors = Context.Doctor.ToList(),
                Patients = Context.Patient.ToList()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddAppointment(AppointmentVM vm)
        {
            if (ModelState.IsValid)
            {
                var isExists = Context.Appointment.Where(x => x.DoctorId == vm.DoctorId &&
                                                              x.PatientId == vm.PatientId).FirstOrDefault();
                if (isExists != null)
                {
                    return Json(new { success = false, message = $"Appointment Already Exists " });

                }
                var appointment = new Appointment()
                {
                    DoctorId = vm.DoctorId,
                    PatientId = vm.PatientId,                    
                    AppointmentTime = vm.AppointmentTime,
                    Details = vm.Details
                };
                try
                {
                    Context.Add(appointment);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Added" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = ModelErrors() });
            }
        }

        public IActionResult EditAppointment(int Id)
        {
            if (Id > 0)
            {
                var app = Context.Appointment.Where(x => x.Id == Id).FirstOrDefault();
                var doctors = Context.Doctor.ToList();
                var patients = Context.Patient.ToList();
                var vm = new AppointmentVM()
                {
                    Id = app.Id,
                    PatientId = app.PatientId,
                    DoctorId = app.DoctorId,
                    AppointmentTime = app.AppointmentTime,
                    Details = app.Details,
                    Doctors = doctors,
                    Patients = patients,
                };
                return View(vm);
            }

            return NotFound();
        }
        [HttpPost]
        public IActionResult EditAppointment(AppointmentVM vm)
        {
            if (ModelState.IsValid)
            {
                var appoint = Context.Appointment.Where(x => x.Id == vm.Id).FirstOrDefault();
                appoint.DoctorId = vm.DoctorId;
                appoint.PatientId = vm.PatientId;
                appoint.AppointmentTime = vm.AppointmentTime;
                appoint.Details = vm.Details;
                try
                {
                    Context.Update(appoint);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Updated" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = ModelErrors() });
            }
        }
        [HttpPost]
        public IActionResult DeleteAppointment(int Id)
        {
            if (Id > 0)
            {
                var appointment = Context.Appointment.Where(x => x.Id == Id).FirstOrDefault();
                try
                {
                    Context.Remove(appointment);
                    Context.SaveChanges();
                    return Json(new { success = true, message = "Successfully Deleted" });
                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException });
                }
            }
            else
            {
                return Json(new { success = false, message = "something wrong" });
            }
        }
        #endregion
        public List<string> ModelErrors()
        {
            var errorList = ModelState.Values
                .SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return errorList;
        }
    }

}
