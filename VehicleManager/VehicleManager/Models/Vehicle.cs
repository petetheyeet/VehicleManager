using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManager.Models
{
    public class Vehicle
    {
        public int Vehicle_ID { get; set; }
        public string Owner_FName { get; set; }
        public string Owner_LName { get; set; }
        public string Owner_Phone { get; set; }
        public string Owner_Unit { get; set; }
        public string Owner_Apartment { get; set; }
        public string Vehicle_Make { get; set; }
        public string Vehicle_Model { get; set; }
        public string Vehicle_Color { get; set; }
        public string Vehicle_Registration { get; set; }
        public DateTime Registration_Date { get; set; }
        public string Audit_User { get; set; }
        public DateTime Audit_TIme { get; set; }
    }
}
