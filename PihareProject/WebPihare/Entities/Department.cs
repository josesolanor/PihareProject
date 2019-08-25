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
        [DisplayName("Piso")]
        public int NumberFloor { get; set; }
        [DisplayName("Numero de dormitorios")]
        public int NumberBedrooms { get; set; }
        [DisplayName("Precio")]
        public decimal DeparmentPrice { get; set; }
        [DisplayName("Comentarios")]
        public string Comments { get; set; }
        [DisplayName("Tipo")]
        public int DepartmentTypeId { get; set; }
        [DisplayName("Estado")]
        public int DepartmentStateId { get; set; }
        [DisplayName("Estado")]
        public Departmentstate DepartmentState { get; set; }
        [DisplayName("Tipo")]
        public Departmenttype DepartmentType { get; set; }
        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
