using System;
using System.Collections.Generic;

namespace WebPihareii.Entities
{
    public partial class Departmenttype
    {
        public Departmenttype()
        {
            Department = new HashSet<Department>();
        }

        public int DepartmentTypeId { get; set; }
        public string DepartmentTypeValue { get; set; }
        public string DepartmentTypeDescription { get; set; }

        public ICollection<Department> Department { get; set; }
    }
}
