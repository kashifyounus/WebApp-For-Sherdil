using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAppForSherdilCloud.Models;

namespace WebAppForSherdilCloud.VeiwModel
{
    public class AppointmentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Select Doctor")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please Select Patient")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Appointment Time is required")]
        public DateTime AppointmentTime { get; set; }
        public string Details { get; set; }

        //for view
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
    public class Dashboard
    {
        public int DoctorsCount { get; set; }
        public int PatientsCount { get; set; }
        public int TotalAppointments { get; set; }
    }
}
