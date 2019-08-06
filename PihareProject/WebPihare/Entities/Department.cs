using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebPihare.Entities
{
    public partial class Department
    {
        public Department()
        {
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int DepartmentId { get; set; }
        [DisplayName("Codigo Departamento")]
        public int DepartmentCode { get; set; }
        [DisplayName("Numero de pisos")]
        public int NumberFloor { get; set; }
        [DisplayName("Numero de dormitorios")]
        public int NumberBedrooms { get; set; }
        [DisplayName("Descripcion del departamento")]
        public string DepartmentDescription { get; set; }
        [DisplayName("Precio")]
        public decimal DeparmentPrice { get; set; }
        
        public int DepartmentTypeId { get; set; }
        
        public int DepartmentStateId { get; set; }
        [DisplayName("Estado")]
        public Departmentstate DepartmentState { get; set; }
        [DisplayName("Tipo")]
        public Departmenttype DepartmentType { get; set; }
        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
