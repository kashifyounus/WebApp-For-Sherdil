using System;

namespace WebAppForSherdilCloud.Models
{
    public class Appointment : BaseModel
    {
        public int DoctorId{ get; set; }
        public virtual Doctor Doctor{ get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public DateTime AppointmentTime{ get; set; }
        public string Details{ get; set; }
    }
}
