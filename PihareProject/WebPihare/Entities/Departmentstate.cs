using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebPihare.Entities
{
    public partial class Departmentstate
    {
        public Departmentstate()
        {
            Department = new HashSet<Department>();
        }

        public int DepartmentStateId { get; set; }
        [DisplayName("Nombre")]
        public string DepartmentStateValue { get; set; }
        [DisplayName("Descripción")]
        public string DepartmentStateDescription { get; set; }

        public ICollection<Department> Department { get; set; }
    }
}
