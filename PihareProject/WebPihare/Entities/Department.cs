using System;
using System.Collections.Generic;

namespace WebPihare.Entities
{
    public partial class Department
    {
        public Department()
        {
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int DepartmentId { get; set; }
        public int DepartmentCode { get; set; }
        public int NumberFloor { get; set; }
        public int NumberBedrooms { get; set; }
        public string DepartmentDescription { get; set; }
        public decimal DeparmentPrice { get; set; }
        public int DepartmentTypeId { get; set; }
        public int DepartmentStateId { get; set; }

        public Departmentstate DepartmentState { get; set; }
        public Departmenttype DepartmentType { get; set; }
        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
