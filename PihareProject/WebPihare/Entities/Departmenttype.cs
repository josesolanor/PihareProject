using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebPihare.Entities
{
    public partial class Departmenttype
    {
        public Departmenttype()
        {
            Department = new HashSet<Department>();
        }

        public int DepartmentTypeId { get; set; }
        [DisplayName("Nombre")]
        public string DepartmentTypeValue { get; set; }
        [DisplayName("Descripción")]
        public string DepartmentTypeDescription { get; set; }

        public ICollection<Department> Department { get; set; }
    }
}
